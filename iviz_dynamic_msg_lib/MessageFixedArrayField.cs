using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic;

public sealed class MessageFixedArrayField<T> : IField<T[]> where T : IMessage, new()
{
    static readonly Serializer<T> Serializer = new T().CreateSerializer();
    static readonly Deserializer<T> Deserializer = new T().CreateDeserializer();

    public readonly int Count;

    T[] value;

    public T[] Value
    {
        get => value;
        set => this.value = value;
    }

    object IField.Value => value;
        
    public FieldType Type => FieldType.MessageFixedArray;

    public MessageFixedArrayField(int count)
    {
        Count = count;
        value = new T[count];
        for (int i = 0; i < count; i++)
        {
            value[i] = new T();
        }
    }

    public int RosLength
    {
        get
        {
            int size = 0;
            for (int i = 0; i < Count; i++)
            {
                size += Serializer.RosMessageLength(value[i]);
            }

            return size;
        }
    }

    public void RosValidate()
    {
        if (value == null) BuiltIns.ThrowNullReference(nameof(value));

        if (value.Length != Count)
        {
            BuiltIns.ThrowInvalidSizeForFixedArray(value.Length, Count);
        }

        for (int i = 0; i < Count; i++)
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
        for (int i = 0; i < Count; i++)
        {
            Serializer.RosSerialize(value[i], ref b);
        }
    }

    public void RosDeserializeInPlace(ref ReadBuffer b)
    {
        for (int i = 0; i < Count; i++)
        {
            Deserializer.RosDeserialize(ref b, out value[i]);
        }
    }

    public IField Generate()
    {
        return new MessageFixedArrayField<T>(Count);
    }
}