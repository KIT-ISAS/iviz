/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexCosts : IDeserializableRos1<MeshVertexCosts>, IDeserializableRos2<MeshVertexCosts>, IMessageRos1, IMessageRos2
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
            b.DeserializeStructArray(out Costs);
        }
        
        /// Constructor with buffer.
        public MeshVertexCosts(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Costs);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexCosts(ref b);
        
        public MeshVertexCosts RosDeserialize(ref ReadBuffer b) => new MeshVertexCosts(ref b);
        
        public MeshVertexCosts RosDeserialize(ref ReadBuffer2 b) => new MeshVertexCosts(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Costs);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Costs);
        }
        
        public void RosValidate()
        {
            if (Costs is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Costs.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Costs);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshVertexCosts";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ade4ce6a157397b6c023e12482bc76c8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O50nLyE0uMjaJjFZLzi0uKubgAIEHWRioAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
