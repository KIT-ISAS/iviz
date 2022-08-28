/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexCosts : IDeserializable<MeshVertexCosts>, IMessage
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
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Costs = array;
            }
        }
        
        public MeshVertexCosts(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Costs = array;
            }
        }
        
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Costs length
            c += 4 * Costs.Length;
            return c;
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
    }
}
