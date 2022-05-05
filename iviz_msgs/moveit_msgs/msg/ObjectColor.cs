/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class ObjectColor : IDeserializable<ObjectColor>, IMessage
    {
        // The object id for which we specify color
        [DataMember (Name = "id")] public string Id;
        // The value of the color
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
    
        /// Constructor for empty message.
        public ObjectColor()
        {
            Id = "";
        }
        
        /// Explicit constructor.
        public ObjectColor(string Id, in StdMsgs.ColorRGBA Color)
        {
            this.Id = Id;
            this.Color = Color;
        }
        
        /// Constructor with buffer.
        public ObjectColor(ref ReadBuffer b)
        {
            b.DeserializeString(out Id);
            b.Deserialize(out Color);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectColor(ref b);
        
        public ObjectColor RosDeserialize(ref ReadBuffer b) => new ObjectColor(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(in Color);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 20 + BuiltIns.GetStringSize(Id);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/ObjectColor";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "ec3bd6f103430e64b2ae706a67d8488e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NWCMlIVchPykpNLlHITFFIyy9SKM/ITM5QKE9VKC5ITc5Mq1RIzs/JL+IqLinKzEsH" +
                "KuLiUgbrKkvMKQXqTVMoAXJgalLic4vTi/WdQdwgdydHqASXLZUBl2+wu5UCpn1caTn5iSXGRgpFcFY6" +
                "nJUEZyVycQEAkGygffsAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
