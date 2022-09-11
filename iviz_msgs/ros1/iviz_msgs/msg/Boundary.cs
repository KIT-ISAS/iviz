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
        public const byte TYPE_COLLIDER = 1;
        public const byte TYPE_COLLIDABLE = 2;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
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
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            b.DeserializeString(out Caption);
        }
        
        public Boundary(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.Align4();
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Align8();
            b.Deserialize(out Pose);
            b.Deserialize(out Scale);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            b.DeserializeString(out Caption);
        }
        
        public Boundary RosDeserialize(ref ReadBuffer b) => new Boundary(ref b);
        
        public Boundary RosDeserialize(ref ReadBuffer2 b) => new Boundary(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
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
                int size = 122;
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
        public const string Md5Sum = "6db1643d6f3f2ea4ee94125903df75a4";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V32vjRhB+118xkIdLSuK75MpRAnlwYjc1OI0vMQelFDOWxtJSaVe3u4pP/ev77cqW" +
                "7btA+tDEGGs8O/PtfPNLjdL+FxrezCf3vy+GoxFd0Yek2Vc+jO/uv4yhP39OP5xOcXSRbM7mf8zGi8fJ" +
                "3Ww63kOK2pv76XQyGj/sIe3ph9fRA0DOZ4vK5e79b8KZWCriI1m2XohTr4yGiVU6J5V1Wt/WkuRiKvG2" +
                "7XxnxgnV+PlO/0VSb+xHcimXsrvqxpTGPtxeDykN0nMHTlKjs8X2PAaQch3jSa7+509y93h7Sd9lIjmi" +
                "R886Y5sRKHHGnmllkCGVF2LPSnmSEk5c1ZJRPA2ZcQM4zgvlCN9ctFguy5YaByNvQLiqGq1SDolUlRz4" +
                "w1NpYqrZepU2JVvYG5spHcxXlisJ6Pg6+dqIToUmo0vYaKSr8QoBtUBIrbALCZuMKJb+40VwoCP688G4" +
                "87+So/nanEEvOQreR0G+YB+ilm+1FRcCZneJy37qWA5wyeWmMI6Oo26Bv+6EcBtikdqkBR2Dwqz1hdEA" +
                "FHpiq3hZSgBGF5RAfRec3p3sIesIrVmbLXyHuLvjv8DqHjdwOitQvDKkwTU5MgnD2ponlcF02UaQtFSi" +
                "PZVqadm2SfDqrkyOfg3JhhG8YmnwZOdMqlCJjNbKF9uujGVZYDheqS1/nDQQHJKVUCSEz2EmyKzi/IX+" +
                "WVkBjZpTOQ3tFtTZ5lxFW+SFjFVb3wElM4Nu6A2Szw1YWh1xd3ZvRRChbEcIveBZaRer1ccPLpiRGPIB" +
                "3WRVGvaffqZvvdT20j9vE/4udVsOfaHQQQf5PAw+/Pu6yzsWTTVIXmC0ldZvw22zzZ8jRk/x7JDSIGyq" +
                "SVwpRmMzVcIoGZZg7wnHTFlJuzacY60KiKNvlafMiCNtQi9U/DcgBfMdvLmuAYZta1m7sksl1HA5lkE+" +
                "OKV1IbqzCvMZ12pcxColq3KVdZ4hw70z04bcKfnVBea7LLuYu8vQfgCxpivcyYAmK2pNQ+tACILd7H9D" +
                "S+njiuvJG3Malv8G4pleR1qc4zw0gPN487xY9dcp9Y/v4O5KvDtsL+W9tOwlTpJ/AV9vwMTWCAAA";
                
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
