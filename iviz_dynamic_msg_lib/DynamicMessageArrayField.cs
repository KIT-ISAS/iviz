using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
    internal sealed class DynamicMessageArrayField : IField
    {
        readonly DynamicMessage generator;

        public DynamicMessage[] Value { get; set; } = Array.Empty<DynamicMessage>();

        object IField.Value => Value;

        public FieldType Type => FieldType.DynamicMessageArray;

        public DynamicMessageArrayField(DynamicMessage generator)
        {
            this.generator = generator;
        }

        public int RosLength
        {
            get
            {
                int size = 4;
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
                BuiltIns.ThrowNullReference(nameof(Value));
            }

            foreach (DynamicMessage field in Value)
            {
                field.RosValidate();
            }
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Value);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            b.Deserialize(out uint count);
            if (count == 0)
            {
                Value = Array.Empty<DynamicMessage>();
                return;
            }

            Value = new DynamicMessage[count];
            for (int i = 0; i < count; i++)
            {
                Value[i] = new DynamicMessage(generator);
                Value[i].RosDeserializeInPlace(ref b);
            }
        }

        public IField Generate()
        {
            return new DynamicMessageArrayField(generator);
        }
    }
}