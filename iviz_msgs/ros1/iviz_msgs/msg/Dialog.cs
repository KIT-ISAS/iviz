/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Dialog : IDeserializable<Dialog>, IMessage
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
        /// <summary> If not zero, the dialog will be removed after this time </summary>
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
    
        public Dialog()
        {
            Id = "";
            Title = "";
            Caption = "";
            MenuEntries = EmptyArray<string>.Value;
        }
        
        public Dialog(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.DeserializeString(out Id);
            b.Deserialize(out Lifetime);
            b.Deserialize(out Scale);
            b.Deserialize(out Type);
            b.Deserialize(out Buttons);
            b.Deserialize(out Icon);
            b.Deserialize(out BackgroundColor);
            b.DeserializeString(out Title);
            b.DeserializeString(out Caption);
            b.Deserialize(out CaptionAlignment);
            b.DeserializeStringArray(out MenuEntries);
            b.Deserialize(out BindingType);
            b.Deserialize(out TfOffset);
            b.Deserialize(out DialogDisplacement);
            b.Deserialize(out TfDisplacement);
        }
        
        public Dialog(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.Align4();
            b.DeserializeString(out Id);
            b.Align4();
            b.Deserialize(out Lifetime);
            b.Align8();
            b.Deserialize(out Scale);
            b.Deserialize(out Type);
            b.Deserialize(out Buttons);
            b.Deserialize(out Icon);
            b.Align4();
            b.Deserialize(out BackgroundColor);
            b.DeserializeString(out Title);
            b.Align4();
            b.DeserializeString(out Caption);
            b.Align2();
            b.Deserialize(out CaptionAlignment);
            b.Align4();
            b.DeserializeStringArray(out MenuEntries);
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
            b.Serialize(CaptionAlignment);
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
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Title is null) BuiltIns.ThrowNullReference();
            if (Caption is null) BuiltIns.ThrowNullReference();
            if (MenuEntries is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                if (MenuEntries[i] is null) BuiltIns.ThrowNullReference(nameof(MenuEntries), i);
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 127;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetStringSize(Title);
                size += WriteBuffer.GetStringSize(Caption);
                size += WriteBuffer.GetArraySize(MenuEntries);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c += 1; // Action
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Id);
            c = WriteBuffer2.Align4(c);
            c += 8; // Lifetime
            c = WriteBuffer2.Align8(c);
            c += 8; // Scale
            c += 1; // Type
            c += 1; // Buttons
            c += 1; // Icon
            c = WriteBuffer2.Align4(c);
            c += 16; // BackgroundColor
            c = WriteBuffer2.AddLength(c, Title);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Caption);
            c = WriteBuffer2.Align2(c);
            c += 2; // CaptionAlignment
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, MenuEntries);
            c += 1; // BindingType
            c = WriteBuffer2.Align8(c);
            c += 24; // TfOffset
            c += 24; // DialogDisplacement
            c += 24; // TfDisplacement
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Dialog";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bbe9e66b575853ee815ca21e671ed62b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWW2/iOBR+z684Uh+mXbVsoZftVpoHCqHNDhA2hBlVo1FkEidYTWLGdtoyv36PnQvh" +
                "Mtp92KlQSY6/8/nzuZmC5eoO+gPfcadBfziEj3BpFW2jZ0/czzbau8fs/fEYl3pWteY/z+xgNu47U9BM" +
                "ACdAIGIk5Qm8MbUCxVRKz0HRd3UOJI9wWfA34DEsC6V4LqHNNH9yPV8zdUsmmZE0peKQ0VBp0rb31PWd" +
                "gVbeO9RBcmAhzysJ+54Te7oAve/VEU/IaF5oxVmRKrbGzflaMZTeZnhY+L47RYbrkkGxfANhysIXskSP" +
                "krDt4AwQrre8+ZegadnHQ1cnodx7HrifYOdvm9oa8WzPp+4OonvAMehPB/Z4i+jtIUau96XvDVscV8cR" +
                "D/3BJ4PEoOwhmqWG46Y+jI4LZnJq7x3B2AeeO5/vCjf25uRbucbeaN2KNPYj0ox96PTH7mMlqGVfzBr+" +
                "2x28+2Va2f9o253pyK3sd2077lnj/2zbbc9zvepcl4eC5mZh58R/L+y5bku9UHVj9xb6Y+dxisXsB0N7" +
                "1F+M/f1S2MGM7VEDaMV0BzPAf7bXwvQOMZ7z+OS3ea4PMX8tUO7IsYc15u4QMxov5k87em4PQY+2O7F9" +
                "77kShok9osh3ZwBtpt7NEaqJM9xF3XSPcD24WLOTtqrL3rVlSRUFmUzk70+URDiiVubLWm4UDqdQzweE" +
                "CJYnwCIrKgTRJkhZTBXLqO55FkPOFfyggmOvr+h2BKQpLCkImvFXim0fK+RXKyZBu1pxyom6vQYZkpSW" +
                "G6rNunqqR4N50aNjK3TAUy68x4c+LEn4kghe5FEQamOt1Myd+iUkZsrVAaleA5KyJMeJqCrc129mPgZo" +
                "EYxWGy9ZHuFaYHQllGdUiU2p4jMNFRdXoOKAx7Gk6ifrZTCCiMl1SkJqdvwp0w7Ksj7+z3/WZP54D3sZ" +
                "t05grnAsExFhBBSJiCIQc6wElqyouEjpK03RiWRrTKJZ1eGQHXT0dTLxk9CcCrziNlBIBCkOIc+yImch" +
                "0VnFbO/4oyfDKwzWRCgWFikRiOcCQ63hsSAZ1ez4kfR7QfOQgjO8R0wuaVgohoI2yBAKSqROMda/Se9V" +
                "TztgSX71uOx+s078N36BdprowqtVYAkSpVXT97WgUgsm8h43+608ZQc3wShR3C6ScGpsAb7KM8DdUAtd" +
                "83AFp3iE2UatsBt00b8SwcwVicShvu0j+KCdPpy1mHNDnZOc1/Ql43aP/0KbN7z6TBcrTF6qwyCLBCOJ" +
                "wLXgryxC6HJjSPD+xoLCpl0KIjaWaVyzpXUy0sEum9KkBr+JlDxkmInI3OJ1H5m0BDgEfnVZNv1dTghM" +
                "qmiekuZp2TyRX6XoaJvWVS+oLh4MKwYMXs2aLupYUIztGnu4o+vXMYXGc6zXjBLMAbZG42l+LQlqhmwH" +
                "Wamg2Hf4g4kpiDiVeqwiR0ZekJJi1rU3Wa+RDHtQkFym5TRGM7qc0k7SOYe3Fc1LlM6aaTbTniwEwRIW" +
                "lZ64UdY4E6gOh/M77pWD22guN8MSQhLBlXE464ATw4YX8KYPhA+imgpcT/talylaxfFKKLRwQ7Eb0BnH" +
                "1sSwSEkSrO9cKpxHHau5F96bp03z9MP6B/O18Dr5CwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
