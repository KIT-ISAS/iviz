namespace Iviz.Msgs.rosapi
{
    public sealed class TypeDef : IMessage
    {
        public string type;
        public string[] fieldnames;
        public string[] fieldtypes;
        public int[] fieldarraylen;
        public string[] examples;
        public string[] constnames;
        public string[] constvalues;
    
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
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out type, ref ptr, end);
            BuiltIns.Deserialize(out fieldnames, ref ptr, end, 0);
            BuiltIns.Deserialize(out fieldtypes, ref ptr, end, 0);
            BuiltIns.Deserialize(out fieldarraylen, ref ptr, end, 0);
            BuiltIns.Deserialize(out examples, ref ptr, end, 0);
            BuiltIns.Deserialize(out constnames, ref ptr, end, 0);
            BuiltIns.Deserialize(out constvalues, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(type, ref ptr, end);
            BuiltIns.Serialize(fieldnames, ref ptr, end, 0);
            BuiltIns.Serialize(fieldtypes, ref ptr, end, 0);
            BuiltIns.Serialize(fieldarraylen, ref ptr, end, 0);
            BuiltIns.Serialize(examples, ref ptr, end, 0);
            BuiltIns.Serialize(constnames, ref ptr, end, 0);
            BuiltIns.Serialize(constvalues, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 48;
            size += type.Length;
            for (int i = 0; i < fieldnames.Length; i++)
            {
                size += fieldnames[i].Length;
            }
            for (int i = 0; i < fieldtypes.Length; i++)
            {
                size += fieldtypes[i].Length;
            }
            size += 4 * fieldarraylen.Length;
            for (int i = 0; i < examples.Length; i++)
            {
                size += examples[i].Length;
            }
            for (int i = 0; i < constnames.Length; i++)
            {
                size += constnames[i].Length;
            }
            for (int i = 0; i < constvalues.Length; i++)
            {
                size += constvalues[i].Length;
            }
            return size;
        }
    
        public IMessage Create() => new TypeDef();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "rosapi/TypeDef";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "80597571d79bbeef6c9c4d98f30116a0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1coqSxI5SoGs6NjFdIyU3NS8hJzU4vRxEDKirky80qMjWBCiUVFiZU5qXkI" +
                "lakVibkFOch6k/PzikvQzAOLlSXmlAIFuQB/w6D2hgAAAA==";
                
    }
}
