using System.Runtime.InteropServices;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class StructField<T> : IField where T : unmanaged
    {
        public T Value { get; set; }
        
        public FieldType Type => FieldType.Struct;

        object IField.Value => Value;

        public int RosLength => Marshal.SizeOf<T>();

        public void RosValidate()
        {
        }

        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Value);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            Value = b.Deserialize<T>();
        }

        public IField Generate()
        {
            return new StructField<T>();
        }
    }
}