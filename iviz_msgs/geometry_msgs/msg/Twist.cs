/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Twist : IMessage, System.IEquatable<Twist>, IDeserializable<Twist>
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear;
        [DataMember (Name = "angular")] public Vector3 Angular;
    
        /// Explicit constructor.
        public Twist(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Twist(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b) => new Twist(ref b);
        
        readonly Twist IDeserializable<Twist>.RosDeserialize(ref Buffer b) => new Twist(ref b);
        
        public override readonly int GetHashCode() => (Linear, Angular).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Twist s && Equals(s);
        
        public readonly bool Equals(Twist o) => (Linear, Angular) == (o.Linear, o.Angular);
        
        public static bool operator==(in Twist a, in Twist b) => a.Equals(b);
        
        public static bool operator!=(in Twist a, in Twist b) => !a.Equals(b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Twist";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1RwUrEMBC95ysGvCiUCCoeBM+yB0FQvMpsO82GTTNlMutu/Xon21LZu4XCJH3vzXuv" +
                "V/CxiwXoNAqVQgW+KXEbdYKYoRciKCO2BFvhPWW7VIaoBVLMhAKYO3vDIdk8omjx7pNaZbmHBfJ3XnDO" +
                "Pf/z417fX54gEA+kMn0NJZTbZau7mvMJ1XyUzTlaxPrtMqAHg24UDMs5TTAQZgULuzKN2EUxauTsTZWE" +
                "ehZqrA7o2JrLrKYx4N4kKReqbBxHE0NQwVwSVm69Nso1+eAbOO6s1TMq5mBAUwiUSWILEkPsZqYtGlYy" +
                "whKuAe3v4BhTmj3Py3RHJiKsZ8KNh00PEx/gWAPZINChmiOGrVlcfOE2Vb/cwKEaP0tcFvrG9u+tllIw" +
                "kHVXlLDzzvWJUR8f4LRO0zr9uF+x1E1SXwIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Twist Zero = (Vector3.Zero, Vector3.Zero);
        public static implicit operator Twist(in (Vector3 linear, Vector3 angular) p) => new Twist(p.linear, p.angular);
    }
}
