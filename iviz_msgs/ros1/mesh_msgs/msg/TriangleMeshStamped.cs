/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class TriangleMeshStamped : IDeserializable<TriangleMeshStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "mesh")] public MeshMsgs.TriangleMesh Mesh;
    
        public TriangleMeshStamped()
        {
            Mesh = new MeshMsgs.TriangleMesh();
        }
        
        public TriangleMeshStamped(in StdMsgs.Header Header, MeshMsgs.TriangleMesh Mesh)
        {
            this.Header = Header;
            this.Mesh = Mesh;
        }
        
        public TriangleMeshStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Mesh = new MeshMsgs.TriangleMesh(ref b);
        }
        
        public TriangleMeshStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Mesh = new MeshMsgs.TriangleMesh(ref b);
        }
        
        public TriangleMeshStamped RosDeserialize(ref ReadBuffer b) => new TriangleMeshStamped(ref b);
        
        public TriangleMeshStamped RosDeserialize(ref ReadBuffer2 b) => new TriangleMeshStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Mesh.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Mesh.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Mesh is null) BuiltIns.ThrowNullReference();
            Mesh.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Mesh.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = Mesh.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "mesh_msgs/TriangleMeshStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3e766dd12107291d682eb5e6c7442b9d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XXW/bNhR9168g4Ickbex06zAUGYZ9pEubhwBFmrcgMGjpSuJGkSpJxXZ+/c6lRNlO" +
                "7awPSw0Hlsh7Dy/v54kPxbzxlT/7SLIgJ+r4kzXk63791ilpKk3XWBG8nGW//s+f7Przh3Phdy3JJuJz" +
                "kKaQrsCxQRYySFFaWKiqmtxU0wNpKMmmpULE3bBuyc+geFsrL/CtyJCTWq9F5yEUrMht03RG5TKQCArX" +
                "2daHpjJCila6oPJOSwd56wplWLx0siFGx9fTl45MTuLq/TlkjKe8CwoGrYGQO5JemQqbIuuUCW9/ZAUx" +
                "EXc31v9wn01ul3aKdarg8NEKEWoZ2GpatY48Gyz9OQ571d9yhkPgJcJxhRfHcW2OV38icBpsodbmtTjG" +
                "FT6tQ20NAEk8SMRvoYmBc7gCqEesdHSyhWwitJHGJvgecXPGt8CaEZfvNK0RPM1u8F0FT0KwdfZBFRBd" +
                "rCNIrhWZILRaOOnWGWv1R2aTS3Y2hKAVQ4Nf6b3NFSJRiKUKdeaDY/QYlrkqXiot91dCNpmI91Qqo4KC" +
                "S2yJtAnD/lAlSfzKFConf3c/CnikglY+sNq49kt0iTIFreBd3UHKUckJYkVrfTzHc34+EGcnto/h4PhG" +
                "q7mxrpHanwpVigqJaE6yiizqxq174z9ZJBxsGLUn8TyZh07qzWrMwUb+Q6Jro0C8y8S2fLzU58+gbqzI" +
                "xlK+sNq6mw9//rGRyXnpgEhyRhJ69jD8hc6xLErUb3Usjs818gRgGuKlzGneDO84mIy3rpe8amRFfG4P" +
                "9RTkEpoXuvNQhVDeP/nvl2hD5mSHU+3Ij6EbOs3d29FBalB/IXv3BCe1XvSQIJXxMYNS8vaWtyzHaVw6" +
                "QrW3cHFWaivDzz+J1fi0Hp8eX3zcjAnYn4lm7cananxajE/y5RNgO4PTBEnpHjvEnvIRsWayhbVa1NKn" +
                "8ngx9z0toxR73MLjdZMD0ojOYO6moaYG6eM3p+LNSezsAU2uxUQvA9QcZjbnyiCXZTvMRAyfiRiWN/PT" +
                "17bTGC7c1b50asi6OFVGOLHvM2KlWbIFxZ0PA67fYpwcv04eArJOVSqmeq/wNVCOkddf8Hmk16uk3JcM" +
                "xgBXE/DrWD9xWjx3p9frXYDCLs23KT7uKuLPximkpYku+E+Eq15mdCf4GoKCabWzOiBdRDdcmdIegksp" +
                "9YQBbOzgZCu1ysMhBJZcUC0flI1MpEMRoaMSWMNQXjVFx25UeuB++TRxs1NhumbRh8/ZZWq6MKeAPV9p" +
                "x+W9yijWrjHozVw1JDRVPIX7sc8kF+TSFkxvpKPYLRV4hXf5WQSep20/y1uwJXb42nZiKftE8QNxVo9w" +
                "mjC0FIks8YDGdf5GYKHmrJ+CGTv/O9MRP/O2czlBqKKZoRBDhkIvuIapkUozheN+zoZF3GTILEt8bLQ8" +
                "ueKvtIBrt2pF2ovpVOSghwYEviFpsHmKykEFxicPs/cHkiMJbsLDwzYxqIlG9YdHfgTmqruCzrY71FOv" +
                "1X3c3yEq84XCvwkF5ukQOb9FO8e930YiH6jdMeiy05pzATE0FZIAFizWYZjH78AbItCWwt0NOsB9Yl4I" +
                "h1OrKNTfnA04jqe8iil28n3GzRbXySbpYaAUA4dKfGJkgynkWi5IZ9m/fi3yuksOAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
