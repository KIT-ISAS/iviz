using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
    public sealed class StructArrayField<T> : IField where T : unmanaged
    {
        public T[] Value { get; set; } = Array.Empty<T>();

        object IField.Value => Value;

        public FieldType Type => FieldType.StructArray;

        public int RosLength => 4 + Value.Length * Unsafe.SizeOf<T>();

        public void RosValidate()
        {
            if (Value == null)
            {
                BuiltIns.ThrowNullReference(nameof(Value));
            }
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Value);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out T[] val);
            Value = val;
        }

        public IField Generate()
        {
            return new StructField<T>();
        }
    }
}