/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Duration")]
    public sealed class Duration : IDeserializable<Duration>, IMessage
    {
        [DataMember (Name = "data")] public duration Data;
    
        /// <summary> Constructor for empty message. </summary>
        public Duration()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Duration(duration Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Duration(ref Buffer b)
        {
            Data = b.Deserialize<duration>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Duration(ref b);
        }
        
        Duration IDeserializable<Duration>.RosDeserialize(ref Buffer b)
        {
            return new Duration(ref b);
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
        [Preserve] public const string RosMessageType = "std_msgs/Duration";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3e286caf4241d664e55f3ad380e2ae46";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPU0hJLEnk4gIAtVhIcg8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
