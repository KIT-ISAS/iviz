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
            Name = string.Empty;
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
        internal Mesh(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Mesh(ref b);
        
        Mesh IDeserializable<Mesh>.RosDeserialize(ref Buffer b) => new Mesh(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Mesh";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a5cea1d5d993b3cd934f9a3c7f16378c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1SMQ6DMAzc84r8oBIsVaVODJ06tepSVZEJhkYKjpSkiPL6BggI9kIWn8+xcvHZeauo" +
                "4gQ1sgdKb2xaPl+8QeuVRLfkyNga9IryQBWSX3G5EjN9xzYzxhauv4utkEPCMqONzd5AhDpUZJ8KOeah" +
                "yarQrzFUSug1fBT5NOE1eAwlLRQV2DJ2/vNh19vlxFWjOlG7yh2mP7FSG+gFtDP6zqjbXsc8xOWU4yQ3" +
                "f3xp1ehbmkyW7fV8mgwrcOQ2xirGPEbYwYS4lNMywgRGDQFIxn6WZhj2TQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
