// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;
using OpenAI;

namespace OpenAI.Responses
{
    internal readonly partial struct InternalItemContentInputAudioFormat : IEquatable<InternalItemContentInputAudioFormat>
    {
        private readonly string _value;
        private const string Mp3Value = "mp3";
        private const string WavValue = "wav";

        public InternalItemContentInputAudioFormat(string value)
        {
            Argument.AssertNotNull(value, nameof(value));

            _value = value;
        }

        internal static InternalItemContentInputAudioFormat Mp3 { get; } = new InternalItemContentInputAudioFormat(Mp3Value);

        internal static InternalItemContentInputAudioFormat Wav { get; } = new InternalItemContentInputAudioFormat(WavValue);

        public static bool operator ==(InternalItemContentInputAudioFormat left, InternalItemContentInputAudioFormat right) => left.Equals(right);

        public static bool operator !=(InternalItemContentInputAudioFormat left, InternalItemContentInputAudioFormat right) => !left.Equals(right);

        public static implicit operator InternalItemContentInputAudioFormat(string value) => new InternalItemContentInputAudioFormat(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is InternalItemContentInputAudioFormat other && Equals(other);

        public bool Equals(InternalItemContentInputAudioFormat other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value) : 0;

        public override string ToString() => _value;
    }
}
