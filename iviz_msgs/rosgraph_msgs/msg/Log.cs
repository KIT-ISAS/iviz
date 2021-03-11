/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [Preserve, DataContract (Name = "rosgraph_msgs/Log")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Log : IMessage, System.IEquatable<Log>, IDeserializable<Log>
    {
        //#
        //# Severity level constants
        //#
        public const byte DEBUG = 1; //debug level
        public const byte INFO = 2; //general level
        public const byte WARN = 4; //warning level
        public const byte ERROR = 8; //error level
        public const byte FATAL = 16; //fatal/critical level
        //#
        //# Fields
        //#
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "level")] public byte Level;
        [DataMember (Name = "name")] public string? Name; // name of the node
        [DataMember (Name = "msg")] public string? Msg; // message 
        [DataMember (Name = "file")] public string? File; // file the message came from
        [DataMember (Name = "function")] public string? Function; // function the message came from
        [DataMember (Name = "line")] public uint Line; // line the message came from
        [DataMember (Name = "topics")] public string[]? Topics; // topic names that the node publishes
    
        /// <summary> Explicit constructor. </summary>
        public Log(in StdMsgs.Header Header, byte Level, string Name, string Msg, string File, string Function, uint Line, string[] Topics)
        {
            this.Header = Header;
            this.Level = Level;
            this.Name = Name;
            this.Msg = Msg;
            this.File = File;
            this.Function = Function;
            this.Line = Line;
            this.Topics = Topics;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Log(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Level = b.Deserialize<byte>();
            Name = b.DeserializeString();
            Msg = b.DeserializeString();
            File = b.DeserializeString();
            Function = b.DeserializeString();
            Line = b.Deserialize<uint>();
            Topics = b.DeserializeStringArray();
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Log(ref b);
        }
        
        readonly Log IDeserializable<Log>.RosDeserialize(ref Buffer b)
        {
            return new Log(ref b);
        }
        
        public override readonly int GetHashCode() => (Header, Level, Name, Msg, File, Function, Line, Topics).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Log s && Equals(s);
        
        public readonly bool Equals(Log o) => (Header, Level, Name, Msg, File, Function, Line, Topics) == (o.Header, o.Level, o.Name, o.Msg, o.File, o.Function, o.Line, o.Topics);
        
        public static bool operator==(in Log a, in Log b) => a.Equals(b);
        
        public static bool operator!=(in Log a, in Log b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
            b.Serialize(Name ?? string.Empty);
            b.Serialize(Msg ?? string.Empty);
            b.Serialize(File ?? string.Empty);
            b.Serialize(Function ?? string.Empty);
            b.Serialize(Line);
            b.SerializeArray(Topics ?? System.Array.Empty<string>(), 0);
        }
        
        public readonly void Dispose()
        {
        }
        
        public readonly void RosValidate()
        {
            if (Topics != null)
            {
                for (int i = 0; i < Topics.Length; i++)
                {
                    if (Topics[i] is null) throw new System.NullReferenceException($"{nameof(Topics)}[{i}]");
                }
            }
        }
    
        public readonly int RosMessageLength
        {
            get {
                int size = 25;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Name ?? string.Empty);
                size += BuiltIns.UTF8.GetByteCount(Msg ?? string.Empty);
                size += BuiltIns.UTF8.GetByteCount(File ?? string.Empty);
                size += BuiltIns.UTF8.GetByteCount(Function ?? string.Empty);
                if (Topics != null)
                {
                    size += 4 * Topics.Length;
                    foreach (string s in Topics)
                    {
                        size += BuiltIns.UTF8.GetByteCount(s);
                    }
                }
                return size;
            }
        }
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosgraph_msgs/Log";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "acffd30cd6b6de30f120938c17c593fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TXWvbMBR9N+Q/XPBD20Fa2o0xAn7ISNMVtnakGXsYIyjSjS2wJU+Sk/nf98iqswzG" +
                "2MOCQbLvOed+neR5luf0xHt2OvRU41KTtMYHYYJHLNv2gWlx+/7LXXFNueJtVyZYitw/LB+LG6K8ZMNO" +
                "1Kexr/PVQ/EGsYNwRpvfeLer1eOqeEc5O2fdaWQ5X88/FtdvKd+JIOoricq0PCqnipeaazXU94GFYkfV" +
                "cCSFBPTBxZxGNEx5OuyOQsVkrOIx3PgS0Ya9FyXT+HWn60gajsgY4zKq7JxtjsDOyKCtieDx+mdCp014" +
                "fUO1NlF5OP6m/O07Bdtq6QEeLkMHHhwRjk1Q221r7Sv22SQr/vNvkn16upuRD2qDIfmrNOhJBrvAHEo4" +
                "heKDUFgS7bDCSpcVu2myEPzTtKxoiIa+ZX8J4rrSnvC8eKXuqfMABQvLNU1nsGasL2g0esoHUxsS1AoH" +
                "J3S1cMBbp7SJ8J3DYKI6Hs8/OjYStlzMBhuz7IJGQT0UpGPh49LuFzSuA4QsXx/sFK9cwkfH5GnSKJZ/" +
                "tg5LQjHCz5DjVWruEtqYDiOL8nQ+fNvg1V8Qkkgmbq2s6ByVf+5D9eKLvXBabOEqCMPTNVTPIuns4kQ5" +
                "lj3Dvo0d5ZPirxz/IhtVkm7saVphZ3BdSb4rMUAAW2f3WgG67QcRWWs2AdbcOuH6LLJSyixfxhkDBNaw" +
                "EZzCeys1FqDooEN1/ENE5EYrGPIZqCdAFFwEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
