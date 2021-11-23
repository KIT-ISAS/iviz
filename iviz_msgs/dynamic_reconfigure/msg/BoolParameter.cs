/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class BoolParameter : IDeserializable<BoolParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public bool Value;
    
        /// Constructor for empty message.
        public BoolParameter()
        {
            Name = string.Empty;
        }
        
        /// Explicit constructor.
        public BoolParameter(string Name, bool Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        internal BoolParameter(ref Buffer b)
        {
            Name = b.DeserializeString();
            Value = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new BoolParameter(ref b);
        
        BoolParameter IDeserializable<BoolParameter>.RosDeserialize(ref Buffer b) => new BoolParameter(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/BoolParameter";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "23f05028c1a699fb83e22401228c3a9e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5UrKz89RKEvMKU3l4gIAls+kWRgAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
