
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/NavSatStatus";
    
        public IMessage Create() => new NavSatStatus();
    
        public int GetLength() => 3;
    
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "331cdbddfa4bc96ffc3b9ad98900a54c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACo2RT0/CQBDF7038DpNwlggxxosmRZGQIBjrv5vZttN2kmWX7M6CfHtnoVVBD+6tM32/" +
                "mXmvB3O1ploxWQOZYtSaGKGiD/CsOHiorANltjDRNlf679+zrWdcJkkPXhvkBh2wBRt4FVi0oEK9RMNY" +
                "7rjkoURGtyQjlXwLueUGRBW7guDtCkVV7kpaeQamJUJJVYVOMCRbFNY5LOIWHjZSBflCWmPZB0iF0c5Z" +
                "K00lbBo03TXXV5A9pU/P2fvd9K2fJGT4sqvMF7EIVwCnA2hfD4JRucZ4UISurKc49kDYquI7OxD+vPtA" +
                "kY3STvZj1IbECN+5eporL9IWsrP8gDH5ZgyPGLWzwZR/AZIdYW9GzGtEHOOoyJCpxSkqmn8kDZ5qo/Te" +
                "e6GEOEeSjIm1STgxN8iwwQVk48eX6c34ffKQtTYNfrVmi3maxfbwuHWzuH/Yt867G8kUOpToYYR0a0P/" +
                "Fy2dTWfjhUguv5bw6NZUYHKSfALQV8nU8wIAAA==";
                
    }
}
