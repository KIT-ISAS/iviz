/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt64 : IDeserializableRos1<UInt64>, IDeserializableRos2<UInt64>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "data")] public ulong Data;
    
        /// Constructor for empty message.
        public UInt64()
        {
        }
        
        /// Explicit constructor.
        public UInt64(ulong Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt64(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        /// Constructor with buffer.
        public UInt64(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new UInt64(ref b);
        
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt64";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1b2a79973e8bf53d7b53acb71299cb57";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxM1FISSxJ5AIAPtIFtgwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
