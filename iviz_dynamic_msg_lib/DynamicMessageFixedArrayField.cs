using Iviz.Msgs;

namespace Iviz.MsgsGen.Dynamic;

internal sealed class DynamicMessageFixedArrayField : IField
{
    readonly DynamicMessage generator;
    public readonly int Count;
    
    public DynamicMessage[] Value { get; set; }

    object IField.Value => Value;

    public FieldType Type => FieldType.DynamicMessageFixedArray;

    public DynamicMessageFixedArrayField(int count, DynamicMessage generator)
    {
        Count = count;
        this.generator = generator;

        Value = new DynamicMessage[count];
        for (int i = 0; i < count; i++)
        {
            Value[i] = new DynamicMessage(generator);
        }
    }

    public int RosLength
    {
        get
        {
            int size = 0;
            foreach (DynamicMessage field in Value)
            {
                size += field.RosLength;
            }

            return size;
        }
    }

    public void RosValidate()
    {
        if (Value == null) BuiltIns.ThrowNullReference(nameof(Value));
        if (Value.Length != Count) BuiltIns.ThrowInvalidSizeForFixedArray(Value.Length, Count);
    }

    public void RosSerialize(ref WriteBuffer b)
    {
        for (int i = 0; i < Count; i++)
        {
            Value[i].RosSerialize(ref b);
        }        
    }

    public void RosDeserializeInPlace(ref ReadBuffer b)
    {
        foreach (DynamicMessage t in Value)
        {
            t.RosDeserializeInPlace(ref b);
        }
    }

    public IField Generate()
    {
        return new DynamicMessageFixedArrayField(Count, generator);
    }
}