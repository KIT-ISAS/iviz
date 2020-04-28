using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Char : IMessage
    {
        public sbyte data;
    
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
    
        public IMessage Create() => new Char();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "std_msgs/Char";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "1bf77f25acecdedba0e224b162199717";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCxSSEksSeQCADeiGH4KAAAA";
                
    }
}
