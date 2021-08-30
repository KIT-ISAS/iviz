/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Int64")]
    public sealed class Int64 : IDeserializable<Int64>, IMessage
    {
        [DataMember (Name = "data")] public long Data;
    
        /// <summary> Constructor for empty message. </summary>
        public Int64()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int64(long Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int64(ref Buffer b)
        {
            Data = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int64(ref b);
        }
        
        Int64 IDeserializable<Int64>.RosDeserialize(ref Buffer b)
        {
            return new Int64(ref b);
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
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int64";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "34add168574510e6e17f5d23ecc077ef";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUUhJLEnkAgBZU74aCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
