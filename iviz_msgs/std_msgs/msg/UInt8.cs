/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt8")]
    public sealed class UInt8 : IDeserializable<UInt8>, IMessage
    {
        [DataMember (Name = "data")] public byte Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public UInt8()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt8(byte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt8(ref Buffer b)
        {
            Data = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt8(ref b);
        }
        
        UInt8 IDeserializable<UInt8>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7c8164229e7d2c17eb95e9231617fdee";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxUEhJLEnk4gIAgcsUlwwAAAA=";
                
    }
}
