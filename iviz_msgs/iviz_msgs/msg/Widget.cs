/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Widget")]
    public sealed class Widget : IDeserializable<Widget>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "main_color")] public StdMsgs.ColorRGBA MainColor;
        [DataMember (Name = "secondary_color")] public StdMsgs.ColorRGBA SecondaryColor;
        [DataMember (Name = "scale")] public double Scale;
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "caption")] public string Caption;
    
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
        internal Widget(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Action = b.Deserialize<byte>();
            Id = b.DeserializeString();
            Type = b.Deserialize<byte>();
            b.Deserialize(out MainColor);
            b.Deserialize(out SecondaryColor);
            Scale = b.Deserialize<double>();
            b.Deserialize(out Pose);
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
            b.Serialize(MainColor);
            b.Serialize(SecondaryColor);
            b.Serialize(Scale);
            b.Serialize(Pose);
            b.Serialize(Caption);
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
                size += BuiltIns.GetStringSize(Id);
                size += BuiltIns.GetStringSize(Caption);
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
                "H4sIAAAAAAAACr1UwW4TMRC9W8o/jNRDW0SCBIhDJA6FitIDUqG9RxPvZNeS197a3rTL1/PsZDcEkOBA" +
                "G62y4/XM85s3M46pWrWxjq8+C1cSqCkvtR6SEOtkvFMxBeNqMtXuaxo6UVPYR299+Hb14YJaNm6l8/JP" +
                "u1G0dxWHYe+ysZ7Tu7cUNVtRtfhWEjZL1I2PQh3+xqM1d4WJmqn3//k3U19ur5b0iwwzdUK3iTPhisCM" +
                "K05MGw99TN1ImFvZikUUt51UVHazLnGBwLvGRMJTi5PA1g7URzglT9q3be+M5iyjaeUoHpHGEVPHIRnd" +
                "Ww7w96EyLrtvAreS0fFEue/FaaHryyV8HLTtkwGhAQg6CMcs2vUlqd649OZ1DlAndw9+jqXUqPJ0OKWG" +
                "UyYrj12QmHlyXOKMF7vkFsBe7osX6ax8W2EZzwmHgIJ0Xjd0BuY3Q2q8A6DQloPhtZUMjPpaoJ7moNPz" +
                "n5Az7SU5dn6E3yEezvgXWDfh5pzmDWpmc/axryEgHLvgt6aC63ooINoacYmsWQd0o8pRuyPVyaesMZwQ" +
                "VSqCN8fotUEBKnowqRkbslRjhYl4+oacRmi2mxmUcz89sOrJWk8WPx2p38c0D8oFBcnNA1k5jyn5TZne" +
                "3M6bIJC3Yy0vc/fnz9V+3xRf1It8MGPsgtSNR5dODuprD/WDK7gHv+fLEWRm41CjTRNuuVgaaUoB6WBq" +
                "C+ujjNV4xz1O1jBZ358rg4N+UxpTudDfR6oe88+r+4P6uP3ahfpLUqP1gPR+AERhZ9hbBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
