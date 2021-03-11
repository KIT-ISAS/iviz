/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt32")]
    public sealed class UInt32 : IDeserializable<UInt32>, IMessage
    {
        [DataMember (Name = "data")] public uint Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt32()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt32(uint Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt32(ref Buffer b)
        {
            Data = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt32(ref b);
        }
        
        UInt32 IDeserializable<UInt32>.RosDeserialize(ref Buffer b)
        {
            return new UInt32(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "304a39449588c7f8ce2df6e8001c5fce";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACivNzCsxNlJISSxJ5OUCAOXEuTANAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
