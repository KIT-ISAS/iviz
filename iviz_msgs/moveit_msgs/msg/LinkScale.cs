/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LinkScale : IDeserializable<LinkScale>, IMessage
    {
        //name for the link
        [DataMember (Name = "link_name")] public string LinkName;
        // scaling to apply to the link
        [DataMember (Name = "scale")] public double Scale;
    
        /// Constructor for empty message.
        public LinkScale()
        {
            LinkName = string.Empty;
        }
        
        /// Explicit constructor.
        public LinkScale(string LinkName, double Scale)
        {
            this.LinkName = LinkName;
            this.Scale = Scale;
        }
        
        /// Constructor with buffer.
        public LinkScale(ref ReadBuffer b)
        {
            LinkName = b.DeserializeString();
            Scale = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LinkScale(ref b);
        
        public LinkScale RosDeserialize(ref ReadBuffer b) => new LinkScale(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(LinkName);
            b.Serialize(Scale);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(LinkName);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/LinkScale";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "19faf226446bfb2f645a4da6f2a56166";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1POS8xNVUjLL1IoyUhVyMnMy+YqLinKzEsHs+NBslxcygrFyYk5IMGSfIXEgoKcShAD" +
                "riEtJz+xxMwErAioGgCWgqioUwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
