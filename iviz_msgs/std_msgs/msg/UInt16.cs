/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt16")]
    public sealed class UInt16 : IDeserializable<UInt16>, IMessage
    {
        [DataMember (Name = "data")] public ushort Data;
    
        /// <summary> Constructor for empty message. </summary>
        public UInt16()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt16(ushort Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt16(ref Buffer b)
        {
            Data = b.Deserialize<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt16(ref b);
        }
        
        UInt16 IDeserializable<UInt16>.RosDeserialize(ref Buffer b)
        {
            return new UInt16(ref b);
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
        [Preserve] public const int RosFixedMessageLength = 2;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1df79edf208b629fe6b81923a544552d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNFNISSxJ5OICAF50RNUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
