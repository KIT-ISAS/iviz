/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [DataContract (Name = "rosgraph_msgs/Log")]
    public sealed class Log : IDeserializable<Log>, IMessage
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "level")] public byte Level { get; set; }
        [DataMember (Name = "name")] public string Name { get; set; } // name of the node
        [DataMember (Name = "msg")] public string Msg { get; set; } // message 
        [DataMember (Name = "file")] public string File { get; set; } // file the message came from
        [DataMember (Name = "function")] public string Function { get; set; } // function the message came from
        [DataMember (Name = "line")] public uint Line { get; set; } // line the message came from
        [DataMember (Name = "topics")] public string[] Topics { get; set; } // topic names that the node publishes
    
        /// <summary> Constructor for empty message. </summary>
        public Log()
        {
            Header = new StdMsgs.Header();
            Name = "";
            Msg = "";
            File = "";
            Function = "";
            Topics = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Log(StdMsgs.Header Header, byte Level, string Name, string Msg, string File, string Function, uint Line, string[] Topics)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Log(ref b);
        }
        
        Log IDeserializable<Log>.RosDeserialize(ref Buffer b)
        {
            return new Log(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
            b.Serialize(Name);
            b.Serialize(Msg);
            b.Serialize(File);
            b.Serialize(Function);
            b.Serialize(Line);
            b.SerializeArray(Topics, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Msg is null) throw new System.NullReferenceException(nameof(Msg));
            if (File is null) throw new System.NullReferenceException(nameof(File));
            if (Function is null) throw new System.NullReferenceException(nameof(Function));
            if (Topics is null) throw new System.NullReferenceException(nameof(Topics));
            for (int i = 0; i < Topics.Length; i++)
            {
                if (Topics[i] is null) throw new System.NullReferenceException($"{nameof(Topics)}[{i}]");
            }
        }
    
        public int RosMessageLength
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
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosgraph_msgs/Log";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "acffd30cd6b6de30f120938c17c593fb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TwWrbQBC9G/wPAzokKTghaSnF4IOL4zTQJsV26aEUs9odSQvSrrq7squ/z1tJtuND" +
                "Sw81NrPSvPdmduY5ScajJKE179jp0FKJQ0nSGh+ECT4mx6O0DUyL+4/fHma3lChOm7wHDqnHp+Xz7I4o" +
                "ydmwE+VZ8vt89TR7h+ReOKPNOfN+tXpezT5Qws5Zd5Zazjfzz7Pb95RkIojyRqI/LU/iQ+dLzaUa+vzE" +
                "QrGjoguDzID2wcXaRlRMSR9sRqFgMlbxMV/5HOmKvRc50/F1pstI60LkHAAy6mTOVidkY2TQ1kT04fgH" +
                "RqNNeHtHpTZRuwt/1f7xk4KttfRAd4fuGh4kEY43obpJS+0LxkjGo9l//oxHX9YPU/JBbTEpf9MPHMOn" +
                "NeyihFNoPwiFhVGGfRY6L9hNelPBUVXNirpsaGv215G5KbQnfAfvlC01Hqhg4cKqagx2jjUGjau+FohU" +
                "bUhQLRx80ZTCgWCd0ibiM4fZdPrx5/lXw0bCqYtpZ26WTdBoqoWGdCx83N3jgo5bAQPEzd5O8Mw5XHXs" +
                "oB84OubftcOy0JHw01jmTX/Ha8hjSIxCytNl926LR39FqIMuuLayoEu0/7UNxeCQnXBapDAYlGHzErIX" +
                "kXRx9Vo6tj7F4o096PeSpyL/ohtVBuF4rUmB5cGAOfkmxxyBrJ3daQVs2nYqstRsAlyaOuHa8SjS+qIQ" +
                "WcZhAwZetxtE4b2VGptQtNehOP0/InSrVXTnC3Zg1tl8BAAA";
                
    }
}
