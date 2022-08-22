/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int64 : IDeserializable<Int64>, IMessage
    {
        [DataMember (Name = "data")] public long Data;
    
        public Int64()
        {
        }
        
        public Int64(long Data)
        {
            this.Data = Data;
        }
        
        public Int64(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Int64(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Data);
        }
        
        public Int64 RosDeserialize(ref ReadBuffer b) => new Int64(ref b);
        
        public Int64 RosDeserialize(ref ReadBuffer2 b) => new Int64(ref b);
    
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
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/Int64";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "34add168574510e6e17f5d23ecc077ef";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzEzUUhJLEnkAgBZU74aCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
