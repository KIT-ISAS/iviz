/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class NavSatStatus : IDeserializable<NavSatStatus>, IMessage
    {
        // Navigation Satellite fix status for any Global Navigation Satellite System
        // Whether to output an augmented fix is determined by both the fix
        // type and the last time differential corrections were received.  A
        // fix is valid when status >= STATUS_FIX.
        public const sbyte STATUS_NO_FIX = -1; // unable to fix position
        public const sbyte STATUS_FIX = 0; // unaugmented fix
        public const sbyte STATUS_SBAS_FIX = 1; // with satellite-based augmentation
        public const sbyte STATUS_GBAS_FIX = 2; // with ground-based augmentation
        [DataMember (Name = "status")] public sbyte Status;
        // Bits defining which Global Navigation Satellite System signals were
        // used by the receiver.
        public const ushort SERVICE_GPS = 1;
        public const ushort SERVICE_GLONASS = 2;
        public const ushort SERVICE_COMPASS = 4; // includes BeiDou.
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
        internal NavSatStatus(ref ReadBuffer b)
        {
            Status = b.Deserialize<sbyte>();
            Service = b.Deserialize<ushort>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new NavSatStatus(ref b);
        
        public NavSatStatus RosDeserialize(ref ReadBuffer b) => new NavSatStatus(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Status);
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 3;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/NavSatStatus";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "331cdbddfa4bc96ffc3b9ad98900a54c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE42RT0/DMAzF7/0UlnZmYgghLiB1MKZJY0OUfzeUtm5rKUumxNnYt8ehLWyMA7nV7vvZ" +
                "fm8AC7WhWjFZA5li1JoYoaIP8Kw4eKisA2V2MNU2V/rv37OdZ1wlyQBeG+QGHbAFG3gdWLSgQr1Cw1h+" +
                "cclDiYxuRUYq+Q5yyw2IKnYFwbs1iqr8KmnlGZhWCCVVFTrBkGxRWOewiFt42EoV5Atpg+UQIBVGN2ej" +
                "NJWwbdD011xfQfaUPj1n73ezt2GSkOHLvrJYxiJcAZyMoHsDCEblGuNBEbq2nuLYA2Gniu/0QLh/94Ei" +
                "G6e9bG/UlsQI37t6kisv0g6ijqZOfxhnvxi1s8GUfwFaQmtGzGtMHOOoyJCpxSkqmn8kDZ5qo3TrvVCC" +
                "b5OMiXVJODE3yLDRBWSTx5fZzeR9+pB1No2OWvPlIs1i++x362Z5/9C2zvsbyRQ6lOhhjHRrw/CIls5n" +
                "88lSJJffS3h0GyowST4B7Ndp4/ICAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
