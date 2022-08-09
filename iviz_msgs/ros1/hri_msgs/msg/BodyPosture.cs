/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class BodyPosture : IDeserializable<BodyPosture>, IMessage
    {
        // Describes the general body posture in a symbolic manner.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        public const byte STANDING = 1;
        public const byte SITTING = 2;
        public const byte CROUCHING = 3;
        public const byte LAYING = 4;
        public const byte OTHER = 0;
        /// <summary> One of the above constants </summary>
        [DataMember (Name = "posture")] public byte Posture;
    
        public BodyPosture()
        {
        }
        
        public BodyPosture(in StdMsgs.Header Header, byte Posture)
        {
            this.Header = Header;
            this.Posture = Posture;
        }
        
        public BodyPosture(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Posture);
        }
        
        public BodyPosture(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Posture);
        }
        
        public BodyPosture RosDeserialize(ref ReadBuffer b) => new BodyPosture(ref b);
        
        public BodyPosture RosDeserialize(ref ReadBuffer2 b) => new BodyPosture(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Posture);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Posture);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 1 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c += 1;  // Posture
            return c;
        }
    
        public const string MessageType = "hri_msgs/BodyPosture";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "52f95070a71954a985e0ab92dd4d4eb9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RwW7bMAy96ysI5NB2QLK13WEI0EPRbE2ArR0S7zAMQ0BLjC3AljxRTue/HyXH3Xbr" +
                "oYYAwU98j+R7M1gR62BLYog1QUWOAjZQejNA5zn2gcA6QOChLX1jNbTopGah1oSGAtT5Uqq3Ln6AXXH7" +
                "sNo83MMNXE7QpihG5OqE3G0fv92tR+z6hH2+/T4C7yepx2L9cSvAuwmYxpmBdwT+kAfG0h8JtHcc0UVW" +
                "6uaVP/Vld78EjmbfcsVvx63VDHbSz2Aw0FJEgxHh4MUNW9UU5g0dqRESth0ZyK9x6IgXQixqyyDnZHUz" +
                "QM9SFL1s0ba9sxojQbQt/ccXZs6hwxCt7hsMUu+DsS6VHwK2lNTlMP3qyWmCzWqZnSHdRysDDaKgAyFb" +
                "V8kjZF+vrxJBTP2x9Xz5U82KJz8XnCoJ93kK8Rpjmpp+d4E4DYy8lGZvxi0X0kRcImlnGM4ztpdfvgDp" +
                "JrNQ53UN57LC1yHW3uXwjhgslg0lYS1WiOpZIp1d/KPssrRD5yf5UfFvj5fIumfdtNO8lvCaZAP3lTgp" +
                "hV3wR2uktByyiG4suQiNLQOGQSXW2FLNPiWzpUhYORq5kdlrK0kYeLKxVhxDUs+x7K1R6g/FaZ1VaQMA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
