using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class TypeDef : IMessage
    {
        [DataMember] public string type { get; set; }
        [DataMember] public string[] fieldnames { get; set; }
        [DataMember] public string[] fieldtypes { get; set; }
        [DataMember] public int[] fieldarraylen { get; set; }
        [DataMember] public string[] examples { get; set; }
        [DataMember] public string[] constnames { get; set; }
        [DataMember] public string[] constvalues { get; set; }
    
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
            this.fieldnames = b.DeserializeStringArray();
            this.fieldtypes = b.DeserializeStringArray();
            this.fieldarraylen = b.DeserializeStructArray<int>();
            this.examples = b.DeserializeStringArray();
            this.constnames = b.DeserializeStringArray();
            this.constvalues = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TypeDef(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
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
            for (int i = 0; i < fieldnames.Length; i++)
            {
                if (fieldnames[i] is null) throw new System.NullReferenceException();
            }
            if (fieldtypes is null) throw new System.NullReferenceException();
            for (int i = 0; i < fieldtypes.Length; i++)
            {
                if (fieldtypes[i] is null) throw new System.NullReferenceException();
            }
            if (fieldarraylen is null) throw new System.NullReferenceException();
            if (examples is null) throw new System.NullReferenceException();
            for (int i = 0; i < examples.Length; i++)
            {
                if (examples[i] is null) throw new System.NullReferenceException();
            }
            if (constnames is null) throw new System.NullReferenceException();
            for (int i = 0; i < constnames.Length; i++)
            {
                if (constnames[i] is null) throw new System.NullReferenceException();
            }
            if (constvalues is null) throw new System.NullReferenceException();
            for (int i = 0; i < constvalues.Length; i++)
            {
                if (constvalues[i] is null) throw new System.NullReferenceException();
            }
        }
    
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
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosapi/TypeDef";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "80597571d79bbeef6c9c4d98f30116a0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1coqSxI5SoGs6NjFdIyU3NS8hJzU4vRxEDKirky80qMjWBCiUVFiZU5qXkI" +
                "lakVibkFOch6k/PzikvQzAOLlSXmlAIFuQB/w6D2hgAAAA==";
                
    }
}
