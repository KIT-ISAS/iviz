using System;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class MessageFixedArrayField<T> : IField<T[]> where T : IMessage, IDeserializable<T>, new()
    {
        static readonly IDeserializable<T> Generator = new T();

        public int Count { get; }

        public T[] Value { get; set; }

        object IField.Value => Value;
        
        public FieldType Type => FieldType.MessageFixedArray;

        public MessageFixedArrayField(int count)
        {
            Count = count;
            Value = new T[count];
            for (int i = 0; i < count; i++)
            {
                Value[i] = new T();
            }
        }

        public int RosLength
        {
            get
            {
                int size = 0;
                for (int i = 0; i < Count; i++)
                {
                    size += Value[i].RosMessageLength;
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

            if (Value.Length != Count)
            {
                throw new RosInvalidSizeForFixedArrayException();
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
            b.SerializeArray(Value, Count);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            for (int i = 0; i < Count; i++)
            {
                Value[i] = Generator.RosDeserialize(ref b);
            }
        }

        public IField Generate()
        {
            return new MessageFixedArrayField<T>(Count);
        }
    }
}