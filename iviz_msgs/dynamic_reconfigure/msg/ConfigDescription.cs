/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/ConfigDescription")]
    public sealed class ConfigDescription : IDeserializable<ConfigDescription>, IMessage
    {
        [DataMember (Name = "groups")] public Group[] Groups { get; set; }
        [DataMember (Name = "max")] public Config Max { get; set; }
        [DataMember (Name = "min")] public Config Min { get; set; }
        [DataMember (Name = "dflt")] public Config Dflt { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ConfigDescription()
        {
            Groups = System.Array.Empty<Group>();
            Max = new Config();
            Min = new Config();
            Dflt = new Config();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ConfigDescription(Group[] Groups, Config Max, Config Min, Config Dflt)
        {
            this.Groups = Groups;
            this.Max = Max;
            this.Min = Min;
            this.Dflt = Dflt;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ConfigDescription(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ConfigDescription(ref b);
        }
        
        ConfigDescription IDeserializable<ConfigDescription>.RosDeserialize(ref Buffer b)
        {
            return new ConfigDescription(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Groups, 0);
            Max.RosSerialize(ref b);
            Min.RosSerialize(ref b);
            Dflt.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                foreach (var i in Groups)
                {
                    size += i.RosMessageLength;
                }
                size += Max.RosMessageLength;
                size += Min.RosMessageLength;
                size += Dflt.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/ConfigDescription";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "757ce9d44ba8ddd801bb30bc456f946f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72UQWrDMBBF93MK3aDQli4K2TQB00Uh4GUJQbHGrkCWjGYUmttXsuNETtpdkTYefSH8" +
                "/sxHlXdh+NyJLn0J1s62uhO9/L6U2s6lag0DrP55wUddvQp1srLXzd5jM/4seHyoEhMQe207EY9xrvk0" +
                "IGyll/0GqfF6YO1sNDEkCRk9gbb89JgEtCzOO61K0t/y/WkkTHQGj2jmA3V/D5XmfXT35YramGYPb86Z" +
                "7dze2OpD3BO8W87FaISgZp9rkZ5g48LBYC6rUSEYZ1yzZLyGsKC7ha3FhJJBcZQmYEmevKELnCkjxXny" +
                "Yf4W4OJAN0laMLXGSX55Lg91DfF9hGiULw9Q/i4B/AA3YEEyfgUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}