using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshMaterialsStamped : IMessage
    {
        // Mesh Attribute Message
        public std_msgs.Header header { get; set; }
        public string uuid { get; set; }
        public mesh_msgs.MeshMaterials mesh_materials { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshMaterialsStamped()
        {
            header = new std_msgs.Header();
            uuid = "";
            mesh_materials = new mesh_msgs.MeshMaterials();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshMaterialsStamped(std_msgs.Header header, string uuid, mesh_msgs.MeshMaterials mesh_materials)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
            this.mesh_materials = mesh_materials ?? throw new System.ArgumentNullException(nameof(mesh_materials));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshMaterialsStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.uuid = b.DeserializeString();
            this.mesh_materials = new mesh_msgs.MeshMaterials(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshMaterialsStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.Serialize(this.uuid);
            this.mesh_materials.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (uuid is null) throw new System.NullReferenceException();
            if (mesh_materials is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(uuid);
                size += mesh_materials.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshMaterialsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "80683ad6336327fea277cb1ed5f58927";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTWvbQBC9768Y8CFJwSm0N0MPqUPSHAylMb2UIkbSWFpY7ar74dj/vm/lSnZSDD00" +
                "MTKaXc17++ZrZ7SS0NJNjF6XKUpeBm5EhVgXXWjC+y/CtXhqhxe2vbYNpaRr1QF58MkcK47iNZtAh/1x" +
                "qdSn//xTq8f7Bb0QqGb0GNnW7GsIiFxzZNo4CNdNK35uZCsGIO56qWn4Gve9hGsA160OhKcRK56N2VMK" +
                "cIqOKtd1yeoKwVDUCOwUD6S2xNSzj7pKhj38na+1ze4bz51kdjxBfiWxldDD7QI+NkiVooagPRgqLxxy" +
                "Uh9uSSVt48cPGaBm6yc3x1IapH86nGLLMYuVXe9RK4jhsMAZ7w7BXYMbyRGcUge6HPYKLMMV4RBIkN5V" +
                "LV1C+dd9bJ0FodCWUavSSCaukAGwXmTQxdUJsx2oLVs30h8Yj2f8C62deHNM8xY1Mzn6kBokEI69d1td" +
                "w7XcDySV0WIjGV169nuVUYcj1ewu5xhOQA0VwZtDcJVGAWp60rEdG3aoRoGmfaVuPDMLiPLMfD0H3HEl" +
                "S5MCYD9+IuLBCmcmDB7H6To0zBF0MnjP0d/FR9mtZbfMPRqA2A47Rf4Pfftqo3o2VjUbjSmMDb4W2ta6" +
                "EmTP9VE7y2Yso+FSzNvWcBxJpCkmP2iT3fF6XDrj/Lf7zzeYa1iqdM5Qy6H44//q198kQG2M46zUT1Yz" +
                "WeVk8duk70W7/T0Ia9y+k6g0WVulfgPgQWQKlAYAAA==";
                
    }
}
