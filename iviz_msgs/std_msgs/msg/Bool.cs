/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Bool")]
    public sealed class Bool : IMessage
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
        internal Bool(Buffer b)
        {
            Data = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Bool(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 1;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Bool";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8b94c1b53db61fb6aed406028ad6332a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vKz89RSEksSeQCAGFR0NcKAAAA";
                
    }
}
