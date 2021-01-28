/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Float32")]
    public sealed class Float32 : IDeserializable<Float32>, IMessage
    {
        [DataMember (Name = "data")] public float Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Float32()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float32(float Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Float32(ref Buffer b)
        {
            Data = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Float32(ref b);
        }
        
        Float32 IDeserializable<Float32>.RosDeserialize(ref Buffer b)
        {
            return new Float32(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Float32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "73fcbf46b49191e672908e50842a83d4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA0vLyU8sMTZSSEksSeQCAK0qjc8NAAAA";
                
    }
}
