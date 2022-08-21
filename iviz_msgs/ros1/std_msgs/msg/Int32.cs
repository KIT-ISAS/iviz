/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int32 : IDeserializable<Int32>, IMessage
    {
        [DataMember (Name = "data")] public int Data;
    
        public Int32()
        {
        }
        
        public Int32(int Data)
        {
            this.Data = Data;
        }
        
        public Int32(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Int32(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Int32 RosDeserialize(ref ReadBuffer b) => new Int32(ref b);
        
        public Int32 RosDeserialize(ref ReadBuffer2 b) => new Int32(ref b);
    
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
    
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/Int32";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UkhJLEnkAgAHaI4xCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
