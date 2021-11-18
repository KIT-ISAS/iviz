/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshGeometryStamped")]
    public sealed class MeshGeometryStamped : IDeserializable<MeshGeometryStamped>, IMessage
    {
        // Mesh Geometry Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_geometry")] public MeshMsgs.MeshGeometry MeshGeometry;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshGeometryStamped()
        {
            Uuid = string.Empty;
            MeshGeometry = new MeshMsgs.MeshGeometry();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshGeometryStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshGeometry MeshGeometry)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshGeometry = MeshGeometry;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshGeometryStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Uuid = b.DeserializeString();
            MeshGeometry = new MeshMsgs.MeshGeometry(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshGeometryStamped(ref b);
        }
        
        MeshGeometryStamped IDeserializable<MeshGeometryStamped>.RosDeserialize(ref Buffer b)
        {
            return new MeshGeometryStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshGeometry.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshGeometry is null) throw new System.NullReferenceException(nameof(MeshGeometry));
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
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshGeometryStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2d62dc21b3d9b8f528e4ee7f76a77fb7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TTWvbQBS8C/wfHvjgpOAUmtKDoTfT1IdAIL6FYp53n6SF1a66u3Ks/vrOSrViggk9" +
                "NBUCaaWZefO+5nQvsaY78Y2k0OdT5EqKmPSuiVX8+F1YS6B6eOBzMK6irjO6aEAcMVliUhg+V39Oxaz4" +
                "+o+vWXH/eLeiVwZnxZweEzvNQcNCYs2JqfRwbqpawtLKQSxY3LSiafib+lbiDYjb2kTCXYmTwNb21EWA" +
                "kiflm6ZzRnESSgapnfPBNI6YWg7JqM5yAN4HbVyGl4Ebyeq4o/zsxCmhzXoFjIuiumRgqIeCCsIxV3Wz" +
                "pqIzLt1+yoRivn32SxylQv2n4JRqTtmsHNuAZsEMxxVifBiTu4E2qiOIoiNdDd92OMZrQhBYkNarmq7g" +
                "/KFPtXcQFDpwMLy3koUVKgDVRSYtrs+Us+0VOXb+JD8qvsT4G1k36eacljV6ZnP2satQQADb4A9GA7rv" +
                "BxFljbhE1uwDY6IyawxZzL/lGgME1tARPDlGrwwaoOnZpPo0sUM3dpjadxvIy+uQ5/Lyhp1WZOQ8eHT6" +
                "6QcdJM+SxDd+y3HnfGjYxrMV3KLQrrKycTrTAS05y7xbuhf8zU6rpLxLbFwc2tf6aJLBQPgy70oG5rUp" +
                "g6CNLUwWpfWcvnym4/TWT2+//kfDXhUv57GW0rgz35iiAbKILy0ad/XpdmqLGfmw/Bsvgm17VwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
