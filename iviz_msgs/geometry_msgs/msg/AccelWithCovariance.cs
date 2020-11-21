/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/AccelWithCovariance")]
    public sealed class AccelWithCovariance : IDeserializable<AccelWithCovariance>, IMessage
    {
        // This expresses acceleration in free space with uncertainty.
        [DataMember (Name = "accel")] public Accel Accel { get; set; }
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelWithCovariance(in Accel Accel, double[] Covariance)
        {
            this.Accel = Accel;
            this.Covariance = Covariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AccelWithCovariance(ref Buffer b)
        {
            Accel = new Accel(ref b);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovariance(ref b);
        }
        
        AccelWithCovariance IDeserializable<AccelWithCovariance>.RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovariance(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Accel.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new System.IndexOutOfRangeException();
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ad5a718d699c6be72a02b8d6a139f334";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TTWvbQBC9G/QfHviSgKJCU3wI9NBTyaFQ2lD6QSljaSRvI+2I2XUs5dd3VlIcm/bQ" +
                "Q6nBsBrNezPv6e0adzsXwEOvHAIHUFlyy0rRiYfzqJUZoaeScXBxh70vWSM5H8ciW2WrN6l/RqXHNT7I" +
                "4aqjn6JQTqTs40wmNeKOsRk2KOWB1JFRoaOobkjAO3sp6o79PSl1HFkD9oFBqN3A1RUNtu85tS2yxq1N" +
                "0Io1n6acgEn5JjVcDDnGHI85VJYRtJV9xGckzt/KX/5c/jqVL7NV3QrFzatv15vvJ4KSCa//8S9bvfv4" +
                "9gYNi0nS8UcXmvBiMn727W8/4FblnlMxClwMaJ1nUpCv7N/sWzubbzGYn5+4jKLXWHpOCkvnf9O5DD4q" +
                "PX56k4qH6eW5zAJTHCKsWXw7omPyEab5CDVk5dSwKTwpeMq1KOfmCioxE73ERNLRvZGyt/gZnPre2AhR" +
                "yYd2NnhyEhdcNEWOw87cnbqcb6wxUTTsWV0JdY2rZqiNstQvaMIi0HJbv7Q71rbz1vM0y3JiecrgZYHb" +
                "GqPscUia7KCoKNpOgq0tuWxG2zZtLPl0cxaOc1vfi8XArAmBGjYDQ2Sqphu95Bp2KZ+O4/PxMVv9AiH7" +
                "Y6o0BAAA";
                
    }
}
