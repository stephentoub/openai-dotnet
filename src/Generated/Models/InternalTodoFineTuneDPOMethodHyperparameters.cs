// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace OpenAI.FineTuning
{
    internal partial class InternalTodoFineTuneDPOMethodHyperparameters
    {
        private protected IDictionary<string, BinaryData> _additionalBinaryDataProperties;

        public InternalTodoFineTuneDPOMethodHyperparameters()
        {
        }

        internal InternalTodoFineTuneDPOMethodHyperparameters(BinaryData beta, BinaryData batchSize, BinaryData learningRateMultiplier, BinaryData nEpochs, IDictionary<string, BinaryData> additionalBinaryDataProperties)
        {
            Beta = beta;
            BatchSize = batchSize;
            LearningRateMultiplier = learningRateMultiplier;
            NEpochs = nEpochs;
            _additionalBinaryDataProperties = additionalBinaryDataProperties;
        }

        public BinaryData Beta { get; set; }

        public BinaryData BatchSize { get; set; }

        public BinaryData LearningRateMultiplier { get; set; }

        public BinaryData NEpochs { get; set; }

        internal IDictionary<string, BinaryData> SerializedAdditionalRawData
        {
            get => _additionalBinaryDataProperties;
            set => _additionalBinaryDataProperties = value;
        }
    }
}
