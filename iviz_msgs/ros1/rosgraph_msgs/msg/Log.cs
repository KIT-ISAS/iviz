/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [DataContract]
    public sealed class Log : IDeserializable<Log>, IHasSerializer<Log>, IMessage
    {
        //#
        //# Severity level constants
        //#
        /// <summary> Debug level </summary>
        public const byte DEBUG = 1;
        /// <summary> General level </summary>
        public const byte INFO = 2;
        /// <summary> Warning level </summary>
        public const byte WARN = 4;
        /// <summary> Error level </summary>
        public const byte ERROR = 8;
        /// <summary> Fatal/critical level </summary>
        public const byte FATAL = 16;
        //#
        //# Fields
        //#
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "level")] public byte Level;
        /// <summary> Name of the node </summary>
        [DataMember (Name = "name")] public string Name;
        /// <summary> Message </summary>
        [DataMember (Name = "msg")] public string Msg;
        /// <summary> [Ignore] </summary>
        [DataMember (Name = "file")] public string File;
        /// <summary> [Ignore] </summary>
        [DataMember (Name = "function")] public string Function;
        /// <summary> Line the message came from </summary>
        [DataMember (Name = "line")] public uint Line;
        //string[] topics # topic names that the node publishes
        /// <summary> [Ignore] </summary>
        [DataMember (Name = "topics")] public string[] Topics;
    
        public Log()
        {
            Name = "";
            Msg = "";
            File = "";
            Function = "";
            Topics = EmptyArray<string>.Value;
        }
        
        public Log(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Level);
            b.DeserializeString(out Name);
            b.DeserializeString(out Msg);
            b.SkipString(out File);
            b.SkipString(out Function);
            b.Deserialize(out Line);
            b.SkipStringArray(out Topics);
        }
        
        public Log(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Level);
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeString(out Msg);
            b.Align4();
            b.SkipString(out File);
            b.Align4();
            b.SkipString(out Function);
            b.Align4();
            b.Deserialize(out Line);
            b.SkipStringArray(out Topics);
        }
        
        public Log RosDeserialize(ref ReadBuffer b) => new Log(ref b);
        
        public Log RosDeserialize(ref ReadBuffer2 b) => new Log(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
            b.Serialize(Name);
            b.Serialize(Msg);
            b.Serialize(File);
            b.Serialize(Function);
            b.Serialize(Line);
            b.SerializeArray(Topics);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
            b.Serialize(Name);
            b.Serialize(Msg);
            b.Serialize(File);
            b.Serialize(Function);
            b.Serialize(Line);
            b.SerializeArray(Topics);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Msg is null) BuiltIns.ThrowNullReference();
            if (File is null) BuiltIns.ThrowNullReference();
            if (Function is null) BuiltIns.ThrowNullReference();
            if (Topics is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Topics.Length; i++)
            {
                if (Topics[i] is null) BuiltIns.ThrowNullReference(nameof(Topics), i);
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 25;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Msg);
                size += WriteBuffer.GetStringSize(File);
                size += WriteBuffer.GetStringSize(Function);
                size += WriteBuffer.GetArraySize(Topics);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Level
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Msg);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, File);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Function);
            size = WriteBuffer2.Align4(size);
            size += 4; // Line
            size = WriteBuffer2.AddLength(size, Topics);
            return size;
        }
    
        public const string MessageType = "rosgraph_msgs/Log";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "acffd30cd6b6de30f120938c17c593fb";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61T0WrbMBR991dc0EPbQVrSjVECfshI0wW2dqQZeyglKNaNLZAlT5KT+e93ZC9exl72" +
                "MGOQrHvPuVf3HAuRCUHPfGCvY0cGG0OFsyFKGwNi2a6LTIv7D18f8ikJxbu2HNKGyOpx+ZTfEomSLXtp" +
                "zmPf5uvH/B1iR+mttn/g7tfrp3V+R4K9d/48spxv5p/y6XsSexmluSnQmS5GZtF3vNRsVN/fR5aKPVX9" +
                "MjAMiSH6VNPKmkkMi9tTrJisU3wK16FEtOYQZMl0Ot1rk0Avq9I6z6/jcWuLqJ09D7Xaxre3ZLRNiH5J" +
                "NU6MRaq7967OxEDy8krRNboIyO43fWsBIBnH7qhpd0aHikP2N2osneX/+ck+Pz/MKES1xVjCzTDaDP6A" +
                "G5T0CreKUkEV2kOzSpcV+8ngGRimblhRH41dw+EawE2lA+H9ZQ7TURuQFB08Vtetha7QK2oM4BwPpLYk" +
                "qZEe0rdGeuQ7r7RN6XuPgSV2vIG/t2wL+HAx633LRRs1GurAUHiWIem2WtBJJwDSCNcuTF8zsTm6Cc65" +
                "hIPGLgYp0DX/aDxkRFcyzFDszXDLaxTBlBjlVKDL/myLz3BFqIZeuHFFRZe4wpcuVrBLkvUgvZY72ArE" +
                "cLMB60UCXVydMdue2krrTvQD4+8a/0JrR950p0kF8UwaQ2hLTBKJjXcHrZC663qSwmi2Eebdeem7LKGG" +
                "kplYpmEjCaheGqwyBFdoKKHoqGM1/hwpc6tVlv0Ea64vs1UEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Log> CreateSerializer() => new Serializer();
        public Deserializer<Log> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Log>
        {
            public override void RosSerialize(Log msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Log msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Log msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Log msg) => msg.Ros2MessageLength;
            public override void RosValidate(Log msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Log>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Log msg) => msg = new Log(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Log msg) => msg = new Log(ref b);
        }
    }
}
