/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/IntParameter")]
    public sealed class IntParameter : IDeserializable<IntParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "value")] public int Value { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public IntParameter()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public IntParameter(string Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public IntParameter(ref Buffer b)
        {
            Name = b.DeserializeString();
            Value = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new IntParameter(ref b);
        }
        
        IntParameter IDeserializable<IntParameter>.RosDeserialize(ref Buffer b)
        {
            return new IntParameter(ref b);
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
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/IntParameter";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "65fedc7a0cbfb8db035e46194a350bf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5crMKzE2UihLzClN5eICAPL2vOMZAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
