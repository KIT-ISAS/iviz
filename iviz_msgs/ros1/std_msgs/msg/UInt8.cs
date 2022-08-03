/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt8 : IDeserializable<UInt8>, IMessage
    {
        [DataMember (Name = "data")] public byte Data;
    
        public UInt8()
        {
        }
        
        public UInt8(byte Data)
        {
            this.Data = Data;
        }
        
        public UInt8(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt8(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt8 RosDeserialize(ref ReadBuffer b) => new UInt8(ref b);
        
        public UInt8 RosDeserialize(ref ReadBuffer2 b) => new UInt8(ref b);
    
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
    
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        public const string MessageType = "std_msgs/UInt8";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7c8164229e7d2c17eb95e9231617fdee";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
