namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class DiagnosticArray : IMessage
    {
        // This message is used to send diagnostic information about the state of the robot
        public std_msgs.Header header; //for timestamp
        public DiagnosticStatus[] status; // an array of components being reported on
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticArray()
        {
            header = new std_msgs.Header();
            status = System.Array.Empty<DiagnosticStatus>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.DeserializeArray(out status, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.SerializeArray(status, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 4;
            size += header.GetLength();
            for (int i = 0; i < status.Length; i++)
            {
                size += status[i].GetLength();
            }
            return size;
        }
    
        public IMessage Create() => new DiagnosticArray();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "diagnostic_msgs/DiagnosticArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "60810da900de1dd6ddd437c3503511da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
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
