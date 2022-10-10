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
        public const byte TYPE_CIRCLE_HIGHLIGHT = 1;
        public const byte TYPE_SQUARE_HIGHLIGHT = 2;
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
            b.Align4();
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Behavior);
            b.Align8();
            b.Serialize(in Pose);
            b.Serialize(in Scale);
            b.Serialize(in Color);
            b.Serialize(in SecondColor);
            b.Serialize(Caption);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference(nameof(Id));
            if (Caption is null) BuiltIns.ThrowNullReference(nameof(Caption));
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
        public const string Md5Sum = "ee6b78ec18122b0691b8f6ec17631801";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VbW/aSBD+7l8xUj40OSXcNT1VVaR8oEATSyRQQiNF1Qkt9mCvzt51d9dQ36/vs2sw" +
                "0EZqP1yDeBlmZ56dZ95cS+XeUX8wjyf3i/5wSNf0V1QfKmeju8njCPrXz+n74zGOLqPt2fxpOlo8xHfT" +
                "8egAKWgH8WwwHi1u45vbMT7zA8TW6+On/uz4vIN9P7rtP8aT2eJ+cn8I3OkHk/E4Ho5mB6AHPvP4w1Nr" +
                "8oDIW+DIunRR2sz+ecsiZUN5+ImWjWMSiZNawcRIlZFMW61rKm6lJediLbWJMtYlO9O0SFNtmSp8fad/" +
                "5MRp84ZsIgreXzzQhTazm/d9Srz03IHlRKt0sTsP4SSiCtFF1//zK7p7uLmi7/ISndCDEyoVJiVQEqlw" +
                "glYa+ZJZzuai4DUXcBJlxSmFU58n24PjPJeW8M5YsRFF0VBtYeQ0CJdlrWQifFplyUf+8JSKBFXCOJnU" +
                "hTCw1yaVypuvjCjZo+Nt+UvNKmGKh1ewUUhX7SQCaoCQGBbWJyweUuiJN5fegU7o80zb1/9EJ/ONvoCe" +
                "M5S/i4JcLpyPmr9Whq0PWNgrXPZHy7KHS662hbF0GnQL/LVnhNsQC1c6yekUFKaNy7UCINNaGCmWBXtg" +
                "dEEB1Ffe6dXZAbIK0EoovYNvEfd3/Aqs6nA9p4scxSt8GmydIZMwrIxeyxSmyyaAJIVk5aiQSyNME3mv" +
                "9sro5INPNozgFUqDX2GtTiQqkdJGunzXlaEsC4zKb2rLHycNBPtk2BcJ4Qs/E6RXYf58/6wMg0YlEj73" +
                "7ebV6fZcBlvkhbSRO98eRVONbugMoo81WBoVcPd2L0UQoexGCL3ghFQ2VKuLH1wwIyHkI7rRqtDCvf2b" +
                "vnZS00n/vUz4+9TtOHSFQgcd5fM4eP/vyz7vWDRlL/oJo520eRlu223+HDFah7NjSj2/qeKwUrTCZipZ" +
                "oGRYgp0nHFNpOGnbcI61yiCOvpWOUs2WlPa9UIp/AcmYb+8tqgpg2LZGKFu0qYQaLqfcy3rntMlZtVZ+" +
                "PsNaDYtYJmRkJtPW02e4cxa0JXdObnWJ+S6KNub2MrQfQIxuC3fWo3hFja5p4wlBMNv9r/GA7OIK68lp" +
                "fe6X/xbimV5HWqwVmW8A6/Dk+WnVf0+pf3wGt1fi2WE6KeukZSeJKPoGLrRpHkwJAAA=";
                
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
