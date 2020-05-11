using System.Runtime.Serialization;

namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class DiagnosticArray : IMessage
    {
        // This message is used to send diagnostic information about the state of the robot
        public std_msgs.Header header { get; set; } //for timestamp
        public DiagnosticStatus[] status { get; set; } // an array of components being reported on
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticArray()
        {
            header = new std_msgs.Header();
            status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DiagnosticArray(std_msgs.Header header, DiagnosticStatus[] status)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.status = status ?? throw new System.ArgumentNullException(nameof(status));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DiagnosticArray(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.status = b.DeserializeArray<DiagnosticStatus>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new DiagnosticArray(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.SerializeArray(this.status, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (status is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += header.RosMessageLength;
                for (int i = 0; i < status.Length; i++)
                {
                    size += status[i].RosMessageLength;
                }
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "diagnostic_msgs/DiagnosticArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "60810da900de1dd6ddd437c3503511da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UwW7bMAy96ysI5NB2QNutvRXIoUC7rei2FmmwHYahoG3WFmpLniQn89/vSYqTrMCA" +
                "HbYFDiTZ5CP5+KgZLRvtqRPvuRbCdvBSUbDkxVRUaa6N9UGXpM2TdR0HbQ1xYYdAoRHygYOQfUoHZwsb" +
                "1HvhShw1eZnBi4JGgMBdr662gA/wHPzXbwli8DQjBrBzPEa40na9NWKCp0K0qclJb11Aatao+V/+qY8P" +
                "7y6QR/XY+dqf5gLUjJCiqdhVoCdwxYEpFtPouhF33MpKWkpVIav0NYy9+BM4Jk7x1GLEcduOW1ZRVzcY" +
                "XUbWtqxM/vDU4IB6diBoaNnB3rpKm2j+5LiTiI7Hy/dBTCl0c3UBG+OlHIJGQiMQSifsI2c3V6QGbcL5" +
                "WXRQs+XaHuMotey1BJ3jEJOVH72DDJAM+wvEeJWLOwE2yBFEqTwdpnePOPojQhCkgMaUDR0i8/sxNFBH" +
                "lMKKneaiTYoqwQBQD6LTwdEesknQhiGIDXxG3MX4E1izxY01HTfoWRur90MNAmHYO7vSFUyLMYGUrYaw" +
                "qNWFYzeq6JVDqtnbyDGM4JU6gpW9t6XmKL21Do3ywUX01I1HXal/pMbd5GVRvhycSWXT5Da2BX/TSGKe" +
                "MEQYKG0qjeIHbncj9cu4JkHhf2+915HZpOvkbnuIN867V8UIAd7dzl/n3ZfLxaf5m7y/XizuFvOzfHhY" +
                "Xn64np+rfMojMtus+4gkZujiPqqtsCuhiVcTG4C7gCrxpdN9st4kHKDY010Z+UqA0+Q7cfEb90zMZNxg" +
                "sNfsYg+Tw3QmjCeGi7KZupXxM7eD4KJaxfXlRbV5+UIl+/H+l0KmTKcCn2VErus43bh4Wi7Qg6TslDI+" +
                "iKGVlvUegflLZCPv4Bccl8+EDuUrQ6mfaRVQPzIGAAA=";
                
    }
}
