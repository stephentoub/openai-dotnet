// <auto-generated/>

#nullable disable

using System;
using System.ClientModel.Primitives;
using System.Text.Json;
using OpenAI;

namespace OpenAI.Evals
{
    [PersistableModelProxy(typeof(InternalUnknownEvalGraderParams))]
    internal abstract partial class InternalEvalGraderParams : IJsonModel<InternalEvalGraderParams>
    {
        internal InternalEvalGraderParams()
        {
        }

        void IJsonModel<InternalEvalGraderParams>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            JsonModelWriteCore(writer, options);
            writer.WriteEndObject();
        }

        protected virtual void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalEvalGraderParams>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalEvalGraderParams)} does not support writing '{format}' format.");
            }
            if (_additionalBinaryDataProperties?.ContainsKey("type") != true)
            {
                writer.WritePropertyName("type"u8);
                writer.WriteStringValue(Kind.ToString());
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

        InternalEvalGraderParams IJsonModel<InternalEvalGraderParams>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => JsonModelCreateCore(ref reader, options);

        protected virtual InternalEvalGraderParams JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalEvalGraderParams>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalEvalGraderParams)} does not support reading '{format}' format.");
            }
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeInternalEvalGraderParams(document.RootElement, options);
        }

        internal static InternalEvalGraderParams DeserializeInternalEvalGraderParams(JsonElement element, ModelReaderWriterOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            if (element.TryGetProperty("type"u8, out JsonElement discriminator))
            {
                switch (discriminator.GetString())
                {
                    case "label_model":
                        return InternalEvalGraderLabelModelParams.DeserializeInternalEvalGraderLabelModelParams(element, options);
                    case "string_check":
                        return InternalEvalGraderStringCheckParams.DeserializeInternalEvalGraderStringCheckParams(element, options);
                    case "text_similarity":
                        return InternalEvalGraderTextSimilarityParams.DeserializeInternalEvalGraderTextSimilarityParams(element, options);
                    case "python":
                        return InternalEvalGraderPythonParams.DeserializeInternalEvalGraderPythonParams(element, options);
                    case "score_model":
                        return InternalEvalGraderScoreModelParams.DeserializeInternalEvalGraderScoreModelParams(element, options);
                }
            }
            return InternalUnknownEvalGraderParams.DeserializeInternalUnknownEvalGraderParams(element, options);
        }

        BinaryData IPersistableModel<InternalEvalGraderParams>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

        protected virtual BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalEvalGraderParams>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options, OpenAIContext.Default);
                default:
                    throw new FormatException($"The model {nameof(InternalEvalGraderParams)} does not support writing '{options.Format}' format.");
            }
        }

        InternalEvalGraderParams IPersistableModel<InternalEvalGraderParams>.Create(BinaryData data, ModelReaderWriterOptions options) => PersistableModelCreateCore(data, options);

        protected virtual InternalEvalGraderParams PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalEvalGraderParams>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    using (JsonDocument document = JsonDocument.Parse(data))
                    {
                        return DeserializeInternalEvalGraderParams(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(InternalEvalGraderParams)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<InternalEvalGraderParams>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";
    }
}
