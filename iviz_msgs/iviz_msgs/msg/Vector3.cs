/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Vector3")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IMessage, System.IEquatable<Vector3>, IDeserializable<Vector3>
    {
        [DataMember (Name = "x")] public float X { get; set; }
        [DataMember (Name = "y")] public float Y { get; set; }
        [DataMember (Name = "z")] public float Z { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Vector3(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Vector3(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Vector3(ref b);
        }
        
        readonly Vector3 IDeserializable<Vector3>.RosDeserialize(ref Buffer b)
        {
            return new Vector3(ref b);
        }
        
        public override readonly int GetHashCode() => (X, Y, Z).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Vector3 s && Equals(s);
        
        public readonly bool Equals(Vector3 o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Vector3 a, in Vector3 b) => a.Equals(b);
        
        public static bool operator!=(in Vector3 a, in Vector3 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 12;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Vector3";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSqOBKg7Iq4awqLi4A6Ofahh8AAAA=";
                
    }
}
