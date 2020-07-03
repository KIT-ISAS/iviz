using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Char")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Char : IMessage
    {
        [DataMember (Name = "data")] public sbyte Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Char(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Char(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(Buffer b)
        {
            return new Char(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public readonly void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 1;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Char";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1bf77f25acecdedba0e224b162199717";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCxSSEksSeQCADeiGH4KAAAA";
                
    }
}
