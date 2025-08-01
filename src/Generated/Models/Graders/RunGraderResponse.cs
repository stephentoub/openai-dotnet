// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OpenAI.Graders
{
    [Experimental("OPENAI001")]
    public partial class RunGraderResponse
    {
        private protected IDictionary<string, BinaryData> _additionalBinaryDataProperties;

        internal RunGraderResponse(float reward, RunGraderResponseMetadata metadata, BinaryData subRewards, BinaryData modelGraderTokenUsagePerModel)
        {
            Reward = reward;
            Metadata = metadata;
            SubRewards = subRewards;
            ModelGraderTokenUsagePerModel = modelGraderTokenUsagePerModel;
        }

        internal RunGraderResponse(float reward, RunGraderResponseMetadata metadata, BinaryData subRewards, BinaryData modelGraderTokenUsagePerModel, IDictionary<string, BinaryData> additionalBinaryDataProperties)
        {
            Reward = reward;
            Metadata = metadata;
            SubRewards = subRewards;
            ModelGraderTokenUsagePerModel = modelGraderTokenUsagePerModel;
            _additionalBinaryDataProperties = additionalBinaryDataProperties;
        }

        public float Reward { get; }

        public RunGraderResponseMetadata Metadata { get; }

        public BinaryData SubRewards { get; }

        public BinaryData ModelGraderTokenUsagePerModel { get; }

        internal IDictionary<string, BinaryData> SerializedAdditionalRawData
        {
            get => _additionalBinaryDataProperties;
            set => _additionalBinaryDataProperties = value;
        }
    }
}
