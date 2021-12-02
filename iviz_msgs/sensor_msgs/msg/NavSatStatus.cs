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
        internal NavSatStatus(ref Buffer b)
        {
            Status = b.Deserialize<sbyte>();
            Service = b.Deserialize<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new NavSatStatus(ref b);
        
        NavSatStatus IDeserializable<NavSatStatus>.RosDeserialize(ref Buffer b) => new NavSatStatus(ref b);
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACo2RT0/CQBDF7/0Uk3CWiDGGiyZFkZAgGOu/m9m203aSZZfszoJ8e2ehVUAP7q0zfb+Z" +
                "ea8Hc7WmWjFZA5li1JoYoaJP8Kw4eKisA2W2MNE2V/rv37OtZ1wmSQ/eGuQGHbAFG3gVWLSgQr1Ew1ju" +
                "uOShREa3JCOVfAu55QZEFbuC4O0KRVXuSlp5BqYlQklVhU4wJFsU1jks4hYeNlIF+UJaY9kHSIXRzlkr" +
                "TSVsGjTdNTfXkD2nzy/Zx/30vZ8kZHjYVeaLWIRrgLMBtK8HwahcYzwoQlfWUxx7JGxV8Z0fCQ/vPlJk" +
                "o7STHYzakBjhO1fPcuVF2kJ2lh8xJj+MixNG7Www5V+AZEfYmxHzGhHHOCoyZGpxiormH0mDp9oovfde" +
                "KCHOkSRjYm0STswNMmxwBdn46XV6O/6YPGatTYNfrdlinmaxfXHaul08PO5bl92NZAodSvQwQrqzof+L" +
                "ls6ms/FCJMPvJTy6NRWYJF/s12nj8gIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
