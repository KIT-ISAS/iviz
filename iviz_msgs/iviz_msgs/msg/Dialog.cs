/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Dialog")]
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
        internal Dialog(ref Buffer b)
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
            b.Serialize(BackgroundColor);
            b.Serialize(Title);
            b.Serialize(Caption);
            b.Serialize(CaptionAlignment);
            b.SerializeArray(MenuEntries, 0);
            b.Serialize(BindingType);
            b.Serialize(TfOffset);
            b.Serialize(DialogDisplacement);
            b.Serialize(TfDisplacement);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Dialog";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "55f9895157fe16b721a27e4c5483f8ef";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVTW/UMBC9R9r/MFIPtKhdxIc4VOLAh4AekBBUXBCKJvYkserYwXZawq/n2ckGikDi" +
                "QFeRYjvz3oznzczGpOshdvHBW2Etgfryqpo5CbFKxrsqpmBcR0ZXegqcj8iaVpIZpGqt5/T0CUXFVhZU" +
                "msd11UwpeReXjVGFavX20lsfPrx58ZwaVldd8JPTtcqHB3fJJDCuG8VjCWUyLj18etjWbE3nBnFptfv8" +
                "hbCbapwEI6vjxjiNb3WJqxM/SArzEsUnUcmHx5Ta2rdtlPSX79qw9V2tTRwtKyke/8p0y6raVc/+829X" +
                "vfv45px+E25XHdHHxE5z0EhCYs2JqfVQ1HS9hDMr12KB4mEUTeVrzkjcA3jZm0h4OnES2NqZpgij5En5" +
                "YZicUZyFheC38EAaR0wjh2TUZDnA3gdkO5u3gQfJ7HiifJ3EKaGLV+ewcVHUlAwCmsGggnDMKl+8oqLw" +
                "40cZUB1d3vgzbKVDXW7OKfWccrDybQwSc5wcz+Hj/nK5PbiRHYEXHem4nNXYxhOCE4Qgo1c9HSPy93Pq" +
                "UcypF7rmYLixKNOI6rIWrPcy6N7JL8w57HNy7PyBfmH86eNfaN3Gm+901kMzm28fpw4JhOEY/LXRMG3m" +
                "QqKsQSmh55rAYa4yanFZHb3OOYYRUEURvDlGrwwE0HRjUn/ooKJGjR6++4Lcenu3jAfIGbZVt62abcV3" +
                "F9Qfu3R3KPkguYSQXKSNrsvHXNFtEGR4RA/vc/FelHLzDsU6CEMJ9MWGBFCbACjm0R6sEgRNJ6dkEmkv" +
                "kZxP4Bj4CpQC7QloHkeQoQEDu2iXkYpjQI5l3+1P6aYXNFa2ytqVTiu9aRQF0xm0ZkbC0bCBmdbbnWII" +
                "PYL21i4xL85QSCAJPhXAyZ4uWpr9RDf5QliEdSR4ahDiGlcp3eT9aZ4HK8XtjL73aFCkJUbuUOUuJkyj" +
                "fbX9L3zbVvO2+r6rfgC1ynVEdgYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
