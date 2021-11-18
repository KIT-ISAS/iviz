/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/DoubleParameter")]
    public sealed class DoubleParameter : IDeserializable<DoubleParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public double Value;
    
        /// <summary> Constructor for empty message. </summary>
        public DoubleParameter()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public DoubleParameter(string Name, double Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DoubleParameter(ref Buffer b)
        {
            Name = b.DeserializeString();
            Value = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DoubleParameter(ref b);
        }
        
        DoubleParameter IDeserializable<DoubleParameter>.RosDeserialize(ref Buffer b)
        {
            return new DoubleParameter(ref b);
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
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/DoubleParameter";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d8512f27253c0f65f928a67c329cd658";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1fIS8xN5UrLyU8sMTNRKEvMKU3l4uUCALoOdXwcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
