/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class DoubleParameter : IDeserializable<DoubleParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public double Value;
    
        /// Constructor for empty message.
        public DoubleParameter()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public DoubleParameter(string Name, double Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        public DoubleParameter(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Value);
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
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(Name);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/DoubleParameter";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d8512f27253c0f65f928a67c329cd658";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5UrLyU8sMTNRKEvMKU3l4gIAQejvOBsAAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
