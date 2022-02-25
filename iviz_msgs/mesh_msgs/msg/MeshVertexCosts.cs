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
        public MeshVertexCosts(ref ReadBuffer b)
        {
            Costs = b.DeserializeStructArray<float>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexCosts(ref b);
        
        public MeshVertexCosts RosDeserialize(ref ReadBuffer b) => new MeshVertexCosts(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Costs);
        }
        
        public void RosValidate()
        {
            if (Costs is null) BuiltIns.ThrowNullReference(nameof(Costs));
        }
    
        public int RosMessageLength => 4 + 4 * Costs.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexCosts";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ade4ce6a157397b6c023e12482bc76c8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O50nLyE0uMjaJjFZLzi0uKubgAIEHWRioAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
