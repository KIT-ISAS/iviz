/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexCosts : IHasSerializer<MeshVertexCosts>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "costs")] public float[] Costs;
    
        public MeshVertexCosts()
        {
            Costs = EmptyArray<float>.Value;
        }
        
        public MeshVertexCosts(float[] Costs)
        {
            this.Costs = Costs;
        }
        
        public MeshVertexCosts(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                float[] array;
                if (n == 0) array = EmptyArray<float>.Value;
                else
                {
                     array = new float[n];
                    b.DeserializeStructArray(array);
                }
                Costs = array;
            }
        }
        
        public MeshVertexCosts(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                float[] array;
                if (n == 0) array = EmptyArray<float>.Value;
                else
                {
                     array = new float[n];
                    b.DeserializeStructArray(array);
                }
                Costs = array;
            }
        }
        
        public MeshVertexCosts RosDeserialize(ref ReadBuffer b) => new MeshVertexCosts(ref b);
        
        public MeshVertexCosts RosDeserialize(ref ReadBuffer2 b) => new MeshVertexCosts(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Costs.Length);
            b.SerializeStructArray(Costs);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Costs.Length);
            b.SerializeStructArray(Costs);
        }
        
        public void RosValidate()
        {
            if (Costs is null) BuiltIns.ThrowNullReference(nameof(Costs));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 4 * Costs.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Costs.Length
            size += 4 * Costs.Length;
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshVertexCosts";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ade4ce6a157397b6c023e12482bc76c8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O50nLyE0uMjaJjFZLzi0uKubgAIEHWRioAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshVertexCosts> CreateSerializer() => new Serializer();
        public Deserializer<MeshVertexCosts> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshVertexCosts>
        {
            public override void RosSerialize(MeshVertexCosts msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshVertexCosts msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshVertexCosts msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshVertexCosts msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshVertexCosts msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshVertexCosts>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshVertexCosts msg) => msg = new MeshVertexCosts(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshVertexCosts msg) => msg = new MeshVertexCosts(ref b);
        }
    }
}
