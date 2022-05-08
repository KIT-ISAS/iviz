/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
        
        /// Constructor with buffer.
        public Mesh(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeStructArray(out Vertices);
            b.DeserializeStructArray(out Normals);
            b.DeserializeStructArray(out Tangents);
            b.DeserializeStructArray(out BiTangents);
            b.DeserializeArray(out TexCoords);
            for (int i = 0; i < TexCoords.Length; i++)
            {
                TexCoords[i] = new TexCoords(ref b);
            }
            b.DeserializeArray(out ColorChannels);
            for (int i = 0; i < ColorChannels.Length; i++)
            {
                ColorChannels[i] = new ColorChannel(ref b);
            }
            b.DeserializeStructArray(out Faces);
            b.Deserialize(out MaterialIndex);
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
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Vertices is null) BuiltIns.ThrowNullReference();
            if (Normals is null) BuiltIns.ThrowNullReference();
            if (Tangents is null) BuiltIns.ThrowNullReference();
            if (BiTangents is null) BuiltIns.ThrowNullReference();
            if (TexCoords is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < TexCoords.Length; i++)
            {
                if (TexCoords[i] is null) BuiltIns.ThrowNullReference(nameof(TexCoords), i);
                TexCoords[i].RosValidate();
            }
            if (ColorChannels is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < ColorChannels.Length; i++)
            {
                if (ColorChannels[i] is null) BuiltIns.ThrowNullReference(nameof(ColorChannels), i);
                ColorChannels[i].RosValidate();
            }
            if (Faces is null) BuiltIns.ThrowNullReference();
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/Mesh";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a5cea1d5d993b3cd934f9a3c7f16378c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71SOw7CMAzdc4rcAKldEBJTByYmEAtCkZu6JVLqSEmoSk9PCknU7kAWPz/H8ufZeauo" +
                "4wQ9sgtKb2zZXm98QOuVRLfkyNge9IryQB2SX3G1Epk+41gZYxs3/8VRyLfDKqONre5AhDpE5OwK+fFD" +
                "klUhX2OItDD38FDky4L34DGEtFDU4MjY/suPHU+HHVeDmkTvOrdJM7FWG5gbGDN6ZjT9vo+8xOWW4yZ/" +
                "Xnwp1Ue3skiS/at8WbxPYMtttF20dbTwBxHiUaZjhATqBCRjL5ZmGPZNAwAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
