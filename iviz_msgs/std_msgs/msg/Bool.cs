/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Bool : IDeserializable<Bool>, IMessage
    {
        [DataMember (Name = "data")] public bool Data;
    
        /// Constructor for empty message.
        public Bool()
        {
        }
        
        /// Explicit constructor.
        public Bool(bool Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Bool(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Bool(ref b);
        
        public Bool RosDeserialize(ref ReadBuffer b) => new Bool(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Bool";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "8b94c1b53db61fb6aed406028ad6332a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0vKz89RSEksSeQCAGFR0NcKAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
