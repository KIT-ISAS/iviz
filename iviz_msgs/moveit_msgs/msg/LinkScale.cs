/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class LinkScale : IDeserializable<LinkScale>, IMessage
    {
        //name for the link
        [DataMember (Name = "link_name")] public string LinkName;
        // scaling to apply to the link
        [DataMember (Name = "scale")] public double Scale;
    
        /// Constructor for empty message.
        public LinkScale()
        {
            LinkName = "";
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
            b.DeserializeString(out LinkName);
            b.Deserialize(out Scale);
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
            if (LinkName is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(LinkName);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/LinkScale";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "19faf226446bfb2f645a4da6f2a56166";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1POS8xNVUjLL1IoyUhVyMnMy+YqLinKzEsHs+NBslxcygrFyYk5IMGSfIXEgoKcShAD" +
                "riEtJz+xxMwErAioGgCWgqioUwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
