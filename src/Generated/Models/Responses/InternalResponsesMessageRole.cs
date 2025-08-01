// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;
using OpenAI;

namespace OpenAI.Responses
{
    internal readonly partial struct InternalResponsesMessageRole : IEquatable<InternalResponsesMessageRole>
    {
        private readonly string _value;
        private const string SystemValue = "system";
        private const string DeveloperValue = "developer";
        private const string UserValue = "user";
        private const string AssistantValue = "assistant";

        public InternalResponsesMessageRole(string value)
        {
            Argument.AssertNotNull(value, nameof(value));

            _value = value;
        }

        internal static InternalResponsesMessageRole System { get; } = new InternalResponsesMessageRole(SystemValue);

        internal static InternalResponsesMessageRole Developer { get; } = new InternalResponsesMessageRole(DeveloperValue);

        internal static InternalResponsesMessageRole User { get; } = new InternalResponsesMessageRole(UserValue);

        internal static InternalResponsesMessageRole Assistant { get; } = new InternalResponsesMessageRole(AssistantValue);

        public static bool operator ==(InternalResponsesMessageRole left, InternalResponsesMessageRole right) => left.Equals(right);

        public static bool operator !=(InternalResponsesMessageRole left, InternalResponsesMessageRole right) => !left.Equals(right);

        public static implicit operator InternalResponsesMessageRole(string value) => new InternalResponsesMessageRole(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is InternalResponsesMessageRole other && Equals(other);

        public bool Equals(InternalResponsesMessageRole other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value) : 0;

        public override string ToString() => _value;
    }
}
