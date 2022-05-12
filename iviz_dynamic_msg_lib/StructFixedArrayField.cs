using System.Runtime.InteropServices;
using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic
{
    public sealed class StructFixedArrayField<T> : IField where T : unmanaged
    {
        public int Count { get; }

        public T[] Value { get; set; }

        object IField.Value => Value;
        
        public FieldType Type => FieldType.StructFixedArray;

        public int RosLength => Count * Marshal.SizeOf<T>();

        public StructFixedArrayField(int count)
        {
            Count = count;
            Value = new T[Count];
        }

        public void RosValidate()
        {
            if (Value == null)
            {
                BuiltIns.ThrowNullReference(nameof(Value));
            }

            if (Value.Length != Count)
            {
                BuiltIns.ThrowInvalidSizeForFixedArray(Value.Length, Count);
            }
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Value, Count);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            b.DeserializeStructArray(Count, out T[] val);
            Value = val;
        }

        public IField Generate()
        {
            return new StructFixedArrayField<T>(Count);
        }
    }
}