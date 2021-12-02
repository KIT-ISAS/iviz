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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Dialog(ref b);
        
        Dialog IDeserializable<Dialog>.RosDeserialize(ref Buffer b) => new Dialog(ref b);
    
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
            b.Serialize(ref BackgroundColor);
            b.Serialize(Title);
            b.Serialize(Caption);
            b.Serialize(CaptionAlignment);
            b.SerializeArray(MenuEntries);
            b.Serialize(BindingType);
            b.Serialize(ref TfOffset);
            b.Serialize(ref DialogDisplacement);
            b.Serialize(ref TfDisplacement);
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
                "H4sIAAAAAAAACrVVQYveNhC9+1cM7CG7ZfcrTUoOCz2kDU33UAhN6KUEM5bGtogsudJ4N+6v75Psz8mW" +
                "BHpIPgyW5HlvRvNm5stq2ykP+fvfhK0kGuur6VYVYqMuhiZrcmEgZxu7JC5H5F0v6iZpeh9Zn/9I2bCX" +
                "DaXrvK+6RTWGvG2cqVS7t1+ij+mPVz+/oI7N+yHFJdjWlMOzO3UKxn1jeK6hLC7oD8/P25a9G8IkQXe7" +
                "v94RdkuLk+Rkd9y5YPGtrXENEifRtG5R/ClGY3pG2rex77PoF75bxz4OrXV59mykevwi0yOrpvnpK/+a" +
                "39+8uqX/yNZc0BvlYDlZZEDZsjL1EXK6YZR04+VePEA8zWKpfi3pyCcA344uE55BgiT2fqUlw0gjmThN" +
                "S3CGi6pQ+xEeSBeIaeakziyeE+xjQqqLeZ94ksKOJ8vfiwQjdPfyFjYhi1nUIaAVDCYJ5yLx3Uuq8j57" +
                "WgDNxduHeIOtDCjKwznpyFqClQ9zklzi5HwLH99tlzuBG8kReLGZLutZi22+IjhBCDJHM9IlIn+96ohK" +
                "1lHonpPjzqNGM0rLe7A+KaAnV58wl7BvKXCIZ/qN8aOP/0MbDt5yp5sRmvly+7wMSCAM5xTvnYVpt1YS" +
                "4x3qCA3XJU5rU1Cby+bi15JjGAFVFcGbc47GQQBLD07Hc/tUNVo08LeuxqOtt8EALdOxGo5Vd6z4W0X0" +
                "2e48F3uSUjxIKxJG9/VbqeU+CXI7o3VPpWzvaqHFgDKdhKEBOuJAAmhdAhRj6ARWSYJ2k2tySjZKphAV" +
                "HBO/B6VAdQKa5xlkaL3EIfttkuIYkEs5DadrehgFLVWsimq1x2pXOkPJDQ5NWZBwNB1gpv1y15g9T6G6" +
                "91vMmzOUEEhS1Aq4OtFdT2tc6KFcCIu0D4NIHULc46pFqzFel0mwUzxO6OuI1kRacuYB9R2yYgydmuPv" +
                "4MOxWo/VP82/Z+WsWGwGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
