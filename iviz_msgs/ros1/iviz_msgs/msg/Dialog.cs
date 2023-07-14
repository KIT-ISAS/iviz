/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Dialog : IHasSerializer<Dialog>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        /// <summary> A dialog with title, text, and a row of buttons </summary>
        public const byte TYPE_PLAIN = 0;
        /// <summary> A smaller dialog with title and text </summary>
        public const byte TYPE_SHORT = 1;
        /// <summary> A dialog with an icon and a text </summary>
        public const byte TYPE_NOTICE = 2;
        /// <summary> A dialog with a menu of multiple options </summary>
        public const byte TYPE_MENU = 3;
        /// <summary> A tiny clickable dialog </summary>
        public const byte TYPE_BUTTON = 4;
        /// <summary> A dialog with title, text, icon, and a row of buttons </summary>
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
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        /// <summary> If not zero, the dialog will be removed after this time </summary>
        [DataMember (Name = "lifetime")] public duration Lifetime;
        [DataMember (Name = "scale")] public double Scale;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "buttons")] public byte Buttons;
        [DataMember (Name = "icon")] public byte Icon;
        [DataMember (Name = "background_color")] public StdMsgs.ColorRGBA BackgroundColor;
        [DataMember (Name = "title")] public string Title;
        [DataMember (Name = "caption")] public string Caption;
        [DataMember (Name = "menu_entries")] public string[] MenuEntries;
        [DataMember (Name = "binding_type")] public byte BindingType;
        [DataMember (Name = "tf_offset")] public GeometryMsgs.Vector3 TfOffset;
        [DataMember (Name = "dialog_displacement")] public GeometryMsgs.Vector3 DialogDisplacement;
        [DataMember (Name = "tf_displacement")] public GeometryMsgs.Vector3 TfDisplacement;
    
        public Dialog()
        {
            Id = "";
            Title = "";
            Caption = "";
            MenuEntries = EmptyArray<string>.Value;
        }
        
        public Dialog(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            Id = b.DeserializeString();
            b.Deserialize(out Lifetime);
            b.Deserialize(out Scale);
            b.Deserialize(out Type);
            b.Deserialize(out Buttons);
            b.Deserialize(out Icon);
            b.Deserialize(out BackgroundColor);
            Title = b.DeserializeString();
            Caption = b.DeserializeString();
            MenuEntries = b.DeserializeStringArray();
            b.Deserialize(out BindingType);
            b.Deserialize(out TfOffset);
            b.Deserialize(out DialogDisplacement);
            b.Deserialize(out TfDisplacement);
        }
        
        public Dialog(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            b.Align4();
            Id = b.DeserializeString();
            b.Align4();
            b.Deserialize(out Lifetime);
            b.Align8();
            b.Deserialize(out Scale);
            b.Deserialize(out Type);
            b.Deserialize(out Buttons);
            b.Deserialize(out Icon);
            b.Align4();
            b.Deserialize(out BackgroundColor);
            Title = b.DeserializeString();
            b.Align4();
            Caption = b.DeserializeString();
            b.Align4();
            MenuEntries = b.DeserializeStringArray();
            b.Deserialize(out BindingType);
            b.Align8();
            b.Deserialize(out TfOffset);
            b.Deserialize(out DialogDisplacement);
            b.Deserialize(out TfDisplacement);
        }
        
        public Dialog RosDeserialize(ref ReadBuffer b) => new Dialog(ref b);
        
        public Dialog RosDeserialize(ref ReadBuffer2 b) => new Dialog(ref b);
    
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
            b.Serialize(MenuEntries.Length);
            b.SerializeArray(MenuEntries);
            b.Serialize(BindingType);
            b.Serialize(in TfOffset);
            b.Serialize(in DialogDisplacement);
            b.Serialize(in TfDisplacement);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Align4();
            b.Serialize(Id);
            b.Align4();
            b.Serialize(Lifetime);
            b.Align8();
            b.Serialize(Scale);
            b.Serialize(Type);
            b.Serialize(Buttons);
            b.Serialize(Icon);
            b.Align4();
            b.Serialize(in BackgroundColor);
            b.Serialize(Title);
            b.Align4();
            b.Serialize(Caption);
            b.Align4();
            b.Serialize(MenuEntries.Length);
            b.SerializeArray(MenuEntries);
            b.Serialize(BindingType);
            b.Align8();
            b.Serialize(in TfOffset);
            b.Serialize(in DialogDisplacement);
            b.Serialize(in TfDisplacement);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Id, nameof(Id));
            BuiltIns.ThrowIfNull(Title, nameof(Title));
            BuiltIns.ThrowIfNull(Caption, nameof(Caption));
            BuiltIns.ThrowIfNull(MenuEntries, nameof(MenuEntries));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 125;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetStringSize(Title);
                size += WriteBuffer.GetStringSize(Caption);
                size += WriteBuffer.GetArraySize(MenuEntries);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Action
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size = WriteBuffer2.Align4(size);
            size += 8; // Lifetime
            size = WriteBuffer2.Align8(size);
            size += 8; // Scale
            size += 1; // Type
            size += 1; // Buttons
            size += 1; // Icon
            size = WriteBuffer2.Align4(size);
            size += 16; // BackgroundColor
            size = WriteBuffer2.AddLength(size, Title);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Caption);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, MenuEntries);
            size += 1; // BindingType
            size = WriteBuffer2.Align8(size);
            size += 24; // TfOffset
            size += 24; // DialogDisplacement
            size += 24; // TfDisplacement
            return size;
        }
    
        public const string MessageType = "iviz_msgs/Dialog";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6f9efac24afce1d123add4a454755fc5";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VVTW/bOBC961cMkEOTReJtkrbbDdCDYzupUcfK2k6LoigMWqJkIhKpJakk6q/fR8qS" +
                "5cTF7mFrCDA1nHnz5lOlkPY99QeLcThd9odD+kCvg7IrnI1uws8jyE/3yfuTCa7Ogs3d4uvtaHk76Y+n" +
                "5JCIDohRLFimUnoUdk1W2Iwfk+VP9piYjHGt1SOphFaltUoa6iLNP4azhUM6rZFMzrKM65eIHsqBdq2n" +
                "4WI8cMzPXvJgkkSk5IbCc8ub0fSOnN/zPZaUc1k6xnmZWVHAuSqsAPUuwuXdYhFOgfCmRrBCVhRlIrpn" +
                "K1jUgF2D8QDqzuXbf0mao70/dU0Rat/zZfiJdn7b0jYaX0fzabijcfoCY9CfDkaTrcbZM42rcPalPxt2" +
                "MM73a1z2B5+8JpLyTKO9ajHeNsG4vKCS09GzELx8MAvn813iXt5GvqXr5S3XLUkv30PNy4fj/iS83hDq" +
                "yO9uW/x3O/rhl+lG/kdXPp5ehRv5+64cPhv9P7vy0WwWzjZxvX5JaO4vdiL+6240d2PpLjCNxsbL3KTm" +
                "94+cxRiYtf8LVpXFqESuW6GihUxJxEFcauZElImEW5Fz14EiIaks/eBaofPWfNuQWUYrTprn6oGjCRML" +
                "fLsWhpxpkGSK2XdvyEQs47VDWxWbU9Oo/sU18pboQGVKz64v+7Ri0X2qVSnjZeSEDVM/Bc1LxIpOFN++" +
                "+6FccolXvsFfCRnjbundp1zl3OqqdvaZR1bpc7LJUiWJ4fYn93XMy1iYImMRh4+faQJpRysIPvzPv+Bm" +
                "fn1BzwobHNDcYhcwHSMDlsXMMkoUCi7SNdcnGX/gGYxYXqBW/talw/RguHA1w5NyyTX2akWlgZJVFKk8" +
                "L6WImCseirpjD0uBvUkF01ZEZcY09JVGqp16olnOHToew/8uuYw4jYcX0JGGR6UVIFQBIdKcGVfJ8bBe" +
                "+ednzgCd922mzOn34GDxqE4g56nrr4YFOo1Zx5o/FZobR5iZCzj7rY6yByfIEoe72NChly3xao4I3sCF" +
                "Fypa0yFCuK3sGk3vevuBaeH3MoAj94mJ6ZUzenXUQZYeWjKpGvgacevjv8DKFtfFdLJG8TKXBlOmyCQU" +
                "C60eRAzVVeVB8NFAQ2E2V5rpKvDz6V0GB1cu2fXs+dLgnxmjIoFKxP7T0YyLL8sSs/6r27Id43oRoKi6" +
                "PaXtadWe2K9itHdMm67X3DUP0oqE0YO/c02daI7cFpjhnuvfsW80JdGvOWeoAUajtfSfaM39Lu0BlWuO" +
                "ucNXWliKFTduewIjZ/eA5Ki6s2ZFATDMoGbSZPXShRgmh7yX9o7pcc1lreWq5ofNj6eISItUxLUlHOWt" +
                "MaNNcFjTyVm9nz3n2hlaCCBaWW9w1KNxQpUq6dEFhIPebAXllnrDyzetVdj8pSPuIXYTeqswmkiLMSxF" +
                "f0tjsY96Qbv+n9pT1Z5+BP8ABryUK24KAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Dialog> CreateSerializer() => new Serializer();
        public Deserializer<Dialog> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Dialog>
        {
            public override void RosSerialize(Dialog msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Dialog msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Dialog msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Dialog msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Dialog msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Dialog>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Dialog msg) => msg = new Dialog(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Dialog msg) => msg = new Dialog(ref b);
        }
    }
}
