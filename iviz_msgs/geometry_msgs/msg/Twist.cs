/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Twist : IDeserializable<Twist>, IMessage
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear;
        [DataMember (Name = "angular")] public Vector3 Angular;
    
        /// Constructor for empty message.
        public Twist()
        {
        }
        
        /// Explicit constructor.
        public Twist(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// Constructor with buffer.
        public Twist(ref ReadBuffer b)
        {
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Twist(ref b);
        
        public Twist RosDeserialize(ref ReadBuffer b) => new Twist(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Linear);
            b.Serialize(in Angular);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Twist";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0skOO5kJPb3uxtfbkyyRvRsY6GSqqqsq" +
                "V/jY+Qw+jcI5c8Y3h9R4neAjOmFGHqlhbCXtOdpHTfCaEXxkElBs7fSHYPNIorl2n9xoknucIX/vZ5xz" +
                "z//8uNf3lyf0nAZWmb6G3Ofb81Z3teQTLvk4mnOyiOXuMmANg24Uhk0xTBiYosLCrkwjtl6M6lOsTZWF" +
                "uyRcWR1okzUXk5rGQHuT5Ji5sGkcTYygQjEHKlzMDeKa676ucNxZqzPKx96AptBzZPENxPe+XZi2aFjJ" +
                "hHO4Ctrd4ehDWDwvy3THJiJJZ8JNjU2HKR1wLIFsELSkVIS2vPqibSh+U4VDMT5LXBb6luzfWy05U8/W" +
                "XVamtnauC4n08QGndZrW6cf9ArHUTVJfAgAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static Twist Zero => new(Vector3.Zero, Vector3.Zero);
        public static implicit operator Twist(in (Vector3 linear, Vector3 angular) p) => new(p.linear, p.angular);
    }
}
