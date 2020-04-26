using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Int32 : IMessage
    {
        public int data;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 4;
    
        public IMessage Create() => new Int32();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "std_msgs/Int32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UkhJLEnkAgAHaI4xCwAAAA==";
                
    }
}
