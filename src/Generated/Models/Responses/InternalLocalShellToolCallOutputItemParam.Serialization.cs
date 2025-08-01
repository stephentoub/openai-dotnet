// <auto-generated/>

#nullable disable

using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using OpenAI;

namespace OpenAI.Responses
{
    internal partial class InternalLocalShellToolCallOutputItemParam : IJsonModel<InternalLocalShellToolCallOutputItemParam>
    {
        internal InternalLocalShellToolCallOutputItemParam() : this(InternalItemType.LocalShellCallOutput, null, null)
        {
        }

        void IJsonModel<InternalLocalShellToolCallOutputItemParam>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            JsonModelWriteCore(writer, options);
            writer.WriteEndObject();
        }

        protected override void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallOutputItemParam>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalLocalShellToolCallOutputItemParam)} does not support writing '{format}' format.");
            }
            base.JsonModelWriteCore(writer, options);
            if (_additionalBinaryDataProperties?.ContainsKey("output") != true)
            {
                writer.WritePropertyName("output"u8);
                writer.WriteStringValue(Output);
            }
        }

        InternalLocalShellToolCallOutputItemParam IJsonModel<InternalLocalShellToolCallOutputItemParam>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => (InternalLocalShellToolCallOutputItemParam)JsonModelCreateCore(ref reader, options);

        protected override InternalItemParam JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallOutputItemParam>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalLocalShellToolCallOutputItemParam)} does not support reading '{format}' format.");
            }
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeInternalLocalShellToolCallOutputItemParam(document.RootElement, options);
        }

        internal static InternalLocalShellToolCallOutputItemParam DeserializeInternalLocalShellToolCallOutputItemParam(JsonElement element, ModelReaderWriterOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            InternalItemType kind = default;
            IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
            string output = default;
            foreach (var prop in element.EnumerateObject())
            {
                if (prop.NameEquals("type"u8))
                {
                    kind = new InternalItemType(prop.Value.GetString());
                    continue;
                }
                if (prop.NameEquals("output"u8))
                {
                    output = prop.Value.GetString();
                    continue;
                }
                // Plugin customization: remove options.Format != "W" check
                additionalBinaryDataProperties.Add(prop.Name, BinaryData.FromString(prop.Value.GetRawText()));
            }
            return new InternalLocalShellToolCallOutputItemParam(kind, additionalBinaryDataProperties, output);
        }

        BinaryData IPersistableModel<InternalLocalShellToolCallOutputItemParam>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

        protected override BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallOutputItemParam>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options, OpenAIContext.Default);
                default:
                    throw new FormatException($"The model {nameof(InternalLocalShellToolCallOutputItemParam)} does not support writing '{options.Format}' format.");
            }
        }

        InternalLocalShellToolCallOutputItemParam IPersistableModel<InternalLocalShellToolCallOutputItemParam>.Create(BinaryData data, ModelReaderWriterOptions options) => (InternalLocalShellToolCallOutputItemParam)PersistableModelCreateCore(data, options);

        protected override InternalItemParam PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallOutputItemParam>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    using (JsonDocument document = JsonDocument.Parse(data))
                    {
                        return DeserializeInternalLocalShellToolCallOutputItemParam(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(InternalLocalShellToolCallOutputItemParam)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<InternalLocalShellToolCallOutputItemParam>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";
    }
}
