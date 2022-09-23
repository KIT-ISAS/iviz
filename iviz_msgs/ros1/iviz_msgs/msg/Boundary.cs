/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Boundary : IHasSerializer<Boundary>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        public const byte TYPE_SIMPLE = 0;
        public const byte TYPE_AREA_HIGHLIGHT = 3;
        public const byte TYPE_TARGET_HIGHLIGHT = 4;
        public const byte BEHAVIOR_NONE = 0;
        public const byte BEHAVIOR_COLLIDER = 1;
        public const byte BEHAVIOR_NOTIFY_COLLISION = 2;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "behavior")] public byte Behavior;
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "second_color")] public StdMsgs.ColorRGBA SecondColor;
        [DataMember (Name = "caption")] public string Caption;
    
        public Boundary()
        {
            Id = "";
            Caption = "";
        }
        
        public Boundary(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            Id = b.DeserializeString();
            b.Deserialize(out Type);
            b.Deserialize(out Behavior);
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            Caption = b.DeserializeString();
        }
        
        public Boundary(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            b.Align4();
            Id = b.DeserializeString();
            b.Deserialize(out Type);
            b.Deserialize(out Behavior);
            b.Align8();
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            Caption = b.DeserializeString();
        }
        
        public Boundary RosDeserialize(ref ReadBuffer b) => new Boundary(ref b);
        
        public Boundary RosDeserialize(ref ReadBuffer2 b) => new Boundary(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Behavior);
            b.Serialize(in Pose);
            b.Serialize(in Scale);
            b.Serialize(in Color);
            b.Serialize(in SecondColor);
            b.Serialize(Caption);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Behavior);
            b.Serialize(in Pose);
            b.Serialize(in Scale);
            b.Serialize(in Color);
            b.Serialize(in SecondColor);
            b.Serialize(Caption);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Caption is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 123;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetStringSize(Caption);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Action
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size += 1; // Type
            size += 1; // Behavior
            size = WriteBuffer2.Align8(size);
            size += 56; // Pose
            size += 24; // Scale
            size += 16; // Color
            size += 16; // SecondColor
            size = WriteBuffer2.AddLength(size, Caption);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/Boundary";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4836eb7a121c225ef4bf51b4011b4e36";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V227bOBB911cMkIcmReLdJsViEaAPauwmApzY6xgBikVh0NJYIiqRKknZVb9+Dylb" +
                "ttsA3Yc2hi/0XA7nzE2NVO5vim/myeRhEQ+H9I7+jJpD4Wx0P3kaQf7mOXk8HkN1GW1184/T0eIxuZ+O" +
                "RwdIQRrPRvHiLrm9G+Mzh/bqUDuPZ7ej+ZH+7Q70/egufkoms8XD5OEQtpffTMbjZDiaHQR54DNPPnzs" +
                "TB4RdxdtZF22qGxu/7hjkbGhIvxEy9YxidRJrWBipMpJZp3UtTV3pyUXYi21iXLWFTvTdkhTbZlqfH0n" +
                "f+LUaXNFNhUl7y++0aU2s9v3MaX+9JzCcqpVttjpQzipqEN00btf/IruH2+v6bu8RCf06ITKhMkIlEQm" +
                "nKCVRr5kXrC5KHnNJZxEVXNGQevzZAdwnBfSEt45KzaiLFtqLIycBuGqapRMhU+rrPjIH55SkaBaGCfT" +
                "phQG9tpkUnnzlREVe3S8LX9pWKVMyfAaNgrpapxEQC0QUsPC+oQlQwo9cXXpHeiE/p1p++ZTdDLf6AvI" +
                "OUf5+yjIFcL5qPlrbdj6gIW9xmWvO5YDXHK9LYyl0yBb4K89I9yGWLjWaUGnoDBtXaEVAJnWwkixLNkD" +
                "owtKoL7yTq/ODpBVgFZC6R18h7i/4//Aqh7Xc7ooULzSp8E2OTIJw9rotcxgumwDSFpKVo5KuTTCtJH3" +
                "6q6MTj74ZMMIXqE0+BXW6lSiEhltpCt2XRnKssCo/Ka2/HHSQDAmw75ICF/4mSC9CvPn+2dlGDRqkfK5" +
                "bzcvzrZ6GWyRF9JG7nwHFE01uqE3iP5pwNKogLu3eymCCGU3QugFJ6SyoVp9/OCCGQkhH9GNVqUW7q+3" +
                "9LU/tf3p28uEv0/djkNfKHTQUT6Pg/f/vuzzjkVTDaKfMNqdNi/DbbvNnyNG66A7pjTwmyoJK0UrbKaK" +
                "BUqGJdh7wjGThtOuDedYqwzi6FvpKNNsSWnfC5X4DEjGfHtvUdcAw7Y1QtmySyXEcDnlQT44p03BqrPy" +
                "8xnWaljEMiUjc5l1nj7DvbOgLblzcqtLzHdZdjF3l6H9AGJ0V7izASUranVDG08IB7Pd/xoPyD6usJ6c" +
                "1ud++W8hnul1pMVakfsGsA5Pnp9W/feU+sdncHclnh2mP+X9admfRBT9BxYNcExKCQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Boundary> CreateSerializer() => new Serializer();
        public Deserializer<Boundary> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Boundary>
        {
            public override void RosSerialize(Boundary msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Boundary msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Boundary msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Boundary msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Boundary msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Boundary>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Boundary msg) => msg = new Boundary(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Boundary msg) => msg = new Boundary(ref b);
        }
    }
}
