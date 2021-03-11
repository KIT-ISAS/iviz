/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Twist")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Twist : IMessage, System.IEquatable<Twist>, IDeserializable<Twist>
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear;
        [DataMember (Name = "angular")] public Vector3 Angular;
    
        /// <summary> Explicit constructor. </summary>
        public Twist(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Twist(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Twist(ref b);
        }
        
        readonly Twist IDeserializable<Twist>.RosDeserialize(ref Buffer b)
        {
            return new Twist(ref b);
        }
        
        public override readonly int GetHashCode() => (Linear, Angular).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Twist s && Equals(s);
        
        public readonly bool Equals(Twist o) => (Linear, Angular) == (o.Linear, o.Angular);
        
        public static bool operator==(in Twist a, in Twist b) => a.Equals(b);
        
        public static bool operator!=(in Twist a, in Twist b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void Dispose()
        {
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Twist";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1RwUrEMBC9B/oPA3tRKBVUPAieZQ+CoHiV2XaaDZtmymTW3fr1Tral4t1CYZK+9+a9" +
                "1w2870MGOo9COVOGL4rcBp0gJOiFCPKILcFO+EDJLpUhaIYYEqEAps5ef4w2jyiaG/dBrbLcwQL5PS84" +
                "V7mnf34q9/L2/AieeCCV6XPIPt8seyu3mSMKlYiUzDxayvLxb8YGDLpVMCynOMFAmBQs78o0YhfEqIFT" +
                "Y6ok1LNQbY1Ax1ZeYjWNAQ8mSSlTYeM4mhiCCqYcsXDLtVGuqPFNDae9FXtBheQNaAqeEkloQYIP3cy0" +
                "RcNKRljS1aD9LZxCjLPneZnuyUSE9UK4bmDbw8RHOJVANgh0qOaIYWcWF1+4i8Uv13Asxi8Sfxt9Zfv9" +
                "VkvO6Mm6y0rYNc71kVEf7uG8TtM6fVfuBy/yawBjAgAA";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Twist Zero = (Vector3.Zero, Vector3.Zero);
        public static implicit operator Twist(in (Vector3 linear, Vector3 angular) p) => new Twist(p.linear, p.angular);
    }
}
