using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/PoseArray")]
    public sealed class PoseArray : IMessage
    {
        // An array of poses with a header for global reference.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "poses")] public Pose[] Poses { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseArray()
        {
            Header = new StdMsgs.Header();
            Poses = System.Array.Empty<Pose>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseArray(StdMsgs.Header Header, Pose[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseArray(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Poses = b.DeserializeStructArray<Pose>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new PoseArray(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Header);
            b.SerializeStructArray(Poses, 0);
        }
        
        public void Validate()
        {
            if (Header is null) throw new System.NullReferenceException();
            Header.Validate();
            if (Poses is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += 56 * Poses.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "916c28c5764443f268b296bb671b9d97";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTYvbMBC961cM5LC7pUmhLT0Eelgo/TgUtuzeSlkm1tgWyJJ3JCfr/vo+2RunuZQe" +
                "2g2GSNabN2/ejLyi60CsyiPFmvqYJNHB5ZaYWmErSnVUanzcsSeVWlRCJRtjPs+nM8iYG0R+/zETGPP+" +
                "H//M19tPW0rZ3nepSa/m3GZFt5mDZbXUSWbLmSe1rWta0bWXvXgEcdeLpek0j72kDQLvWpcITyNBlL0f" +
                "aUgA5UhV7LohuIqzUHadnMUj0sEu6lmzqwbPCnxU60KB18qdFHY8SR6G4hR9+bAFJiSphuwgaARDpcLJ" +
                "hQaHZAYX8pvXJcCs7g5xja00cHZJTrnlXMTKY6+Sik5OW+R4MRe3ATfMEWSxiS6nd/fYpitCEkiQPlYt" +
                "XUL5zZjbGEAotGd1vPNSiCs4ANaLEnRx9RtzmKgDh3iknxlPOf6GNiy8paZ1i575Un0aGhgIYK9x7yyg" +
                "u3EiqbyTkMm7nbKOpkTNKc3qY/EYIERNHcE/pxQrhwbYaXJNylrYp27cO/u/prGRiKnTcR7JMv8o8Bp3" +
                "pDQJ8jk7ePJ0qcrY1Cooo+dKXpYpK6/t07mbsPCForpj7IZwqzANC8B8G1Clhon3hHuuAiHleHMwC5ld" +
                "SFO3Fv2oBVdjknxWrql95PzuLT0uq3FZ/Xwe+SfrjjUsjcIEnfl5Lr7sHk6+4/vS4ev354qOq4MxvwDa" +
                "GmNpYAUAAA==";
                
    }
}
