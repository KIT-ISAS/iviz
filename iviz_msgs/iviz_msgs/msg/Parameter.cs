/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Parameter : IDeserializable<Parameter>, IMessage
    {
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "bool")] public bool @bool;
        [DataMember (Name = "int32")] public int Int32;
        [DataMember (Name = "float64")] public double Float64;
        [DataMember (Name = "string")] public string @string;
        [DataMember (Name = "bytes")] public byte[] Bytes;
        [DataMember (Name = "int32s")] public int[] Int32s;
        [DataMember (Name = "float64s")] public double[] Float64s;
        [DataMember (Name = "strings")] public string[] Strings;
    
        /// Constructor for empty message.
        public Parameter()
        {
            @string = "";
            Bytes = System.Array.Empty<byte>();
            Int32s = System.Array.Empty<int>();
            Float64s = System.Array.Empty<double>();
            Strings = System.Array.Empty<string>();
        }
        
        /// Constructor with buffer.
        public Parameter(ref ReadBuffer b)
        {
            b.Deserialize(out Type);
            b.Deserialize(out @bool);
            b.Deserialize(out Int32);
            b.Deserialize(out Float64);
            @string = b.DeserializeString();
            Bytes = b.DeserializeStructArray<byte>();
            Int32s = b.DeserializeStructArray<int>();
            Float64s = b.DeserializeStructArray<double>();
            Strings = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Parameter(ref b);
        
        public Parameter RosDeserialize(ref ReadBuffer b) => new Parameter(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
            b.Serialize(@bool);
            b.Serialize(Int32);
            b.Serialize(Float64);
            b.Serialize(@string);
            b.SerializeStructArray(Bytes);
            b.SerializeStructArray(Int32s);
            b.SerializeStructArray(Float64s);
            b.SerializeArray(Strings);
        }
        
        public void RosValidate()
        {
            if (@string is null) BuiltIns.ThrowNullReference(nameof(@string));
            if (Bytes is null) BuiltIns.ThrowNullReference(nameof(Bytes));
            if (Int32s is null) BuiltIns.ThrowNullReference(nameof(Int32s));
            if (Float64s is null) BuiltIns.ThrowNullReference(nameof(Float64s));
            if (Strings is null) BuiltIns.ThrowNullReference(nameof(Strings));
            for (int i = 0; i < Strings.Length; i++)
            {
                if (Strings[i] is null) BuiltIns.ThrowNullReference($"{nameof(Strings)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 34;
                size += BuiltIns.GetStringSize(@string);
                size += Bytes.Length;
                size += 4 * Int32s.Length;
                size += 8 * Float64s.Length;
                size += BuiltIns.GetArraySize(Strings);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Parameter";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b32e29f252ff98ac5d0cd0104be754bc";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxUCipLEjlSsrPz1EAEVxAMWMjBTDJlZaTn1hiZqIApbmKS4oy89IVIBRXKUh7" +
                "dKxCUmVJajFEH5AHpothWoECUFYxVDdQBMIo5gICAJ1TqtGDAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
