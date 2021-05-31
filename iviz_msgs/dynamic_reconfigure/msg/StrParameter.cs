/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/StrParameter")]
    public sealed class StrParameter : IDeserializable<StrParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "value")] public string Value { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public StrParameter()
        {
            Name = string.Empty;
            Value = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public StrParameter(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public StrParameter(ref Buffer b)
        {
            Name = b.DeserializeString();
            Value = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new StrParameter(ref b);
        }
        
        StrParameter IDeserializable<StrParameter>.RosDeserialize(ref Buffer b)
        {
            return new StrParameter(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Value is null) throw new System.NullReferenceException(nameof(Value));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Value);
                return size;
            }
        }
    
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