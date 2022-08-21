/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt64 : IDeserializable<UInt64>, IMessage
    {
        [DataMember (Name = "data")] public ulong Data;
    
        public UInt64()
        {
        }
        
        public UInt64(ulong Data)
        {
            this.Data = Data;
        }
        
        public UInt64(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt64(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt64 RosDeserialize(ref ReadBuffer b) => new UInt64(ref b);
        
        public UInt64 RosDeserialize(ref ReadBuffer2 b) => new UInt64(ref b);
    
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
        
    
        public const string MessageType = "std_msgs/UInt64";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1b2a79973e8bf53d7b53acb71299cb57";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxM1FISSxJ5AIAPtIFtgwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
