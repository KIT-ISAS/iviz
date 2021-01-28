/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [Preserve, DataContract (Name = "rosgraph_msgs/Log")]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Log : IMessage, System.IEquatable<Log>, IDeserializable<Log>
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; }
        [DataMember (Name = "level")] public byte Level { get; }
        [DataMember (Name = "name")] public string Name { get; } // name of the node
        [DataMember (Name = "msg")] public string Msg { get; } // message 
        [DataMember (Name = "file")] public string File { get; } // file the message came from
        [DataMember (Name = "function")] public string Function { get; } // function the message came from
        [DataMember (Name = "line")] public uint Line { get; } // line the message came from
        [DataMember (Name = "topics")] public string[] Topics { get; } // topic names that the node publishes
    
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
        
        public readonly void RosValidate()
        {
            for (int i = 0; i < Topics.Length; i++)
            {
                if (Topics[i] is null) throw new System.NullReferenceException($"{nameof(Topics)}[{i}]");
            }
        }
    
        public readonly int RosMessageLength
        {
            get {
                int size = 25;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Msg);
                size += BuiltIns.UTF8.GetByteCount(File);
                size += BuiltIns.UTF8.GetByteCount(Function);
                size += 4 * Topics.Length;
                foreach (string s in Topics)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
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
                "H4sIAAAAAAAAA61TXWvbMBR996+44Ie2g7S0G6ME/JCRpits7Ugz9jBGkK1rWyBLniQn87/fkdxkGYyx" +
                "hwWDZN9zzv06yfMsz+mZd+xUGEnjoqmyxgdhgkcsK8fAtLx79/m+uKZccjk0E2yKPDyunooborxhw07o" +
                "09iXxfqxeIPYXjijzG+8u/X6aV3cUs7OWXcaWS02iw/F9VvKaxGEvqpQmaqOynmqeKVYy1TfexaSHbXp" +
                "mBQmoA8u5jSiY8qnw9YUWiZjJR/CnW8Q7dh70TAdvtZKR1I6IuMQr6JK7Wx3BA6mCsqaCD5c/0wYlAmv" +
                "b0grE5XT8Tflr98o2F5VHuB0SR14cEQ4NkH9UGrlW/ZZVvznX/bx+X5OPsgtRuSvpjFn8AqcIYWTqDwI" +
                "iQ1Rjf21qmnZzSb/wDxdz5JSNIw9+0sQN63yhOfFKHqkwQMULPzWdYPBjrG7oNDlKR9MZUhQLxxsMGjh" +
                "gLdOKhPhtcNUojoez98HNhU8uZwnD3M1BIWCRihUjoWPG3tY0mEXIGT5Zm9neOUGJjomn8aMYvlH77Ah" +
                "FCP8HDleTc1dQhvDYWSRns7Tty1e/QUhCUrg3lYtnaPyT2NoX0yxE06JEpaCMAytoXoWSWcXJ8omSRth" +
                "7EF+UvyV419kzVE39jRrsTMdu/dDgwEC2Du7UxLQckwilVZsAnxZOuHGLLKmlFm+ijMGCKy0EZzCe1sp" +
                "LEDSXoX2+G+IyK2SWfYTgyjshVgEAAA=";
                
    }
}
