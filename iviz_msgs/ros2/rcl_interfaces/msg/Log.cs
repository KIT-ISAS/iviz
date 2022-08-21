/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Log : IMessage, IDeserializable<Log>
    {
        //#
        //# Severity level constants
        //# 
        //# These logging levels follow the Python Standard
        //# https://docs.python.org/3/library/logging.html#logging-levels
        //# And are implemented in rcutils as well
        //# https://github.com/ros2/rcutils/blob/35f29850064e0c33a4063cbc947ebbfeada11dba/include/rcutils/logging.h#L164-L172
        //# This leaves space for other standard logging levels to be inserted in the middle in the future,
        //# as well as custom user defined levels.
        //# Since there are several other logging enumeration standard for different implementations,
        //# other logging implementations may need to provide level mappings to match their internal implementations.
        //#
        // Debug is for pedantic information, which is useful when debugging issues.
        public const byte DEBUG = 10;
        // Info is the standard informational level and is used to report expected
        // information.
        public const byte INFO = 20;
        // Warning is for information that may potentially cause issues or possibly unexpected
        // behavior.
        public const byte WARN = 30;
        // Error is for information that this node cannot resolve.
        public const byte ERROR = 40;
        // Information about a impending node shutdown.
        public const byte FATAL = 50;
        //#
        //# Fields
        //#
        // Timestamp when this message was generated by the node.
        [DataMember (Name = "stamp")] public time Stamp;
        // Corresponding log level, see above definitions.
        [DataMember (Name = "level")] public byte Level;
        // The name representing the logger this message came from.
        [DataMember (Name = "name")] public string Name;
        // The full log message.
        [DataMember (Name = "msg")] public string Msg;
        // The file the message came from.
        [DataMember (Name = "file")] public string File;
        // The function the message came from.
        [DataMember (Name = "function")] public string Function;
        // The line in the file the message came from.
        [DataMember (Name = "line")] public uint Line;
    
        public Log(time Stamp, byte Level, string Name, string Msg, string File, string Function, uint Line)
        {
            this.Stamp = Stamp;
            this.Level = Level;
            this.Name = Name;
            this.Msg = Msg;
            this.File = File;
            this.Function = Function;
            this.Line = Line;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Log(ref ReadBuffer b)
        {
            Deserialize(ref b, out this);
        }
        
        public static void Deserialize(ref ReadBuffer b, out Log h)
        {
            b.Deserialize(out h.Stamp);
            b.Deserialize(out h.Level);
            b.DeserializeString(out h.Name);
            b.DeserializeString(out h.Msg);
            b.DeserializeString(out h.File);
            b.DeserializeString(out h.Function);
            b.Deserialize(out h.Line);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Log(ref ReadBuffer2 b)
        {
            Deserialize(ref b, out this);
        }
        
        public static void Deserialize(ref ReadBuffer2 b, out Log h)
        {
            b.Align4();
            b.Deserialize(out h.Stamp);
            b.Deserialize(out h.Level);
            b.Align4();
            b.DeserializeString(out h.Name);
            b.Align4();
            b.DeserializeString(out h.Msg);
            b.Align4();
            b.DeserializeString(out h.File);
            b.Align4();
            b.DeserializeString(out h.Function);
            b.Align4();
            b.Deserialize(out h.Line);
        }
        
        public readonly Log RosDeserialize(ref ReadBuffer b) => new Log(ref b);
        
        public readonly Log RosDeserialize(ref ReadBuffer2 b) => new Log(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Stamp);
            b.Serialize(Level);
            b.Serialize(Name ?? "");
            b.Serialize(Msg ?? "");
            b.Serialize(File ?? "");
            b.Serialize(Function ?? "");
            b.Serialize(Line);
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Stamp);
            b.Serialize(Level);
            b.Serialize(Name ?? "");
            b.Serialize(Msg ?? "");
            b.Serialize(File ?? "");
            b.Serialize(Function ?? "");
            b.Serialize(Line);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength
        {
            get {
                int size = 29;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Msg);
                size += WriteBuffer.GetStringSize(File);
                size += WriteBuffer.GetStringSize(Function);
                return size;
            }
        }
        
        public readonly int Ros2MessageLength => AddRos2MessageLength(0);
        
        public readonly int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 8; // Stamp
            c += 1; // Level
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Name);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Msg);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, File);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Function);
            c = WriteBuffer2.Align4(c);
            c += 4; // Line
            return c;
        }
    
        public const string MessageType = "rcl_interfaces/Log";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2e550226619c297add0debbcdcf7d29b";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE31US0/jMBC+91dY6hWatimvlXroLrBCQrAqrDiuHHuSWHLsyI92++93xnFDQVoOqDie" +
                "7zHfTDKdTqZT9gI7cCocmMZ/NBPW+MBN8HRHf68teGDaNo0yzVDkWW21tnsWWmC/DqG1hr0gSHInCdKG" +
                "0PtvRSGt8LM+3c+sa4qy0Kpy3B2KTDdrQ6en+XA+cBPBxkjGHTDV9Ro6MAEkU4Y5EYNCde7ZHrQ+lWpU" +
                "aGM1E7YrnPXLIpcWlbZVUV7Uy5vri/n8cgVzUZZ8Nb8sRSVuVldQVTVwyRcLWfFCGaGjhBE92pw+Li5X" +
                "54+Lq+UQifKYBN+BZ77nAjAPxyym4ZjPOXxOLFhWYUPGg8vdUHidklLD8VTHEB2ckURukX5F9MF2LCKS" +
                "SaiVQfhAOkvzQ9NAcMyLMvM0T66znaMLMLHDx0HhqEaL5FqqukaoCe9hpyqfbHwk+VTBOn5gBtAONtc7" +
                "u1MS8hZ1vO8RkdrueBAtGVQOGw3gDLr7REWdTCZTdgtVRB2frPUgcRGVQBQeu1R5xvatQjoswUDqqPEM" +
                "BnNB3ODR+wjIVx0CsNu7779/rhdzon5AEoJR0GMCJ8xoavCOV5k+Neagty4w+NuDwMkh0wko6zw83T+v" +
                "l0nmjTsz+Eg9nNSiMg8ps94G7FxxrQ9McBTKrhk1bb1XFV5EcyJZQct3yrqs97bZPq3LpHfnHMn8Ry3Q" +
                "phqLcxHcGBuwG2/1DjLP3Xb7vF2vxnyOWF7ZGBinKYGR1E7i8G0M0u6PXd9vXjeP6wtCpy/JvQItfZ7k" +
                "q+oAY+76YUDJCD7xvAG2x61uwNA+YsbVIc2EFJA4Kh2U+ZMWpcZXyxfExBIV8f6wDnvo7WALN3OY2hnu" +
                "PZDvHQwvicp7FZHpeqhJtkiJIyGOFXloDEhD+rTkuOsfjAqqrJ3tZhMfXMoBnxx5cPt0cpDLx6LON2ON" +
                "0jC86f+lpJJ3SiPy9L6E5LIjTONHYfyIfKFIYZTLVD6Z/ANxvulQ/wUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}