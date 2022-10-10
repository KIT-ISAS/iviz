/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Twist : IHasSerializer<Twist>, IMessage
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear;
        [DataMember (Name = "angular")] public Vector3 Angular;
    
        public Twist()
        {
        }
        
        public Twist(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        public Twist(ref ReadBuffer b)
        {
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        public Twist(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        public Twist RosDeserialize(ref ReadBuffer b) => new Twist(ref b);
        
        public Twist RosDeserialize(ref ReadBuffer2 b) => new Twist(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Linear);
            b.Serialize(in Angular);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(in Linear);
            b.Serialize(in Angular);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 48;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Twist";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    
        public Serializer<Twist> CreateSerializer() => new Serializer();
        public Deserializer<Twist> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Twist>
        {
            public override void RosSerialize(Twist msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Twist msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Twist _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Twist _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Twist>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Twist msg) => msg = new Twist(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Twist msg) => msg = new Twist(ref b);
        }
    }
}
