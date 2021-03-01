/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlannerInterfaceDescription")]
    public sealed class PlannerInterfaceDescription : IDeserializable<PlannerInterfaceDescription>, IMessage
    {
        // The name of the planner interface
        [DataMember (Name = "name")] public string Name { get; set; }
        // The names of the planner ids within the interface
        [DataMember (Name = "planner_ids")] public string[] PlannerIds { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlannerInterfaceDescription()
        {
            Name = string.Empty;
            PlannerIds = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlannerInterfaceDescription(string Name, string[] PlannerIds)
        {
            this.Name = Name;
            this.PlannerIds = PlannerIds;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlannerInterfaceDescription(ref Buffer b)
        {
            Name = b.DeserializeString();
            PlannerIds = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlannerInterfaceDescription(ref b);
        }
        
        PlannerInterfaceDescription IDeserializable<PlannerInterfaceDescription>.RosDeserialize(ref Buffer b)
        {
            return new PlannerInterfaceDescription(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.SerializeArray(PlannerIds, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (PlannerIds is null) throw new System.NullReferenceException(nameof(PlannerIds));
            for (int i = 0; i < PlannerIds.Length; i++)
            {
                if (PlannerIds[i] is null) throw new System.NullReferenceException($"{nameof(PlannerIds)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += 4 * PlannerIds.Length;
                foreach (string s in PlannerIds)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlannerInterfaceDescription";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ea5f6e6129aa1b110ccda9900d2bf25e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWCMlIVchLzE1VyE9TKAGyC3IS8/JSixQy80pSi9ISk1O5ikuKMvPSwYq4uJThGoox" +
                "dKQUK5RnlmRk5oGF0Q2IjoWpjAeq5OICAIb3GGd7AAAA";
                
    }
}
