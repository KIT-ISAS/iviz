/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RclInterfaces
{
    [DataContract]
    public sealed class ParameterValue : IDeserializableRos2<ParameterValue>, IMessageRos2
    {
        // Used to determine which of the next *_value fields are set.
        // ParameterType.PARAMETER_NOT_SET indicates that the parameter was not set
        // (if gotten) or is uninitialized.
        // Values are enumerated in `ParameterType.msg`.
        // The type of this parameter, which corresponds to the appropriate field below.
        [DataMember (Name = "type")] public byte Type;
        // "Variant" style storage of the parameter value. Only the value corresponding
        // the type field will have valid information.
        // Boolean value, can be either true or false.
        [DataMember (Name = "bool_value")] public bool BoolValue;
        // Integer value ranging from -9,223,372,036,854,775,808 to
        // 9,223,372,036,854,775,807.
        [DataMember (Name = "integer_value")] public long IntegerValue;
        // A double precision floating point value following IEEE 754.
        [DataMember (Name = "double_value")] public double DoubleValue;
        // A textual value with no practical length limit.
        [DataMember (Name = "string_value")] public string StringValue;
        // An array of bytes, used for non-textual information.
        [DataMember (Name = "byte_array_value")] public byte[] ByteArrayValue;
        // An array of boolean values.
        [DataMember (Name = "bool_array_value")] public bool[] BoolArrayValue;
        // An array of 64-bit integer values.
        [DataMember (Name = "integer_array_value")] public long[] IntegerArrayValue;
        // An array of 64-bit floating point values.
        [DataMember (Name = "double_array_value")] public double[] DoubleArrayValue;
        // An array of string values.
        [DataMember (Name = "string_array_value")] public string[] StringArrayValue;
    
        /// Constructor for empty message.
        public ParameterValue()
        {
            StringValue = "";
            ByteArrayValue = System.Array.Empty<byte>();
            BoolArrayValue = System.Array.Empty<bool>();
            IntegerArrayValue = System.Array.Empty<long>();
            DoubleArrayValue = System.Array.Empty<double>();
            StringArrayValue = System.Array.Empty<string>();
        }
        
        /// Constructor with buffer.
        public ParameterValue(ref ReadBuffer2 b)
        {
            b.Deserialize(out Type);
            b.Deserialize(out BoolValue);
            b.Deserialize(out IntegerValue);
            b.Deserialize(out DoubleValue);
            b.DeserializeString(out StringValue);
            b.DeserializeStructArray(out ByteArrayValue);
            b.DeserializeStructArray(out BoolArrayValue);
            b.DeserializeStructArray(out IntegerArrayValue);
            b.DeserializeStructArray(out DoubleArrayValue);
            b.DeserializeStringArray(out StringArrayValue);
        }
        
        public ParameterValue RosDeserialize(ref ReadBuffer2 b) => new ParameterValue(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Type);
            b.Serialize(BoolValue);
            b.Serialize(IntegerValue);
            b.Serialize(DoubleValue);
            b.Serialize(StringValue);
            b.SerializeStructArray(ByteArrayValue);
            b.SerializeStructArray(BoolArrayValue);
            b.SerializeStructArray(IntegerArrayValue);
            b.SerializeStructArray(DoubleArrayValue);
            b.SerializeArray(StringArrayValue);
        }
        
        public void RosValidate()
        {
            if (StringValue is null) BuiltIns.ThrowNullReference();
            if (ByteArrayValue is null) BuiltIns.ThrowNullReference();
            if (BoolArrayValue is null) BuiltIns.ThrowNullReference();
            if (IntegerArrayValue is null) BuiltIns.ThrowNullReference();
            if (DoubleArrayValue is null) BuiltIns.ThrowNullReference();
            if (StringArrayValue is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < StringArrayValue.Length; i++)
            {
                if (StringArrayValue[i] is null) BuiltIns.ThrowNullReference(nameof(StringArrayValue), i);
            }
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Type);
            WriteBuffer2.AddLength(ref c, BoolValue);
            WriteBuffer2.AddLength(ref c, IntegerValue);
            WriteBuffer2.AddLength(ref c, DoubleValue);
            WriteBuffer2.AddLength(ref c, StringValue);
            WriteBuffer2.AddLength(ref c, ByteArrayValue);
            WriteBuffer2.AddLength(ref c, BoolArrayValue);
            WriteBuffer2.AddLength(ref c, IntegerArrayValue);
            WriteBuffer2.AddLength(ref c, DoubleArrayValue);
            WriteBuffer2.AddLength(ref c, StringArrayValue);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rcl_interfaces/ParameterValue";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
