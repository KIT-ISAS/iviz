/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshGeometryStamped")]
    public sealed class MeshGeometryStamped : IDeserializable<MeshGeometryStamped>, IMessage
    {
        // Mesh Geometry Message
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "mesh_geometry")] public MeshMsgs.MeshGeometry MeshGeometry { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshGeometryStamped()
        {
            Header = new StdMsgs.Header();
            Uuid = "";
            MeshGeometry = new MeshMsgs.MeshGeometry();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshGeometryStamped(StdMsgs.Header Header, string Uuid, MeshMsgs.MeshGeometry MeshGeometry)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshGeometry = MeshGeometry;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshGeometryStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshGeometry is null) throw new System.NullReferenceException(nameof(MeshGeometry));
            MeshGeometry.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
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
                "H4sIAAAAAAAACr1TTWvbQBC9C/QfBnJwUnAKTenB0Jtp6kMgEN9CMRPtSFpY7aq7K8fqr+8buf4gpKWH" +
                "pkZGWunNmzfzZi7oTlJLtxI6yXHUU+JGyiJls+lSk95/FTYSqZ1u+j5a39AwWFMWHWL3KGU5kkyvm1+n" +
                "siiLz//4VxZ3D7cLeqGxLC7oIbM3HA00ZDacmeoA8bZpJc6dbMUhirteDE1f89hLutbIdWsT4WrES2Tn" +
                "RhoSUDlQFbpu8LbiLJQtijsn0FDriannmG01OI4ICNFYr/g6cicTv/6TfB/EV0Kr5QIon6QasoWoERxV" +
                "FE7a29US4MH6fPNBIxC4fg5znKWBEUcFlFvOqlh2fYRtUMRpoWne7Wu8Bj2aJEhkEl1O7zY4pitCHqiQ" +
                "PlQtXUL+/Zjb4MEotOVo+cmJMlfoA2hnGjS7OqdW6Qvy7MOBf095SvI3vP5ErGXNW5jntAVpaNBHIPsY" +
                "ttYA+zROLJWz4jM5+xRZZ0vD9klB8kWbDRjiJm9w55RCZeGEoWeb2+P8Tr5sdIbfbDpfXw6t9Tcrd9iY" +
                "fdB9gOOP32grOleS/vRddhsfYscOqFPaNRruGycrb5QA2Jonojcr+RWFx9XClGS2Pk029iHZbDEZodbV" +
                "UaBuUR0FbvZQWRa1C5w/faTd6RHNOzz++C/Gveig1rKU2voz7RinCTJLZ07tl/fx5uiOPRCUxU8qHNfr" +
                "cgUAAA==";
                
    }
}
