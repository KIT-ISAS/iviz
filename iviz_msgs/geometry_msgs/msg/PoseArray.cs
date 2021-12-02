/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal PoseArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Poses = b.DeserializeStructArray<Pose>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PoseArray(ref b);
        
        PoseArray IDeserializable<PoseArray>.RosDeserialize(ref Buffer b) => new PoseArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Poses);
        }
        
        public void RosValidate()
        {
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 56 * Poses.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "916c28c5764443f268b296bb671b9d97";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC961cM5LC7pUmhLT0Eelgo/TgUtuzeSlkm1tgWyJJ3JCfr/vo+2RunuZQe" +
                "2g2GSNabN2/ejLyi60CsyiPFmvqYJNHB5ZaYWmErSnVUanzcsSeVWlRCJRtjPs+nM8iYG0R+/zETGPP+" +
                "H//M19tPW0rZ3nepSa/m3GZFt5mDZbXUSWbLmSe1rWta0bWXvXgEcdeLpek0j72kDQLvWpcITyNBlL0f" +
                "aUgA5UhV7LohuIqzUHadnMUj0sEu6lmzqwbPCnxU60KB18odrFkVWJKHoThFXz5sgQlJqiE7CBrBUKlw" +
                "cqHBIZnBhfzmdQkwq7tDXGMrDZxdklNuORex8tirpKKT0xY5XszFbcANcwRZbKLL6d09tumKkAQSpI9V" +
                "S5dQfjPmNgYQCu1ZHe+8FOIKDoD1ogRdXP3GXGRvKXCIR/qZ8ZTjb2jDwltqWrfomS/Vp6GBgQD2GvfO" +
                "ArobJ5LKOwmZvNsp62hK1JzSrD4WjwFC1NQR/HNKsXJogJ0m16SshX3qxr2z/2saG4mYOh3nkSzzjwKv" +
                "cUdKkyCfs4MnT5eqjE2tgjJ6ruRlmbLy2j6duwkLXyiqO8ZuCLcK07AAzLcBVWqYeE+45yoQUo43B7OQ" +
                "2YU0dWvRj1pwNSbJZ+Wa2kfO797S47Ial9XP55F/su5Yw9IoTNCZn+fiy+7h5Du+Lx2+fn+u6Lg6GPML" +
                "2hpjaWAFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
