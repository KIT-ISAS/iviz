/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class String : IDeserializable<String>, IMessage
    {
        [DataMember (Name = "data")] public string Data;
    
        /// Constructor for empty message.
        public String()
        {
            Data = "";
        }
        
        /// Explicit constructor.
        public String(string Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public String(ref ReadBuffer b)
        {
            b.DeserializeString(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new String(ref b);
        
        public String RosDeserialize(ref ReadBuffer b) => new String(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Data);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/String";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "992ce8a1687cec8c8bd883ec73ca41d1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1dISSxJ5OICADpmzaUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
