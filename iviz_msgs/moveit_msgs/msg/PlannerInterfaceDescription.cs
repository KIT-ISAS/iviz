/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class PlannerInterfaceDescription : IDeserializable<PlannerInterfaceDescription>, IMessage
    {
        // The name of the planner interface
        [DataMember (Name = "name")] public string Name;
        // The names of the planner ids within the interface
        [DataMember (Name = "planner_ids")] public string[] PlannerIds;
    
        /// Constructor for empty message.
        public PlannerInterfaceDescription()
        {
            Name = "";
            PlannerIds = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public PlannerInterfaceDescription(string Name, string[] PlannerIds)
        {
            this.Name = Name;
            this.PlannerIds = PlannerIds;
        }
        
        /// Constructor with buffer.
        public PlannerInterfaceDescription(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeStringArray(out PlannerIds);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlannerInterfaceDescription(ref b);
        
        public PlannerInterfaceDescription RosDeserialize(ref ReadBuffer b) => new PlannerInterfaceDescription(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.SerializeArray(PlannerIds);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (PlannerIds is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < PlannerIds.Length; i++)
            {
                if (PlannerIds[i] is null) BuiltIns.ThrowNullReference(nameof(PlannerIds), i);
            }
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetArraySize(PlannerIds);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/PlannerInterfaceDescription";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ea5f6e6129aa1b110ccda9900d2bf25e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NWCMlIVchLzE1VyE9TKAGyC3IS8/JSixQy80pSi9ISk1O5ikuKMvPSwYq4uJThGoox" +
                "dKQUK5RnlmRk5oGF0Q2IjoWpjAeq5OICAIb3GGd7AAAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
