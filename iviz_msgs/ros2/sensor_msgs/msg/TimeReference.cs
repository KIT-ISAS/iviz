/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class TimeReference : IDeserializableRos2<TimeReference>, IMessageRos2
    {
        // Measurement from an external time source not actively synchronized with the system clock.
        /// <summary> Stamp is system time for which measurement was valid </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is not used
        /// <summary> Corresponding time from this external source </summary>
        [DataMember (Name = "time_ref")] public time TimeRef;
        /// <summary> (optional) name of time source </summary>
        [DataMember (Name = "source")] public string Source;
    
        /// Constructor for empty message.
        public TimeReference()
        {
            Source = "";
        }
        
        /// Explicit constructor.
        public TimeReference(in StdMsgs.Header Header, time TimeRef, string Source)
        {
            this.Header = Header;
            this.TimeRef = TimeRef;
            this.Source = Source;
        }
        
        /// Constructor with buffer.
        public TimeReference(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out TimeRef);
            b.DeserializeString(out Source);
        }
        
        public TimeReference RosDeserialize(ref ReadBuffer2 b) => new TimeReference(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(TimeRef);
            b.Serialize(Source);
        }
        
        public void RosValidate()
        {
            if (Source is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, TimeRef);
            WriteBuffer2.AddLength(ref c, Source);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/TimeReference";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
