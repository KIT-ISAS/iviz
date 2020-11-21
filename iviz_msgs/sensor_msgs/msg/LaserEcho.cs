/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/LaserEcho")]
    public sealed class LaserEcho : IDeserializable<LaserEcho>, IMessage
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        [DataMember (Name = "echoes")] public float[] Echoes { get; set; } // Multiple values of ranges or intensities.
        // Each array represents data from the same angle increment.
    
        /// <summary> Constructor for empty message. </summary>
        public LaserEcho()
        {
            Echoes = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LaserEcho(float[] Echoes)
        {
            this.Echoes = Echoes;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LaserEcho(ref Buffer b)
        {
            Echoes = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LaserEcho(ref b);
        }
        
        LaserEcho IDeserializable<LaserEcho>.RosDeserialize(ref Buffer b)
        {
            return new LaserEcho(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Echoes, 0);
        }
        
        public void RosValidate()
        {
            if (Echoes is null) throw new System.NullReferenceException(nameof(Echoes));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Echoes.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/LaserEcho";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8bc5ae449b200fba4d552b4225586696";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACmWOsW7DQAxDdwP+BwLeO7Tf4C2Z0q3ooPho+4DznSHJAfL3lRF0CidSIB414HvNho1m" +
                "shBhBXbc/3ObcT2K53Fa20WMepukQmo6m7U5cnXWxNR3A7zhThzGBOMuKs7y/Oi7vptLE//6/PkFA0QD" +
                "hhd3L8RDyhGnmFKpy+n0hbXsmRYAvGnAKNMKUZUnlLvSWN2QxAWztg2+EiYb49klRnKdlFt0AvcHDSGe" +
                "DvQAAAA=";
                
    }
}
