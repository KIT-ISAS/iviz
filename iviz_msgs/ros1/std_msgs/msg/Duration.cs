/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Duration : IDeserializable<Duration>, IMessage
    {
        [DataMember (Name = "data")] public duration Data;
    
        public Duration()
        {
        }
        
        public Duration(duration Data)
        {
            this.Data = Data;
        }
        
        public Duration(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Duration(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Data);
        }
        
        public Duration RosDeserialize(ref ReadBuffer b) => new Duration(ref b);
        
        public Duration RosDeserialize(ref ReadBuffer2 b) => new Duration(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/Duration";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3e286caf4241d664e55f3ad380e2ae46";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0spLUosyczPU0hJLEnk4gIAtVhIcg8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
