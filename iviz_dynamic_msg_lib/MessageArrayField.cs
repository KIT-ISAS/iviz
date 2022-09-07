using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic;

public sealed class MessageArrayField<T> : IField<T[]> where T : IMessage, new()
{
    static readonly Serializer<T> Serializer = new T().CreateSerializer();
    static readonly Deserializer<T> Deserializer = new T().CreateDeserializer();

    T[] value = Array.Empty<T>();

    public T[] Value
    {
        get => value;
        set => this.value = value;
    }

    object IField.Value => value;
        
    public FieldType Type => FieldType.MessageArray;
        
    public int RosLength
    {
        get
        {
            int size = 4;
            foreach (var t in value)
            {
                size += Serializer.RosMessageLength(t);
            }

            return size;
        }
    }

    public void RosValidate()
    {
        if (value == null) BuiltIns.ThrowNullReference(nameof(value));

        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] is null)
            {
                BuiltIns.ThrowNullReference(nameof(value), i);
            }

            Serializer.RosValidate(value[i]);
        }
    }

    public void RosSerialize(ref WriteBuffer b)
    {
        b.Serialize(value.Length);
        foreach (var t in value)
        {
            Serializer.RosSerialize(t, ref b);
        }
    }

    public void RosDeserializeInPlace(ref ReadBuffer b)
    {
        b.DeserializeArray(out T[] array);
        value = array;
        for (int i = 0; i < value.Length; i++)
        {
            Deserializer.RosDeserialize(ref b, out value[i]);
        }
    }

    public IField Generate()
    {
        return new MessageArrayField<T>();
    }
}