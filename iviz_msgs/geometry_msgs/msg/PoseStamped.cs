/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PoseStamped")]
    public sealed class PoseStamped : IDeserializable<PoseStamped>, IMessage
    {
        // A Pose with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "pose")] public Pose Pose;
    
        /// <summary> Constructor for empty message. </summary>
        public PoseStamped()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseStamped(in StdMsgs.Header Header, in Pose Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PoseStamped(ref b);
        }
        
        PoseStamped IDeserializable<PoseStamped>.RosDeserialize(ref Buffer b)
        {
            return new PoseStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 56 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d3812c3cbc69362b77dc0b19b345f8f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC9C/IfBnLY3dKk0JYeAj0slH4cClt272FijW2BLXmlcbLur++T0jgNPbSH" +
                "do2xJWvmzXvz4SXd0l1IQgenLUWpJYqvhKoQonWeVaiO3Auxt6Sul6TcD+azsJVIbXmZAjDgYRbm/T++" +
                "Fubr/acNJbXbPjXp1THywizpXsGJo6VelC0rUx1AyTWtxFUne+mokBVL5VSnQdIajg+tS4S7ES+Ru26i" +
                "McFIA1T3/ehdlWXPYk/+8HSemAaO6qqx4/hbljI67iSPY8nilw8b2Pgk1agOhCYgVFE4Od/gkMzovL55" +
                "nR3M8uEQVthKg8TOwUlb1kxWnoYoKfPktEGMF0dxa2AjO4IoNtF1+bbFNt0QgoCCDKFq6RrM7yZtgweg" +
                "0J6j410nGbhCBoB6lZ2ubn5BzrQ35NmHE/wR8Rzjb2D9jJs1rVrUrMvq09gggTAcYtg7C9PdVECqzolX" +
                "6twucpxM9jqGNMuPpRM1l69UBG9OKVQOBbClg03SmNFLNbbO/r+GbCSg7+J07Mo8AbknbzFCuU5QwOqQ" +
                "llCXwcidU0eBkoEreZkbLX+2P89dsc0jFqI7+a4Jg4WGmA3MtxFCoy+4Z7vn0wgyi9P8oCOUnU+lZrME" +
                "yMGAFNYXik3dBdZ3b+lpXk3z6vtzKTjnb5YxlwutdJHVS/5593jOPn40/dr8QdRpdYC8Hz4DsiVnBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
