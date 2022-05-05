/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Float64 : IDeserializable<Float64>, IMessage
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Float64(ref b);
        
        public Float64 RosDeserialize(ref ReadBuffer b) => new Float64(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Float64";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "fdb28210bfa9d7c91146260178d9a584";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
