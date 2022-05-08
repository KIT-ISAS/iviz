/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class StrParameter : IDeserializable<StrParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public string Value;
    
        /// Constructor for empty message.
        public StrParameter()
        {
            Name = "";
            Value = "";
        }
        
        /// Explicit constructor.
        public StrParameter(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        public StrParameter(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Value);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new StrParameter(ref b);
        
        public StrParameter RosDeserialize(ref ReadBuffer b) => new StrParameter(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Value is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(Value);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/StrParameter";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsMsSc0pTubgAuEJhxBoAAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
