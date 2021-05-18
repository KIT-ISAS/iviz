/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Widget")]
    public sealed class Widget : IDeserializable<Widget>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "action")] public byte Action { get; set; }
        [DataMember (Name = "id")] public string Id { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "main_color")] public StdMsgs.ColorRGBA MainColor { get; set; }
        [DataMember (Name = "secondary_color")] public StdMsgs.ColorRGBA SecondaryColor { get; set; }
        [DataMember (Name = "scale")] public double Scale { get; set; }
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        [DataMember (Name = "caption")] public string Caption { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Widget()
        {
            Id = string.Empty;
            Caption = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Widget(in StdMsgs.Header Header, byte Action, string Id, byte Type, in StdMsgs.ColorRGBA MainColor, in StdMsgs.ColorRGBA SecondaryColor, double Scale, in GeometryMsgs.Pose Pose, string Caption)
        {
            this.Header = Header;
            this.Action = Action;
            this.Id = Id;
            this.Type = Type;
            this.MainColor = MainColor;
            this.SecondaryColor = SecondaryColor;
            this.Scale = Scale;
            this.Pose = Pose;
            this.Caption = Caption;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Widget(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Action = b.Deserialize<byte>();
            Id = b.DeserializeString();
            Type = b.Deserialize<byte>();
            MainColor = new StdMsgs.ColorRGBA(ref b);
            SecondaryColor = new StdMsgs.ColorRGBA(ref b);
            Scale = b.Deserialize<double>();
            Pose = new GeometryMsgs.Pose(ref b);
            Caption = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Widget(ref b);
        }
        
        Widget IDeserializable<Widget>.RosDeserialize(ref Buffer b)
        {
            return new Widget(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            MainColor.RosSerialize(ref b);
            SecondaryColor.RosSerialize(ref b);
            b.Serialize(Scale);
            Pose.RosSerialize(ref b);
            b.Serialize(Caption);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Caption is null) throw new System.NullReferenceException(nameof(Caption));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 106;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += BuiltIns.UTF8.GetByteCount(Caption);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Widget";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3b0c5b861de617f50925ad80644446b3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UwW7bMAy96ysI9NB2WDJgG3YIsEO3Yl0PA7K194CRGVuALLmSnNT7+j0psdNgA7bD" +
                "2sCIKYl8enwkHVO1amMd33wVriRQU15qPSQh1sl4p2IKxtVkqv1uGjrB3iHss7c+/Lj5dEUtG7fSefmn" +
                "0yjau4rDcHDZWM/pw3uKmq2oWnwrCYclaumjUIe/8WrNXWGi1Mf//FPf7m4WFE9FUGd0lzizrQi0uOLE" +
                "tPEQx9SNhJmVrVgEcdtJReU0ixLnCLxvTCQ8tTgJbO1AfYRT8qR92/bOaM4amlZO4hFpHDF1HJLRveUA" +
                "fx8q47L7JnArGR1PlIdenBa6vV7Ax0HYPhkQGoCgg3DMit1ek+qNS+/e5gB1dr/zMyylRomnyyk1nDJZ" +
                "eeyCxMyT4wJ3vNonNwf24lC5SBdlb4VlvCRcAgrSed3QBZgvh9R4B0ChLQfDaysZGMW1QD3PQeeXT5Bd" +
                "gXbs/Ai/Rzze8S+wbsLNOc0a1Mzm7GNfQ0A4dsFvTQXX9VBAtDXiElmzDmhFlaP2V6qzL1ljOCGqVARv" +
                "jtFrgwJUtDOpGbuxVGOFcXjubpymZz8tqGWYrHqy1pPFz8Xo9/GE5FcUJLcNBOU8neQ3ZWhzI2+CQNiO" +
                "tbzOfZ+3q8O5Kb6oFPlgxtg5qaVHf04O6nsP3YMruEe/l0oQVMZZRncmfNli6Z+JP3LBsBbKJ+lO37XH" +
                "yRom6+fL0D9KN+YwFQo9faLnKfm8ejjqji9eO1d/yWi0dkr9AjyVAORLBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
