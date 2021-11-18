/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/GroupState")]
    public sealed class GroupState : IDeserializable<GroupState>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "state")] public bool State;
        [DataMember (Name = "id")] public int Id;
        [DataMember (Name = "parent")] public int Parent;
    
        /// <summary> Constructor for empty message. </summary>
        public GroupState()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GroupState(string Name, bool State, int Id, int Parent)
        {
            this.Name = Name;
            this.State = State;
            this.Id = Id;
            this.Parent = Parent;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GroupState(ref Buffer b)
        {
            Name = b.DeserializeString();
            State = b.Deserialize<bool>();
            Id = b.Deserialize<int>();
            Parent = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GroupState(ref b);
        }
        
        GroupState IDeserializable<GroupState>.RosDeserialize(ref Buffer b)
        {
            return new GroupState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(State);
            b.Serialize(Id);
            b.Serialize(Parent);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 13 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/GroupState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a2d87f51dc22930325041a2f8b1571f8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1fIS8xN5UrKz89RKC5JLEnlyswrMTZSyEyBMgoSi1LzSrh4uQCVj5nKLwAA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
