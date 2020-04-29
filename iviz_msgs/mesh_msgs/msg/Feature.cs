using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class Feature : IMessage
    {
        public geometry_msgs.Point location;
        public std_msgs.Float32[] descriptor;
    
        /// <summary> Constructor for empty message. </summary>
        public Feature()
        {
            descriptor = System.Array.Empty<std_msgs.Float32>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            location.Deserialize(ref ptr, end);
            BuiltIns.DeserializeArray(out descriptor, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            location.Serialize(ref ptr, end);
            BuiltIns.SerializeArray(descriptor, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 28;
                size += 4 * descriptor.Length;
                return size;
            }
        }
    
        public IMessage Create() => new Feature();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/Feature";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "ac711cf3ef6eb8582240a7afe5b9a573";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7XQsQrCQAwG4D1PEfABhFYcBFedBMFuIhKuaRtoL8clg/XptRQ66KqZ/vxLPtKyDux5" +
                "vA/W2vqsEh17DeSiEczruT/0Sl4W1xvWbCFLcs0A+x8PnC7HHbbfIlhh1Ylh0Ogk0dA7xqQmkxK1QXpv" +
                "k1wiNpkZLVFgaCb1doOPJY1Lev6L//mz+WJZYE1O8AKMerHtbwEAAA==";
                
    }
}
