/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Dialog")]
    public sealed class Dialog : IDeserializable<Dialog>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "action")] public byte Action { get; set; }
        [DataMember (Name = "id")] public string Id { get; set; }
        [DataMember (Name = "lifetime")] public duration Lifetime { get; set; }
        [DataMember (Name = "scale")] public double Scale { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "buttons")] public byte Buttons { get; set; }
        [DataMember (Name = "icon")] public byte Icon { get; set; }
        [DataMember (Name = "background_color")] public StdMsgs.ColorRGBA BackgroundColor { get; set; }
        [DataMember (Name = "title")] public string Title { get; set; }
        [DataMember (Name = "caption")] public string Caption { get; set; }
        [DataMember (Name = "caption_alignment")] public ushort CaptionAlignment { get; set; }
        [DataMember (Name = "menu_entries")] public string[] MenuEntries { get; set; }
        [DataMember (Name = "binding_type")] public byte BindingType { get; set; }
        [DataMember (Name = "tf_offset")] public GeometryMsgs.Vector3 TfOffset { get; set; }
        [DataMember (Name = "dialog_displacement")] public GeometryMsgs.Vector3 DialogDisplacement { get; set; }
        [DataMember (Name = "tf_displacement")] public GeometryMsgs.Vector3 TfDisplacement { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Dialog()
        {
            Id = string.Empty;
            Title = string.Empty;
            Caption = string.Empty;
            MenuEntries = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public Dialog(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Action = b.Deserialize<byte>();
            Id = b.DeserializeString();
            Lifetime = b.Deserialize<duration>();
            Scale = b.Deserialize<double>();
            Type = b.Deserialize<byte>();
            Buttons = b.Deserialize<byte>();
            Icon = b.Deserialize<byte>();
            BackgroundColor = new StdMsgs.ColorRGBA(ref b);
            Title = b.DeserializeString();
            Caption = b.DeserializeString();
            CaptionAlignment = b.Deserialize<ushort>();
            MenuEntries = b.DeserializeStringArray();
            BindingType = b.Deserialize<byte>();
            TfOffset = new GeometryMsgs.Vector3(ref b);
            DialogDisplacement = new GeometryMsgs.Vector3(ref b);
            TfDisplacement = new GeometryMsgs.Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Dialog(ref b);
        }
        
        Dialog IDeserializable<Dialog>.RosDeserialize(ref Buffer b)
        {
            return new Dialog(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Lifetime);
            b.Serialize(Scale);
            b.Serialize(Type);
            b.Serialize(Buttons);
            b.Serialize(Icon);
            BackgroundColor.RosSerialize(ref b);
            b.Serialize(Title);
            b.Serialize(Caption);
            b.Serialize(CaptionAlignment);
            b.SerializeArray(MenuEntries, 0);
            b.Serialize(BindingType);
            TfOffset.RosSerialize(ref b);
            DialogDisplacement.RosSerialize(ref b);
            TfDisplacement.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += BuiltIns.UTF8.GetByteCount(Title);
                size += BuiltIns.UTF8.GetByteCount(Caption);
                size += 4 * MenuEntries.Length;
                foreach (string s in MenuEntries)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Dialog";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "55f9895157fe16b721a27e4c5483f8ef";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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
