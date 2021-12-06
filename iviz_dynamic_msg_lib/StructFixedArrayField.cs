using System;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
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
                throw new NullReferenceException(nameof(Value));
            }

            if (Value.Length != Count)
            {
                throw new RosInvalidSizeForFixedArrayException();
            }
        }

        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Value, Count);
        }

        public void RosDeserializeInPlace(ref ReadBuffer b)
        {
            Value = b.DeserializeStructArray<T>(Count);
        }

        public IField Generate()
        {
            return new StructFixedArrayField<T>(Count);
        }
    }
}