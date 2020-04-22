
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PoseStamped : IMessage
    {
        // A Pose with reference coordinate frame and timestamp
        public std_msgs.Header header;
        public Pose pose;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseStamped";
    
        public IMessage Create() => new PoseStamped();
    
        public int GetLength()
        {
            int size = 56;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseStamped()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            pose.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            pose.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d3812c3cbc69362b77dc0b19b345f8f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC9C/IfBnLY3dKk0JYeAj0slH4cClt272FijW2BLXmlcbLur++T0jgNPbSH" +
                "do2xJWvmzXvz4SXd0l1IQgenLUWpJYqvhKoQonWeVaiO3Auxt6Sul6TcD+azsJVIbXmZAjDgYRbm/T++" +
                "Fubr/acNJbXbPjXp1THywizpXsGJo6VelC0rUx1AyTWtxFUne+mokBVL5VSnQdIajg+tS4S7ES+Ru26i" +
                "McFIA1T3/ehdlWXPYk/+8HSemAaO6qqx4/hbljI67iSPY8nilw8b2Pgk1agOhCYgVFE4Od/gkMzovL55" +
                "nR3M8uEQVthKg8TOwUlb1kxWnoYoKfPktEGMF0dxa2AjO4IoNtF1+bbFNt0QgoCCDKFq6RrM7yZtgweg" +
                "0J6j410nGbhCBoB6lZ2ubn5BzrQ35NmHE/wR8Rzjb2D9jJs1rVrUrMvq09gggTAcYtg7C9PdVECqzolX" +
                "6twucpxM9jqGNMuPpRM1l69UBG9OKVQOBbClg03SmNFLNbbO/r+GbCSg7+J07Mo8AbknbzFCuU5QwOqQ" +
                "llCXwcidU0eBkoEreZkbLX+2P89dsc0jFqI7+a4Jg4WGmA3MtxFCoy+4Z7vn0wgyi9P8oCOUnU+lZrME" +
                "yMGAFNYXik3dBdZ3b+lpXk3z6vtzKTjnb5YxlwutdJHVS/5593jOPn40/dr8QdRpdYC8Hz4DsiVnBQAA";
                
    }
}
