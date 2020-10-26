/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Twist")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Twist : IMessage, System.IEquatable<Twist>, IDeserializable<Twist>
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear { get; set; }
        [DataMember (Name = "angular")] public Vector3 Angular { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Twist(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Twist(ref Buffer b)
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
        
        public override readonly bool Equals(object o) => o is Twist s && Equals(s);
        
        public readonly bool Equals(Twist o) => (Linear, Angular) == (o.Linear, o.Angular);
        
        public static bool operator==(in Twist a, in Twist b) => a.Equals(b);
        
        public static bool operator!=(in Twist a, in Twist b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 48;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Twist";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0skOO5kJPb3uxtfbkyyRvRsY6GSqqqsq" +
                "V/jY+Qw+jcI5c8Y3h9R4neAjOmFGHqlhbCXtOdpHTfCaEXxkElBs7fSHYPNIorl2n9xoknucIX/vZ5xz" +
                "z//8uNf3lyf0nAZWmb6G3Ofb81Z3teQTLvk4mnOyiOXuMmANg24Uhk0xTBiYosLCrkwjtl6M6lOsTZWF" +
                "uyRcWR1okzUXk5rGQHuT5Ji5sGkcTYygQjEHKlzMDeKa676ucNxZqzPKx96AptBzZPENxPe+XZi2aFjJ" +
                "hHO4Ctrd4ehDWDwvy3THJiJJZ8JNjU2HKR1wLIFsELSkVIS2vPqibSh+U4VDMT5LXBb6luzfWy05U8/W" +
                "XVamtnauC4n08QGndZrW6cf9ArHUTVJfAgAA";
                
    }
}
