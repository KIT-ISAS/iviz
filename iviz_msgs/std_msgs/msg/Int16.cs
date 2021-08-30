/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Int16")]
    public sealed class Int16 : IDeserializable<Int16>, IMessage
    {
        [DataMember (Name = "data")] public short Data;
    
        /// <summary> Constructor for empty message. </summary>
        public Int16()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int16(short Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int16(ref Buffer b)
        {
            Data = b.Deserialize<short>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int16(ref b);
        }
        
        Int16 IDeserializable<Int16>.RosDeserialize(ref Buffer b)
        {
            return new Int16(ref b);
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
        [Preserve] public const string RosMessageType = "std_msgs/Int16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8524586e34fbd7cb1c08c5f5f1ca0e57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE0U0hJLEnk4gIAJDs+BgwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
