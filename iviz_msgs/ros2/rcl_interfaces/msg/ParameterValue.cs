/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ParameterValue : IDeserializable<ParameterValue>, IMessage
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
    
        public ParameterValue()
        {
            StringValue = "";
            ByteArrayValue = EmptyArray<byte>.Value;
            BoolArrayValue = EmptyArray<bool>.Value;
            IntegerArrayValue = EmptyArray<long>.Value;
            DoubleArrayValue = EmptyArray<double>.Value;
            StringArrayValue = EmptyArray<string>.Value;
        }
        
        public ParameterValue(ref ReadBuffer b)
        {
            b.Deserialize(out Type);
            b.Deserialize(out BoolValue);
            b.Deserialize(out IntegerValue);
            b.Deserialize(out DoubleValue);
            b.DeserializeString(out StringValue);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<byte>.Value
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                ByteArrayValue = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<bool>.Value
                    : new bool[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                BoolArrayValue = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<long>.Value
                    : new long[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 8);
                }
                IntegerArrayValue = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<double>.Value
                    : new double[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 8);
                }
                DoubleArrayValue = array;
            }
            b.DeserializeStringArray(out StringArrayValue);
        }
        
        public ParameterValue(ref ReadBuffer2 b)
        {
            b.Deserialize(out Type);
            b.Deserialize(out BoolValue);
            b.Align8();
            b.Deserialize(out IntegerValue);
            b.Deserialize(out DoubleValue);
            b.DeserializeString(out StringValue);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<byte>.Value
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                ByteArrayValue = array;
            }
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<bool>.Value
                    : new bool[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                BoolArrayValue = array;
            }
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<long>.Value
                    : new long[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 8);
                }
                IntegerArrayValue = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<double>.Value
                    : new double[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 8);
                }
                DoubleArrayValue = array;
            }
            b.DeserializeStringArray(out StringArrayValue);
        }
        
        public ParameterValue RosDeserialize(ref ReadBuffer b) => new ParameterValue(ref b);
        
        public ParameterValue RosDeserialize(ref ReadBuffer2 b) => new ParameterValue(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public int RosMessageLength
        {
            get {
                int size = 42;
                size += WriteBuffer.GetStringSize(StringValue);
                size += ByteArrayValue.Length;
                size += BoolArrayValue.Length;
                size += 8 * IntegerArrayValue.Length;
                size += 8 * DoubleArrayValue.Length;
                size += WriteBuffer.GetArraySize(StringArrayValue);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Type
            c += 1; // BoolValue
            c = WriteBuffer2.Align8(c);
            c += 8; // IntegerValue
            c += 8; // DoubleValue
            c = WriteBuffer2.AddLength(c, StringValue);
            c = WriteBuffer2.Align4(c);
            c += 4; // ByteArrayValue length
            c += 1 * ByteArrayValue.Length;
            c = WriteBuffer2.Align4(c);
            c += 4; // BoolArrayValue length
            c += 1 * BoolArrayValue.Length;
            c = WriteBuffer2.Align4(c);
            c += 4; // IntegerArrayValue length
            c = WriteBuffer2.Align8(c);
            c += 8 * IntegerArrayValue.Length;
            c += 4; // DoubleArrayValue length
            c = WriteBuffer2.Align8(c);
            c += 8 * DoubleArrayValue.Length;
            c = WriteBuffer2.AddLength(c, StringArrayValue);
            return c;
        }
    
        public const string MessageType = "rcl_interfaces/ParameterValue";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "587c645177f28280b87cb5a8349befed";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE4WTy27bMBBF9/qKQbJpC1kI/O7SBbTIok2QutkUhUNJI5kARQrkKK779R2Skh9Ak278" +
                "oOaeO3M5uoUfDisgAxUS2lZqhMNelnswNdAeQeNvgk+7V6F6hFqiqhwIi+CQsuQWHoUVrVdujx1mj5un" +
                "zdd8mz/tvj1sd9/zLUhdyVIQOoYJCsRulMBBONCGPItRH2QNjSFC/RGMBemg11JLkkLJP1h5t2ffRfRH" +
                "3bdomVyxB7xc99G65iVLWLBlP+KTOA0jT+bpMGZprEXXGc1zcQq+QdF11nRWMjxODAUqc8iSXmpaB55n" +
                "3zwLrtF0A46OihMhY0WDY3DnMUN2GTxodQxPYpZnY6kbxtHYarQ8SKVgL15DufRD1sa2gqTRYbIvxigU" +
                "OsJSKPlnwalIxlggywacYS2UwywpuBb8R7xGL7/XhM3YG1ihG+4CamtamHxOp9NZOltN07vZMl0v5ulq" +
                "tUjXdzy6Yelbj1dZwvks59xqYJ/NNlCZvuCIOouldDwC1MrwLGzZGS4f2qiN4pz96X2e57BazLMkFDI0" +
                "Ei6ZxJvZCzVoDzw5LxNbiJJ45RQo1A2fKdlKXlVH1oPj1wVG8zZZcfS3Vhx5T1Po/QvBYTNNT0aTq/h9" +
                "4c9foX4X1G/wLq/IxWvwMn8R78iW80khaUzxpA7ZsnxM9/+Ef2XsTokyasj0HdKQ2iiNf1k5xHil/Asg" +
                "1nPLSwQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
