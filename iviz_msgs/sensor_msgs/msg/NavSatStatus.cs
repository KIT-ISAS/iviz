/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/NavSatStatus")]
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
        [DataMember (Name = "status")] public sbyte Status { get; set; }
        // Bits defining which Global Navigation Satellite System signals were
        // used by the receiver.
        public const ushort SERVICE_GPS = 1;
        public const ushort SERVICE_GLONASS = 2;
        public const ushort SERVICE_COMPASS = 4; // includes BeiDou.
        public const ushort SERVICE_GALILEO = 8;
        [DataMember (Name = "service")] public ushort Service { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public NavSatStatus()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public NavSatStatus(sbyte Status, ushort Service)
        {
            this.Status = Status;
            this.Service = Service;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public NavSatStatus(ref Buffer b)
        {
            Status = b.Deserialize<sbyte>();
            Service = b.Deserialize<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new NavSatStatus(ref b);
        }
        
        NavSatStatus IDeserializable<NavSatStatus>.RosDeserialize(ref Buffer b)
        {
            return new NavSatStatus(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Status);
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 3;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/NavSatStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "331cdbddfa4bc96ffc3b9ad98900a54c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACo2RT2/CMAzF70h8B0ucQWOaJi5MKowhJAZo3b8bSlu3tRQSlDgwvv2S0mp07LDcavf9" +
                "bL/Xg5U4UCGYtIJYMEpJjJDTF1gW7Czk2oBQJ5hLnQj59+/xyTLuup1upwcfJXKJBliDdrx37NUgXLFD" +
                "xZhVZLKQIaPZkfKV5ASJ5hK8KnQDg0979LKsqklhGZh2CBnlORrPIb9Iqo3BNCxi4eir4L+QDpgNAKIA" +
                "qScdhKQMjiWq5qKHMcSv0etbvH1afA7C1qR41NRW61CGMUB/CPXrgVMikRiOCti9thQmt5W1LLyblvLy" +
                "+LYknkSN7mLYkbwdtnG3nwjrtTWlsr4Nmf9Abn9BCqOdyv4k1IyzJ90quglxSCYnRarwllFa/iN2sFQo" +
                "Ic8pBIwLw3yqIbw6FFPZ7PzE4T3Es5f3xXS2nW/i2q/hdW+5XkVx6N9e9abr5825d9ccSyqVLkMLE6RH" +
                "7fy037xouVjO1l4zutjEojlQ6pfudr4BiBdi6wkDAAA=";
                
    }
}
