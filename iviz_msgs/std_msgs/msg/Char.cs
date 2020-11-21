/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Char")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Char : IMessage, System.IEquatable<Char>, IDeserializable<Char>
    {
        [DataMember (Name = "data")] public sbyte Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Char(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Char(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Char(ref b);
        }
        
        readonly Char IDeserializable<Char>.RosDeserialize(ref Buffer b)
        {
            return new Char(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Char s && Equals(s);
        
        public readonly bool Equals(Char o) => (Data) == (o.Data);
        
        public static bool operator==(in Char a, in Char b) => a.Equals(b);
        
        public static bool operator!=(in Char a, in Char b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 1;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Char";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1bf77f25acecdedba0e224b162199717";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACkvOSCxSSEksSeTlAgD5LVXFCwAAAA==";
                
    }
}
