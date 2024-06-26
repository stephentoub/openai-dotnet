// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace OpenAI.VectorStores
{
    public readonly partial struct VectorStoreFileAssociationError
    {
        private readonly IDictionary<string, BinaryData> _serializedAdditionalRawData;

        internal VectorStoreFileAssociationError(VectorStoreFileAssociationErrorCode code, string message)
        {
            Argument.AssertNotNull(message, nameof(message));

            Code = code;
            Message = message;
        }

        internal VectorStoreFileAssociationError(VectorStoreFileAssociationErrorCode code, string message, IDictionary<string, BinaryData> serializedAdditionalRawData)
        {
            Code = code;
            Message = message;
            _serializedAdditionalRawData = serializedAdditionalRawData;
        }

        public VectorStoreFileAssociationError()
        {
        }

        public VectorStoreFileAssociationErrorCode Code { get; }
        public string Message { get; }
    }
}
