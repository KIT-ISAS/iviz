/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Float32 : IDeserializable<Float32>, IMessage
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
        internal Float32(ref Buffer b)
        {
            Data = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Float32(ref b);
        
        Float32 IDeserializable<Float32>.RosDeserialize(ref Buffer b) => new Float32(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Float32";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "73fcbf46b49191e672908e50842a83d4";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSSEksSeQCAK0qjc8NAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
