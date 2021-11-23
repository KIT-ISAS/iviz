/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LinkPadding : IDeserializable<LinkPadding>, IMessage
    {
        //name for the link
        [DataMember (Name = "link_name")] public string LinkName;
        // padding to apply to the link
        [DataMember (Name = "padding")] public double Padding;
    
        /// Constructor for empty message.
        public LinkPadding()
        {
            LinkName = string.Empty;
        }
        
        /// Explicit constructor.
        public LinkPadding(string LinkName, double Padding)
        {
            this.LinkName = LinkName;
            this.Padding = Padding;
        }
        
        /// Constructor with buffer.
        internal LinkPadding(ref Buffer b)
        {
            LinkName = b.DeserializeString();
            Padding = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LinkPadding(ref b);
        
        LinkPadding IDeserializable<LinkPadding>.RosDeserialize(ref Buffer b) => new LinkPadding(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(LinkName);
            b.Serialize(Padding);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(LinkName);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/LinkPadding";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b3ea75670df55c696fedee97774d5947";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1POS8xNVUjLL1IoyUhVyMnMy+YqLinKzEsHs+NBslxcygoFiSkpIMGSfIXEgoKcShAD" +
                "riEtJz+xxMwEpoiLCwCaqbVAVQAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
