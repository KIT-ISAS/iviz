/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Float64 : IDeserializableRos1<Float64>, IDeserializableRos2<Float64>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "data")] public double Data;
    
        /// Constructor for empty message.
        public Float64()
        {
        }
        
        /// Explicit constructor.
        public Float64(double Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Float64(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        /// Constructor with buffer.
        public Float64(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Float64(ref b);
        
        public Float64 RosDeserialize(ref ReadBuffer b) => new Float64(ref b);
        
        public Float64 RosDeserialize(ref ReadBuffer2 b) => new Float64(ref b);
    
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
        public const string MessageType = "std_msgs/Float64";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "fdb28210bfa9d7c91146260178d9a584";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
