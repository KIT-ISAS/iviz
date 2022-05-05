/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class LinkPadding : IDeserializable<LinkPadding>, IMessage
    {
        //name for the link
        [DataMember (Name = "link_name")] public string LinkName;
        // padding to apply to the link
        [DataMember (Name = "padding")] public double Padding;
    
        /// Constructor for empty message.
        public LinkPadding()
        {
            LinkName = "";
        }
        
        /// Explicit constructor.
        public LinkPadding(string LinkName, double Padding)
        {
            this.LinkName = LinkName;
            this.Padding = Padding;
        }
        
        /// Constructor with buffer.
        public LinkPadding(ref ReadBuffer b)
        {
            b.DeserializeString(out LinkName);
            b.Deserialize(out Padding);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LinkPadding(ref b);
        
        public LinkPadding RosDeserialize(ref ReadBuffer b) => new LinkPadding(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(LinkName);
            b.Serialize(Padding);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(LinkName);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/LinkPadding";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "b3ea75670df55c696fedee97774d5947";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1POS8xNVUjLL1IoyUhVyMnMy+YqLinKzEsHs+NBslxcygoFiSkpIMGSfIXEgoKcShAD" +
                "riEtJz+xxMwEpoiLCwCaqbVAVQAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
