/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt16 : IDeserializable<UInt16>, IMessage
    {
        [DataMember (Name = "data")] public ushort Data;
    
        public UInt16()
        {
        }
        
        public UInt16(ushort Data)
        {
            this.Data = Data;
        }
        
        public UInt16(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt16(ref ReadBuffer2 b)
        {
            b.Align2();
            b.Deserialize(out Data);
        }
        
        public UInt16 RosDeserialize(ref ReadBuffer b) => new UInt16(ref b);
        
        public UInt16 RosDeserialize(ref ReadBuffer2 b) => new UInt16(ref b);
    
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
        
    
        public const string MessageType = "std_msgs/UInt16";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1df79edf208b629fe6b81923a544552d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNFNISSxJ5OICAF50RNUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}