// <auto-generated/>

#nullable disable

using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;

namespace OpenAI.FineTuning
{
    internal partial class InternalFinetuneCompletionRequestInput : IJsonModel<InternalFinetuneCompletionRequestInput>
    {
        void IJsonModel<InternalFinetuneCompletionRequestInput>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<InternalFinetuneCompletionRequestInput>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalFinetuneCompletionRequestInput)} does not support writing '{format}' format.");
            }

            writer.WriteStartObject();
            if (Optional.IsDefined(Prompt))
            {
                writer.WritePropertyName("prompt"u8);
                writer.WriteStringValue(Prompt);
            }
            if (Optional.IsDefined(Completion))
            {
                writer.WritePropertyName("completion"u8);
                writer.WriteStringValue(Completion);
            }
            if (true && _serializedAdditionalRawData != null)
            {
                foreach (var item in _serializedAdditionalRawData)
                {
                    writer.WritePropertyName(item.Key);
#if NET6_0_OR_GREATER
				writer.WriteRawValue(item.Value);
#else
                    using (JsonDocument document = JsonDocument.Parse(item.Value))
                    {
                        JsonSerializer.Serialize(writer, document.RootElement);
                    }
#endif
                }
            }
            writer.WriteEndObject();
        }

        InternalFinetuneCompletionRequestInput IJsonModel<InternalFinetuneCompletionRequestInput>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<InternalFinetuneCompletionRequestInput>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalFinetuneCompletionRequestInput)} does not support reading '{format}' format.");
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeInternalFinetuneCompletionRequestInput(document.RootElement, options);
        }

        internal static InternalFinetuneCompletionRequestInput DeserializeInternalFinetuneCompletionRequestInput(JsonElement element, ModelReaderWriterOptions options = null)
        {
            options ??= ModelSerializationExtensions.WireOptions;

            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            string prompt = default;
            string completion = default;
            IDictionary<string, BinaryData> serializedAdditionalRawData = default;
            Dictionary<string, BinaryData> rawDataDictionary = new Dictionary<string, BinaryData>();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("prompt"u8))
                {
                    prompt = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("completion"u8))
                {
                    completion = property.Value.GetString();
                    continue;
                }
                if (true)
                {
                    rawDataDictionary.Add(property.Name, BinaryData.FromString(property.Value.GetRawText()));
                }
            }
            serializedAdditionalRawData = rawDataDictionary;
            return new InternalFinetuneCompletionRequestInput(prompt, completion, serializedAdditionalRawData);
        }

        BinaryData IPersistableModel<InternalFinetuneCompletionRequestInput>.Write(ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<InternalFinetuneCompletionRequestInput>)this).GetFormatFromOptions(options) : options.Format;

            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options);
                default:
                    throw new FormatException($"The model {nameof(InternalFinetuneCompletionRequestInput)} does not support writing '{options.Format}' format.");
            }
        }

        InternalFinetuneCompletionRequestInput IPersistableModel<InternalFinetuneCompletionRequestInput>.Create(BinaryData data, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<InternalFinetuneCompletionRequestInput>)this).GetFormatFromOptions(options) : options.Format;

            switch (format)
            {
                case "J":
                    {
                        using JsonDocument document = JsonDocument.Parse(data);
                        return DeserializeInternalFinetuneCompletionRequestInput(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(InternalFinetuneCompletionRequestInput)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<InternalFinetuneCompletionRequestInput>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

        internal static InternalFinetuneCompletionRequestInput FromResponse(PipelineResponse response)
        {
            using var document = JsonDocument.Parse(response.Content);
            return DeserializeInternalFinetuneCompletionRequestInput(document.RootElement);
        }

        internal virtual BinaryContent ToBinaryContent()
        {
            return BinaryContent.Create(this, ModelSerializationExtensions.WireOptions);
        }
    }
}
