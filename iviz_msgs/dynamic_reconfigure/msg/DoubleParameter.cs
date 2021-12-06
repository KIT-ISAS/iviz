/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class DoubleParameter : IDeserializable<DoubleParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public double Value;
    
        /// Constructor for empty message.
        public DoubleParameter()
        {
            Name = string.Empty;
        }
        
        /// Explicit constructor.
        public DoubleParameter(string Name, double Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        internal DoubleParameter(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Value = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new DoubleParameter(ref b);
        
        public DoubleParameter RosDeserialize(ref ReadBuffer b) => new DoubleParameter(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/DoubleParameter";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d8512f27253c0f65f928a67c329cd658";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5UrLyU8sMTNRKEvMKU3l4gIAQejvOBsAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
