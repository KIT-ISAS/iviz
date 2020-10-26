/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int8")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Int8 : IMessage, System.IEquatable<Int8>, IDeserializable<Int8>
    {
        [DataMember (Name = "data")] public sbyte Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Int8(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int8(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int8(ref b);
        }
        
        readonly Int8 IDeserializable<Int8>.RosDeserialize(ref Buffer b)
        {
            return new Int8(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Int8 s && Equals(s);
        
        public readonly bool Equals(Int8 o) => (Data) == (o.Data);
        
        public static bool operator==(in Int8 a, in Int8 b) => a.Equals(b);
        
        public static bool operator!=(in Int8 a, in Int8 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 1;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "27ffa0c9c4b8fb8492252bcad9e5c57b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMK7FQSEksSeTiAgDmSq87CwAAAA==";
                
    }
}
