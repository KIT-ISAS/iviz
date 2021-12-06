/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshVertexColors : IDeserializable<MeshVertexColors>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "vertex_colors")] public StdMsgs.ColorRGBA[] VertexColors;
    
        /// Constructor for empty message.
        public MeshVertexColors()
        {
            VertexColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        /// Explicit constructor.
        public MeshVertexColors(StdMsgs.ColorRGBA[] VertexColors)
        {
            this.VertexColors = VertexColors;
        }
        
        /// Constructor with buffer.
        internal MeshVertexColors(ref ReadBuffer b)
        {
            VertexColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexColors(ref b);
        
        public MeshVertexColors RosDeserialize(ref ReadBuffer b) => new MeshVertexColors(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(VertexColors);
        }
        
        public void RosValidate()
        {
            if (VertexColors is null) throw new System.NullReferenceException(nameof(VertexColors));
        }
    
        public int RosMessageLength => 4 + 16 * VertexColors.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexColors";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "2af51ba6de42b829b6f716360dfdf4d9";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O5iktS4nOL04v1nfNz8ouC3J0co2MVylKLSlIr" +
                "4pNBQsVcXLZUBly+we5WCpg2c6Xl5CeWGBspFMFZ6XBWEpyVyMUFAF0TsDnPAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
