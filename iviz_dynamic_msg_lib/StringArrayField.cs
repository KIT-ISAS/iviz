using System;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class StringArrayField : IField<string[]>
    {
        public string[] Value { get; set; } = Array.Empty<string>();

        object IField.Value => Value;
        
        public FieldType Type => FieldType.StringArray;

        public int RosLength
        {
            get
            {
                int size = 4 + 4 * Value.Length;
                foreach (string t in Value)
                {
                    size += BuiltIns.UTF8.GetByteCount(t);
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
            }
        }

        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Value);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            Value = b.DeserializeStringArray();
        }

        public IField Generate()
        {
            return new StringArrayField();
        }
    }
}