using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class TypeDef : IMessage
    {
        public string type { get; set; }
        public string[] fieldnames { get; set; }
        public string[] fieldtypes { get; set; }
        public int[] fieldarraylen { get; set; }
        public string[] examples { get; set; }
        public string[] constnames { get; set; }
        public string[] constvalues { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TypeDef()
        {
            type = "";
            fieldnames = System.Array.Empty<string>();
            fieldtypes = System.Array.Empty<string>();
            fieldarraylen = System.Array.Empty<int>();
            examples = System.Array.Empty<string>();
            constnames = System.Array.Empty<string>();
            constvalues = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TypeDef(string type, string[] fieldnames, string[] fieldtypes, int[] fieldarraylen, string[] examples, string[] constnames, string[] constvalues)
        {
            this.type = type ?? throw new System.ArgumentNullException(nameof(type));
            this.fieldnames = fieldnames ?? throw new System.ArgumentNullException(nameof(fieldnames));
            this.fieldtypes = fieldtypes ?? throw new System.ArgumentNullException(nameof(fieldtypes));
            this.fieldarraylen = fieldarraylen ?? throw new System.ArgumentNullException(nameof(fieldarraylen));
            this.examples = examples ?? throw new System.ArgumentNullException(nameof(examples));
            this.constnames = constnames ?? throw new System.ArgumentNullException(nameof(constnames));
            this.constvalues = constvalues ?? throw new System.ArgumentNullException(nameof(constvalues));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TypeDef(Buffer b)
        {
            this.type = b.DeserializeString();
            this.fieldnames = b.DeserializeStringArray(0);
            this.fieldtypes = b.DeserializeStringArray(0);
            this.fieldarraylen = b.DeserializeStructArray<int>(0);
            this.examples = b.DeserializeStringArray(0);
            this.constnames = b.DeserializeStringArray(0);
            this.constvalues = b.DeserializeStringArray(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TypeDef(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.type);
            b.SerializeArray(this.fieldnames, 0);
            b.SerializeArray(this.fieldtypes, 0);
            b.SerializeStructArray(this.fieldarraylen, 0);
            b.SerializeArray(this.examples, 0);
            b.SerializeArray(this.constnames, 0);
            b.SerializeArray(this.constvalues, 0);
        }
        
        public void Validate()
        {
            if (type is null) throw new System.NullReferenceException();
            if (fieldnames is null) throw new System.NullReferenceException();
            if (fieldtypes is null) throw new System.NullReferenceException();
            if (fieldarraylen is null) throw new System.NullReferenceException();
            if (examples is null) throw new System.NullReferenceException();
            if (constnames is null) throw new System.NullReferenceException();
            if (constvalues is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 28;
                size += BuiltIns.UTF8.GetByteCount(type);
                size += 4 * fieldnames.Length;
                for (int i = 0; i < fieldnames.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(fieldnames[i]);
                }
                size += 4 * fieldtypes.Length;
                for (int i = 0; i < fieldtypes.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(fieldtypes[i]);
                }
                size += 4 * fieldarraylen.Length;
                size += 4 * examples.Length;
                for (int i = 0; i < examples.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(examples[i]);
                }
                size += 4 * constnames.Length;
                for (int i = 0; i < constnames.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(constnames[i]);
                }
                size += 4 * constvalues.Length;
                for (int i = 0; i < constvalues.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(constvalues[i]);
                }
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosapi/TypeDef";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "80597571d79bbeef6c9c4d98f30116a0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1coqSxI5SoGs6NjFdIyU3NS8hJzU4vRxEDKirky80qMjWBCiUVFiZU5qXkI" +
                "lakVibkFOch6k/PzikvQzAOLlSXmlAIFuQB/w6D2hgAAAA==";
                
    }
}
