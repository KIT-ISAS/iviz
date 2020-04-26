using System.Text;
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
        public std_msgs.Header header;
        public byte level;
        public string name; // name of the node
        public string msg; // message 
        public string file; // file the message came from
        public string function; // function the message came from
        public uint line; // line the message came from
        public string[] topics; // topic names that the node publishes
    
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
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out level, ref ptr, end);
            BuiltIns.Deserialize(out name, ref ptr, end);
            BuiltIns.Deserialize(out msg, ref ptr, end);
            BuiltIns.Deserialize(out file, ref ptr, end);
            BuiltIns.Deserialize(out function, ref ptr, end);
            BuiltIns.Deserialize(out line, ref ptr, end);
            BuiltIns.Deserialize(out topics, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(level, ref ptr, end);
            BuiltIns.Serialize(name, ref ptr, end);
            BuiltIns.Serialize(msg, ref ptr, end);
            BuiltIns.Serialize(file, ref ptr, end);
            BuiltIns.Serialize(function, ref ptr, end);
            BuiltIns.Serialize(line, ref ptr, end);
            BuiltIns.Serialize(topics, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 25;
                size += header.RosMessageLength;
                size += Encoding.UTF8.GetByteCount(name);
                size += Encoding.UTF8.GetByteCount(msg);
                size += Encoding.UTF8.GetByteCount(file);
                size += Encoding.UTF8.GetByteCount(function);
                size += 4 * topics.Length;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(topics[i]);
                }
                return size;
            }
        }
    
        public IMessage Create() => new Log();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "rosgraph_msgs/Log";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "acffd30cd6b6de30f120938c17c593fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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
