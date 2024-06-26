// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace OpenAI.Batch
{
    internal partial class InternalBatchRequestInput
    {
        internal IDictionary<string, BinaryData> _serializedAdditionalRawData;

        internal InternalBatchRequestInput()
        {
        }

        internal InternalBatchRequestInput(string customId, string method, Uri url, IDictionary<string, BinaryData> serializedAdditionalRawData)
        {
            CustomId = customId;
            Method = method;
            Url = url;
            _serializedAdditionalRawData = serializedAdditionalRawData;
        }

        public string CustomId { get; }
        public string Method { get; }
        public Uri Url { get; }
    }
}
