/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Num : IDeserializable<Num>, IMessage
    {
        [DataMember (Name = "num")] public long Num_;
    
        /// Constructor for empty message.
        public Num()
        {
        }
        
        /// Explicit constructor.
        public Num(long Num_)
        {
            this.Num_ = Num_;
        }
        
        /// Constructor with buffer.
        public Num(ref ReadBuffer b)
        {
            b.Deserialize(out Num_);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Num(ref b);
        
        public Num RosDeserialize(ref ReadBuffer b) => new Num(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
