/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TypeDef : IDeserializable<TypeDef>, IMessage
    {
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "fieldnames")] public string[] Fieldnames;
        [DataMember (Name = "fieldtypes")] public string[] Fieldtypes;
        [DataMember (Name = "fieldarraylen")] public int[] Fieldarraylen;
        [DataMember (Name = "examples")] public string[] Examples;
        [DataMember (Name = "constnames")] public string[] Constnames;
        [DataMember (Name = "constvalues")] public string[] Constvalues;
    
        /// Constructor for empty message.
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
        
        /// Constructor with buffer.
        public TypeDef(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
            Fieldnames = b.DeserializeStringArray();
            Fieldtypes = b.DeserializeStringArray();
            Fieldarraylen = b.DeserializeStructArray<int>();
            Examples = b.DeserializeStringArray();
            Constnames = b.DeserializeStringArray();
            Constvalues = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TypeDef(ref b);
        
        public TypeDef RosDeserialize(ref ReadBuffer b) => new TypeDef(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
            b.SerializeArray(Fieldnames);
            b.SerializeArray(Fieldtypes);
            b.SerializeStructArray(Fieldarraylen);
            b.SerializeArray(Examples);
            b.SerializeArray(Constnames);
            b.SerializeArray(Constvalues);
        }
        
        public void RosValidate()
        {
            if (Type is null) BuiltIns.ThrowNullReference(nameof(Type));
            if (Fieldnames is null) BuiltIns.ThrowNullReference(nameof(Fieldnames));
            for (int i = 0; i < Fieldnames.Length; i++)
            {
                if (Fieldnames[i] is null) BuiltIns.ThrowNullReference($"{nameof(Fieldnames)}[{i}]");
            }
            if (Fieldtypes is null) BuiltIns.ThrowNullReference(nameof(Fieldtypes));
            for (int i = 0; i < Fieldtypes.Length; i++)
            {
                if (Fieldtypes[i] is null) BuiltIns.ThrowNullReference($"{nameof(Fieldtypes)}[{i}]");
            }
            if (Fieldarraylen is null) BuiltIns.ThrowNullReference(nameof(Fieldarraylen));
            if (Examples is null) BuiltIns.ThrowNullReference(nameof(Examples));
            for (int i = 0; i < Examples.Length; i++)
            {
                if (Examples[i] is null) BuiltIns.ThrowNullReference($"{nameof(Examples)}[{i}]");
            }
            if (Constnames is null) BuiltIns.ThrowNullReference(nameof(Constnames));
            for (int i = 0; i < Constnames.Length; i++)
            {
                if (Constnames[i] is null) BuiltIns.ThrowNullReference($"{nameof(Constnames)}[{i}]");
            }
            if (Constvalues is null) BuiltIns.ThrowNullReference(nameof(Constvalues));
            for (int i = 0; i < Constvalues.Length; i++)
            {
                if (Constvalues[i] is null) BuiltIns.ThrowNullReference($"{nameof(Constvalues)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 28;
                size += BuiltIns.GetStringSize(Type);
                size += BuiltIns.GetArraySize(Fieldnames);
                size += BuiltIns.GetArraySize(Fieldtypes);
                size += 4 * Fieldarraylen.Length;
                size += BuiltIns.GetArraySize(Examples);
                size += BuiltIns.GetArraySize(Constnames);
                size += BuiltIns.GetArraySize(Constvalues);
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
                "H4sIAAAAAAAAEysuKcrMS1coqSxI5SoGs6NjFdIyU3NS8hJzU4vRxEDKirky80qMjWBCiUVFiZU5qXkI" +
                "lakVibkFOch6k/PzikvQzAOLlSXmlAIFuQB/w6D2hgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
