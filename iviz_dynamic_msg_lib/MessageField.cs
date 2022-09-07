using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic;

public sealed class MessageField<T> : IField<T> where T : IMessage, new()
{
    static readonly Serializer<T> Serializer = new T().CreateSerializer();
    static readonly Deserializer<T> Deserializer = new T().CreateDeserializer();

    T value = new T();

    public T Value
    {
        get => value;
        set => this.value = value;
    }

    object IField.Value => value;

    public FieldType Type => FieldType.Message;

    public int RosLength => Serializer.RosMessageLength(value);

    public void RosValidate()
    {
        Serializer.RosValidate(value);
    }

    public void RosSerialize(ref WriteBuffer b)
    {
        Serializer.RosSerialize(value, ref b);
    }

    public void RosDeserializeInPlace(ref ReadBuffer b)
    {
        Deserializer.RosDeserialize(ref b, out value);
    }

    public IField Generate()
    {
        return new MessageField<T>();
    }
}