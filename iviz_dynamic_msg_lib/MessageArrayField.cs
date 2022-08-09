using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic;

public sealed class MessageArrayField<T> : IField<T[]> where T : IMessage, IDeserializable<T>, new()
{
    static readonly IDeserializableRos1<T> Generator = new T();

    public T[] Value { get; set; } = Array.Empty<T>();

    object IField.Value => Value;
        
    public FieldType Type => FieldType.MessageArray;
        
    public int RosLength
    {
        get
        {
            int size = 4;
            foreach (T t in Value)
            {
                size += t.RosMessageLength;
            }

            return size;
        }
    }

    public void RosValidate()
    {
        if (Value == null)
        {
            BuiltIns.ThrowNullReference(nameof(Value));
        }

        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i] is null)
            {
                BuiltIns.ThrowNullReference(nameof(Value), i);
            }

            Value[i].RosValidate();
        }
    }

    public void RosSerialize(ref WriteBuffer b)
    {
        b.SerializeArrayGeneric(Value);
    }

    public void RosDeserializeInPlace(ref ReadBuffer b)
    {
        b.DeserializeArray(out T[] array);
        Value = array;
        for (int i = 0; i < Value.Length; i++)
        {
            Value[i] = Generator.RosDeserialize(ref b);
        }
    }

    public IField Generate()
    {
        return new MessageArrayField<T>();
    }
}