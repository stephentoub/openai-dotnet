// <auto-generated/>

#nullable disable

using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using OpenAI;

namespace OpenAI.Realtime
{
    public partial class InputAudioTranscriptionFailedUpdate : IJsonModel<InputAudioTranscriptionFailedUpdate>
    {
        internal InputAudioTranscriptionFailedUpdate() : this(RealtimeUpdateKind.InputTranscriptionFailed, null, null, null, default, null)
        {
        }

        void IJsonModel<InputAudioTranscriptionFailedUpdate>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            JsonModelWriteCore(writer, options);
            writer.WriteEndObject();
        }

        protected override void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InputAudioTranscriptionFailedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InputAudioTranscriptionFailedUpdate)} does not support writing '{format}' format.");
            }
            base.JsonModelWriteCore(writer, options);
            if (_additionalBinaryDataProperties?.ContainsKey("item_id") != true)
            {
                writer.WritePropertyName("item_id"u8);
                writer.WriteStringValue(ItemId);
            }
            if (_additionalBinaryDataProperties?.ContainsKey("content_index") != true)
            {
                writer.WritePropertyName("content_index"u8);
                writer.WriteNumberValue(ContentIndex);
            }
            if (_additionalBinaryDataProperties?.ContainsKey("error") != true)
            {
                writer.WritePropertyName("error"u8);
                writer.WriteObjectValue(_error, options);
            }
        }

        InputAudioTranscriptionFailedUpdate IJsonModel<InputAudioTranscriptionFailedUpdate>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => (InputAudioTranscriptionFailedUpdate)JsonModelCreateCore(ref reader, options);

        protected override RealtimeUpdate JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InputAudioTranscriptionFailedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InputAudioTranscriptionFailedUpdate)} does not support reading '{format}' format.");
            }
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeInputAudioTranscriptionFailedUpdate(document.RootElement, options);
        }

        internal static InputAudioTranscriptionFailedUpdate DeserializeInputAudioTranscriptionFailedUpdate(JsonElement element, ModelReaderWriterOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            RealtimeUpdateKind kind = default;
            string eventId = default;
            IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
            string itemId = default;
            int contentIndex = default;
            InternalRealtimeServerEventConversationItemInputAudioTranscriptionFailedError error = default;
            foreach (var prop in element.EnumerateObject())
            {
                if (prop.NameEquals("type"u8))
                {
                    kind = prop.Value.GetString().ToRealtimeUpdateKind();
                    continue;
                }
                if (prop.NameEquals("event_id"u8))
                {
                    eventId = prop.Value.GetString();
                    continue;
                }
                if (prop.NameEquals("item_id"u8))
                {
                    itemId = prop.Value.GetString();
                    continue;
                }
                if (prop.NameEquals("content_index"u8))
                {
                    contentIndex = prop.Value.GetInt32();
                    continue;
                }
                if (prop.NameEquals("error"u8))
                {
                    error = InternalRealtimeServerEventConversationItemInputAudioTranscriptionFailedError.DeserializeInternalRealtimeServerEventConversationItemInputAudioTranscriptionFailedError(prop.Value, options);
                    continue;
                }
                // Plugin customization: remove options.Format != "W" check
                additionalBinaryDataProperties.Add(prop.Name, BinaryData.FromString(prop.Value.GetRawText()));
            }
            return new InputAudioTranscriptionFailedUpdate(
                kind,
                eventId,
                additionalBinaryDataProperties,
                itemId,
                contentIndex,
                error);
        }

        BinaryData IPersistableModel<InputAudioTranscriptionFailedUpdate>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

        protected override BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InputAudioTranscriptionFailedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options, OpenAIContext.Default);
                default:
                    throw new FormatException($"The model {nameof(InputAudioTranscriptionFailedUpdate)} does not support writing '{options.Format}' format.");
            }
        }

        InputAudioTranscriptionFailedUpdate IPersistableModel<InputAudioTranscriptionFailedUpdate>.Create(BinaryData data, ModelReaderWriterOptions options) => (InputAudioTranscriptionFailedUpdate)PersistableModelCreateCore(data, options);

        protected override RealtimeUpdate PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InputAudioTranscriptionFailedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    using (JsonDocument document = JsonDocument.Parse(data))
                    {
                        return DeserializeInputAudioTranscriptionFailedUpdate(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(InputAudioTranscriptionFailedUpdate)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<InputAudioTranscriptionFailedUpdate>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";
    }
}
