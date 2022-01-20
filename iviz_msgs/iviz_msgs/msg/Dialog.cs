/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Dialog : IDeserializable<Dialog>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        /// A dialog with title, text, and a row of buttons
        public const byte TYPE_PLAIN = 0;
        /// A smaller dialog with title and text
        public const byte TYPE_SHORT = 1;
        /// A dialog with an icon and a text
        public const byte TYPE_NOTICE = 2;
        /// A dialog with a menu of multiple options
        public const byte TYPE_MENU = 3;
        /// A tiny clickable dialog
        public const byte TYPE_BUTTON = 4;
        /// A dialog with title, text, icon, and a row of buttons
        public const byte TYPE_ICON = 5;
        public const byte BUTTONS_OK = 0;
        public const byte BUTTONS_YESNO = 1;
        public const byte BUTTONS_OKCANCEL = 2;
        public const byte BUTTONS_FORWARD = 3;
        public const byte BUTTONS_FORWARDBACKWARD = 4;
        public const byte BUTTONS_BACKWARD = 5;
        public const byte ICON_NONE = 0;
        public const byte ICON_CROSS = 1;
        public const byte ICON_OK = 2;
        public const byte ICON_FORWARD = 3;
        public const byte ICON_BACKWARD = 4;
        public const byte ICON_DIALOG = 5;
        public const byte ICON_UP = 6;
        public const byte ICON_DOWN = 7;
        public const byte ICON_INFO = 8;
        public const byte ICON_WARN = 9;
        public const byte ICON_ERROR = 10;
        public const byte ICON_DIALOGS = 11;
        public const byte ICON_QUESTION = 12;
        public const ushort ALIGNMENT_DEFAULT = 0;
        public const ushort ALIGNMENT_LEFT = 1;
        public const ushort ALIGNMENT_CENTER = 2;
        public const ushort ALIGNMENT_RIGHT = 4;
        public const ushort ALIGNMENT_JUSTIFIED = 8;
        public const ushort ALIGNMENT_FLUSH = 16;
        public const ushort ALIGNMENT_GEOMETRYCENTER = 32;
        public const ushort ALIGNMENT_TOP = 256;
        public const ushort ALIGNMENT_MID = 512;
        public const ushort ALIGNMENT_BOTTOM = 1024;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        /// If not zero, the dialog will be removed after this time
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
        [Preserve] public const string RosMd5Sum = "bbe9e66b575853ee815ca21e671ed62b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VWW2/iOBR+z684Uh+mXbVsSy/brTQPFEKbHSAshBlVq1VkEidYk8QZ22nL/Po9di4k" +
                "wGj3YadCJTn+zufP52YKlql7GAw9x535g9EIPsKlVbSNC3vqfrbRfnXMPphMcKlvVWvey9z255OBMwPN" +
                "BHACBEJGEh7DG1MbUEwl9BwUfVfnQLIQlwV/Ax7BulCKZxLaTMtnd+FppquSSaYkSag4ZDRUmrTtPXM9" +
                "Z6iV9w91kAxYwLNKwr7n1J6tQO97fcQTUpoVWnFaJIrluDnPFUPpbYbHlee5M2S4KRkUy7YQJCz4Stbo" +
                "URK2HZwhwvWWt/8SNC37eOjqJJR7L333E3T+dqmtES/2cuZ2EFcHHMPBbGhPdoj+HmLsLr4MFqMWx/Vx" +
                "xONg+MkgMSh7iGap4bitD6Pjgpmc2XtHMPbhwl0uu8KNvTn5Tq6xN1p3Io39iDRjHzmDiftUCWrZV/OG" +
                "/66Dd7/MKvtvbbszG7uV/b5txz1r/O9tu71YuIvqXJeHgpZmoXPiP1f2UrelXqi68eoOBhPnaYbF7Pkj" +
                "ezxYTbz9UuhgJva4AbRi2sEM8Z+9aGH6h5iF8/TstXluDjF/rFDu2LFHNeb+EDOerJbPHT13h6An253a" +
                "3uKlEoaJPaLIc+cAbab+7RGqqTPqom6vjnA9uliz07aqy/6NZUkV+qmM5a/PlIQ4ojbmy1pvFQ6nQM8H" +
                "hAiWxcBCKywE0SZIWEQVS6nueRZBxhV8p4Jjr2/obgQkCawpCJryV4ptHynkVxsmQbtaUcKJursBGZCE" +
                "lhuqbV491aPBvOjRsRM65AkXi6fHAaxJ8DUWvMhCP9DGWqmZO/VLQMyUqwNSvfokYXGGE1FVuL/+NvPR" +
                "R4tgtNp4zbIQ13yjK6Y8pUpsSxWfaaC4uAYV+TyKJFU/WC+D4YdM5gkJqNnxh0wdlGV9/J//rOny6QH2" +
                "Mm6dwFLhWCYixAgoEhJFIOJYCSzeUHGR0FeaoBNJc0yiWdXhkD109HQy8RPTjAq84rZQSAQpDgFP0yJj" +
                "AdFZxWx3/NGT4RUGORGKBUVCBOK5wFBreCRISjU7fiT9VtAsoOCMHhCTSRoUiqGgLTIEghKpU4z1b9J7" +
                "3dcO1on3xi/wlca63urNsfKI0mLpey6o1DqJfMA9fikP10NuDA7FXUIJp8bm46s8A9wEJdCcBxs4ReXz" +
                "rdpgE+hafyWCmZsRiQN9yYfwQTt9OGsxZ4Y6Ixmv6UvG3R7/hTZrePWZLjaYs0SfXhYxBhCBueCvLETo" +
                "emtI8NrGOsJeXQsitpbpV7OldTLWMS570WQEv4mUPGCYgNBc3nX7mGz42Ps/uxqbti4HA+ZSNE9x87Ru" +
                "nsjPUnS0O+tiF1QXD4YVAwavZk3XciQoxjbH1u3psnVMofEMyzSlBHOAHdF4mh9JgprZ2kNWKii2G/5O" +
                "YgpCTqWepsiRkq9ISTHr2pvkOZJh6wmSyaQcwmhGl1Pai3vn8LahWYnSWTM9ZrqSBSBYzMLSEzdKG2cC" +
                "1eFwbEf9cl4bzeVmWEJIIrgyDmc9cCLY8gLe9IHwQVTDgOshX+syRas43gSFFm4ougGdc2xNDIuUJMb6" +
                "zqTCMdSzmuvgvXnaNk/frX8AdZvtHvALAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
