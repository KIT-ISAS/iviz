/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/LinkScale")]
    public sealed class LinkScale : IDeserializable<LinkScale>, IMessage
    {
        //name for the link
        [DataMember (Name = "link_name")] public string LinkName { get; set; }
        // scaling to apply to the link
        [DataMember (Name = "scale")] public double Scale { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LinkScale()
        {
            LinkName = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public LinkScale(string LinkName, double Scale)
        {
            this.LinkName = LinkName;
            this.Scale = Scale;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LinkScale(ref Buffer b)
        {
            LinkName = b.DeserializeString();
            Scale = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LinkScale(ref b);
        }
        
        LinkScale IDeserializable<LinkScale>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(LinkName);
            b.Serialize(Scale);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(LinkName);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/LinkScale";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "19faf226446bfb2f645a4da6f2a56166";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1POS8xNVUjLL1IoyUhVyMnMy+YqLinKzEsHs+NBslxcygrFyYk5IMGSfIXEgoKcShAD" +
                "riEtJz+xxMwErAioGgCWgqioUwAAAA==";
                
    }
}
