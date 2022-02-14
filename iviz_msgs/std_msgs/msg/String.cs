/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            Data = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new String(ref b);
        
        public String RosDeserialize(ref ReadBuffer b) => new String(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Data);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/String";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "992ce8a1687cec8c8bd883ec73ca41d1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1dISSxJ5OICADpmzaUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
