/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Bool")]
    public sealed class Bool : IDeserializable<Bool>, IMessage
    {
        [DataMember (Name = "data")] public bool Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Bool()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Bool(bool Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Bool(ref Buffer b)
        {
            Data = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Bool(ref b);
        }
        
        Bool IDeserializable<Bool>.RosDeserialize(ref Buffer b)
        {
            return new Bool(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Bool";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8b94c1b53db61fb6aed406028ad6332a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACkvKz89RSEksSeTlAgDLEfRHCwAAAA==";
                
    }
}
