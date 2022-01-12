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
        public ConfigDescription(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ConfigDescription(ref b);
        
        public ConfigDescription RosDeserialize(ref ReadBuffer b) => new ConfigDescription(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
                "H4sIAAAAAAAAE72UQWrDMBBF93MK3aDQli4K2TQB00Uh4GUJQbHGrkCWjGYUmttXsuNETtpdkTYefSH8" +
                "/sxHlXdh+NyJLn0J1s62uhO9/L6U2s6lag0DrP55wUddvQp1srLXzd5jM/4seHyoEhMQe207EY9xrvk0" +
                "IGyll/0GqfF6YO1sNDEkCRk9gbb89JgEtCzOO61K0t/y/WkkTHQGj2jmA3V/D5XmfXT35YramGYPb86Z" +
                "7dze2OpD3BO8W87FaISgZp9rkZ5g48LBYC6rUSEYZ1yzZLyGsKC7ha3FhJJBcZQmYEmevKELnCkjxXny" +
                "Yf4W4OJAN0laMLXGSX55Lg91DfF9hGiULw9Q/i4B/AA3YEEyfgUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
