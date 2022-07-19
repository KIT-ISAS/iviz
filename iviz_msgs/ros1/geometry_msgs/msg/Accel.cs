/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Accel : IDeserializableRos1<Accel>, IMessageRos1
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear;
        [DataMember (Name = "angular")] public Vector3 Angular;
    
        /// Constructor for empty message.
        public Accel()
        {
        }
        
        /// Explicit constructor.
        public Accel(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// Constructor with buffer.
        public Accel(ref ReadBuffer b)
        {
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Accel(ref b);
        
        public Accel RosDeserialize(ref ReadBuffer b) => new Accel(ref b);
    
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
        public const string MessageType = "geometry_msgs/Accel";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0pkddjITenrdja+3kyyRvRsY6GSqqqsq" +
                "V/jYhQI+DcKlcAE1DUcW0pATQkInzCgDNYyt5D1PHzUjaEEMiUlAqbXjD9HmgURL7T650Sz3OEP+3s84" +
                "557/+XGv7y9P8Jx7Vhm/+uLL7Xmru1oyCk8ZOZlzwvd8dxmwhkE3CsPmFEf0TElhYVemEdsgRrVyalNl" +
                "4S4LV1YH2mztpaym0dPeJDkVntg0DCZGUKFU4lLs3CCuufZ1hePOWp1RIXkDmoLnxBIaSPChXZi2qF/J" +
                "hHO4Ctrd4RhiXDwvy3THJiJZZ8JNjU2HMR9wnALZIGhJaRLa8uqLtnHymyscJuOzxGWhb9n+vdVSCnm2" +
                "7ooytbVzXcykjw84rdO4Tj/uF/F1eHFjAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
