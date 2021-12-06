using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class StringField : IField
    {
        public string Value { get; set; } = "";

        object IField.Value => Value;

        public FieldType Type => FieldType.String;
        
        public int RosLength => 4 + BuiltIns.UTF8.GetByteCount(Value);

        public void RosValidate()
        {
            if (Value == null)
            {
                throw new NullReferenceException(nameof(Value));
            }
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Value);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            Value = b.DeserializeString();
        }

        public IField Generate()
        {
            return new StringField();
        }
    }
}