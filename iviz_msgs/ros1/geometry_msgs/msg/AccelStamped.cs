/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class AccelStamped : IDeserializable<AccelStamped>, IMessageRos1
    {
        // An accel with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public Accel Accel;
    
        /// Constructor for empty message.
        public AccelStamped()
        {
            Accel = new Accel();
        }
        
        /// Explicit constructor.
        public AccelStamped(in StdMsgs.Header Header, Accel Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// Constructor with buffer.
        public AccelStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new Accel(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AccelStamped(ref b);
        
        public AccelStamped RosDeserialize(ref ReadBuffer b) => new AccelStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Accel is null) BuiltIns.ThrowNullReference();
            Accel.RosValidate();
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/AccelStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d8a98a5d81351b6eb0578c78557e7659";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTWvbQBC961cM+JCk2CokpYdAD4HSNodCIKHXMN4dS0ukXXV3ZEf99X27spWGXnpo" +
                "awTWx8ybeW/e7IpuPLEx0tHBaUtRdhLFGyETQrTOswrtIvdC7C2p6yUp90P1RdhKpLb8VTcFoeBU1Ye/" +
                "/Ku+3n++pqT2sU9NejtXrlZ0r2iJo6VelC0r0y6gI9e0Ejed7NFR6VUsla86DZJqJD60LhGuRrxE7rqJ" +
                "xoQgDSDd96N3JrNeuJ7ykekgFg0c1Zmx4/ibSBkdV5LvYxHx9uM1YnwSM6pDQxMQTBROzjf4SNXovF5d" +
                "5oRq9XAIGzxKA12X4qQta25WnocoKffJ6Ro13szkamBDHEEVm+i8vHvEY7ogFEELMgTT0jk6v5u0DR6A" +
                "QnuOjredZGADBYB6lpPOLn5B9gXasw8n+BnxpcafwPoFN3PatJhZl9mnsYGACBxi2DuL0O1UQEznxCt1" +
                "bhs5TlXOmktWq0/FiJrHVyaCf04pGIcB2GLgKmnM6GUaj87+Kzc2EuC6OM2WLPY/Ges0qDTvAxymDvpA" +
                "qV0UUBkYGm5jeJL8EqZzmsDWC+TIO8a+Kd7KNoNdv4nREK/oGPLyfIz7PwyPVU8co2SOGBNI0r58e02w" +
                "zmtwW4wbPGzfC2OmILtkItG6iFSIUwMVxw7WV9aQg2yAej4oMHp+AqTARTmbhwFgWOXIPnWzsEVBOpe6" +
                "qdd0aKFqicouKDtbttwZiq5xds5EoX5JZjqSW5PuLuGirpt7novBkgCJQUvCRU23O5rCSIdMCDfxeLgE" +
                "2srSV1kCDWGdT5YjxGtB7wJmD1lS4gb74pPiWKuratcF1vfv6Hm5m5a7H9VP4NsfYa8FAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
