/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class DiagnosticArray : IDeserializableRos1<DiagnosticArray>, IDeserializableRos2<DiagnosticArray>, IMessageRos1, IMessageRos2
    {
        // This message is used to send diagnostic information about the state of the robot
        /// <summary> For timestamp </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> An array of components being reported on </summary>
        [DataMember (Name = "status")] public DiagnosticStatus[] Status;
    
        /// Constructor for empty message.
        public DiagnosticArray()
        {
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// Explicit constructor.
        public DiagnosticArray(in StdMsgs.Header Header, DiagnosticStatus[] Status)
        {
            this.Header = Header;
            this.Status = Status;
        }
        
        /// Constructor with buffer.
        public DiagnosticArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeArray(out Status);
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        /// Constructor with buffer.
        public DiagnosticArray(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeArray(out Status);
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new DiagnosticArray(ref b);
        
        public DiagnosticArray RosDeserialize(ref ReadBuffer b) => new DiagnosticArray(ref b);
        
        public DiagnosticArray RosDeserialize(ref ReadBuffer2 b) => new DiagnosticArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Status);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Status);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] is null) BuiltIns.ThrowNullReference(nameof(Status), i);
                Status[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + WriteBuffer.GetArraySize(Status);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Status);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "diagnostic_msgs/DiagnosticArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "60810da900de1dd6ddd437c3503511da";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
