/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int64 : IDeserializableRos1<Int64>, IDeserializableRos2<Int64>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "data")] public long Data;
    
        /// Constructor for empty message.
        public Int64()
        {
        }
        
        /// Explicit constructor.
        public Int64(long Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int64(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        /// Constructor with buffer.
        public Int64(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Int64(ref b);
        
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
        public const string MessageType = "std_msgs/Int64";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "34add168574510e6e17f5d23ecc077ef";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzEzUUhJLEnkAgBZU74aCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
