/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshVertexCosts : IDeserializable<MeshVertexCosts>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "costs")] public float[] Costs;
    
        /// Constructor for empty message.
        public MeshVertexCosts()
        {
            Costs = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public MeshVertexCosts(float[] Costs)
        {
            this.Costs = Costs;
        }
        
        /// Constructor with buffer.
        internal MeshVertexCosts(ref Buffer b)
        {
            Costs = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshVertexCosts(ref b);
        
        MeshVertexCosts IDeserializable<MeshVertexCosts>.RosDeserialize(ref Buffer b) => new MeshVertexCosts(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Costs);
        }
        
        public void RosValidate()
        {
            if (Costs is null) throw new System.NullReferenceException(nameof(Costs));
        }
    
        public int RosMessageLength => 4 + 4 * Costs.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexCosts";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ade4ce6a157397b6c023e12482bc76c8";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClNW8E0tzlBwLCkpykwqLUkFcYsT01O50nLyE0uMjaJjFZLzi0uKubgAIEHWRioAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
