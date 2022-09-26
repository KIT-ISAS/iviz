using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic;

public sealed class StringFixedArrayField : IField<string[]>
{
    public int Count { get; }

    public string[] Value { get; set; }

    object IField.Value => Value;
        
    public FieldType Type => FieldType.StringFixedArray;

    public StringFixedArrayField(int count)
    {
        Count = count;
        Value = new string[count];
        for (int i = 0; i < Value.Length; i++)
        {
            Value[i] = "";
        }
    }

    public int RosLength
    {
        get
        {
            int size = 4 * Count;
            for (int i = 0; i < Count; i++)
            {
                size += BuiltIns.UTF8.GetByteCount(Value[i]);
            }

            return size;
        }
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

        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i] is null)
            {
                BuiltIns.ThrowNullReference(nameof(Value), i);
            }
        }
    }

    public void RosSerialize(ref WriteBuffer b)
    {
        b.SerializeArray(Value, Count);
    }

    public void RosDeserializeInPlace(ref ReadBuffer b)
    {
        Value = b.DeserializeStringArray(Count);;
    }

    public IField Generate()
    {
        return new StringFixedArrayField(Count);
    }
}