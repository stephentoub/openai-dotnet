// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using OpenAI;

namespace OpenAI.Responses
{
    internal partial class InternalComputerUsePreviewToolCallItemParam : InternalItemParam
    {
        public InternalComputerUsePreviewToolCallItemParam(string callId, ComputerCallAction action, IEnumerable<ComputerCallSafetyCheck> pendingSafetyChecks) : base(InternalItemType.ComputerCall)
        {
            Argument.AssertNotNull(callId, nameof(callId));
            Argument.AssertNotNull(action, nameof(action));
            Argument.AssertNotNull(pendingSafetyChecks, nameof(pendingSafetyChecks));

            CallId = callId;
            Action = action;
            PendingSafetyChecks = pendingSafetyChecks.ToList();
        }

        internal InternalComputerUsePreviewToolCallItemParam(InternalItemType kind, IDictionary<string, BinaryData> additionalBinaryDataProperties, string callId, ComputerCallAction action, IList<ComputerCallSafetyCheck> pendingSafetyChecks) : base(kind, additionalBinaryDataProperties)
        {
            // Plugin customization: ensure initialization of collections
            CallId = callId;
            Action = action;
            PendingSafetyChecks = pendingSafetyChecks ?? new ChangeTrackingList<ComputerCallSafetyCheck>();
        }

        public string CallId { get; }

        public ComputerCallAction Action { get; }

        public IList<ComputerCallSafetyCheck> PendingSafetyChecks { get; }
    }
}
