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
        [DataMember (Name = "caption_alignment")] public int CaptionAlignment { get; set; }
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
        public Dialog(in StdMsgs.Header Header, byte Action, string Id, duration Lifetime, double Scale, byte Type, byte Buttons, byte Icon, in StdMsgs.ColorRGBA BackgroundColor, string Title, string Caption, int CaptionAlignment, string[] MenuEntries, byte BindingType, in GeometryMsgs.Vector3 TfOffset, in GeometryMsgs.Vector3 DialogDisplacement, in GeometryMsgs.Vector3 TfDisplacement)
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
            CaptionAlignment = b.Deserialize<int>();
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
                int size = 129;
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
        [Preserve] public const string RosMd5Sum = "686c721c087828d6bf310fa5d5f78f19";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VVwW7cNhC96ysG8CF2YG+BpMjBQA9pg6Y+BDBio5egEEbkSCJCkSo5sq1+fR+pXaUG" +
                "GiCHZCFAJDXvzXDezGxW2055yD/9IWwl0VhfTbeqEBt1MTRZkwsDOdvYJXE5Iu96UTdJ0/vI+uZnyoa9" +
                "bChd5+OqW1RjyNvGmUp19PZb9DF9fP/rW+rYfB5SXIJtTTk8uVOnYDxuDM81FBf09avTrmXvhjBJ0KPZ" +
                "p78Iu6XFSXJy9Nu5YPGtrWENEifRtG5B/ClGY3pN2rex77PoV75bxz4OrXV59mykevwq0zOrpvnlO/+a" +
                "D3fvryk/V605ozvlYDlZZEDZsjL1EWq6YZR05eVBPEA8zWKpfi3pyAcA70eXCc8gQRJ7v9KSYaSRTJym" +
                "JTjDRVSI/QwPpAvENHNSZxbPCfYxIdXFvE88SWHHk+XvRYIRunl3DZuQxSzqENAKBpOEc1H45h01yyYv" +
                "AM3Z/WO8wlYG1OTunHRkLcHK05wklzg5X8PHy+1yB3AjOQIvNtN5PWuxzRcEJwhB5mhGOkfkt6uOKGQd" +
                "hR44Oe68FGKUsQfriwJ6cfEf5lCpA4d4ot8Yv/j4Ftqw85Y7XY3QzJfb52VAAmE4p/jgLEy7tZIY71BH" +
                "6LcucVqbgtpcNme/lxzDCKiqCN6cczQOAlh6dDqeuqeq0aJ/f3Q17l29zQVomfbVsK+6fcU/KqL/7c5T" +
                "sScpxYO0ImH0UL+VWu6TILczWvdQyvamFloMKNNJGBqgI3YkgNYlqfPxAFZJgnaTS3JKNkqmEBUcE38G" +
                "pUD1guZ5BhlaL3HIfhukOAbkXA7D4ZIeRwmbVVGt9ljtSmcoucHZDQlH0w5mOl7uErPnFVT3fot5c4YS" +
                "AkmKWgEXB7rpaY0LPZYLYZGOwyBSJ3tctWg1xssyCY4UzxN6G9GaSEvOPKC+Q1aMoUOz/xs87at1X/3T" +
                "/AvkRo3wawYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
