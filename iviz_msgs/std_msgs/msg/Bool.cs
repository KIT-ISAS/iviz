using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Bool : IMessage
    {
        public bool data;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out data, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(data, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 1;
    
        public IMessage Create() => new Bool();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "std_msgs/Bool";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "8b94c1b53db61fb6aed406028ad6332a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vKz89RSEksSeQCAGFR0NcKAAAA";
                
    }
}
