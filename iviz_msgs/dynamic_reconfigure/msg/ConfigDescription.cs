/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ConfigDescription : IDeserializable<ConfigDescription>, IMessage
    {
        [DataMember (Name = "groups")] public Group[] Groups;
        [DataMember (Name = "max")] public Config Max;
        [DataMember (Name = "min")] public Config Min;
        [DataMember (Name = "dflt")] public Config Dflt;
    
        /// Constructor for empty message.
        public ConfigDescription()
        {
            Groups = System.Array.Empty<Group>();
            Max = new Config();
            Min = new Config();
            Dflt = new Config();
        }
        
        /// Explicit constructor.
        public ConfigDescription(Group[] Groups, Config Max, Config Min, Config Dflt)
        {
            this.Groups = Groups;
            this.Max = Max;
            this.Min = Min;
            this.Dflt = Dflt;
        }
        
        /// Constructor with buffer.
        internal ConfigDescription(ref Buffer b)
        {
            Groups = b.DeserializeArray<Group>();
            for (int i = 0; i < Groups.Length; i++)
            {
                Groups[i] = new Group(ref b);
            }
            Max = new Config(ref b);
            Min = new Config(ref b);
            Dflt = new Config(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ConfigDescription(ref b);
        
        ConfigDescription IDeserializable<ConfigDescription>.RosDeserialize(ref Buffer b) => new ConfigDescription(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Groups);
            Max.RosSerialize(ref b);
            Min.RosSerialize(ref b);
            Dflt.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Groups is null) throw new System.NullReferenceException(nameof(Groups));
            for (int i = 0; i < Groups.Length; i++)
            {
                if (Groups[i] is null) throw new System.NullReferenceException($"{nameof(Groups)}[{i}]");
                Groups[i].RosValidate();
            }
            if (Max is null) throw new System.NullReferenceException(nameof(Max));
            Max.RosValidate();
            if (Min is null) throw new System.NullReferenceException(nameof(Min));
            Min.RosValidate();
            if (Dflt is null) throw new System.NullReferenceException(nameof(Dflt));
            Dflt.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.GetArraySize(Groups);
                size += Max.RosMessageLength;
                size += Min.RosMessageLength;
                size += Dflt.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/ConfigDescription";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "757ce9d44ba8ddd801bb30bc456f946f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2UQWrDMBBF93MK3aCQli4C2bQG00Uh4GUIQbHGrkCWjDQKze0r2XUiJemuSBuPvhB+" +
                "f+aj2ho/7vasj18H70Z3smcD/76UUi+l6BQBbP55wWdTr5k4az7I9mCxnX7mLT7VkQkcWal7Fo5xqek8" +
                "Imy55UOFrrVyJGl0MDFGCQmtA6npeRUF1MR+d1KUpL/l+9OIn+kUnlAtB+L+HgpJh+DuyxS1Mc8e3oxR" +
                "26W9odXHsHfwoSkVgxEHDdlUC/QOKuOPClNZTIqDacYNccJrCAu6y2xlE4oG2YkrjyV50oZmOHNGivOk" +
                "w3wU4OJAVZ6kjKlThtPrS3moa4jvI+Qm+fIApe8SwA83YEEyfgUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
