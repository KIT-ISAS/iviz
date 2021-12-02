/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        [DataMember (Name = "name")] public string Name; // name of the node
        [DataMember (Name = "msg")] public string Msg; // message 
        [DataMember (Name = "file")] public string File; // file the message came from
        [DataMember (Name = "function")] public string Function; // function the message came from
        [DataMember (Name = "line")] public uint Line; // line the message came from
        [DataMember (Name = "topics")] public string[] Topics; // topic names that the node publishes
    
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
        internal Log(ref Buffer b)
        {
            Deserialize(ref b, out this);
        }
        
        internal static void Deserialize(ref Buffer b, out Log h)
        {
            StdMsgs.Header.Deserialize(ref b, out h.Header);
            h.Level = b.Deserialize<byte>();
            h.Name = b.DeserializeString();
            h.Msg = b.DeserializeString();
            h.File = b.DeserializeString();
            h.Function = b.DeserializeString();
            h.Line = b.Deserialize<uint>();
            h.Topics = b.SkipStringArray();
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b) => new Log(ref b);
        
        readonly Log IDeserializable<Log>.RosDeserialize(ref Buffer b) => new Log(ref b);
        
        public override readonly int GetHashCode() => (Header, Level, Name, Msg, File, Function, Line, Topics).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Log s && Equals(s);
        
        public readonly bool Equals(Log o) => (Header, Level, Name, Msg, File, Function, Line, Topics) == (o.Header, o.Level, o.Name, o.Msg, o.File, o.Function, o.Line, o.Topics);
        
        public static bool operator==(in Log a, in Log b) => a.Equals(b);
        
        public static bool operator!=(in Log a, in Log b) => !a.Equals(b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
            b.Serialize(Name ?? string.Empty);
            b.Serialize(Msg ?? string.Empty);
            b.Serialize(File ?? string.Empty);
            b.Serialize(Function ?? string.Empty);
            b.Serialize(Line);
            b.SerializeArray(Topics ?? System.Array.Empty<string>());
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
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Msg);
                size += BuiltIns.GetStringSize(File);
                size += BuiltIns.GetStringSize(Function);
                size += BuiltIns.GetArraySize(Topics);
                return size;
            }
        }
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosgraph_msgs/Log";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "acffd30cd6b6de30f120938c17c593fb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TXWvbMBR996+44Ie2g7S0G6ME/JCRpits7Ugz9jBGkK0bWyBLniQn87/fkVVnGYyx" +
                "hwWDZN9zzv06yfMsz+mZ9+xUGEjjoqmyxgdhgkcsK4fAtLx79/m+uKZcctnXCZYiD4+rp+KGKK/ZsBP6" +
                "NPZlsX4s3iB2EM4o8xvvbr1+Whe3lLNz1p1GVovN4kNx/ZbynQhCX1WoTFVH5VTxSrGWY33vWUh21IxH" +
                "UkhAH1zMaUTLlKfD7ig0TMZKnsKtrxFt2XtRM01fd0pH0nhExhSvosrO2fYI7E0VlDURPF3/TOiVCa9v" +
                "SCsTlcfjb8pfv1Gwnao8wONl7MCDI8KxCer6UivfsM+y4j//so/P93PyQW4xIn+VxpzBK3CGFE6i8iAk" +
                "NkQ77K9RdcNulvwD87QdSxqjYejYX4K4aZQnPC9G0QP1HqBg4be27Q12jN0FhS5P+WAqQ4I64WCDXgsH" +
                "vHVSmQjfOUwlquPx/L1nU8GTy/noYa76oFDQAIXKsfBxYw9LmnYBQpZvDnaGV65homPyNGYUyz86hw2h" +
                "GOHnyPEqNXcJbQyHkUV6Oh+/bfHqLwhJUAJ3tmroHJV/GkLzYoq9cEqUsBSEYWgN1bNIOrs4UY5lz7Fs" +
                "Yyf5pPgrx7/IRpWkG3uaNdgZLFeT72sMEMDO2b2SgJbDKFJpxSbAl6UTbsgiK6XM8lWcMUBgjRvBKby3" +
                "lcICJB1UaI7/hojcKpllPwGDKOyFWAQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
