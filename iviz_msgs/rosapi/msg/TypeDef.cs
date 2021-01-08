/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [Preserve, DataContract (Name = "rosapi/TypeDef")]
    public sealed class TypeDef : IDeserializable<TypeDef>, IMessage
    {
        [DataMember (Name = "type")] public string Type { get; set; }
        [DataMember (Name = "fieldnames")] public string[] Fieldnames { get; set; }
        [DataMember (Name = "fieldtypes")] public string[] Fieldtypes { get; set; }
        [DataMember (Name = "fieldarraylen")] public int[] Fieldarraylen { get; set; }
        [DataMember (Name = "examples")] public string[] Examples { get; set; }
        [DataMember (Name = "constnames")] public string[] Constnames { get; set; }
        [DataMember (Name = "constvalues")] public string[] Constvalues { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TypeDef()
        {
            Type = "";
            Fieldnames = System.Array.Empty<string>();
            Fieldtypes = System.Array.Empty<string>();
            Fieldarraylen = System.Array.Empty<int>();
            Examples = System.Array.Empty<string>();
            Constnames = System.Array.Empty<string>();
            Constvalues = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TypeDef(string Type, string[] Fieldnames, string[] Fieldtypes, int[] Fieldarraylen, string[] Examples, string[] Constnames, string[] Constvalues)
        {
            this.Type = Type;
            this.Fieldnames = Fieldnames;
            this.Fieldtypes = Fieldtypes;
            this.Fieldarraylen = Fieldarraylen;
            this.Examples = Examples;
            this.Constnames = Constnames;
            this.Constvalues = Constvalues;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TypeDef(ref Buffer b)
        {
            Type = b.DeserializeString();
            Fieldnames = b.DeserializeStringArray();
            Fieldtypes = b.DeserializeStringArray();
            Fieldarraylen = b.DeserializeStructArray<int>();
            Examples = b.DeserializeStringArray();
            Constnames = b.DeserializeStringArray();
            Constvalues = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TypeDef(ref b);
        }
        
        TypeDef IDeserializable<TypeDef>.RosDeserialize(ref Buffer b)
        {
            return new TypeDef(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Type);
            b.SerializeArray(Fieldnames, 0);
            b.SerializeArray(Fieldtypes, 0);
            b.SerializeStructArray(Fieldarraylen, 0);
            b.SerializeArray(Examples, 0);
            b.SerializeArray(Constnames, 0);
            b.SerializeArray(Constvalues, 0);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
            if (Fieldnames is null) throw new System.NullReferenceException(nameof(Fieldnames));
            for (int i = 0; i < Fieldnames.Length; i++)
            {
                if (Fieldnames[i] is null) throw new System.NullReferenceException($"{nameof(Fieldnames)}[{i}]");
            }
            if (Fieldtypes is null) throw new System.NullReferenceException(nameof(Fieldtypes));
            for (int i = 0; i < Fieldtypes.Length; i++)
            {
                if (Fieldtypes[i] is null) throw new System.NullReferenceException($"{nameof(Fieldtypes)}[{i}]");
            }
            if (Fieldarraylen is null) throw new System.NullReferenceException(nameof(Fieldarraylen));
            if (Examples is null) throw new System.NullReferenceException(nameof(Examples));
            for (int i = 0; i < Examples.Length; i++)
            {
                if (Examples[i] is null) throw new System.NullReferenceException($"{nameof(Examples)}[{i}]");
            }
            if (Constnames is null) throw new System.NullReferenceException(nameof(Constnames));
            for (int i = 0; i < Constnames.Length; i++)
            {
                if (Constnames[i] is null) throw new System.NullReferenceException($"{nameof(Constnames)}[{i}]");
            }
            if (Constvalues is null) throw new System.NullReferenceException(nameof(Constvalues));
            for (int i = 0; i < Constvalues.Length; i++)
            {
                if (Constvalues[i] is null) throw new System.NullReferenceException($"{nameof(Constvalues)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 28;
                size += BuiltIns.UTF8.GetByteCount(Type);
                size += 4 * Fieldnames.Length;
                foreach (string s in Fieldnames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * Fieldtypes.Length;
                foreach (string s in Fieldtypes)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * Fieldarraylen.Length;
                size += 4 * Examples.Length;
                foreach (string s in Examples)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * Constnames.Length;
                foreach (string s in Constnames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * Constvalues.Length;
                foreach (string s in Constvalues)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosapi/TypeDef";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "80597571d79bbeef6c9c4d98f30116a0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1coqSxI5eUqBnOiYxXSMlNzUvISc1OL0QVBCoGCmXklxkYwscSiosTKnNQ8" +
                "JLWpFYm5BTko2pPz84pL0M0EC5Yl5pSCRHm5ADhUyV2OAAAA";
                
    }
}
