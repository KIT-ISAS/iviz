/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int32")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Int32 : IMessage, System.IEquatable<Int32>
    {
        [DataMember (Name = "data")] public int Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Int32(int Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int32(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int32(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Int32 s && Equals(s);
        
        public readonly bool Equals(Int32 o) => (Data) == (o.Data);
        
        public static bool operator==(in Int32 a, in Int32 b) => a.Equals(b);
        
        public static bool operator!=(in Int32 a, in Int32 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 4;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UkhJLEnkAgAHaI4xCwAAAA==";
                
    }
}
