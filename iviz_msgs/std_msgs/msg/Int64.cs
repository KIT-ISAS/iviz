/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int64")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Int64 : IMessage, System.IEquatable<Int64>, IDeserializable<Int64>
    {
        [DataMember (Name = "data")] public long Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Int64(long Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int64(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int64(ref b);
        }
        
        readonly Int64 IDeserializable<Int64>.RosDeserialize(ref Buffer b)
        {
            return new Int64(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Int64 s && Equals(s);
        
        public readonly bool Equals(Int64 o) => (Data) == (o.Data);
        
        public static bool operator==(in Int64 a, in Int64 b) => a.Equals(b);
        
        public static bool operator!=(in Int64 a, in Int64 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 8;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "34add168574510e6e17f5d23ecc077ef";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUUhJLEnkAgBZU74aCwAAAA==";
                
    }
}
