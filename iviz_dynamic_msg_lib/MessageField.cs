using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic;

public sealed class MessageField<T> : IField<T> where T : IMessage, IDeserializable<T>, new()
{
    static readonly IDeserializable<T> Generator = new T();

    public T Value { get; set; } = new();

    object IField.Value => Value;

    public FieldType Type => FieldType.Message;

    public int RosLength => Value.RosMessageLength;

    public void RosValidate()
    {
        if (Value == null)
        {
            BuiltIns.ThrowNullReference(nameof(Value));
        }

        Value.RosValidate();
    }

    public void RosSerialize(ref WriteBuffer b)
    {
        Value.RosSerialize(ref b);
    }

    public void RosDeserializeInPlace(ref ReadBuffer b)
    {
        Value = Generator.RosDeserialize(ref b);
    }

    public IField Generate()
    {
        return new MessageField<T>();
    }
}