﻿using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace OpenAI.Assistants;

[Experimental("OPENAI001")]
public readonly partial struct RunStepCollectionOrder : IEquatable<RunStepCollectionOrder>
{
    public static RunStepCollectionOrder Ascending { get; } = new RunStepCollectionOrder("asc");

    public static RunStepCollectionOrder Descending { get; } = new RunStepCollectionOrder("desc");

    private readonly string _value;
    private const string AscValue = "asc";
    private const string DescValue = "desc";

    public RunStepCollectionOrder(string value)
    {
        Argument.AssertNotNull(value, nameof(value));

        _value = value;
    }

    public static bool operator ==(RunStepCollectionOrder left, RunStepCollectionOrder right) => left.Equals(right);

    public static bool operator !=(RunStepCollectionOrder left, RunStepCollectionOrder right) => !left.Equals(right);

    public static implicit operator RunStepCollectionOrder(string value) => new RunStepCollectionOrder(value);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Equals(object obj) => obj is RunStepCollectionOrder other && Equals(other);

    public bool Equals(RunStepCollectionOrder other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int GetHashCode() => _value != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value) : 0;

    public override string ToString() => _value;
}
