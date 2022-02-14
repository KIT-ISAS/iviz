/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Mesh : IDeserializable<Mesh>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "vertices")] public Vector3f[] Vertices;
        [DataMember (Name = "normals")] public Vector3f[] Normals;
        [DataMember (Name = "tangents")] public Vector3f[] Tangents;
        [DataMember (Name = "bi_tangents")] public Vector3f[] BiTangents;
        [DataMember (Name = "tex_coords")] public TexCoords[] TexCoords;
        [DataMember (Name = "color_channels")] public ColorChannel[] ColorChannels;
        [DataMember (Name = "faces")] public Triangle[] Faces;
        [DataMember (Name = "material_index")] public uint MaterialIndex;
    
        /// Constructor for empty message.
        public Mesh()
        {
            Name = "";
            Vertices = System.Array.Empty<Vector3f>();
            Normals = System.Array.Empty<Vector3f>();
            Tangents = System.Array.Empty<Vector3f>();
            BiTangents = System.Array.Empty<Vector3f>();
            TexCoords = System.Array.Empty<TexCoords>();
            ColorChannels = System.Array.Empty<ColorChannel>();
            Faces = System.Array.Empty<Triangle>();
        }
        
        /// Explicit constructor.
        public Mesh(string Name, Vector3f[] Vertices, Vector3f[] Normals, Vector3f[] Tangents, Vector3f[] BiTangents, TexCoords[] TexCoords, ColorChannel[] ColorChannels, Triangle[] Faces, uint MaterialIndex)
        {
            this.Name = Name;
            this.Vertices = Vertices;
            this.Normals = Normals;
            this.Tangents = Tangents;
            this.BiTangents = BiTangents;
            this.TexCoords = TexCoords;
            this.ColorChannels = ColorChannels;
            this.Faces = Faces;
            this.MaterialIndex = MaterialIndex;
        }
        
        /// Constructor with buffer.
        public Mesh(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Vertices = b.DeserializeStructArray<Vector3f>();
            Normals = b.DeserializeStructArray<Vector3f>();
            Tangents = b.DeserializeStructArray<Vector3f>();
            BiTangents = b.DeserializeStructArray<Vector3f>();
            TexCoords = b.DeserializeArray<TexCoords>();
            for (int i = 0; i < TexCoords.Length; i++)
            {
                TexCoords[i] = new TexCoords(ref b);
            }
            ColorChannels = b.DeserializeArray<ColorChannel>();
            for (int i = 0; i < ColorChannels.Length; i++)
            {
                ColorChannels[i] = new ColorChannel(ref b);
            }
            Faces = b.DeserializeStructArray<Triangle>();
            MaterialIndex = b.Deserialize<uint>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Mesh(ref b);
        
        public Mesh RosDeserialize(ref ReadBuffer b) => new Mesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(Normals);
            b.SerializeStructArray(Tangents);
            b.SerializeStructArray(BiTangents);
            b.SerializeArray(TexCoords);
            b.SerializeArray(ColorChannels);
            b.SerializeStructArray(Faces);
            b.Serialize(MaterialIndex);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Vertices is null) throw new System.NullReferenceException(nameof(Vertices));
            if (Normals is null) throw new System.NullReferenceException(nameof(Normals));
            if (Tangents is null) throw new System.NullReferenceException(nameof(Tangents));
            if (BiTangents is null) throw new System.NullReferenceException(nameof(BiTangents));
            if (TexCoords is null) throw new System.NullReferenceException(nameof(TexCoords));
            for (int i = 0; i < TexCoords.Length; i++)
            {
                if (TexCoords[i] is null) throw new System.NullReferenceException($"{nameof(TexCoords)}[{i}]");
                TexCoords[i].RosValidate();
            }
            if (ColorChannels is null) throw new System.NullReferenceException(nameof(ColorChannels));
            for (int i = 0; i < ColorChannels.Length; i++)
            {
                if (ColorChannels[i] is null) throw new System.NullReferenceException($"{nameof(ColorChannels)}[{i}]");
                ColorChannels[i].RosValidate();
            }
            if (Faces is null) throw new System.NullReferenceException(nameof(Faces));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += BuiltIns.GetStringSize(Name);
                size += 12 * Vertices.Length;
                size += 12 * Normals.Length;
                size += 12 * Tangents.Length;
                size += 12 * BiTangents.Length;
                size += BuiltIns.GetArraySize(TexCoords);
                size += BuiltIns.GetArraySize(ColorChannels);
                size += 12 * Faces.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Mesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a5cea1d5d993b3cd934f9a3c7f16378c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71SOw7CMAzdc4rcAKldEBJTByYmEAtCkZu6JVLqSEmoSk9PCknU7kAWPz/H8ufZeauo" +
                "4wQ9sgtKb2zZXm98QOuVRLfkyNge9IryQB2SX3G1Epk+41gZYxs3/8VRyLfDKqONre5AhDpE5OwK+fFD" +
                "klUhX2OItDD38FDky4L34DGEtFDU4MjY/suPHU+HHVeDmkTvOrdJM7FWG5gbGDN6ZjT9vo+8xOWW4yZ/" +
                "Xnwp1Ue3skiS/at8WbxPYMtttF20dbTwBxHiUaZjhATqBCRjL5ZmGPZNAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
