// <auto-generated/>

#nullable disable

using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using OpenAI;

namespace OpenAI.Responses
{
    internal partial class InternalLocalShellToolCallItemResource : IJsonModel<InternalLocalShellToolCallItemResource>
    {
        internal InternalLocalShellToolCallItemResource() : this(InternalItemType.LocalShellCall, null, null, default, null, null)
        {
        }

        void IJsonModel<InternalLocalShellToolCallItemResource>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            JsonModelWriteCore(writer, options);
            writer.WriteEndObject();
        }

        protected override void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallItemResource>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalLocalShellToolCallItemResource)} does not support writing '{format}' format.");
            }
            base.JsonModelWriteCore(writer, options);
            if (_additionalBinaryDataProperties?.ContainsKey("status") != true)
            {
                writer.WritePropertyName("status"u8);
                writer.WriteStringValue(Status.ToString());
            }
            if (_additionalBinaryDataProperties?.ContainsKey("call_id") != true)
            {
                writer.WritePropertyName("call_id"u8);
                writer.WriteStringValue(CallId);
            }
            if (_additionalBinaryDataProperties?.ContainsKey("action") != true)
            {
                writer.WritePropertyName("action"u8);
                writer.WriteObjectValue(Action, options);
            }
        }

        InternalLocalShellToolCallItemResource IJsonModel<InternalLocalShellToolCallItemResource>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => (InternalLocalShellToolCallItemResource)JsonModelCreateCore(ref reader, options);

        protected override ResponseItem JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallItemResource>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(InternalLocalShellToolCallItemResource)} does not support reading '{format}' format.");
            }
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeInternalLocalShellToolCallItemResource(document.RootElement, options);
        }

        internal static InternalLocalShellToolCallItemResource DeserializeInternalLocalShellToolCallItemResource(JsonElement element, ModelReaderWriterOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            InternalItemType kind = default;
            string id = default;
            IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
            InternalLocalShellToolCallItemResourceStatus status = default;
            string callId = default;
            InternalLocalShellExecAction action = default;
            foreach (var prop in element.EnumerateObject())
            {
                if (prop.NameEquals("type"u8))
                {
                    kind = new InternalItemType(prop.Value.GetString());
                    continue;
                }
                if (prop.NameEquals("id"u8))
                {
                    id = prop.Value.GetString();
                    continue;
                }
                if (prop.NameEquals("status"u8))
                {
                    status = new InternalLocalShellToolCallItemResourceStatus(prop.Value.GetString());
                    continue;
                }
                if (prop.NameEquals("call_id"u8))
                {
                    callId = prop.Value.GetString();
                    continue;
                }
                if (prop.NameEquals("action"u8))
                {
                    action = InternalLocalShellExecAction.DeserializeInternalLocalShellExecAction(prop.Value, options);
                    continue;
                }
                // Plugin customization: remove options.Format != "W" check
                additionalBinaryDataProperties.Add(prop.Name, BinaryData.FromString(prop.Value.GetRawText()));
            }
            return new InternalLocalShellToolCallItemResource(
                kind,
                id,
                additionalBinaryDataProperties,
                status,
                callId,
                action);
        }

        BinaryData IPersistableModel<InternalLocalShellToolCallItemResource>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

        protected override BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallItemResource>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options, OpenAIContext.Default);
                default:
                    throw new FormatException($"The model {nameof(InternalLocalShellToolCallItemResource)} does not support writing '{options.Format}' format.");
            }
        }

        InternalLocalShellToolCallItemResource IPersistableModel<InternalLocalShellToolCallItemResource>.Create(BinaryData data, ModelReaderWriterOptions options) => (InternalLocalShellToolCallItemResource)PersistableModelCreateCore(data, options);

        protected override ResponseItem PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<InternalLocalShellToolCallItemResource>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    using (JsonDocument document = JsonDocument.Parse(data))
                    {
                        return DeserializeInternalLocalShellToolCallItemResource(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(InternalLocalShellToolCallItemResource)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<InternalLocalShellToolCallItemResource>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";
    }
}
