/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/VectorField")]
    public sealed class VectorField : IDeserializable<VectorField>, IMessage
    {
        [DataMember (Name = "positions")] public GeometryMsgs.Point[] Positions;
        [DataMember (Name = "vectors")] public GeometryMsgs.Vector3[] Vectors;
    
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
        internal VectorField(ref Buffer b)
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
    
        public int RosMessageLength => 8 + 24 * Positions.Length + 24 * Vectors.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9da8d62df10048ede4a91e419a35679d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1SwUrFMBC8B/oPC14USgUVD4JneQdBULyIyL52mwbbbMnus9avd9s+Kg8ensSetsnM" +
                "ZGYST9yRpvGtEy/nDxyivrxCzxI0cBTnD/afqVROl4b4mCdxmbv94y9z9493N3DEWOZO4KkJAiVHxRAF" +
                "tKHVK3ANaH8GhBChTkQgPZbk6pZRr6/gc53Gdfr6rwT76tYMifpEQlHFXC9tHtouwKAbyyLAsR2hI7Rk" +
                "yj9MI1YhGdXSF6ZKiWpOlENQqJgEIqtpdPhukhSFJjb2vYkhaMIoLc7N2bJRTqnwRQ5DQ3FBhegNaAqe" +
                "IqVQQgo+VAvTDupWMsI+XQ5aX8AQ2nbxvBxmt2QiiXUmnBWwqWHkHQxTIBsSVKjmiGFrFve+cNtOfjmH" +
                "3WR8ljjyJqwWEfRk3YkSVoX7/boz9w0AiLXb8gIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
