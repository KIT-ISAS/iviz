/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Float64")]
    public sealed class Float64 : IDeserializable<Float64>, IMessage
    {
        [DataMember (Name = "data")] public double Data;
    
        /// <summary> Constructor for empty message. </summary>
        public Float64()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float64(double Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Float64(ref Buffer b)
        {
            Data = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Float64(ref b);
        }
        
        Float64 IDeserializable<Float64>.RosDeserialize(ref Buffer b)
        {
            return new Float64(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Float64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fdb28210bfa9d7c91146260178d9a584";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTNRSEksSeQCAPMRveQNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
