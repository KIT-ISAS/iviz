/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Float32")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float32 : IMessage, System.IEquatable<Float32>, IDeserializable<Float32>
    {
        [DataMember (Name = "data")] public float Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Float32(float Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float32(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Float32(ref b);
        }
        
        readonly Float32 IDeserializable<Float32>.RosDeserialize(ref Buffer b)
        {
            return new Float32(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Float32 s && Equals(s);
        
        public readonly bool Equals(Float32 o) => (Data) == (o.Data);
        
        public static bool operator==(in Float32 a, in Float32 b) => a.Equals(b);
        
        public static bool operator!=(in Float32 a, in Float32 b) => !a.Equals(b);
    
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
        [Preserve] public const string RosMessageType = "std_msgs/Float32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "73fcbf46b49191e672908e50842a83d4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSSEksSeQCAK0qjc8NAAAA";
                
    }
}
