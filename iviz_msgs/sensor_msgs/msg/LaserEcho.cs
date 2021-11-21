/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/LaserEcho")]
    public sealed class LaserEcho : IDeserializable<LaserEcho>, IMessage
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        [DataMember (Name = "echoes")] public float[] Echoes; // Multiple values of ranges or intensities.
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
        internal LaserEcho(ref Buffer b)
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
    
        public int RosMessageLength => 4 + 4 * Echoes.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/LaserEcho";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8bc5ae449b200fba4d552b4225586696";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACmWOsQ7CMBBD90j8g6XuHeAb2GCCDTEcjdtGSpPq7orE3xOEmPBkn6zn63Cdk2GhmUxE" +
                "swLbHr9cR5y37Ok4zPUkRr0MUiAlfpqlOlJxlsgYOnjFg9iMEcZVVJz51Ycw5ip+2N/uYKPQgO4LXTPx" +
                "lLy1U9tRKdPH6ZdpyROtD/hTh6MMM0RVXlCuSmNxQxQXjFoX+EyYLGyPTm0jlUG5tE6/C28kkSwo8AAA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
