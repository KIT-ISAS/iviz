/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ObjectColor")]
    public sealed class ObjectColor : IDeserializable<ObjectColor>, IMessage
    {
        // The object id for which we specify color
        [DataMember (Name = "id")] public string Id { get; set; }
        // The value of the color
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectColor()
        {
            Id = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectColor(string Id, in StdMsgs.ColorRGBA Color)
        {
            this.Id = Id;
            this.Color = Color;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectColor(ref Buffer b)
        {
            Id = b.DeserializeString();
            Color = new StdMsgs.ColorRGBA(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectColor(ref b);
        }
        
        ObjectColor IDeserializable<ObjectColor>.RosDeserialize(ref Buffer b)
        {
            return new ObjectColor(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            Color.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ObjectColor";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ec3bd6f103430e64b2ae706a67d8488e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWCMlIVchPykpNLlHITFFIyy9SKM/ITM5QKE9VKC5ITc5Mq1RIzs/JL+IqLinKzEsH" +
                "KuLiUgbrKkvMKQXqTVMoAXJgalLic4vTi/WdQdwgdydHqASXLZUBl2+wu5UCpn1caTn5iSXGRgpFcFY6" +
                "nJUEZyVycQEAkGygffsAAAA=";
                
    }
}
