/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PoseArray : IDeserializable<PoseArray>, IMessage
    {
        // An array of poses with a header for global reference.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public Pose[] Poses;
    
        /// Constructor for empty message.
        public PoseArray()
        {
            Poses = System.Array.Empty<Pose>();
        }
        
        /// Explicit constructor.
        public PoseArray(in StdMsgs.Header Header, Pose[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// Constructor with buffer.
        public PoseArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Poses);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PoseArray(ref b);
        
        public PoseArray RosDeserialize(ref ReadBuffer b) => new PoseArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Poses);
        }
        
        public void RosValidate()
        {
            if (Poses is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 56 * Poses.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "916c28c5764443f268b296bb671b9d97";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
