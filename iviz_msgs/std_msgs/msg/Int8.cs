using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int8")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Int8 : IMessage
    {
        [DataMember (Name = "data")] public sbyte Data { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Int8(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int8(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(Buffer b)
        {
            return new Int8(b ?? throw new System.ArgumentNullException(nameof(b)));
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
        [Preserve] public const string RosMessageType = "std_msgs/Int8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "27ffa0c9c4b8fb8492252bcad9e5c57b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMK7FQSEksSeTiAgDmSq87CwAAAA==";
                
    }
}
