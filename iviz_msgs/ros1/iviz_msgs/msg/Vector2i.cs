/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Vector2i : IDeserializable<Vector2i>, IMessage
    {
        [DataMember (Name = "x")] public int X;
        [DataMember (Name = "y")] public int Y;
    
        public Vector2i()
        {
        }
        
        public Vector2i(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        public Vector2i(ref ReadBuffer b)
        {
            b.Deserialize(out X);
            b.Deserialize(out Y);
        }
        
        public Vector2i(ref ReadBuffer2 b)
        {
            b.Deserialize(out X);
            b.Deserialize(out Y);
        }
        
        public Vector2i RosDeserialize(ref ReadBuffer b) => new Vector2i(ref b);
        
        public Vector2i RosDeserialize(ref ReadBuffer2 b) => new Vector2i(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(X);
            b.Serialize(Y);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.Align4(c);
            c += 4; /* X */
            c += 4; /* Y */
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Vector2i";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bd7b43fd41d4c47bf5c703cc7d016709";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UqjgygTTlVxcAN81niARAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
