/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Path : IDeserializable<Path>, IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public GeometryMsgs.PoseStamped[] Poses;
    
        /// Constructor for empty message.
        public Path()
        {
            Poses = System.Array.Empty<GeometryMsgs.PoseStamped>();
        }
        
        /// Explicit constructor.
        public Path(in StdMsgs.Header Header, GeometryMsgs.PoseStamped[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// Constructor with buffer.
        internal Path(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Poses = b.DeserializeArray<GeometryMsgs.PoseStamped>();
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new GeometryMsgs.PoseStamped(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Path(ref b);
        
        Path IDeserializable<Path>.RosDeserialize(ref Buffer b) => new Path(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Poses);
        }
        
        public void RosValidate()
        {
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) throw new System.NullReferenceException($"{nameof(Poses)}[{i}]");
                Poses[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Poses);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/Path";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6227e2b7e9cce15051f669a5e197bbf7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC961cM5LC7pUmhLT0Eelgo/TgUUnZvpSwTe2ILbMk7kpN1f32f5I29WSjt" +
                "oY0xWLJm3rw3H1pcO2JVHsjvqPNBAsWaI6l0KkFcDMS04VjTziuW6rc+UvTYNo0/mM/CpSjV+WMq8a1E" +
                "He7aUIVXG8DdRG47Kb//GMGNef+PH/P15tOaQizHmCMfsyAEdiVrSSDEJUfOAmpb1aLLRvbSwClzo3wa" +
                "h07CCo63tQ2EtxInyk0zUB9gBMmFb9ve2YKjULStnPjD0yKV1LFGW/QNK+y9ltYl851yKwkdb5D7Xlwh" +
                "9OXDGjYuSNFHC0IDEAoVDtZVOCTTWxffvE4OZnF78EtspUK2p+BjrUBWHlK5Ek8Oa8R4MYpbARvJEUQp" +
                "A13mf3fYhitCEFCQzhc1XYL5Zoi1dwAU2rNa3jaSgAtkAKgXyeni6glyor0mx84f4UfEOcbfwLoJN2la" +
                "1qhZk9SHvkICYdip39sSptshgxSNRVNSY7fKOpjkNYY0i48pxzCCV64IvhyCLywKUNLBxtqEqAk9V+PO" +
                "lv+rG387BtB5TWmf6WDIdqK5FZ63CiERc5mfTVkGSON0Pv6Z+HQncLSo6eOFkdp+p4IydFzIyzQl6Xf5" +
                "eG6zbZLj1R59VwQR6ObJwHzroV1dxp3tziUQVI6Tj16ObF26BmXmDy0Y7Uz5RK7ZNZ7ju7f0MK2GafXz" +
                "PPTn1B01PL28T/J5Sj7t7ue8435sV+YPio6rgzG/AHCa3/87BgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
