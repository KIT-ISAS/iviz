/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class StrParameter : IDeserializable<StrParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public string Value;
    
        /// Constructor for empty message.
        public StrParameter()
        {
            Name = string.Empty;
            Value = string.Empty;
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
            Name = b.DeserializeString();
            Value = b.DeserializeString();
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
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Value is null) throw new System.NullReferenceException(nameof(Value));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(Value);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/StrParameter";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bc6ccc4a57f61779c8eaae61e9f422e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsMsSc0pTubgAuEJhxBoAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
