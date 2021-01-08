/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshMaterialsStamped")]
    public sealed class MeshMaterialsStamped : IDeserializable<MeshMaterialsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "mesh_materials")] public MeshMsgs.MeshMaterials MeshMaterials { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshMaterialsStamped()
        {
            Header = new StdMsgs.Header();
            Uuid = "";
            MeshMaterials = new MeshMsgs.MeshMaterials();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshMaterialsStamped(StdMsgs.Header Header, string Uuid, MeshMsgs.MeshMaterials MeshMaterials)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshMaterials = MeshMaterials;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshMaterialsStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            MeshMaterials = new MeshMsgs.MeshMaterials(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshMaterialsStamped(ref b);
        }
        
        MeshMaterialsStamped IDeserializable<MeshMaterialsStamped>.RosDeserialize(ref Buffer b)
        {
            return new MeshMaterialsStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshMaterials.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshMaterials is null) throw new System.NullReferenceException(nameof(MeshMaterials));
            MeshMaterials.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                size += MeshMaterials.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshMaterialsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "80683ad6336327fea277cb1ed5f58927";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBnJIUkgK7c3QQ+qQNIdAaUwvpZiRNJYW1rvqfjj2v+8bubackEAO" +
                "jY2FZlfz3r752hO6l9jRVUrBVDmJLiO3UhYxNfNlbOPHb8KNBOqGl+4H41rK2TRlsQR466U095wkGLaR" +
                "tvu7ZVmUxZf//CuL+4fbCT1TWRYn9JDYNRwaiEjccGJaeMg3bSfhwspKLFC87KWh4Wva9BIvFTnrTCT8" +
                "W3ES2NoN5Qiv5Kn2y2V2pkZElAyiOyRQqHHE1HNIps6WAwA+NMap/yLwUgZ+faL8yeJqobvrCbxclDon" +
                "A1EbcNRBOGp2767hnI1Lnz8pAsDZo7/AWlqUYq+AUsdJFcu6DygcFHGc6DEftjFegh5JEhzURDob9uZY" +
                "xnPCOVAhva87OoP875vUeQdGoRWjbJUVZa6RB9CeKuj0/JBapU/IsfM7/i3leMhbeN1IrGFddCie1RTE" +
                "3CKP8OyDX5kGvtVmYKmtEZfImipw2JSFwraHguRGkw034Iba4M0x+tqgEg09mtTtO3ioy1y7+N2685Xx" +
                "0GBfG7unkBuuZWpzBPDXb0Q+WMC/TAyXg5nbts8IO5zHp/ifEpKsZ7KeatdGQFbDzlyfoZPfc4RfjRhp" +
                "2lv7YBb4PjeuMbVoHn2fjHds91W1XIk9ekn3w4qMpRwGhbI+uESn3vrw4/brFWYeVllU3lvqOM7/IY5x" +
                "R053IspiYT2rXijZme1oVqPJR8vlsy58YUhmuKlHZXk0VyryLzRTwTvLBgAA";
                
    }
}
