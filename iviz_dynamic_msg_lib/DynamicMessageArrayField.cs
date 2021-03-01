using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
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

        public int RosMessageLength
        {
            get
            {
                int size = 4;
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

            foreach (DynamicMessage field in Value)
            {
                field.RosValidate();
            }
        }

        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Value);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            uint count = b.Deserialize<uint>();
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