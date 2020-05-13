using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [DataContract]
    public sealed class Twist : IMessage
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        [DataMember] public Vector3 linear { get; set; }
        [DataMember] public Vector3 angular { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Twist()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Twist(Vector3 linear, Vector3 angular)
        {
            this.linear = linear;
            this.angular = angular;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Twist(Buffer b)
        {
            this.linear = new Vector3(b);
            this.angular = new Vector3(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Twist(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.linear);
            b.Serialize(this.angular);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 48;
    
        string IMessage.RosType => RosMessageType;
    
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
