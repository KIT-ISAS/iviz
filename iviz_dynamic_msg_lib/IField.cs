using Iviz.Msgs;
using Newtonsoft.Json;

namespace Iviz.MsgsGen.Dynamic;

public enum FieldType
{
    DynamicMessage,
    DynamicMessageArray,
    DynamicMessageFixedArray,
    Message,
    MessageArray,
    MessageFixedArray,
    String,
    StringArray,
    StringFixedArray,
    Struct,
    StructArray,
    StructFixedArray
}
    
[JsonConverter(typeof(FieldJsonConverter))]
public interface IField
{
    FieldType Type { get; }
    object Value { get; }
    int RosLength { get; }
    void RosValidate();
    void RosSerialize(ref WriteBuffer b);
    void RosDeserializeInPlace(ref ReadBuffer b);
    IField Generate();
}

public interface IField<T> : IField
{
    new T Value { get; set; }
}