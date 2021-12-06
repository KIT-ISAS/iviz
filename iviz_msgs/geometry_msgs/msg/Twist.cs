/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Twist : IMessage, IDeserializable<Twist>
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
        internal Twist(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Twist(ref b);
        
        public readonly Twist RosDeserialize(ref ReadBuffer b) => new Twist(ref b);
        
        public readonly bool Equals(in Twist o) => (Linear, Angular) == (o.Linear, o.Angular);
        
        public static bool operator==(in Twist a, in Twist b) => a.Equals(b);
        
        public static bool operator!=(in Twist a, in Twist b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
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
                "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0skOO5kJPb3uxtfbkyyRvRsY6GSqqqsq" +
                "V/jY+Qw+jcI5c8Y3h9R4neAjOmFGHqlhbCXtOdpHTfCaEXxkElBs7fSHYPNIorl2n9xoknucIX/vZ5xz" +
                "z//8uNf3lyf0nAZWmb6G3Ofb81Z3teQTLvk4mnOyiOXuMmANg24Uhk0xTBiYosLCrkwjtl6M6lOsTZWF" +
                "uyRcWR1okzUXk5rGQHuT5Ji5sGkcTYygQjEHKlzMDeKa676ucNxZqzPKx96AptBzZPENxPe+XZi2aFjJ" +
                "hHO4Ctrd4ehDWDwvy3THJiJJZ8JNjU2HKR1wLIFsELSkVIS2vPqibSh+U4VDMT5LXBb6luzfWy05U8/W" +
                "XVamtnauC4n08QGndZrW6cf9ArHUTVJfAgAA";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static readonly Twist Zero = (Vector3.Zero, Vector3.Zero);
        public static implicit operator Twist(in (Vector3 linear, Vector3 angular) p) => new Twist(p.linear, p.angular);
    }
}
