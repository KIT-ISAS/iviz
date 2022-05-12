using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
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
                BuiltIns.ThrowNullReference(nameof(Value));
            }

            if (Value.Length != Count)
            {
                BuiltIns.ThrowInvalidSizeForFixedArray(Value.Length, Count);
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