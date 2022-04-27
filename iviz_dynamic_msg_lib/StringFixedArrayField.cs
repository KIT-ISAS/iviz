using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class StringFixedArrayField : IField
    {
        public int Count { get; }

        public string[] Value { get; set; }

        object IField.Value => Value;
        
        public FieldType Type => FieldType.StringFixedArray;

        public StringFixedArrayField(int count)
        {
            Count = count;
            Value = new string[count];
            for (int i = 0; i < count; i++)
            {
                Value[i] = "";
            }
        }

        public int RosLength
        {
            get
            {
                int size = 4 * Count;
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
                throw new RosInvalidSizeForFixedArrayException();
            }

            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i] is null)
                {
                    throw new NullReferenceException($"{nameof(Value)}[{i}]");
                }
            }
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Value, Count);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            b.DeserializeStringArray(Count, out string[] val);
            Value = val;
        }

        public IField Generate()
        {
            return new StringFixedArrayField(Count);
        }
    }
}