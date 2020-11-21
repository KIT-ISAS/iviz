/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/PoseStamped")]
    public sealed class PoseStamped : IDeserializable<PoseStamped>, IMessage
    {
        // A Pose with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "pose")] public Pose Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseStamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseStamped(StdMsgs.Header Header, in Pose Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PoseStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Pose = new Pose(ref b);
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
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 56;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d3812c3cbc69362b77dc0b19b345f8f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC9G/wfBvawu6VJoS09BHpYKP04FFJ272FijW2BLXklOVn31/eN0tgNveyh" +
                "XRPbkjXz5r35yBXd0dZHoaNNLQWpJYirhCrvg7GOk1AduBdiZyjZXmLifiiLr8JGArX5VRYZYsCjLMri" +
                "4z++yuL7/ZcNxWR2fWzim1Pssrii+wRaHAz1kthwYqo9SNmmlbDq5CAdZb5iKJ+maZC4Vs+H1kbCrxEn" +
                "gbtuojHCKnko7/vR2Uqlz4LPAOpqHTENHJKtxo7DX6nK+HpHeRxzMr992sDKRanGZEFqAkYVhKN1DQ5h" +
                "PFqX3r1VDzg+HP0Ke2mQ4JkBpZaTMpanIUhUshw3GubVSeMa8EiSIJCJdJO/7bCNt4Q4YCGDr1q6Af3t" +
                "lFrvgCh04GB534kiV8gDYK/V6fr2T2ilviHHzp/xT5BLkOfgugVYZa1aFK/TFMSxQR5hOQR/sAa2+ymj" +
                "VJ0Vl6iz+8BhKgt1OwUFyOfcl0kLmWuDN8foK4tKmNzPZRFT0AC5Ljtr/mN3NuLRhGE6tajOg+q8w0hp" +
                "uaCCk0VufJ3HRJuoDgIxA1fyWptOP5vf5zbb6sj5YM++a/TJ1qMxZouy+DFCbHAZebF8QZmgM48TOiOx" +
                "dTGXblYBRRiXzPtCdFnUnef04T09LUvU+Lz8+WIqliTOUuaqoacuUnupQXePSwnw59Nj+p+h7Lw8qvUv" +
                "+Cmy8oQFAAA=";
                
    }
}
