/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshGeometryStamped : IDeserializable<MeshGeometryStamped>, IMessageRos1
    {
        // Mesh Geometry Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_geometry")] public MeshMsgs.MeshGeometry MeshGeometry;
    
        /// Constructor for empty message.
        public MeshGeometryStamped()
        {
            Uuid = "";
            MeshGeometry = new MeshMsgs.MeshGeometry();
        }
        
        /// Explicit constructor.
        public MeshGeometryStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshGeometry MeshGeometry)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshGeometry = MeshGeometry;
        }
        
        /// Constructor with buffer.
        public MeshGeometryStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            MeshGeometry = new MeshMsgs.MeshGeometry(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshGeometryStamped(ref b);
        
        public MeshGeometryStamped RosDeserialize(ref ReadBuffer b) => new MeshGeometryStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshGeometry.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (MeshGeometry is null) BuiltIns.ThrowNullReference();
            MeshGeometry.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Uuid);
                size += MeshGeometry.RosMessageLength;
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshGeometryStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2d62dc21b3d9b8f528e4ee7f76a77fb7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTWvbQBC9768Y8MFJwSk0pQdDb6apD4FAfAvFjLUjaWG1q+6uHKu/vm8lWzHBhB5q" +
                "IZBWeu/Nx5uZ0aPEmh7EN5JCn0+RK1Ex6W0Tq/j5p7CWQPXwwOdgXEVdZ7RqQBwxWWJSGD5Xx5NS3//z" +
                "pR6fH5b0Lj01o+fETnPQiJ9Yc2IqPdI2VS1hYWUvFiRuWtE0/E19K/EOxE1tIuGuxElga3vqIkDJU+Gb" +
                "pnOm4CSUDOo654NpHDG1HJIpOssBeB+0cRleBm4kq+OO8rsTVwitV0tgXJSiSwYJ9VAognDMLV2vSHXG" +
                "pfsvmaBmm1e/wFEqNH8KTqnmlJOVQxvgFJLhuESMT2Nxd9BGcwRRdKSb4dsWx3hLCIIUpPVFTTfI/KlP" +
                "tXcQFNpzMLyzkoULdACq80ya354pu0HasfMn+VHxLca/yLpJN9e0qOGZzdXHrkIDAWyD3xsN6K4fRApr" +
                "xCWyZhcY45RZY0g1+5F7DBBYgyN4coy+MDBA06tJ9WlcBze2GNkrTePlTUCRl3frtBwj5cnD5pdftJc8" +
                "SBI/+C2HrfOhYRvPlm+DLrvKytrpTAe05CxzpVovZHdaIoxFYuPiYFzro0kGo+DLvCUZlxemDAIDW2So" +
                "Sus5fftKh+mtn97+XN+qd31DESspjTtLOh0R8/hmzriiL/eTIeZIV38BEc3WNkoFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
