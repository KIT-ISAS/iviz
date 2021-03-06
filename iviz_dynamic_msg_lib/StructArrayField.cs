using System;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public sealed class StructArrayField<T> : IField where T : unmanaged
    {
        public T[] Value { get; set; } = Array.Empty<T>();

        object IField.Value => Value;
        
        public FieldType Type => FieldType.StructArray;

        public int RosLength => 4 + Value.Length * Marshal.SizeOf<T>();

        public void RosValidate()
        {
            if (Value == null)
            {
                throw new NullReferenceException(nameof(Value));
            }
        }

        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Value);
        }

        public void RosDeserializeInPlace(ref Buffer b)
        {
            Value = b.DeserializeStructArray<T>();
        }

        public IField Generate()
        {
            return new StructField<T>();
        }
    }
}