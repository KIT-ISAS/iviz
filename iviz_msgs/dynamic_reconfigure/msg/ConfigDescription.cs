/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/ConfigDescription")]
    public sealed class ConfigDescription : IDeserializable<ConfigDescription>, IMessage
    {
        [DataMember (Name = "groups")] public Group[] Groups;
        [DataMember (Name = "max")] public Config Max;
        [DataMember (Name = "min")] public Config Min;
        [DataMember (Name = "dflt")] public Config Dflt;
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/ConfigDescription";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "757ce9d44ba8ddd801bb30bc456f946f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsWUwWrEIBCG7wN5B99goS09LPTSBkIPCws5lrK4cZIKRoOOS/ftq0nTaNneSuIl4y+S" +
                "75/5sbLGD2/vrItfBy9Gt7JjPf/8KaWeS9EqggKe/nkVcKirPRNXzXvZnCw24++8xV0VqQpwZKXuWDjH" +
                "uabrgHDklvclusbKgaTRwccQJSS0DqSm+7sooCb2vZNiXQO/Cf/24idAhRdU84FYLs4SCkmnYPDDrOxk" +
                "ykABz8ao49zk0PBz2Dt41ZSKwYuDmmyqBQMOSuPPClNZjIqDcdQ1ccIljasazIzlc4oe2YUrj+sipV3N" +
                "iaawbICUDvVmmDdgKvNQ5VitMpweH7bgWiJ9I04u6surlD5WgfILw41+cpcFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
