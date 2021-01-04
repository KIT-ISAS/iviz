using System;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    internal sealed class StringField : IField
    {
        public string Value { get; set; } = "";

        object IField.Value => Value;

        public int RosMessageLength => 4 + BuiltIns.UTF8.GetByteCount(Value);

        public void RosValidate()
        {
            if (Value == null)
            {
                throw new NullReferenceException(nameof(Value));
            }
        }

        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Value);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            Value = b.DeserializeString();
        }

        public IField Generate()
        {
            return new StringField();
        }
    }
}