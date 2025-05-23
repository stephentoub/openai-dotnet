// <auto-generated/>

#nullable disable

using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using OpenAI;

namespace OpenAI.RealtimeConversation
{
    public partial class ConversationResponseFinishedUpdate : IJsonModel<ConversationResponseFinishedUpdate>
    {
        internal ConversationResponseFinishedUpdate()
        {
        }

        void IJsonModel<ConversationResponseFinishedUpdate>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            JsonModelWriteCore(writer, options);
            writer.WriteEndObject();
        }

        protected override void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ConversationResponseFinishedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(ConversationResponseFinishedUpdate)} does not support writing '{format}' format.");
            }
            base.JsonModelWriteCore(writer, options);
            if (_additionalBinaryDataProperties?.ContainsKey("response") != true)
            {
                writer.WritePropertyName("response"u8);
                writer.WriteObjectValue(_internalResponse, options);
            }
        }

        ConversationResponseFinishedUpdate IJsonModel<ConversationResponseFinishedUpdate>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options) => (ConversationResponseFinishedUpdate)JsonModelCreateCore(ref reader, options);

        protected override ConversationUpdate JsonModelCreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ConversationResponseFinishedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(ConversationResponseFinishedUpdate)} does not support reading '{format}' format.");
            }
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeConversationResponseFinishedUpdate(document.RootElement, options);
        }

        internal static ConversationResponseFinishedUpdate DeserializeConversationResponseFinishedUpdate(JsonElement element, ModelReaderWriterOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            string eventId = default;
            ConversationUpdateKind kind = default;
            IDictionary<string, BinaryData> additionalBinaryDataProperties = new ChangeTrackingDictionary<string, BinaryData>();
            InternalRealtimeResponse internalResponse = default;
            foreach (var prop in element.EnumerateObject())
            {
                if (prop.NameEquals("event_id"u8))
                {
                    eventId = prop.Value.GetString();
                    continue;
                }
                if (prop.NameEquals("type"u8))
                {
                    kind = prop.Value.GetString().ToConversationUpdateKind();
                    continue;
                }
                if (prop.NameEquals("response"u8))
                {
                    internalResponse = InternalRealtimeResponse.DeserializeInternalRealtimeResponse(prop.Value, options);
                    continue;
                }
                additionalBinaryDataProperties.Add(prop.Name, BinaryData.FromString(prop.Value.GetRawText()));
            }
            return new ConversationResponseFinishedUpdate(eventId, kind, additionalBinaryDataProperties, internalResponse);
        }

        BinaryData IPersistableModel<ConversationResponseFinishedUpdate>.Write(ModelReaderWriterOptions options) => PersistableModelWriteCore(options);

        protected override BinaryData PersistableModelWriteCore(ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ConversationResponseFinishedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options);
                default:
                    throw new FormatException($"The model {nameof(ConversationResponseFinishedUpdate)} does not support writing '{options.Format}' format.");
            }
        }

        ConversationResponseFinishedUpdate IPersistableModel<ConversationResponseFinishedUpdate>.Create(BinaryData data, ModelReaderWriterOptions options) => (ConversationResponseFinishedUpdate)PersistableModelCreateCore(data, options);

        protected override ConversationUpdate PersistableModelCreateCore(BinaryData data, ModelReaderWriterOptions options)
        {
            string format = options.Format == "W" ? ((IPersistableModel<ConversationResponseFinishedUpdate>)this).GetFormatFromOptions(options) : options.Format;
            switch (format)
            {
                case "J":
                    using (JsonDocument document = JsonDocument.Parse(data))
                    {
                        return DeserializeConversationResponseFinishedUpdate(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(ConversationResponseFinishedUpdate)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<ConversationResponseFinishedUpdate>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

        public static implicit operator BinaryContent(ConversationResponseFinishedUpdate conversationResponseFinishedUpdate)
        {
            if (conversationResponseFinishedUpdate == null)
            {
                return null;
            }
            return BinaryContent.Create(conversationResponseFinishedUpdate, ModelSerializationExtensions.WireOptions);
        }

        public static explicit operator ConversationResponseFinishedUpdate(ClientResult result)
        {
            using PipelineResponse response = result.GetRawResponse();
            using JsonDocument document = JsonDocument.Parse(response.Content);
            return DeserializeConversationResponseFinishedUpdate(document.RootElement, ModelSerializationExtensions.WireOptions);
        }
    }
}
