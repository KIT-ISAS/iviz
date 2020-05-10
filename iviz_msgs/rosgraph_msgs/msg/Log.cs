using System.Runtime.Serialization;

namespace Iviz.Msgs.rosgraph_msgs
{
    public sealed class Log : IMessage
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
        public std_msgs.Header header { get; set; }
        public byte level { get; set; }
        public string name { get; set; } // name of the node
        public string msg { get; set; } // message 
        public string file { get; set; } // file the message came from
        public string function { get; set; } // function the message came from
        public uint line { get; set; } // line the message came from
        public string[] topics { get; set; } // topic names that the node publishes
    
        /// <summary> Constructor for empty message. </summary>
        public Log()
        {
            header = new std_msgs.Header();
            name = "";
            msg = "";
            file = "";
            function = "";
            topics = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Log(std_msgs.Header header, byte level, string name, string msg, string file, string function, uint line, string[] topics)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.level = level;
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
            this.msg = msg ?? throw new System.ArgumentNullException(nameof(msg));
            this.file = file ?? throw new System.ArgumentNullException(nameof(file));
            this.function = function ?? throw new System.ArgumentNullException(nameof(function));
            this.line = line;
            this.topics = topics ?? throw new System.ArgumentNullException(nameof(topics));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Log(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.level = b.Deserialize<byte>();
            this.name = b.DeserializeString();
            this.msg = b.DeserializeString();
            this.file = b.DeserializeString();
            this.function = b.DeserializeString();
            this.line = b.Deserialize<uint>();
            this.topics = b.DeserializeStringArray(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Log(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.Serialize(this.level);
            b.Serialize(this.name);
            b.Serialize(this.msg);
            b.Serialize(this.file);
            b.Serialize(this.function);
            b.Serialize(this.line);
            b.SerializeArray(this.topics, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (name is null) throw new System.NullReferenceException();
            if (msg is null) throw new System.NullReferenceException();
            if (file is null) throw new System.NullReferenceException();
            if (function is null) throw new System.NullReferenceException();
            if (topics is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 25;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(name);
                size += BuiltIns.UTF8.GetByteCount(msg);
                size += BuiltIns.UTF8.GetByteCount(file);
                size += BuiltIns.UTF8.GetByteCount(function);
                size += 4 * topics.Length;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(topics[i]);
                }
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosgraph_msgs/Log";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "acffd30cd6b6de30f120938c17c593fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61TXWvbMBR996+44Ie2g7S0G6ME/JCRpits7Ugz9jBGkK1rWyBLniQn87/fkdxkGYyx" +
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
