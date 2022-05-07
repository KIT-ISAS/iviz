/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class BoolParameter : IDeserializable<BoolParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public bool Value;
    
        /// Constructor for empty message.
        public BoolParameter()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public BoolParameter(string Name, bool Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        public BoolParameter(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Value);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new BoolParameter(ref b);
        
        public BoolParameter RosDeserialize(ref ReadBuffer b) => new BoolParameter(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Name);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/BoolParameter";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "23f05028c1a699fb83e22401228c3a9e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5UrKz89RKEvMKU3l4gIAls+kWRgAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
