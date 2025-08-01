// <auto-generated/>

#nullable disable

using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using OpenAI;

namespace OpenAI.Graders
{
    public partial class ValidateGraderRequest : IJsonModel<ValidateGraderRequest>
    {
        internal ValidateGraderRequest()
        {
        }

        void IJsonModel<ValidateGraderRequest>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            JsonModelWriteCore(writer, options);
            writer.WriteEndObject();
        }

        protected virtual void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ValidateGraderRequest>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(ValidateGraderRequest)} does not support writing '{format}' format.");
            }
            if (_additionalBinaryDataProperties?.ContainsKey("grader") != true)
            {
                writer.WritePropertyName("grader"u8);
#if NET6_0_OR_GREATER
                writer.WriteRawValue(Grader);
#else
                using (JsonDocument document = JsonDocument.Parse(Grader))
                {
                    JsonSerializer.Serialize(writer, document.RootElement);
                }
#endif
            }
            // Plugin customization: remove options.Format != "W" check
            if (_additionalBinaryDataProperties != null)
            {
                foreach (var item in _additionalBinaryDataProperties)
                {
                    if (ModelSerializationExtensions.IsSentinelValue(item.Value))
                    {
                        continue;
                    }
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
        }

        ValidateGraderRequest IJsonModel<ValidateGraderRequest>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => JsonModelCreateCore(ref reader, options);

        protected virtual ValidateGraderRequest JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ValidateGraderRequest>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(ValidateGraderRequest)} does not support reading '{format}' format.");
            }
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeValidateGraderRequest(document.RootElement, options);
        }

        internal static ValidateGraderRequest DeserializeValidateGraderRequest(JsonElement element, ModelReaderWriterOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            BinaryData grader = default;
            IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
            foreach (var prop in element.EnumerateObject())
            {
                if (prop.NameEquals("grader"u8))
                {
                    grader = BinaryData.FromString(prop.Value.GetRawText());
                    continue;
                }
                // Plugin customization: remove options.Format != "W" check
                additionalBinaryDataProperties.Add(prop.Name, BinaryData.FromString(prop.Value.GetRawText()));
            }
            return new ValidateGraderRequest(grader, additionalBinaryDataProperties);
        }

        BinaryData IPersistableModel<ValidateGraderRequest>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

        protected virtual BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ValidateGraderRequest>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options, OpenAIContext.Default);
                default:
                    throw new FormatException($"The model {nameof(ValidateGraderRequest)} does not support writing '{options.Format}' format.");
            }
        }

        ValidateGraderRequest IPersistableModel<ValidateGraderRequest>.Create(BinaryData data, ModelReaderWriterOptions options) => PersistableModelCreateCore(data, options);

        protected virtual ValidateGraderRequest PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ValidateGraderRequest>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    using (JsonDocument document = JsonDocument.Parse(data))
                    {
                        return DeserializeValidateGraderRequest(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(ValidateGraderRequest)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<ValidateGraderRequest>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";
    }
}
