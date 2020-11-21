/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Mesh")]
    public sealed class Mesh : IDeserializable<Mesh>, IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "vertices")] public Vector3f[] Vertices { get; set; }
        [DataMember (Name = "normals")] public Vector3f[] Normals { get; set; }
        [DataMember (Name = "texCoords")] public Vector2f[] TexCoords { get; set; }
        [DataMember (Name = "colors")] public Color32[] Colors { get; set; }
        [DataMember (Name = "faces")] public Triangle[] Faces { get; set; }
        [DataMember (Name = "materialIndex")] public uint MaterialIndex { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Mesh()
        {
            Name = "";
            Vertices = System.Array.Empty<Vector3f>();
            Normals = System.Array.Empty<Vector3f>();
            TexCoords = System.Array.Empty<Vector2f>();
            Colors = System.Array.Empty<Color32>();
            Faces = System.Array.Empty<Triangle>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Mesh(string Name, Vector3f[] Vertices, Vector3f[] Normals, Vector2f[] TexCoords, Color32[] Colors, Triangle[] Faces, uint MaterialIndex)
        {
            this.Name = Name;
            this.Vertices = Vertices;
            this.Normals = Normals;
            this.TexCoords = TexCoords;
            this.Colors = Colors;
            this.Faces = Faces;
            this.MaterialIndex = MaterialIndex;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Mesh(ref Buffer b)
        {
            Name = b.DeserializeString();
            Vertices = b.DeserializeStructArray<Vector3f>();
            Normals = b.DeserializeStructArray<Vector3f>();
            TexCoords = b.DeserializeStructArray<Vector2f>();
            Colors = b.DeserializeStructArray<Color32>();
            Faces = b.DeserializeStructArray<Triangle>();
            MaterialIndex = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Mesh(ref b);
        }
        
        Mesh IDeserializable<Mesh>.RosDeserialize(ref Buffer b)
        {
            return new Mesh(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Vertices, 0);
            b.SerializeStructArray(Normals, 0);
            b.SerializeStructArray(TexCoords, 0);
            b.SerializeStructArray(Colors, 0);
            b.SerializeStructArray(Faces, 0);
            b.Serialize(MaterialIndex);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Vertices is null) throw new System.NullReferenceException(nameof(Vertices));
            if (Normals is null) throw new System.NullReferenceException(nameof(Normals));
            if (TexCoords is null) throw new System.NullReferenceException(nameof(TexCoords));
            if (Colors is null) throw new System.NullReferenceException(nameof(Colors));
            if (Faces is null) throw new System.NullReferenceException(nameof(Faces));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 28;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += 12 * Vertices.Length;
                size += 12 * Normals.Length;
                size += 8 * TexCoords.Length;
                size += 4 * Colors.Length;
                size += 12 * Faces.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Mesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b76923f54d0c429ac4071dd83be15982";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2SvwrCMBCH90DeIW8gpIsITh3EwUlxEZGYXkMgfyCJpfbpvdim4uAmzfTxS8h9x11M" +
                "QTvFnLBAyRlk8qFqL1fWQUhaQvwKnQ9WmDnjOUvQ196HBtPaG3zIMZSZMDkFLZwygFEr3r89tEsVZ1Yk" +
                "wDuzdw30lFCy/fOh5HDcbZju9HCzUcVVaYOS1niRHbBuwecHhwVt+E+bBRTqcVrjSNYsFFAF7gXEIj5l" +
                "V+YdwbITTSZIMqu8AH4hn0K0AgAA";
                
    }
}
