/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class LaserEcho : IDeserializableRos2<LaserEcho>, IMessageRos2
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        /// <summary> Multiple values of ranges or intensities. </summary>
        [DataMember (Name = "echoes")] public float[] Echoes;
        // Each array represents data from the same angle increment.
    
        /// Constructor for empty message.
        public LaserEcho()
        {
            Echoes = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public LaserEcho(float[] Echoes)
        {
            this.Echoes = Echoes;
        }
        
        /// Constructor with buffer.
        public LaserEcho(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Echoes);
        }
        
        public LaserEcho RosDeserialize(ref ReadBuffer2 b) => new LaserEcho(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Echoes);
        }
        
        public void RosValidate()
        {
            if (Echoes is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Echoes);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/LaserEcho";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
