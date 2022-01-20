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
        /// Debug level
        public const byte DEBUG = 1;
        /// General level
        public const byte INFO = 2;
        /// Warning level
        public const byte WARN = 4;
        /// Error level
        public const byte ERROR = 8;
        /// Fatal/critical level
        public const byte FATAL = 16;
        //#
        //# Fields
        //#
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "level")] public byte Level;
        /// Name of the node
        [DataMember (Name = "name")] public string Name;
        /// Message
        [DataMember (Name = "msg")] public string Msg;
        /// File the message came from
        [DataMember (Name = "file")] public string File;
        /// Function the message came from
        [DataMember (Name = "function")] public string Function;
        /// Line the message came from
        [DataMember (Name = "line")] public uint Line;
        //string[] topics # topic names that the node publishes
        /// [Ignore]
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
            h.File = b.DeserializeString();
            h.Function = b.DeserializeString();
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
                "H4sIAAAAAAAAE61TXWvbMBR996+4oIe2g7S0G2ME8pCRpgts7Ugz9lBKUKwbWyBLniQn87/fkdxkGevD" +
                "HhYMkn3POffrRIhCCHrkHXsdezK4GCqdDVHaGBArNn1kmt1+/HY3uSaheNNVA2yILO7nD5MbIlGxZS/N" +
                "aez7dHk/eYfYXnqr7R+82+XyYTn5QIK9d/40Mp+upp8n1+9JbGWU5qpEZbo8Kotc8VyzUbm+TywVe6rz" +
                "MSgMwBB9ymllwySGw20p1kzWKT6Em1Ah2nAIsmI6fN1qk0j5SIxDvEwqW++aI7CzZdTOJvDh+jqh0za+" +
                "vSGjbVLOx+tAMUg/PVN0rS4D0PmSWwggyXjsgtpuY3SoORR/s54WlXWen4ti8p9/xZfHuzGFqNYYX7ga" +
                "VlDAR3CNkl6hqygVtkdb7LbWVc1+NHgLxmpaVpSjsW85XIK4qnUgPC8mMj11AaDo4MWm6Sz2j71GjQGc" +
                "8sHUliS10sMinZEeeOeVtgm+9RhYUscT+EfHtoRfZ+Psby67qFFQD4XSswxpm4sZHfYEQiFWezfCK1cw" +
                "2DH5sAEUyz9bj+2hGBnGyPFmaO4S2hgOI4sKdJ6/rfEaLghJUAK3rqzpHJV/7WP9Ypid9FpuYDcIw+wG" +
                "qmeJdHZxomyztJXWHeQHxd85/kXWHnVTT6MaOzOp+9BVGCCArXc7rQDd9FmkNJpthGc3Xvq+SKwhZSHm" +
                "acYAgZU3glOG4EqNBSja61gf/ykJudaqKH4BX5ZnFnQEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
