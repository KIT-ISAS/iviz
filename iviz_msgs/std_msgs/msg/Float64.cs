/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Float64")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Float64 : IMessage, System.IEquatable<Float64>, IDeserializable<Float64>
    {
        [DataMember (Name = "data")] public double Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Float64(double Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float64(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Float64(ref b);
        }
        
        readonly Float64 IDeserializable<Float64>.RosDeserialize(ref Buffer b)
        {
            return new Float64(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Float64 s && Equals(s);
        
        public readonly bool Equals(Float64 o) => (Data) == (o.Data);
        
        public static bool operator==(in Float64 a, in Float64 b) => a.Equals(b);
        
        public static bool operator!=(in Float64 a, in Float64 b) => !a.Equals(b);
    
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
        [Preserve] public const string RosMessageType = "std_msgs/Float64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fdb28210bfa9d7c91146260178d9a584";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
    }
}
