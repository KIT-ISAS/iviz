/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/AllowedCollisionMatrix")]
    public sealed class AllowedCollisionMatrix : IDeserializable<AllowedCollisionMatrix>, IMessage
    {
        // The list of entry names in the matrix
        [DataMember (Name = "entry_names")] public string[] EntryNames { get; set; }
        // The individual entries in the allowed collision matrix
        // square, symmetric, with same order as entry_names
        [DataMember (Name = "entry_values")] public AllowedCollisionEntry[] EntryValues { get; set; }
        // In addition to the collision matrix itself, we also have 
        // the default entry value for each entry name.
        // If the allowed collision flag is queried for a pair of names (n1, n2)
        // that is not found in the collision matrix itself, the value of
        // the collision flag is considered to be that of the entry (n1 or n2)
        // specified in the list below. If both n1 and n2 are found in the list 
        // of defaults, the result is computed with an AND operation
        [DataMember (Name = "default_entry_names")] public string[] DefaultEntryNames { get; set; }
        [DataMember (Name = "default_entry_values")] public bool[] DefaultEntryValues { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AllowedCollisionMatrix()
        {
            EntryNames = System.Array.Empty<string>();
            EntryValues = System.Array.Empty<AllowedCollisionEntry>();
            DefaultEntryNames = System.Array.Empty<string>();
            DefaultEntryValues = System.Array.Empty<bool>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AllowedCollisionMatrix(string[] EntryNames, AllowedCollisionEntry[] EntryValues, string[] DefaultEntryNames, bool[] DefaultEntryValues)
        {
            this.EntryNames = EntryNames;
            this.EntryValues = EntryValues;
            this.DefaultEntryNames = DefaultEntryNames;
            this.DefaultEntryValues = DefaultEntryValues;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AllowedCollisionMatrix(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AllowedCollisionMatrix(ref b);
        }
        
        AllowedCollisionMatrix IDeserializable<AllowedCollisionMatrix>.RosDeserialize(ref Buffer b)
        {
            return new AllowedCollisionMatrix(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(EntryNames, 0);
            b.SerializeArray(EntryValues, 0);
            b.SerializeArray(DefaultEntryNames, 0);
            b.SerializeStructArray(DefaultEntryValues, 0);
        }
        
        public void Dispose()
        {
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
                size += 4 * EntryNames.Length;
                foreach (string s in EntryNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                foreach (var i in EntryValues)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * DefaultEntryNames.Length;
                foreach (string s in DefaultEntryNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 1 * DefaultEntryValues.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/AllowedCollisionMatrix";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aedce13587eef0d79165a075659c1879";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62SQU/DMAyF75X2HyztAtI0xI5IHCZAiANc4IbQ5DbuGpHGW5Ju7N/jJG1hDG7sMm12" +
                "3vv87Cm8NARG+wBcA9ngDmCxJQ/aQpBSi8Hpj0nh5cuuX99yzyr1TIpJMU0C2iq906pDk+r66z0aw3tS" +
                "ULERF812VJyC33boaAb+0LYkf1Yz2OvQgBdxYKfIAfpjw2WWuxnU7mJxpNqh6QasBwuolA7RMnBi+ckA" +
                "OngytbhGTs/Q4I4gPo7dimrsTOhDSdJQswPCqvmW1Ly3q/8Ytza4Bu1h25HkopIEwga1i4nnrM/s5Qzs" +
                "4jxbY4j9loP0dlYNSf5JH4sZj+sB/tS/Yuu1RCoIEkdJ2Ygzdh5HMCT2AcRvqNJ1RO4B0pWUJAPO47gl" +
                "y6rkBQqiXYBs8pg3tUch8eiz9BnWkY/BJqh20wWxSHtHC8unW+ANOYx7i8mOd9dLrI7OoWQ2J7XhCorr" +
                "f/4Uj8/3V9DyjnRYtX7tL349Rxl535AM6lKassevbVQNVe8yUByeLJaG1DjF+Lv4BKXOXzeWAwAA";
                
    }
}
