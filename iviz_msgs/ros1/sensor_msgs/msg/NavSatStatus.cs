/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class NavSatStatus : IHasSerializer<NavSatStatus>, IMessage
    {
        // Navigation Satellite fix status for any Global Navigation Satellite System
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
    
        public NavSatStatus()
        {
        }
        
        public NavSatStatus(sbyte Status, ushort Service)
        {
            this.Status = Status;
            this.Service = Service;
        }
        
        public NavSatStatus(ref ReadBuffer b)
        {
            b.Deserialize(out Status);
            b.Deserialize(out Service);
        }
        
        public NavSatStatus(ref ReadBuffer2 b)
        {
            b.Deserialize(out Status);
            b.Align2();
            b.Deserialize(out Service);
        }
        
        public NavSatStatus RosDeserialize(ref ReadBuffer b) => new NavSatStatus(ref b);
        
        public NavSatStatus RosDeserialize(ref ReadBuffer2 b) => new NavSatStatus(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Status);
            b.Serialize(Service);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Status);
            b.Align2();
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 3;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c + Ros2FixedMessageLength;
        
    
        public const string MessageType = "sensor_msgs/NavSatStatus";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "331cdbddfa4bc96ffc3b9ad98900a54c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE42RT0/DMAzF7/0UlnZmYgghLiB1MKZJY0OUfzeUtm5rKUumxNnYt8ehLWyMA7nV7vvZ" +
                "fm8AC7WhWjFZA5li1JoYoaIP8Kw4eKisA2V2MNU2V/rv37OdZ1wlyQBeG+QGHbAFG3gdWLSgQr1Cw1h+" +
                "cclDiYxuRUYq+Q5yyw2IKnYFwbs1iqr8KmnlGZhWCCVVFTrBkGxRWOewiFt42EoV5Atpg+UQIBVGN2ej" +
                "NJWwbdD011xfQfaUPj1n73ezt2GSkOHLvrJYxiJcAZyMoHsDCEblGuNBEbq2nuLYA2Gniu/0QLh/94Ei" +
                "G6e9bG/UlsQI37t6kisv0g6ijqZOfxhnvxi1s8GUfwFaQmtGzGtMHOOoyJCpxSkqmn8kDZ5qo3TrvVCC" +
                "b5OMiXVJODE3yLDRBWSTx5fZzeR9+pB1No2OWvPlIs1i++x362Z5/9C2zvsbyRQ6lOhhjHRrw/CIls5n" +
                "88lSJJffS3h0GyowST4B7Ndp4/ICAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<NavSatStatus> CreateSerializer() => new Serializer();
        public Deserializer<NavSatStatus> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<NavSatStatus>
        {
            public override void RosSerialize(NavSatStatus msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(NavSatStatus msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(NavSatStatus _) => RosFixedMessageLength;
            public override int Ros2MessageLength(NavSatStatus _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<NavSatStatus>
        {
            public override void RosDeserialize(ref ReadBuffer b, out NavSatStatus msg) => msg = new NavSatStatus(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out NavSatStatus msg) => msg = new NavSatStatus(ref b);
        }
    }
}
