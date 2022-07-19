/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int8 : IDeserializableRos1<Int8>, IDeserializableRos2<Int8>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "data")] public sbyte Data;
    
        /// Constructor for empty message.
        public Int8()
        {
        }
        
        /// Explicit constructor.
        public Int8(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int8(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        /// Constructor with buffer.
        public Int8(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Int8(ref b);
        
        public Int8 RosDeserialize(ref ReadBuffer b) => new Int8(ref b);
        
        public Int8 RosDeserialize(ref ReadBuffer2 b) => new Int8(ref b);
    
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
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 1;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int8";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "27ffa0c9c4b8fb8492252bcad9e5c57b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMK7FQSEksSeTiAgDmSq87CwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
