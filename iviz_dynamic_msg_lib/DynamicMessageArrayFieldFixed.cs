using System;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    internal sealed class DynamicMessageArrayFieldFixed : IField
    {
        readonly DynamicMessage generator;
        
        public uint Count { get; }
        public DynamicMessage[] Value { get; set; }

        object IField.Value => Value;

        public FieldType Type => FieldType.DynamicMessageFixedArray;

        public DynamicMessageArrayFieldFixed(uint count, DynamicMessage generator)
        {
            Count = count;
            this.generator = generator;

            Value = new DynamicMessage[count];
            for (int i = 0; i < count; i++)
            {
                Value[i] = new DynamicMessage(generator);
            }
        }

        public int RosMessageLength
        {
            get
            {
                int size = 0;
                foreach (DynamicMessage field in Value)
                {
                    size += field.RosMessageLength;
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
        }

        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Value, Count);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            for (int i = 0; i < Count; i++)
            {
                Value[i].RosDeserializeInPlace(ref b);
            }
        }

        public IField Generate()
        {
            return new DynamicMessageArrayFieldFixed(Count, generator);
        }
    }
}