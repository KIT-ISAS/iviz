/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Accel : IDeserializable<Accel>, IMessage
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
        internal Accel(ref Buffer b)
        {
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Accel(ref b);
        
        Accel IDeserializable<Accel>.RosDeserialize(ref Buffer b) => new Accel(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref Linear);
            b.Serialize(ref Angular);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Accel";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1Ry0rEQBC8z1cUeFEIEVQ8CJ5lD4KgeJXepDM77GQm9PS6G7/ezoOVvRsY6GSqqqsq" +
                "V/jYhQI+DcKlcAE1DUcW0pATQkInzCgDNYyt5D1PHzUjaEEMiUlAqbXjD9HmgURL7T650Sz3WCF/7yvO" +
                "ued/ftzr+8sTPOeeVcavvvhyu251V0tG4SkjJ3NO+J7vLgPWMOhGYdic4oieKSks7JlpxDaIUa2c2lRZ" +
                "uMvCldWBNlt7Katp9LQ3SU6FJzYNg4kRVCiVuBQ7N4hrrn1d4bizVmdUSN6ApuA5sYQGEnxoF6Yt6s9k" +
                "whqugnZ3OIYYF8/LMt2xiUjWmXBTY9NhzAccp0A2CFpSc5SxNYurL9rGyW+ucJiMzxKXhb5l+/dWSynk" +
                "2borytTWznUxkz4+4HSexvP0434B8XV4cWMCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
