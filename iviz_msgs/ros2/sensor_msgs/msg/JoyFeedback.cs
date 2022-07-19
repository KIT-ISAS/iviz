/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class JoyFeedback : IDeserializableRos2<JoyFeedback>, IMessageRos2
    {
        // Declare of the type of feedback
        public const byte TYPE_LED = 0;
        public const byte TYPE_RUMBLE = 1;
        public const byte TYPE_BUZZER = 2;
        [DataMember (Name = "type")] public byte Type;
        // This will hold an id number for each type of each feedback.
        // Example, the first led would be id=0, the second would be id=1
        [DataMember (Name = "id")] public byte Id;
        // Intensity of the feedback, from 0.0 to 1.0, inclusive.  If device is
        // actually binary, driver should treat 0<=x<0.5 as off, 0.5<=x<=1 as on.
        [DataMember (Name = "intensity")] public float Intensity;
    
        /// Constructor for empty message.
        public JoyFeedback()
        {
        }
        
        /// Explicit constructor.
        public JoyFeedback(byte Type, byte Id, float Intensity)
        {
            this.Type = Type;
            this.Id = Id;
            this.Intensity = Intensity;
        }
        
        /// Constructor with buffer.
        public JoyFeedback(ref ReadBuffer2 b)
        {
            b.Deserialize(out Type);
            b.Deserialize(out Id);
            b.Deserialize(out Intensity);
        }
        
        public JoyFeedback RosDeserialize(ref ReadBuffer2 b) => new JoyFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Type);
            b.Serialize(Id);
            b.Serialize(Intensity);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 6;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Type);
            WriteBuffer2.AddLength(ref c, Id);
            WriteBuffer2.AddLength(ref c, Intensity);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/JoyFeedback";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
