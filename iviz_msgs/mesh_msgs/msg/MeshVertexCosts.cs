using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshVertexCosts : IMessage
    {
        // Mesh Attribute Message
        public float[] costs { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexCosts()
        {
            costs = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexCosts(float[] costs)
        {
            this.costs = costs ?? throw new System.ArgumentNullException(nameof(costs));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshVertexCosts(Buffer b)
        {
            this.costs = b.DeserializeStructArray<float>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshVertexCosts(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.costs, 0);
        }
        
        public void Validate()
        {
            if (costs is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * costs.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshVertexCosts";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "ade4ce6a157397b6c023e12482bc76c8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O50nLyE0uMjaJjFZLzi0uKubgAIEHWRioAAAA=";
                
    }
}
