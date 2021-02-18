using System;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class MessageField<T> : IField<T> where T : IMessage, IDeserializable<T>, new()
    {
        static readonly IDeserializable<T> Generator = new T();

        public T Value { get; set; } = new T();

        object IField.Value => Value;
        
        public FieldType Type => FieldType.Message;
        
        public int RosMessageLength => Value.RosMessageLength;

        public void RosValidate()
        {
            if (Value == null)
            {
                throw new NullReferenceException(nameof(Value));
            }

            Value.RosValidate();
        }

        public void RosSerialize(ref Buffer b)
        {
            Value.RosSerialize(ref b);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            Value = Generator.RosDeserialize(ref b);
        }

        public IField Generate()
        {
            return new MessageField<T>();
        }
    }
}