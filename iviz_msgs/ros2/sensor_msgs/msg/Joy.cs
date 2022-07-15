/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class Joy : IDeserializable<Joy>, IMessageRos2
    {
        // Reports the state of a joystick's axes and buttons.
        // The timestamp is the time at which data is received from the joystick.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The axes measurements from a joystick.
        [DataMember (Name = "axes")] public float[] Axes;
        // The buttons measurements from a joystick.
        [DataMember (Name = "buttons")] public int[] Buttons;
    
        /// Constructor for empty message.
        public Joy()
        {
            Axes = System.Array.Empty<float>();
            Buttons = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public Joy(in StdMsgs.Header Header, float[] Axes, int[] Buttons)
        {
            this.Header = Header;
            this.Axes = Axes;
            this.Buttons = Buttons;
        }
        
        /// Constructor with buffer.
        public Joy(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Axes);
            b.DeserializeStructArray(out Buttons);
        }
        
        public Joy RosDeserialize(ref ReadBuffer2 b) => new Joy(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Axes);
            b.SerializeStructArray(Buttons);
        }
        
        public void RosValidate()
        {
            if (Axes is null) BuiltIns.ThrowNullReference();
            if (Buttons is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, Axes);
            WriteBuffer2.Advance(ref c, Buttons);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Joy";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
