/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/VectorField")]
    public sealed class VectorField : IDeserializable<VectorField>, IMessage
    {
        [DataMember (Name = "positions")] public GeometryMsgs.Point[] Positions { get; set; }
        [DataMember (Name = "vectors")] public GeometryMsgs.Vector3[] Vectors { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public VectorField()
        {
            Positions = System.Array.Empty<GeometryMsgs.Point>();
            Vectors = System.Array.Empty<GeometryMsgs.Vector3>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public VectorField(GeometryMsgs.Point[] Positions, GeometryMsgs.Vector3[] Vectors)
        {
            this.Positions = Positions;
            this.Vectors = Vectors;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public VectorField(ref Buffer b)
        {
            Positions = b.DeserializeStructArray<GeometryMsgs.Point>();
            Vectors = b.DeserializeStructArray<GeometryMsgs.Vector3>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new VectorField(ref b);
        }
        
        VectorField IDeserializable<VectorField>.RosDeserialize(ref Buffer b)
        {
            return new VectorField(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Positions, 0);
            b.SerializeStructArray(Vectors, 0);
        }
        
        public void RosValidate()
        {
            if (Positions is null) throw new System.NullReferenceException(nameof(Positions));
            if (Vectors is null) throw new System.NullReferenceException(nameof(Vectors));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 24 * Positions.Length;
                size += 24 * Vectors.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9da8d62df10048ede4a91e419a35679d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1SwUrDQBS8B/IPA14USgQVD4Jn6UEQFC8isk1etovJvrDv1Rq/3rdJaSkIXsScXpaZ" +
                "2Zl564l70jS+9eLl/IFD1JdXDCxBA0cpC38EeKZaOV0a5GOaDFAWt3/8lcX9490NfrBWFid4WgdBzVFd" +
                "iAJd094tuIWzPwMiRLSJCDK4msqi7djp9RU+D+N4GL/+LcWuv32OREMioahizudKj61XyNilBRJw7Eb0" +
                "5Cye8oFqzCYk41oFlclSopYTLRAUDZMg8lRc795NlKJQprthMDUHTS5K56b+7Ng4p1T5aoHtmuKMCtEb" +
                "MEt4ipRCjRR8aGaqXdXv2Q67gAtoe4Ft6LrZ9XybLSurJNaJcVZh2WLkDbY5kw0JjVPzxFiZyZ0zt+qy" +
                "Y15gk63PGj88DqtGxHmyAkXJNVXe6q+L/wZe1MI1AQMAAA==";
                
    }
}
