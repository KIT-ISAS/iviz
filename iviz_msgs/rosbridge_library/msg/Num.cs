/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = "rosbridge_library/Num")]
    public sealed class Num : IDeserializable<Num>, IMessage
    {
        [DataMember (Name = "num")] public long Num_ { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Num()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Num(long Num_)
        {
            this.Num_ = Num_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Num(ref Buffer b)
        {
            Num_ = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Num(ref b);
        }
        
        Num IDeserializable<Num>.RosDeserialize(ref Buffer b)
        {
            return new Num(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Num_);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/Num";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "57d3c40ec3ac3754af76a83e6e73127a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUcgrzeXiAgCjoYsaCwAAAA==";
                
    }
}
