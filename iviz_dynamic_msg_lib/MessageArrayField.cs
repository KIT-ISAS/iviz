using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class MessageArrayField<T> : IField where T : IMessage, IDeserializable<T>, new()
    {
        static readonly IDeserializable<T> Generator = new T();

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
                throw new NullReferenceException(nameof(Value));
            }

            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i] is null)
                {
                    throw new NullReferenceException($"{nameof(Value)}[{i}]");
                }

                Value[i].RosValidate();
            }
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Value);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            Value = b.DeserializeArray<T>();
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
}