using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    public sealed class Empty : IMessage
    {
        /// <summary> Constructor for empty message. </summary>
        public Empty()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Empty(Buffer b)
        {
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Empty(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 0;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Empty";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+MCAJMG1zIBAAAA";
                
    }
}
