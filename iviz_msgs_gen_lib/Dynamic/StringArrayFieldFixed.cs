using System;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    internal sealed class StringArrayFieldFixed : IField
    {
        public uint Count { get; }

        public string[] Value { get; set; }

        object IField.Value => Value;

        public StringArrayFieldFixed(uint count)
        {
            Count = count;
            Value = new string[count];
            for (int i = 0; i < count; i++)
            {
                Value[i] = "";
            }
        }

        public int RosMessageLength
        {
            get
            {
                int size = 4 * (int) Count;
                for (int i = 0; i < Count; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Value[i]);
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
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i] is null)
                {
                    throw new NullReferenceException($"{nameof(Value)}[{i}]");
                }
            }
        }

        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Value, Count);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            Value = b.DeserializeStringArray(Count);
        }

        public IField Generate()
        {
            return new StringArrayFieldFixed(Count);
        }
    }
}