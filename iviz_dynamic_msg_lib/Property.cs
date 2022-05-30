using System;
using System.Runtime.Serialization;

namespace Iviz.MsgsGen.Dynamic;

[DataContract]
public sealed class Property
{
    public Property(string name, IField value)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    [DataMember] public string Name { get; }
    [DataMember] public IField Value { get; }

    public void Deconstruct(out string name, out IField value)
    {
        name = Name;
        value = Value;
    }

    public bool TrySet<T>(T value) where T : unmanaged
    {
        if (Value is not StructField<T> valueAsT)
        {
            return false;
        }

        valueAsT.Value = value;
        return true;
    }

    public bool TryGet<T>(out T value) where T : unmanaged
    {
        if (Value is not StructField<T> valueAsT)
        {
            value = default;
            return false;
        }

        value = valueAsT.Value;
        return true;
    }

    public bool TrySet(string value)
    {
        if (Value is not StringField valueAsString)
        {
            return false;
        }

        valueAsString.Value = value;
        return true;
    }

    public bool TryGet(out string value)
    {
        if (Value is not StringField valueAsString)
        {
            value = "";
            return false;
        }

        value = valueAsString.Value;
        return true;
    }

    public override string ToString() => Msgs.BuiltIns.ToJsonString(this);
}