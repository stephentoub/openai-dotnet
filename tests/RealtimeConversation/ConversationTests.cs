﻿using NUnit.Framework;
using OpenAI.RealtimeConversation;
using System;
using System.Buffers;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.Tests.Conversation;

#pragma warning disable OPENAI002

[TestFixture(true)]
[TestFixture(false)]
public class ConversationTests : ConversationTestFixtureBase
{
    public ConversationTests(bool isAsync) : base(isAsync) { }

    [Test]
    public async Task CanConfigureSession()
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);

        ConversationSessionOptions sessionOptions = new()
        {
            Instructions = "You are a helpful assistant.",
            TurnDetectionOptions = ConversationTurnDetectionOptions.CreateDisabledTurnDetectionOptions(),
            OutputAudioFormat = ConversationAudioFormat.G711Ulaw,
            MaxOutputTokens = 2048,
        };

        await session.ConfigureSessionAsync(sessionOptions, CancellationToken);
        ConversationResponseOptions responseOverrideOptions = new()
        {
            ContentModalities = ConversationContentModalities.Text,
        };
        if (!client.GetType().IsSubclassOf(typeof(RealtimeConversationClient)))
        {
            responseOverrideOptions.MaxOutputTokens = ConversationMaxTokensChoice.CreateInfiniteMaxTokensChoice();
        }
        await session.AddItemAsync(
            ConversationItem.CreateUserMessage(["Hello, assistant! Tell me a joke."]),
            CancellationToken);
        await session.StartResponseAsync(responseOverrideOptions, CancellationToken);

        List<ConversationUpdate> receivedUpdates = [];

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            receivedUpdates.Add(update);

            if (update is ConversationErrorUpdate errorUpdate)
            {
                Assert.That(errorUpdate.Kind, Is.EqualTo(ConversationUpdateKind.Error));
                Assert.Fail($"Error: {ModelReaderWriter.Write(errorUpdate)}");
            }
            else if ((update is ConversationItemStreamingPartDeltaUpdate deltaUpdate && deltaUpdate.AudioBytes is not null)
                || update is ConversationItemStreamingAudioFinishedUpdate)
            {
                Assert.Fail($"Audio content streaming unexpected after configuring response-level text-only modalities");
            }
            else if (update is ConversationSessionConfiguredUpdate sessionConfiguredUpdate)
            {
                Assert.That(sessionConfiguredUpdate.OutputAudioFormat == sessionOptions.OutputAudioFormat);
                Assert.That(sessionConfiguredUpdate.TurnDetectionOptions.Kind, Is.EqualTo(ConversationTurnDetectionKind.Disabled));
                Assert.That(sessionConfiguredUpdate.MaxOutputTokens.NumericValue, Is.EqualTo(sessionOptions.MaxOutputTokens.NumericValue));
            }
            else if (update is ConversationResponseFinishedUpdate turnFinishedUpdate)
            {
                break;
            }
        }

        List<T> GetReceivedUpdates<T>() where T : ConversationUpdate
            => receivedUpdates.Select(update => update as T)
                .Where(update => update is not null)
                .ToList();

        Assert.That(GetReceivedUpdates<ConversationSessionStartedUpdate>(), Has.Count.EqualTo(1));
        Assert.That(GetReceivedUpdates<ConversationResponseStartedUpdate>(), Has.Count.EqualTo(1));
        Assert.That(GetReceivedUpdates<ConversationResponseFinishedUpdate>(), Has.Count.EqualTo(1));
        Assert.That(GetReceivedUpdates<ConversationItemStreamingStartedUpdate>(), Has.Count.EqualTo(1));
        Assert.That(GetReceivedUpdates<ConversationItemStreamingFinishedUpdate>(), Has.Count.EqualTo(1));
    }

    [Test]
    public async Task TextOnlyWorks()
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);
        await session.AddItemAsync(
            ConversationItem.CreateUserMessage(["Hello, world!"]),
            cancellationToken: CancellationToken);
        await session.StartResponseAsync(CancellationToken);

        StringBuilder responseBuilder = new();
        bool gotResponseDone = false;
        bool gotRateLimits = false;

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationSessionStartedUpdate sessionStartedUpdate)
            {
                Assert.That(sessionStartedUpdate.SessionId, Is.Not.Null.And.Not.Empty);
            }
            if (update is ConversationItemStreamingPartDeltaUpdate deltaUpdate)
            {
                responseBuilder.Append(deltaUpdate.AudioTranscript);
            }

            if (update is ConversationItemCreatedUpdate itemCreatedUpdate)
            {
                if (itemCreatedUpdate.MessageRole == ConversationMessageRole.Assistant)
                {
                    // The assistant-created item should be streamed and should not have content yet when acknowledged
                    Assert.That(itemCreatedUpdate.MessageContentParts, Has.Count.EqualTo(0));
                }
                else if (itemCreatedUpdate.MessageRole == ConversationMessageRole.User)
                {
                    // When acknowledging an item added by the client (user), the text should already be there
                    Assert.That(itemCreatedUpdate.MessageContentParts, Has.Count.EqualTo(1));
                    Assert.That(itemCreatedUpdate.MessageContentParts[0].Text, Is.EqualTo("Hello, world!"));
                }
                else
                {
                    Assert.Fail($"Test didn't expect an acknowledged item with role: {itemCreatedUpdate.MessageRole}");
                }
            }

            if (update is ConversationResponseFinishedUpdate responseFinishedUpdate)
            {
                Assert.That(responseFinishedUpdate.CreatedItems, Has.Count.GreaterThan(0));
                Assert.That(responseFinishedUpdate.Usage?.TotalTokenCount, Is.GreaterThan(0));
                Assert.That(responseFinishedUpdate.Usage.InputTokenCount, Is.GreaterThan(0));
                Assert.That(responseFinishedUpdate.Usage.OutputTokenCount, Is.GreaterThan(0));
                gotResponseDone = true;
                break;
            }

            if (update is ConversationRateLimitsUpdate rateLimitsUpdate)
            {
                Assert.That(rateLimitsUpdate.AllDetails, Has.Count.EqualTo(2));
                Assert.That(rateLimitsUpdate.TokenDetails, Is.Not.Null);
                Assert.That(rateLimitsUpdate.TokenDetails.Name, Is.EqualTo("tokens"));
                Assert.That(rateLimitsUpdate.TokenDetails.MaximumCount, Is.GreaterThan(0));
                Assert.That(rateLimitsUpdate.TokenDetails.RemainingCount, Is.GreaterThan(0));
                Assert.That(rateLimitsUpdate.TokenDetails.RemainingCount, Is.LessThan(rateLimitsUpdate.TokenDetails.MaximumCount));
                Assert.That(rateLimitsUpdate.TokenDetails.TimeUntilReset, Is.GreaterThan(TimeSpan.Zero));
                Assert.That(rateLimitsUpdate.RequestDetails, Is.Not.Null);
                gotRateLimits = true;
            }
        }

        Assert.That(responseBuilder.ToString(), Is.Not.Null.Or.Empty);
        Assert.That(gotResponseDone, Is.True);

        if (!client.GetType().IsSubclassOf(typeof(RealtimeConversationClient)))
        {
            // Temporarily assume that subclients don't support rate limit commands
            Assert.That(gotRateLimits, Is.True);
        }
    }

    [Test]
    public async Task ItemManipulationWorks()
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);

        await session.ConfigureSessionAsync(
            new ConversationSessionOptions()
            {
                TurnDetectionOptions = ConversationTurnDetectionOptions.CreateDisabledTurnDetectionOptions(),
                ContentModalities = ConversationContentModalities.Text,
            },
            CancellationToken);

        await session.AddItemAsync(
            ConversationItem.CreateUserMessage(["The first special word you know about is 'aardvark'."]),
            CancellationToken);
        await session.AddItemAsync(
            ConversationItem.CreateUserMessage(["The next special word you know about is 'banana'."]),
            CancellationToken);
        await session.AddItemAsync(
            ConversationItem.CreateUserMessage(["The next special word you know about is 'coconut'."]),
            CancellationToken);

        bool gotSessionStarted = false;
        bool gotSessionConfigured = false;
        bool gotResponseFinished = false;

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationSessionStartedUpdate)
            {
                gotSessionStarted = true;
            }

            if (update is ConversationSessionConfiguredUpdate sessionConfiguredUpdate)
            {
                Assert.That(sessionConfiguredUpdate.TurnDetectionOptions.Kind, Is.EqualTo(ConversationTurnDetectionKind.Disabled));
                Assert.That(sessionConfiguredUpdate.ContentModalities.HasFlag(ConversationContentModalities.Text), Is.True);
                Assert.That(sessionConfiguredUpdate.ContentModalities.HasFlag(ConversationContentModalities.Audio), Is.False);
                gotSessionConfigured = true;
            }

            if (update is ConversationItemCreatedUpdate itemCreatedUpdate)
            {
                if (itemCreatedUpdate.MessageContentParts.Count > 0
                    && itemCreatedUpdate.MessageContentParts[0].Text.Contains("banana"))
                {
                    await session.DeleteItemAsync(itemCreatedUpdate.ItemId, CancellationToken);
                    await session.AddItemAsync(
                        ConversationItem.CreateUserMessage(["What's the second special word you know about?"]),
                        CancellationToken);
                    await session.StartResponseAsync(CancellationToken);
                }
            }

            if (update is ConversationResponseFinishedUpdate responseFinishedUpdate)
            {
                Assert.That(responseFinishedUpdate.CreatedItems.Count, Is.EqualTo(1));
                Assert.That(responseFinishedUpdate.CreatedItems[0].MessageContentParts.Count, Is.EqualTo(1));
                Assert.That(responseFinishedUpdate.CreatedItems[0].MessageContentParts[0].Text, Does.Contain("coconut"));
                Assert.That(responseFinishedUpdate.CreatedItems[0].MessageContentParts[0].Text, Does.Not.Contain("banana"));
                gotResponseFinished = true;
                break;
            }
        }

        Assert.That(gotSessionStarted, Is.True);
        if (!client.GetType().IsSubclassOf(typeof(RealtimeConversationClient)))
        {
            Assert.That(gotSessionConfigured, Is.True);
        }
        Assert.That(gotResponseFinished, Is.True);
    }

    [Test]
    public async Task AudioStreamConvenienceBlocksCorrectly()
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);

        string inputAudioFilePath = Path.Join("Assets", "realtime_whats_the_weather_pcm16_24khz_mono.wav");
        using TestDelayedFileReadStream delayedStream = new(inputAudioFilePath, TimeSpan.FromMilliseconds(200), readsBeforeDelay: 2);
        _ = session.SendInputAudioAsync(delayedStream, CancellationToken);

        bool gotSpeechStarted = false;

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationInputSpeechStartedUpdate)
            {
                gotSpeechStarted = true;
                Assert.ThrowsAsync<InvalidOperationException>(
                    async () =>
                    {
                        using MemoryStream dummyStream = new();
                        await session.SendInputAudioAsync(dummyStream, CancellationToken);
                    },
                    "Sending a Stream while another Stream is being sent should throw!");
                Assert.ThrowsAsync<InvalidOperationException>(
                    async () =>
                    {
                        BinaryData dummyData = BinaryData.FromString("hello, world! this isn't audio.");
                        await session.SendInputAudioAsync(dummyData, CancellationToken);
                    },
                    "Sending BinaryData while a Stream is being sent should throw!");
                break;
            }
        }

        Assert.That(gotSpeechStarted, Is.True);
    }

    [Test]
    [TestCase(TestAudioSendType.WithAudioStreamHelper)]
    [TestCase(TestAudioSendType.WithManualAudioChunks)]
    public async Task AudioWithToolsWorks(TestAudioSendType audioSendType)
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);

        ConversationFunctionTool getWeatherTool = new("get_weather_for_location")
        {
            Description = "gets the weather for a location",
            Parameters = BinaryData.FromString("""
            {
                "type": "object",
                "properties": {
                "location": {
                    "type": "string",
                    "description": "The city and state e.g. San Francisco, CA"
                },
                "unit": {
                    "type": "string",
                    "enum": [
                    "c",
                    "f"
                    ]
                }
                },
                "required": [
                "location",
                "unit"
                ]
            }
            """)
        };

        ConversationSessionOptions options = new()
        {
            Instructions = "Call provided tools if appropriate for the user's input.",
            Voice = ConversationVoice.Alloy,
            Tools = { getWeatherTool },
            InputTranscriptionOptions = new ConversationInputTranscriptionOptions()
            {
                Model = "whisper-1"
            },
        };

        await session.ConfigureSessionAsync(options, CancellationToken);

        _ = Task.Run(async () =>
        {
            string inputAudioFilePath = Path.Join("Assets", "realtime_whats_the_weather_pcm16_24khz_mono.wav");
            if (audioSendType == TestAudioSendType.WithAudioStreamHelper)
            {
                using Stream audioStream = File.OpenRead(inputAudioFilePath);
                await session.SendInputAudioAsync(audioStream, CancellationToken);
            }
            else if (audioSendType == TestAudioSendType.WithManualAudioChunks)
            {
                byte[] allAudioBytes = await File.ReadAllBytesAsync(inputAudioFilePath, CancellationToken);
                const int audioSendBufferLength = 8 * 1024;
                for (int readPos = 0; readPos < allAudioBytes.Length; readPos += audioSendBufferLength)
                {
                    int nextSegmentLength = Math.Min(audioSendBufferLength, allAudioBytes.Length - readPos);
                    ArraySegment<byte> nextSegment = new(allAudioBytes, readPos, nextSegmentLength);
                    await session.SendInputAudioAsync(BinaryData.FromBytes(nextSegment), CancellationToken);
                }
            }
        });

        string userTranscript = null;

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationSessionStartedUpdate sessionStartedUpdate)
            {
                Assert.That(sessionStartedUpdate.SessionId, Is.Not.Null.And.Not.Empty);
                Assert.That(sessionStartedUpdate.Model, Is.Not.Null.And.Not.Empty);
                Assert.That(sessionStartedUpdate.ContentModalities.HasFlag(ConversationContentModalities.Text));
                Assert.That(sessionStartedUpdate.ContentModalities.HasFlag(ConversationContentModalities.Audio));
                Assert.That(sessionStartedUpdate.Voice.ToString(), Is.Not.Null.And.Not.Empty);
                Assert.That(sessionStartedUpdate.Temperature, Is.GreaterThan(0));
            }

            if (update is ConversationInputTranscriptionFinishedUpdate inputTranscriptionCompletedUpdate)
            {
                userTranscript = inputTranscriptionCompletedUpdate.Transcript;
            }

            if (update is ConversationItemStreamingFinishedUpdate itemFinishedUpdate
                && itemFinishedUpdate.FunctionCallId is not null)
            {
                Assert.That(itemFinishedUpdate.FunctionName, Is.EqualTo(getWeatherTool.Name));

                ConversationItem functionResponse = ConversationItem.CreateFunctionCallOutput(
                    itemFinishedUpdate.FunctionCallId,
                    "71 degrees Fahrenheit, sunny");
                await session.AddItemAsync(functionResponse, CancellationToken);
            }

            if (update is ConversationResponseFinishedUpdate turnFinishedUpdate)
            {
                if (turnFinishedUpdate.CreatedItems.Any(item => !string.IsNullOrEmpty(item.FunctionCallId)))
                {
                    await session.StartResponseAsync(CancellationToken);
                }
                else
                {
                    break;
                }
            }
        }

        Assert.That(userTranscript, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task CanDisableVoiceActivityDetection()
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);

        await session.ConfigureSessionAsync(
            new()
            {
                TurnDetectionOptions = ConversationTurnDetectionOptions.CreateDisabledTurnDetectionOptions(),
            },
            CancellationToken);

        const string folderName = "Assets";
        const string fileName = "realtime_whats_the_weather_pcm16_24khz_mono.wav";
