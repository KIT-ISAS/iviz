/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [Preserve, DataContract (Name = "octomap_msgs/OctomapWithPose")]
    public sealed class OctomapWithPose : IDeserializable<OctomapWithPose>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The pose of the octree with respect to the header frame 
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin { get; set; }
        // The actual octree msg
        [DataMember (Name = "octomap")] public OctomapMsgs.Octomap Octomap { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public OctomapWithPose()
        {
            Header = new StdMsgs.Header();
            Octomap = new OctomapMsgs.Octomap();
        }
        
        /// <summary> Explicit constructor. </summary>
        public OctomapWithPose(StdMsgs.Header Header, in GeometryMsgs.Pose Origin, OctomapMsgs.Octomap Octomap)
        {
            this.Header = Header;
            this.Origin = Origin;
            this.Octomap = Octomap;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public OctomapWithPose(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Origin = new GeometryMsgs.Pose(ref b);
            Octomap = new OctomapMsgs.Octomap(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new OctomapWithPose(ref b);
        }
        
        OctomapWithPose IDeserializable<OctomapWithPose>.RosDeserialize(ref Buffer b)
        {
            return new OctomapWithPose(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Origin.RosSerialize(ref b);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Octomap is null) throw new System.NullReferenceException(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 56;
                size += Header.RosMessageLength;
                size += Octomap.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "octomap_msgs/OctomapWithPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "20b380aca6a508a657e95526cddaf618";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTWsUQRC9D8x/KMjBRJINqIgEPIgh6kGMJjeRUNtTO9PQ0z129ySuv95X3dldgx6C" +
                "mIQN89FVr6req6rZozf0/JRGnsh6WlrPcU2rEEfOh8SJPpkcRdrmvXAnkYZyaZu22aPLQWgKSSisKOM+" +
                "FFO6sXmgKGkSkymHclTdaBV5FGqbXsIoOa6vxtSn4/OCEW1v/Q6YTZ7ZbTBh1za4D8izOn2qD2qgV3V8" +
                "/Z//2ubjxbsTSrmrISsFmuBFZt9x7AhVcMeZlTEabD9IPHJyLQ5ePE7SUTnN60nSopZmE+HXi5fIzq1p" +
                "TrACTSaM4+yt4SyU7Sh3ANQV8jBNHLM1s+MIhxA76AX7wmvB1/8k32fxRujD6QmsfBIzZ4uk1sAwUThZ" +
                "3+MQxrP1+fkz9YDj5U04wrP0UGqbAeTjrBnLjwmiarKcTjTM01rjAvAgSRCoS7Rf3l3hMR0Q4iALmYIZ" +
                "aB/pn6/zEHxpiGuOlpdOFNmAB8A+UacnB79Da+on5NmHDX6F3AW5D67fAWtZRwPEc0pBmnvwCMsphmvb" +
                "wXa5LijGWfGZnF1GjEPbqFsNCpCz0sRZhSza4MopBWOhRFe6v21Sjhqg6HJluwfszj9HSet8gwFUuVAF" +
                "ZwtuMKJlVMHWSgcqTWzkUJtOX3e357bYgh4dx43vAn1yHtAYW4u2+Tyj2OgL8s7yEctEOttxQmdktj4V" +
                "6bZVoCKMS8n7TtFts3KB88sX9GN3C403tz8frYodidtStqqhp+5Qe7cGffq+k0DXNab/HpVtbm8esMi/" +
                "renak//4nTlz3OuG7MQHLDveeO8Hj5WmrBwHY+bJSncAzmg1O/1w4A17s958QvYXy3y8CJlW1gmWwTIE" +
                "d4tUw7x1GGOyZRbKEqhtpcNREbZjXQd6j75ICg6rFRLoGhoRvbqmUVdPyhtPHzpJO/Yh8a1fxbmtJwl2" +
                "l7M/txNbvQ/1E6HZXEtMOEmLQdnAHscqxqjeRAtWqi2CoOFfff1WVpOi/wLXFY8W4AcAAA==";
                
    }
}
