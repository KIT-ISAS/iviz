/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Triangle")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle : IMessage, System.IEquatable<Triangle>
    {
        [DataMember (Name = "a")] public uint A { get; set; }
        [DataMember (Name = "b")] public uint B { get; set; }
        [DataMember (Name = "c")] public uint C { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Triangle(uint A, uint B, uint C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Triangle(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(Buffer b)
        {
            return new Triangle(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
        
        public override readonly int GetHashCode() => (A, B, C).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Triangle s && Equals(s);
        
        public readonly bool Equals(Triangle o) => (A, B, C) == (o.A, o.B, o.C);
        
        public static bool operator==(in Triangle a, in Triangle b) => a.Equals(b);
        
        public static bool operator!=(in Triangle a, in Triangle b) => !a.Equals(b);
    
        public readonly void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 12;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Triangle";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7fbd9596e2fe5bfb3fb6622c0cdf3da9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlJI5CqFMJJgjGQuLgA3MPMeHAAAAA==";
                
    }
}
