/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt32 : IDeserializable<UInt32>, IMessage
    {
        [DataMember (Name = "data")] public uint Data;
    
        public UInt32()
        {
        }
        
        public UInt32(uint Data)
        {
            this.Data = Data;
        }
        
        public UInt32(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt32(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new UInt32(ref b);
        
        public UInt32 RosDeserialize(ref ReadBuffer b) => new UInt32(ref b);
        
        public UInt32 RosDeserialize(ref ReadBuffer2 b) => new UInt32(ref b);
    
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
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        public const string MessageType = "std_msgs/UInt32";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "304a39449588c7f8ce2df6e8001c5fce";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNlJISSxJ5AIAYOk1nQwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
