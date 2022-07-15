/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class JoyFeedbackArray : IDeserializable<JoyFeedbackArray>, IMessageRos2
    {
        // This message publishes values for multiple feedback at once.
        [DataMember (Name = "array")] public JoyFeedback[] Array;
    
        /// Constructor for empty message.
        public JoyFeedbackArray()
        {
            Array = System.Array.Empty<JoyFeedback>();
        }
        
        /// Explicit constructor.
        public JoyFeedbackArray(JoyFeedback[] Array)
        {
            this.Array = Array;
        }
        
        /// Constructor with buffer.
        public JoyFeedbackArray(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Array);
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = new JoyFeedback(ref b);
            }
        }
        
        public JoyFeedbackArray RosDeserialize(ref ReadBuffer2 b) => new JoyFeedbackArray(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Array);
        }
        
        public void RosValidate()
        {
            if (Array is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] is null) BuiltIns.ThrowNullReference(nameof(Array), i);
                Array[i].RosValidate();
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Array);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/JoyFeedbackArray";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
