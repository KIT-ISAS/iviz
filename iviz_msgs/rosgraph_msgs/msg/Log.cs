/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Log : IMessage, IDeserializable<Log>, System.IEquatable<Log>
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
    
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Log(ref ReadBuffer b)
        {
            Deserialize(ref b, out this);
        }
        
        public static void Deserialize(ref ReadBuffer b, out Log h)
        {
            StdMsgs.Header.Deserialize(ref b, out h.Header);
            h.Level = b.Deserialize<byte>();
            h.Name = b.DeserializeString();
            h.Msg = b.DeserializeString();
            h.File = b.SkipString();
            h.Function = b.SkipString();
            h.Line = b.Deserialize<uint>();
            h.Topics = b.SkipStringArray();
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Log(ref b);
        
        public readonly Log RosDeserialize(ref ReadBuffer b) => new Log(ref b);
        
        public readonly override int GetHashCode() => (Header, Level, Name, Msg, File, Function, Line, Topics).GetHashCode();
        public readonly override bool Equals(object? o) => o is Log s && Equals(s);
        public readonly bool Equals(Log o) => (Header, Level, Name, Msg, File, Function, Line, Topics) == (o.Header, o.Level, o.Name, o.Msg, o.File, o.Function, o.Line, o.Topics);
        public static bool operator==(in Log a, in Log b) => a.Equals(b);
        public static bool operator!=(in Log a, in Log b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
            b.Serialize(Name ?? "");
            b.Serialize(Msg ?? "");
            b.Serialize(File ?? "");
            b.Serialize(Function ?? "");
            b.Serialize(Line);
            b.SerializeArray(Topics ?? System.Array.Empty<string>());
        }
        
        public readonly void RosValidate()
        {
            if (Topics != null)
            {
                for (int i = 0; i < Topics.Length; i++)
                {
                    if (Topics[i] is null) BuiltIns.ThrowNullReference($"{nameof(Topics)}[{i}]");
                }
            }
        }
    
        public readonly int RosMessageLength
        {
            get {
                int size = 25;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Msg);
                size += BuiltIns.GetStringSize(File);
                size += BuiltIns.GetStringSize(Function);
                size += BuiltIns.GetArraySize(Topics);
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
                "H4sIAAAAAAAAE61TwWrcMBC9+ysGdEhS2ISkpYQFH1I2my60Sdls6SGERbZmbYEsuZK8W/99n+yuu6WX" +
                "HmoMkjXz3ozmPQuRCUHPvGevY08GG0OlsyFKGwNiWdFHpsX9h68P+TUJxUVXjWljZPW4fMpviETFlr00" +
                "p7Fvd+vH/B1iB+mttn/g7tfrp3V+S4K9d/40srzb3H3Kr9+T2MkozVWJznQ5MYuh46Vmo4b+PrJU7Kke" +
                "lpFhTAzRp5pWNkxiXNyOYs1kneJjuAkVog2HICum4+lOmwR6WVXWeX6djjtbRu3saajTNr69IaNtQgxL" +
                "qnFkLFPdnXdNJkaSl1eKrtVlQPawGVoLAMk4dUdtVxgdag7Z36ipdJb/5yf7/PwwpxDVFmMJV+NoM/gD" +
                "blDSK9wqSgVVaAfNal3V7GejZ2CYpmVFQzT2LYdLADe1DoT3lzlMT11AUnTwWNN0FrpCr6gxgFM8kNqS" +
                "pFZ6SN8Z6ZHvvNI2pe88BpbY8Qb+3rEt4cPFfPAtl13UaKgHQ+lZhqTbakFHnQDIxObgZvjkCsaZio8K" +
                "oFn+0Xqoh2ZkmKPGm/Fyl+DGcBhVVKDz4WyLz3BBKIIWuHVlTefo/Esfa7gkqbmXXssCbgIxTGzAepZA" +
                "ZxcnzHagttK6I/3I+LvGv9DaiTfdaVZDM5NuH7oKA0Ri691eK6QW/UBSGs02wrOFl77PEmosmYllmjGS" +
                "gBoUwSpDcKWGAIoOOtbTP5Eyt1pl2U9vGjN+TAQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
