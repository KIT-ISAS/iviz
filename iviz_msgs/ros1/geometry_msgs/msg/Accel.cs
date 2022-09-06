/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Accel : IDeserializable<Accel>, IHasSerializer<Accel>, IMessage
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear;
        [DataMember (Name = "angular")] public Vector3 Angular;
    
        public Accel()
        {
        }
        
        public Accel(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        public Accel(ref ReadBuffer b)
        {
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        public Accel(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        public Accel RosDeserialize(ref ReadBuffer b) => new Accel(ref b);
        
        public Accel RosDeserialize(ref ReadBuffer2 b) => new Accel(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Linear);
            b.Serialize(in Angular);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
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
        
    
        public const string MessageType = "geometry_msgs/Accel";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0pkddjITenrdja+3kyyRvRsY6GSqqqsq" +
                "V/jYhQI+DcKlcAE1DUcW0pATQkInzCgDNYyt5D1PHzUjaEEMiUlAqbXjD9HmgURL7T650Sz3OEP+3s84" +
                "557/+XGv7y9P8Jx7Vhm/+uLL7Xmru1oyCk8ZOZlzwvd8dxmwhkE3CsPmFEf0TElhYVemEdsgRrVyalNl" +
                "4S4LV1YH2mztpaym0dPeJDkVntg0DCZGUKFU4lLs3CCuufZ1hePOWp1RIXkDmoLnxBIaSPChXZi2qF/J" +
                "hHO4Ctrd4RhiXDwvy3THJiJZZ8JNjU2HMR9wnALZIGhJaRLa8uqLtnHymyscJuOzxGWhb9n+vdVSCnm2" +
                "7ooytbVzXcykjw84rdO4Tj/uF/F1eHFjAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Accel> CreateSerializer() => new Serializer();
        public Deserializer<Accel> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Accel>
        {
            public override void RosSerialize(Accel msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Accel msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Accel _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Accel _) => Ros2FixedMessageLength;
        }
        sealed class Deserializer : Deserializer<Accel>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Accel msg) => msg = new Accel(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Accel msg) => msg = new Accel(ref b);
        }
    }
}
