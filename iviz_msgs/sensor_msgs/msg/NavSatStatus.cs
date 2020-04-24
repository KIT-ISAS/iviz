namespace Iviz.Msgs.sensor_msgs
{
    public sealed class NavSatStatus : IMessage
    {
        // Navigation Satellite fix status for any Global Navigation Satellite System
        
        // Whether to output an augmented fix is determined by both the fix
        // type and the last time differential corrections were received.  A
        // fix is valid when status >= STATUS_FIX.
        
        public const sbyte STATUS_NO_FIX = -1; // unable to fix position
        public const sbyte STATUS_FIX = 0; // unaugmented fix
        public const sbyte STATUS_SBAS_FIX = 1; // with satellite-based augmentation
        public const sbyte STATUS_GBAS_FIX = 2; // with ground-based augmentation
        
        public sbyte status;
        
        // Bits defining which Global Navigation Satellite System signals were
        // used by the receiver.
        
        public const ushort SERVICE_GPS = 1;
        public const ushort SERVICE_GLONASS = 2;
        public const ushort SERVICE_COMPASS = 4; // includes BeiDou.
        public const ushort SERVICE_GALILEO = 8;
        
        public ushort service;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out status, ref ptr, end);
            BuiltIns.Deserialize(out service, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(status, ref ptr, end);
            BuiltIns.Serialize(service, ref ptr, end);
        }
    
        public int GetLength() => 3;
    
        public IMessage Create() => new NavSatStatus();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/NavSatStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "331cdbddfa4bc96ffc3b9ad98900a54c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE42RT0/DMAzF7/0UlnZmYgghLiB1MKZJY0OUfzeUtm5rKUumxNnYt8ehLWyMA7nV7vvZ" +
                "fm8AC7WhWjFZA5li1JoYoaIP8Kw4eKisA2V2MNU2V/rv37OdZ1wlyQBeG+QGHbAFG3gdWLSgQr1Cw1h+" +
                "cclDiYxuRUYq+Q5yyw2IKnYFwbs1iqr8KmnlGZhWCCVVFTrBkGxRWOewiFt42EoV5Atpg+UQIBVGN2ej" +
                "NJWwbdD011xfQfaUPj1n73ezt2GSkOHLvrJYxiJcAZyMoHsDCEblGuNBEbq2nuLYA2Gniu/0QLh/94Ei" +
                "G6e9bG/UlsQI37t6kisv0g6ijqZOfxhnvxi1s8GUfwFaQmtGzGtMHOOoyJCpxSkqmn8kDZ5qo3TrvVCC" +
                "b5OMiXVJODE3yLDRBWSTx5fZzeR9+pB1No2OWvPlIs1i++x362Z5/9C2zvsbyRQ6lOhhjHRrw/CIls5n" +
                "88lSJJffS3h0GyowST4B7Ndp4/ICAAA=";
                
    }
}
