/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Time")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Time : IMessage, System.IEquatable<Time>, IDeserializable<Time>
    {
        [DataMember (Name = "data")] public time Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Time(time Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Time(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Time(ref b);
        }
        
        readonly Time IDeserializable<Time>.RosDeserialize(ref Buffer b)
        {
            return new Time(ref b);
        }
        
        public override readonly int GetHashCode() => (Data).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Time s && Equals(s);
        
        public readonly bool Equals(Time o) => (Data) == (o.Data);
        
        public static bool operator==(in Time a, in Time b) => a.Equals(b);
        
        public static bool operator!=(in Time a, in Time b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 8;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Time";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cd7166c74c552c311fbcc2fe5a7bc289";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvJzE1VSEksSeTiAgBuylFyCwAAAA==";
                
    }
}
