/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Float32 : IDeserializableRos1<Float32>, IDeserializableRos2<Float32>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "data")] public float Data;
    
        /// Constructor for empty message.
        public Float32()
        {
        }
        
        /// Explicit constructor.
        public Float32(float Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Float32(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        /// Constructor with buffer.
        public Float32(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Float32(ref b);
        
        public Float32 RosDeserialize(ref ReadBuffer b) => new Float32(ref b);
        
        public Float32 RosDeserialize(ref ReadBuffer2 b) => new Float32(ref b);
    
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
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Float32";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "73fcbf46b49191e672908e50842a83d4";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTZSSEksSeQCAK0qjc8NAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
