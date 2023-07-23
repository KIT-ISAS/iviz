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
            Header.RosValidate();
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
        public const string Md5Sum = "53759db7ec35ef14cd9859ec730c55c0";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VV30/bSBB+918xEg+FE+QKtL0eUh9CCDQqxFwSWlVVFW3ssbPC9vp210D619+369hx" +
                "INXdwzWylPXszDff/HQlC/ue+oPZKBzP+xcX9IFeB1VXOBnehJ+HkB/vkvevr3F1EqzvZl9vh/Pb6/5o" +
                "TA6JaI8ExVJkKqVHaZdkpc34kCw/2UMSRYxrrR5JJbSorFWFoS7S9GM4mTmk4xrJ5CLLWL9E9FAOtGs9" +
                "DmejgWN+8pKHKEhGqlhTeG55MxzfkfN7usOSci4qxzivMitLOFellaDeRTi/m83CMRDe1AhWFiuKMhnd" +
                "iwUsasCuwWgAdefy7b8kzdHenbqmCLXv6Tz8RFu/TWkbja/D6Tjc0jh+gTHojwfD643GyTONy3DypT+5" +
                "6GCc7tY47w8+eU0k5ZlGe9VivG2CcXlBJcfDZyF4+WASTqfbxL28jXxD18tbrhuSXr6DmpdfjPrX4dWa" +
                "UEd+d9viv9vSD7+M1/I/uvLR+DJcy9935fDZ6P/ZlQ8nk3Cyjuv1S0JTf7EV8V93w6kbS3eBaTQ2nucm" +
                "Nb9/ZBFjYJb+b20gIteu0NGySEnGQVxp4USUyYStzNm1oEyoUJZ+sFZovSVvOjLLaMGkOVcPjC5MLBzY" +
                "pTTkTIMkU8K+e0MmEhmvPdpV2RybXq3fXDNvyA5UpvTk6rxPCxHdp1pVRTyPnLAh6yeheYlE2Qnk23c/" +
                "mHMu8MqNg4UsYlzOPYOUVc5Wr2pvnzmySp+STeYqSQzbn9zXcc9jacpMRAwnP9ME0pZWEHz4n3/BzfTq" +
                "jJ5VN9ijqcVCEDpGCqyIhRWUKFRdpkvWRxk/cAYjkZeol7916TA9GM5c3fCkXLDGcl1RZaBkFUUqz6tC" +
                "RsKyL+yWPSwllieVQlsZVZnQ0FcaqXbqiRY5O3Q8hv+uuIiYRhdn0CkMR5WVILQCQqRZGFfK0UW9909P" +
                "nAG679tEmePvwd7sUR1BzqnrsYYFuk1Yx5qfSs3GERbmDM5+q6PswQmyxHAXG9r3sjlezQHBG7hwqaIl" +
                "7SOE25VdovFdfz8ILf1yBnDkvjMxvXJGrw46yIWHLkShGvgacePjv8AWLa6L6WiJ4mUuDaZKkUkollo9" +
                "yBiqi5UHwZcDDYX5XGihV4GfUe8y2Lt0ya7nz5cG/8IYFUlUIvbfj2ZefFnmmPdf3ZbtHNfLAEXV7Slt" +
                "T4v2JH4Vo51j2nS9Ztc8SCsSRg/+zjV1ohm5LTHDPde/I99oqkC/5ixQA4xGa+m/05r9Pu0BlTVj7vCp" +
                "lpZixcZtUGDk4h6QjKo7a1GWAMMMalGYrF68EMNkn3tp75Ael1zUWq5qftj8eMqItExlXFvCUd4aC1oH" +
                "h1WdnNQ72nOunaGFAKKV9QYHPRoltFIVPbqAcNDrraDcYm94+aa1Ctu/csQ9xHZCbxVGE2kxRqTo78JY" +
                "7KNe0H4CntrTqj39CP4B4o3VP3MKAAA=";
                
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
