/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int16 : IDeserializable<Int16>, IMessage
    {
        [DataMember (Name = "data")] public short Data;
    
        public Int16()
        {
        }
        
        public Int16(short Data)
        {
            this.Data = Data;
        }
        
        public Int16(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Int16(ref ReadBuffer2 b)
        {
            b.Align2();
            b.Deserialize(out Data);
        }
        
        public Int16 RosDeserialize(ref ReadBuffer b) => new Int16(ref b);
        
        public Int16 RosDeserialize(ref ReadBuffer2 b) => new Int16(ref b);
    
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
    
        public const int RosFixedMessageLength = 2;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 2;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align2(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/Int16";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8524586e34fbd7cb1c08c5f5f1ca0e57";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE0U0hJLEnk4gIAJDs+BgwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}