/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class GroupState : IDeserializable<GroupState>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "state")] public bool State;
        [DataMember (Name = "id")] public int Id;
        [DataMember (Name = "parent")] public int Parent;
    
        /// Constructor for empty message.
        public GroupState()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public GroupState(string Name, bool State, int Id, int Parent)
        {
            this.Name = Name;
            this.State = State;
            this.Id = Id;
            this.Parent = Parent;
        }
        
        /// Constructor with buffer.
        public GroupState(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out State);
            b.Deserialize(out Id);
            b.Deserialize(out Parent);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GroupState(ref b);
        
        public GroupState RosDeserialize(ref ReadBuffer b) => new GroupState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(State);
            b.Serialize(Id);
            b.Serialize(Parent);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 13 + BuiltIns.GetStringSize(Name);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/GroupState";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a2d87f51dc22930325041a2f8b1571f8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5UrKz89RKC5JLEnlyswrMTZSyEyBMgoSi1LzSri4AH76Q7IuAAAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
