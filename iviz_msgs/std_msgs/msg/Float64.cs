/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            Data = b.Deserialize<double>();
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
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Float64";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "fdb28210bfa9d7c91146260178d9a584";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
