using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshVertexColors")]
    public sealed class MeshVertexColors : IMessage
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
        internal MeshVertexColors(Buffer b)
        {
            VertexColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new MeshVertexColors(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(VertexColors, 0);
        }
        
        public void RosValidate()
        {
            if (VertexColors is null) throw new System.NullReferenceException();
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
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O5iktS4nOL04v1nfNz8ouC3J0co2MVylKLSlIr" +
                "4pNBQsVcXLZUBly+we5WCpg2c6Xl5CeWGBspFMFZ6XBWEpyVyMUFAF0TsDnPAAAA";
                
    }
}
