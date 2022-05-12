using System;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
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
            b.DeserializeString(out string val);
            Value = val;
        }

        public IField Generate()
        {
            return new StringField();
        }
    }
}