/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/BoolParameter")]
    public sealed class BoolParameter : IDeserializable<BoolParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public bool Value;
    
        /// <summary> Constructor for empty message. </summary>
        public BoolParameter()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoolParameter(string Name, bool Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal BoolParameter(ref Buffer b)
        {
            Name = b.DeserializeString();
            Value = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BoolParameter(ref b);
        }
        
        BoolParameter IDeserializable<BoolParameter>.RosDeserialize(ref Buffer b)
        {
            return new BoolParameter(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/BoolParameter";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "23f05028c1a699fb83e22401228c3a9e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1fIS8xN5UrKz89RKEvMKU3l4uUCAOoCo2QZAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
