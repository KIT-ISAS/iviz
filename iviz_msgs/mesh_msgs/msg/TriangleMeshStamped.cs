/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TriangleMeshStamped : IDeserializable<TriangleMeshStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "mesh")] public MeshMsgs.TriangleMesh Mesh;
    
        /// Constructor for empty message.
        public TriangleMeshStamped()
        {
            Mesh = new MeshMsgs.TriangleMesh();
        }
        
        /// Explicit constructor.
        public TriangleMeshStamped(in StdMsgs.Header Header, MeshMsgs.TriangleMesh Mesh)
        {
            this.Header = Header;
            this.Mesh = Mesh;
        }
        
        /// Constructor with buffer.
        public TriangleMeshStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Mesh = new MeshMsgs.TriangleMesh(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TriangleMeshStamped(ref b);
        
        public TriangleMeshStamped RosDeserialize(ref ReadBuffer b) => new TriangleMeshStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Mesh.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Mesh is null) throw new System.NullReferenceException(nameof(Mesh));
            Mesh.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Mesh.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleMeshStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3e766dd12107291d682eb5e6c7442b9d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XXW/bNhR9nn4FAT8kaWOnWIdhyDDsI127PAQo2rwFhUFLVxI3ilRJKrbz63cuKcpO" +
                "6mR9WGo4sETee3h5P098qJadb/zZXyQrcqKNP0VHvk3r105J02i6worg5aL45X/+FFcf350Lf9+SYiY+" +
                "Bmkq6SocG2QlgxS1hYWqacnNNd2ShpLseqpE3A3bnvwCitet8gLfhgw5qfVWDB5CwYrSdt1gVCkDiaBw" +
                "nX19aCojpOilC6octHSQt65ShsVrJztidHw9fR7IlCQu35xDxngqh6Bg0BYIpSPplWmwKYpBmfD6e1Yo" +
                "ZtdrO8crNfDzdLgIrQxsLG16R57tlP4cZ7xIl1sAG84hnFJ5cRzXlnj1JwKHwATqbdmKY1j+fhtaawBI" +
                "4lYibCtNDFzCA0A9YqWjkz1kE6GNNDbDJ8TdGV8DayZcvtO8Rcw0394PDRwIwd7ZW1VBdLWNIKVWZILQ" +
                "auWk2xaslY4sZm/ZxxCCVowIfqX3tlQIQCXWKrSFD47RYzSWqnqubDxcAMVsJt5QrYwKCi6xNbIljPtj" +
                "cWTxS1OpkvzNp0nAixku7QOrTWs/R5coU9EG3tUDpBzVnCBW9NbHczyn5S1xUmL7GA6Ob7RZGus6qf2p" +
                "ULVokH/mpGjIolzcNhn/3iLhYMOkPYvnyTIMUu9WYw528h8SQx8F4l1mtufjpT5/AnVnRTFV8IXV1n14" +
                "98fvO5mSlx4Ryc7IQk8ehr8wOJZFZfq9RsXxuUKeAExDvJYlLbvxHQeT8dYlyctONsTnJqiHIG+heaEH" +
                "D1UIlenJf7tEGzOneDzVjvwUurHB3LyeHKRG9Wey90BwcsdFDwlSGR8zKCdvsrxnOU7j2hGqvYeLi1pb" +
                "GX78QWymp+30dPfsU2ZKwHQmerSbnprpaTU9yedPgP0MzoMjp3vsEAfKR8SaKVbWatFKn8vj2dz3sIxy" +
                "7HELj9ddDkgjBoNxm4eaGqWPX52KVyexswc0uR6DvA5QcxjVnCujXFHcIyRi/MzEuLybn761g8Zw4a72" +
                "eVBj1sWpMsGJQ58JK8+SPSjufBhwaYtxSvw6+RiQdapRMdWTwpdAJUZeuuDTSC83WTmVDMYAVxPw21g/" +
                "cVo8daeX2/sAlV2br1O8u6+IPxunkJYmuuA/ES6TzORO0DQEBdPq3uqIdBHdcGlq+xhcTqkHDGBnBydb" +
                "rVUZHkNgyRW18lbZyEQGFBE6KoE1jOXVUnTsTiUBp+XTzM1OhRm6VQqfs+vcdGFOBXu+0I7LB5VRrENn" +
                "0Ju5akhoangKp7HP3Bac0lZMb6Sj2C0VeIV35VkEXuZtvyh7sCV2+NYOYi1ToviRL6s7OE0YWotMlnhA" +
                "4zp/I7BQc9bPQYid/43piF94O7iSINTQwlCIIUOhV1zD1EmlmcJxP2fDIm42ZFFkPjZZnl3xZ17AtXu1" +
                "Ie3FfC5K0EMD3t6RNNg8ReWgAuOTh9mHA8mRBDfh4WG7GNRMo9LhkR+BueqhorP9DvXQa22K+0+IynKl" +
                "8N9BhXk6Rs7v0c5p79eJvwfq7xn0dtCacwExNA2SABastoGndjwBxCEi7WmMnAuBcGoTd9Od+ejjiP8i" +
                "JtdJsY/wXVa/+YAO8unbDKE9BlTM8sNINEZmlVnGxBFzImi5Il0U/wLYZG8LWA4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
