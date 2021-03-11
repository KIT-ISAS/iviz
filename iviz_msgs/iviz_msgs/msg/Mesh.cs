/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Mesh")]
    public sealed class Mesh : IDeserializable<Mesh>, IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "vertices")] public Vector3f[] Vertices { get; set; }
        [DataMember (Name = "normals")] public Vector3f[] Normals { get; set; }
        [DataMember (Name = "tangents")] public Vector3f[] Tangents { get; set; }
        [DataMember (Name = "bi_tangents")] public Vector3f[] BiTangents { get; set; }
        [DataMember (Name = "tex_coords")] public TexCoords[] TexCoords { get; set; }
        [DataMember (Name = "color_channels")] public ColorChannel[] ColorChannels { get; set; }
        [DataMember (Name = "faces")] public Triangle[] Faces { get; set; }
        [DataMember (Name = "material_index")] public uint MaterialIndex { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Mesh()
        {
            Name = string.Empty;
            Vertices = System.Array.Empty<Vector3f>();
            Normals = System.Array.Empty<Vector3f>();
            Tangents = System.Array.Empty<Vector3f>();
            BiTangents = System.Array.Empty<Vector3f>();
            TexCoords = System.Array.Empty<TexCoords>();
            ColorChannels = System.Array.Empty<ColorChannel>();
            Faces = System.Array.Empty<Triangle>();
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public Mesh(ref Buffer b)
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
            b.SerializeStructArray(Tangents, 0);
            b.SerializeStructArray(BiTangents, 0);
            b.SerializeArray(TexCoords, 0);
            b.SerializeArray(ColorChannels, 0);
            b.SerializeStructArray(Faces, 0);
            b.Serialize(MaterialIndex);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += 12 * Vertices.Length;
                size += 12 * Normals.Length;
                size += 12 * Tangents.Length;
                size += 12 * BiTangents.Length;
                foreach (var i in TexCoords)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in ColorChannels)
                {
                    size += i.RosMessageLength;
                }
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
                "H4sIAAAAAAAACr1SwQrCMAy9F/oP/QNhu4jgaQdPnhQvIiXWbBa6FNo66r7eznVj3mW95OWloa958cFp" +
                "agRBi+yCKlhX1teb6NAFrdAvObKuBfNDBaAGKfxwdy1n+oyxstY9/HAXo1TfhFXWWFc9gQhNqqghlWrM" +
                "U5PTqd9gqtQwaHhpCmUhWgiYSkZqemBknO3/fDg7ng47oTvdy9Y3fjP9irPaWBg0xBm9Z9SvIWWeJF/O" +
                "Os9zhfeXlvHRwLKYvFtPQVnw7zpshcuxyfGeI6ziRl7RUUzaAZjAKCMBlXR8ANsqFdZdAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
