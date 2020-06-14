using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshVertexCosts")]
    public sealed class MeshVertexCosts : IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "costs")] public float[] Costs { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexCosts()
        {
            Costs = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexCosts(float[] Costs)
        {
            this.Costs = Costs;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshVertexCosts(Buffer b)
        {
            Costs = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new MeshVertexCosts(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(Costs, 0);
        }
        
        public void RosValidate()
        {
            if (Costs is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Costs.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexCosts";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ade4ce6a157397b6c023e12482bc76c8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O50nLyE0uMjaJjFZLzi0uKubgAIEHWRioAAAA=";
                
    }
}
