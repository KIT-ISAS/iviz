using System;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    internal sealed class DynamicMessageFixedArrayField : IField
    {
        readonly DynamicMessage generator;
        
        public int Count { get; }
        public DynamicMessage[] Value { get; set; }

        object IField.Value => Value;

        public FieldType Type => FieldType.DynamicMessageFixedArray;

        public DynamicMessageFixedArrayField(int count, DynamicMessage generator)
        {
            Count = count;
            this.generator = generator;

            Value = new DynamicMessage[count];
            for (int i = 0; i < count; i++)
            {
                Value[i] = new DynamicMessage(generator);
            }
        }

        public int RosLength
        {
            get
            {
                int size = 0;
                foreach (DynamicMessage field in Value)
                {
                    size += field.RosLength;
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
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Value, Count);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            foreach (DynamicMessage t in Value)
            {
                t.RosDeserializeInPlace(ref b);
            }
        }

        public IField Generate()
        {
            return new DynamicMessageFixedArrayField(Count, generator);
        }
    }
}