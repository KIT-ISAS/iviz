/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AllowedCollisionMatrix : IDeserializable<AllowedCollisionMatrix>, IMessage
    {
        // The list of entry names in the matrix
        [DataMember (Name = "entry_names")] public string[] EntryNames;
        // The individual entries in the allowed collision matrix
        // square, symmetric, with same order as entry_names
        [DataMember (Name = "entry_values")] public AllowedCollisionEntry[] EntryValues;
        // In addition to the collision matrix itself, we also have 
        // the default entry value for each entry name.
        // If the allowed collision flag is queried for a pair of names (n1, n2)
        // that is not found in the collision matrix itself, the value of
        // the collision flag is considered to be that of the entry (n1 or n2)
        // specified in the list below. If both n1 and n2 are found in the list 
        // of defaults, the result is computed with an AND operation
        [DataMember (Name = "default_entry_names")] public string[] DefaultEntryNames;
        [DataMember (Name = "default_entry_values")] public bool[] DefaultEntryValues;
    
        /// Constructor for empty message.
        public AllowedCollisionMatrix()
        {
            EntryNames = System.Array.Empty<string>();
            EntryValues = System.Array.Empty<AllowedCollisionEntry>();
            DefaultEntryNames = System.Array.Empty<string>();
            DefaultEntryValues = System.Array.Empty<bool>();
        }
        
        /// Explicit constructor.
        public AllowedCollisionMatrix(string[] EntryNames, AllowedCollisionEntry[] EntryValues, string[] DefaultEntryNames, bool[] DefaultEntryValues)
        {
            this.EntryNames = EntryNames;
            this.EntryValues = EntryValues;
            this.DefaultEntryNames = DefaultEntryNames;
            this.DefaultEntryValues = DefaultEntryValues;
        }
        
        /// Constructor with buffer.
        internal AllowedCollisionMatrix(ref Buffer b)
        {
            EntryNames = b.DeserializeStringArray();
            EntryValues = b.DeserializeArray<AllowedCollisionEntry>();
            for (int i = 0; i < EntryValues.Length; i++)
            {
                EntryValues[i] = new AllowedCollisionEntry(ref b);
            }
            DefaultEntryNames = b.DeserializeStringArray();
            DefaultEntryValues = b.DeserializeStructArray<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AllowedCollisionMatrix(ref b);
        
        AllowedCollisionMatrix IDeserializable<AllowedCollisionMatrix>.RosDeserialize(ref Buffer b) => new AllowedCollisionMatrix(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(EntryNames);
            b.SerializeArray(EntryValues);
            b.SerializeArray(DefaultEntryNames);
            b.SerializeStructArray(DefaultEntryValues);
        }
        
        public void RosValidate()
        {
            if (EntryNames is null) throw new System.NullReferenceException(nameof(EntryNames));
            for (int i = 0; i < EntryNames.Length; i++)
            {
                if (EntryNames[i] is null) throw new System.NullReferenceException($"{nameof(EntryNames)}[{i}]");
            }
            if (EntryValues is null) throw new System.NullReferenceException(nameof(EntryValues));
            for (int i = 0; i < EntryValues.Length; i++)
            {
                if (EntryValues[i] is null) throw new System.NullReferenceException($"{nameof(EntryValues)}[{i}]");
                EntryValues[i].RosValidate();
            }
            if (DefaultEntryNames is null) throw new System.NullReferenceException(nameof(DefaultEntryNames));
            for (int i = 0; i < DefaultEntryNames.Length; i++)
            {
                if (DefaultEntryNames[i] is null) throw new System.NullReferenceException($"{nameof(DefaultEntryNames)}[{i}]");
            }
            if (DefaultEntryValues is null) throw new System.NullReferenceException(nameof(DefaultEntryValues));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetArraySize(EntryNames);
                size += BuiltIns.GetArraySize(EntryValues);
                size += BuiltIns.GetArraySize(DefaultEntryNames);
                size += DefaultEntryValues.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/AllowedCollisionMatrix";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aedce13587eef0d79165a075659c1879";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1SzU7DMAy+9yks7QLSNMSOSBwmQIgDXOCG0OQ27hqRxl2SbuztcZKWbYzd6KFSa/v7" +
                "syfw1hAY7QNwDWSD24HFljxoC0FKLQanvwovb7t6/8gty9RSFJM0ra3SG616NKmq98NoDG9JQcVGKDTb" +
                "EW4Cft2joyn4XduS/KumsNWhAS/IwE6RA/RHbIsMdjdiPcTaj6INmj5LerKASukQ6QInHb/5QQdPphbK" +
                "qNEzNLghkNnYrKjG3oQhjIQLNTsgrJqDhGaJqz7jsza4Au1h3ZMEohIAQofaxZxzwhf2egp2fpl4McR2" +
                "y0Fae6vGBM8qj8WsjetB+Cl7xdZriVIESBIlZR4RENuzFREhcQ8yfEeVrqPegT4dRknibha9liwbkgEU" +
                "gXYOssBjtaldcIRhSNFnpY58jDRJars+CEPaNlpYvNwDd+QwLqzYH9oAsDw8gZLZnJTGzd/+81M8vz7e" +
                "QMsb0mHZ+pW/+vMCxe62ITEpm5UcZYH7PVQNVZ9iJxoni6UhNXoYP4tvBP9IgYIDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
