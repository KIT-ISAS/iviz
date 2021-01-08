/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshVertexColors")]
    public sealed class MeshVertexColors : IDeserializable<MeshVertexColors>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "vertex_colors")] public StdMsgs.ColorRGBA[] VertexColors { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexColors()
        {
            VertexColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexColors(StdMsgs.ColorRGBA[] VertexColors)
        {
            this.VertexColors = VertexColors;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshVertexColors(ref Buffer b)
        {
            VertexColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshVertexColors(ref b);
        }
        
        MeshVertexColors IDeserializable<MeshVertexColors>.RosDeserialize(ref Buffer b)
        {
            return new MeshVertexColors(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(VertexColors, 0);
        }
        
        public void RosValidate()
        {
            if (VertexColors is null) throw new System.NullReferenceException(nameof(VertexColors));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 16 * VertexColors.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexColors";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2af51ba6de42b829b6f716360dfdf4d9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClNW8E0tzlBwLCkpykwqLUkFcYsT01N5uYpLUuJzi9OL9Z3zc/KLgtydHKNjFcpSi0pS" +
                "K+KTQULFvFy8XLZUBrxcvsHuVgqYlvNypeXkJ5YYGykUIZjpCGYSgpkIchgANxDEmNkAAAA=";
                
    }
}