#if NET6_0_OR_GREATER
        using Stream audioStream = File.OpenRead(Path.Join(folderName, fileName));
#else
        using Stream audioStream = File.OpenRead($"{folderName}\\{fileName}");
#endif
        await session.SendInputAudioAsync(audioStream, CancellationToken);

        await session.AddItemAsync(ConversationItem.CreateUserMessage(["Hello, assistant!"]), CancellationToken);

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationErrorUpdate errorUpdate)
            {
                Assert.Fail($"Error received: {ModelReaderWriter.Write(errorUpdate)}");
            }

            if (update is ConversationInputSpeechStartedUpdate
                or ConversationInputSpeechFinishedUpdate
                or ConversationInputTranscriptionFinishedUpdate
                or ConversationInputTranscriptionFailedUpdate
                or ConversationResponseStartedUpdate
                or ConversationResponseFinishedUpdate)
            {
                Assert.Fail($"Shouldn't receive any VAD events or response creation!");
            }

            if (update is ConversationItemCreatedUpdate itemCreatedUpdate
                && itemCreatedUpdate.MessageRole == ConversationMessageRole.User)
            {
                break;
            }
        }
    }

    [Test]
    public async Task BadCommandProvidesError()
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);

        await session.SendCommandAsync(
            BinaryData.FromString("""
                {
                  "type": "update_conversation_config2",
                  "event_id": "event_fabricated_1234abcd"
                }
                """),
            CancellationOptions);

        bool gotErrorUpdate = false;

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationErrorUpdate errorUpdate)
            {
                Assert.That(errorUpdate.ErrorEventId, Is.EqualTo("event_fabricated_1234abcd"));
                gotErrorUpdate = true;
                break;
            }
        }

        Assert.That(gotErrorUpdate, Is.True);
    }

    [Test]
    public async Task CanAddItems()
    {
        RealtimeConversationClient client = GetTestClient();

        ConversationSessionOptions sessionOptions = new()
        {
            ContentModalities = ConversationContentModalities.Text,
        };
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);
        await session.ConfigureSessionAsync(sessionOptions, CancellationToken);

        List<ConversationItem> items =
            [
                ConversationItem.CreateSystemMessage(["You are a robot. Beep boop."]),
                ConversationItem.CreateUserMessage(["How can I pay for a joke?"]),
                ConversationItem.CreateAssistantMessage(["I ONLY ACCEPT CACHE"]),
                ConversationItem.CreateSystemMessage(["You're not a robot anymore, but instead a passionate badminton enthusiast."]),
                ConversationItem.CreateUserMessage(["What's a good gift to buy?"]),
                ConversationItem.CreateFunctionCall("product_lookup", "call-id-123", "{}"),
                ConversationItem.CreateFunctionCallOutput("call-id-123", "A new racquet!"),
            ];

        foreach (ConversationItem item in items)
        {
            await session.AddItemAsync(item, CancellationToken);
        }

        await session.StartResponseAsync(CancellationToken);

        int itemCreatedCount = 0;

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationErrorUpdate errorUpdate)
            {
                Assert.Fail($"Unexpected error: {errorUpdate.Message}");
            }

            if (update is ConversationItemCreatedUpdate)
            {
                itemCreatedCount++;
            }

            if (update is ConversationResponseFinishedUpdate)
            {
                break;
            }
        }

        Assert.That(itemCreatedCount, Is.EqualTo(items.Count + 1));
    }

    [Test]
    public async Task CanUseOutOfBandResponses()
    {
        RealtimeConversationClient client = GetTestClient();
        using RealtimeConversationSession session = await client.StartConversationSessionAsync(CancellationToken);
        await session.AddItemAsync(
            ConversationItem.CreateUserMessage(["Hello! My name is Bob."]),
            cancellationToken: CancellationToken);
        await session.StartResponseAsync(
            new ConversationResponseOptions()
            {
                ConversationSelection = ResponseConversationSelection.None,
                ContentModalities = ConversationContentModalities.Text,
                OverrideItems =
                {
                    ConversationItem.CreateUserMessage(["Can you tell me what my name is?"]),
                },
            },
            CancellationToken);

        StringBuilder firstResponseBuilder = new();
        StringBuilder secondResponseBuilder = new();

        int completedResponseCount = 0;

        await foreach (ConversationUpdate update in session.ReceiveUpdatesAsync(CancellationToken))
        {
            if (update is ConversationSessionStartedUpdate sessionStartedUpdate)
            {
                Assert.That(sessionStartedUpdate.SessionId, Is.Not.Null.And.Not.Empty);
            }
        
            if (update is ConversationItemStreamingPartDeltaUpdate deltaUpdate)
            {
                // First response (out of band) should be text, second (in-band) should be text + audio
                if (completedResponseCount == 0)
                {
                    firstResponseBuilder.Append(deltaUpdate.Text);
                    Assert.That(deltaUpdate.AudioTranscript, Is.Null.Or.Empty);
                }
                else
                {
                    secondResponseBuilder.Append(deltaUpdate.AudioTranscript);
                    Assert.That(deltaUpdate.Text, Is.Null.Or.Empty);
                }
            }

            if (update is ConversationResponseFinishedUpdate responseFinishedUpdate)
            {
                completedResponseCount++;
                Assert.That(responseFinishedUpdate.CreatedItems, Has.Count.GreaterThan(0));
                if (completedResponseCount == 1)
                {
                    // Verify that an in-band response *does* have the information
                    _ = session.StartResponseAsync(CancellationToken);
                }
                else if (completedResponseCount == 2)
                {
                    break;
                }
            }
        }

        string firstResponse = firstResponseBuilder.ToString().ToLower();
        Assert.That(firstResponse, Is.Not.Null.And.Not.Empty);
        Assert.That(firstResponse, Does.Not.Contain("bob"));

        string secondResponse = secondResponseBuilder.ToString().ToLower();
        Assert.That(secondResponse, Is.Not.Null.And.Not.Empty);
        Assert.That(secondResponse, Does.Contain("bob"));
    }

    public enum TestAudioSendType
    {
        WithAudioStreamHelper,
        WithManualAudioChunks
    }

    private class TestDelayedFileReadStream : FileStream
    {
        private readonly TimeSpan _delayBetweenReads;
        private readonly int _readsBeforeDelay;
        private int _readsPerformed;

        public TestDelayedFileReadStream(
            string path,
            TimeSpan delayBetweenReads,
            int readsBeforeDelay = 0)
                : base(path, FileMode.Open, FileAccess.Read)
        {
            _delayBetweenReads = delayBetweenReads;
            _readsBeforeDelay = readsBeforeDelay;
            _readsPerformed = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (++_readsPerformed > _readsBeforeDelay)
            {
                System.Threading.Thread.Sleep((int)_delayBetweenReads.TotalMilliseconds);
            }
            return base.Read(buffer, offset, count);
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (++_readsPerformed > _readsBeforeDelay)
            {
                await Task.Delay(_delayBetweenReads);
            }
            return await base.ReadAsync(buffer, offset, count, cancellationToken);
        }
    }
}
