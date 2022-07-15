/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class NavSatStatus : IDeserializable<NavSatStatus>, IMessageRos2
    {
        // Navigation Satellite fix status for any Global Navigation Satellite System.
        //
        // Whether to output an augmented fix is determined by both the fix
        // type and the last time differential corrections were received.  A
        // fix is valid when status >= STATUS_FIX.
        /// <summary> Unable to fix position </summary>
        public const sbyte STATUS_NO_FIX = -1;
        /// <summary> Unaugmented fix </summary>
        public const sbyte STATUS_FIX = 0;
        /// <summary> With satellite-based augmentation </summary>
        public const sbyte STATUS_SBAS_FIX = 1;
        /// <summary> With ground-based augmentation </summary>
        public const sbyte STATUS_GBAS_FIX = 2;
        [DataMember (Name = "status")] public sbyte Status;
        // Bits defining which Global Navigation Satellite System signals were
        // used by the receiver.
        public const ushort SERVICE_GPS = 1;
        public const ushort SERVICE_GLONASS = 2;
        /// <summary> Includes BeiDou. </summary>
        public const ushort SERVICE_COMPASS = 4;
        public const ushort SERVICE_GALILEO = 8;
        [DataMember (Name = "service")] public ushort Service;
    
        /// Constructor for empty message.
        public NavSatStatus()
        {
        }
        
        /// Explicit constructor.
        public NavSatStatus(sbyte Status, ushort Service)
        {
            this.Status = Status;
            this.Service = Service;
        }
        
        /// Constructor with buffer.
        public NavSatStatus(ref ReadBuffer2 b)
        {
            b.Deserialize(out Status);
            b.Deserialize(out Service);
        }
        
        public NavSatStatus RosDeserialize(ref ReadBuffer2 b) => new NavSatStatus(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Status);
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 3;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Status);
            WriteBuffer2.Advance(ref c, Service);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/NavSatStatus";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
