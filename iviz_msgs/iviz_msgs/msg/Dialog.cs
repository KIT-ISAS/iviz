/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Dialog : IDeserializable<Dialog>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "lifetime")] public duration Lifetime;
        [DataMember (Name = "scale")] public double Scale;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "buttons")] public byte Buttons;
        [DataMember (Name = "icon")] public byte Icon;
        [DataMember (Name = "background_color")] public StdMsgs.ColorRGBA BackgroundColor;
        [DataMember (Name = "title")] public string Title;
        [DataMember (Name = "caption")] public string Caption;
        [DataMember (Name = "caption_alignment")] public ushort CaptionAlignment;
        [DataMember (Name = "menu_entries")] public string[] MenuEntries;
        [DataMember (Name = "binding_type")] public byte BindingType;
        [DataMember (Name = "tf_offset")] public GeometryMsgs.Vector3 TfOffset;
        [DataMember (Name = "dialog_displacement")] public GeometryMsgs.Vector3 DialogDisplacement;
        [DataMember (Name = "tf_displacement")] public GeometryMsgs.Vector3 TfDisplacement;
    
        /// Constructor for empty message.
        public Dialog()
        {
            Id = string.Empty;
            Title = string.Empty;
            Caption = string.Empty;
            MenuEntries = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public Dialog(in StdMsgs.Header Header, byte Action, string Id, duration Lifetime, double Scale, byte Type, byte Buttons, byte Icon, in StdMsgs.ColorRGBA BackgroundColor, string Title, string Caption, ushort CaptionAlignment, string[] MenuEntries, byte BindingType, in GeometryMsgs.Vector3 TfOffset, in GeometryMsgs.Vector3 DialogDisplacement, in GeometryMsgs.Vector3 TfDisplacement)
        {
            this.Header = Header;
            this.Action = Action;
            this.Id = Id;
            this.Lifetime = Lifetime;
            this.Scale = Scale;
            this.Type = Type;
            this.Buttons = Buttons;
            this.Icon = Icon;
            this.BackgroundColor = BackgroundColor;
            this.Title = Title;
            this.Caption = Caption;
            this.CaptionAlignment = CaptionAlignment;
            this.MenuEntries = MenuEntries;
            this.BindingType = BindingType;
            this.TfOffset = TfOffset;
            this.DialogDisplacement = DialogDisplacement;
            this.TfDisplacement = TfDisplacement;
        }
        
        /// Constructor with buffer.
        public Dialog(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Action = b.Deserialize<byte>();
            Id = b.DeserializeString();
            Lifetime = b.Deserialize<duration>();
            Scale = b.Deserialize<double>();
            Type = b.Deserialize<byte>();
            Buttons = b.Deserialize<byte>();
            Icon = b.Deserialize<byte>();
            b.Deserialize(out BackgroundColor);
            Title = b.DeserializeString();
            Caption = b.DeserializeString();
            CaptionAlignment = b.Deserialize<ushort>();
            MenuEntries = b.DeserializeStringArray();
            BindingType = b.Deserialize<byte>();
            b.Deserialize(out TfOffset);
            b.Deserialize(out DialogDisplacement);
            b.Deserialize(out TfDisplacement);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Dialog(ref b);
        
        public Dialog RosDeserialize(ref ReadBuffer b) => new Dialog(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Lifetime);
            b.Serialize(Scale);
            b.Serialize(Type);
            b.Serialize(Buttons);
            b.Serialize(Icon);
            b.Serialize(in BackgroundColor);
            b.Serialize(Title);
            b.Serialize(Caption);
            b.Serialize(CaptionAlignment);
            b.SerializeArray(MenuEntries);
            b.Serialize(BindingType);
            b.Serialize(in TfOffset);
            b.Serialize(in DialogDisplacement);
            b.Serialize(in TfDisplacement);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Title is null) throw new System.NullReferenceException(nameof(Title));
            if (Caption is null) throw new System.NullReferenceException(nameof(Caption));
            if (MenuEntries is null) throw new System.NullReferenceException(nameof(MenuEntries));
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                if (MenuEntries[i] is null) throw new System.NullReferenceException($"{nameof(MenuEntries)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 127;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Id);
                size += BuiltIns.GetStringSize(Title);
                size += BuiltIns.GetStringSize(Caption);
                size += BuiltIns.GetArraySize(MenuEntries);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Dialog";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "55f9895157fe16b721a27e4c5483f8ef";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VVTY/UOBC951eUNAdm0EwjPsRhJA67ixbmsBJaEBeEoopdSSwcO9iVGbK/fp+d7sBI" +
                "IHGAVqTYTr1X5XpV1VltO+UhP3otbCXRWF9Nt6oQG3UxNFmTCwM529glcTki73pRN0nT+8j6/Bllw142" +
                "lK7zcdUtqjHkbeNMpTp6+yv6mP599ecf1LH5NKS4BNuacnhyp07BeNwYnmsoiwv6+Plp27J3Q5gk6NHu" +
                "w0fCbmlxkpwcHXcuWHxra1yDxEk0rVsU78VoTE9J+zb2fRb9wXfr2MehtS7Pno1Ujz9kumfVNC9+8a/5" +
                "5+2ra8r3ZWvO6K1ysJwsMqBsWZn6CDndMEq68nIrHiCeZrFUv5Z05AOA70aXCc8gQRJ7v9KSYaSRTJym" +
                "JTjDRVWofQ8PpAvENHNSZxbPCfYxIdXFvE88SWHHk+XzIsEI3by8hk3IYhZ1CGgFg0nCuUh885KqvE+f" +
                "FEBz9u4uXmErA4pyd046spZg5cucJJc4OV/Dx8PtcgdwIzkCLzbTeT1rsc0XBCcIQeZoRjpH5G9WHVHJ" +
                "OgrdcnLceSnEqGMP1gcF9ODiG+ZQqQOHeKLfGL/6+BnasPOWO12N0MyX2+dlQAJhOKd46yxMu7WSGO9Q" +
                "R2i4LnFam4LaXDZnf5ccwwioqgjenHM0DgJYunM6ntqnqtGigX93Ne5tvQ0GaJn21bCvun3Fvyui73bn" +
                "qdiTlOJBWpEwuq3fSi33SZDbGa17KGV7UwstBpTpJAwN0BE7EkDrktQBeQCrJEG7ySU5JRslU4gKjok/" +
                "gVKgekHzPIMMrZc4ZL9NUhwDci6H4XBJd6OEzaqoVnusdqUzlNzg7IaEo2kHMx0vd4nZ8wSqe7/FvDlD" +
                "CYEkRa2AiwPd9LTGhe7KhbBIx2EQqZM9rlq0GuNlmQRHivsJfRPRmkhLzjygvkNWjKFDs/8dfNlX6776" +
                "r/kfZ+WsWGwGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
