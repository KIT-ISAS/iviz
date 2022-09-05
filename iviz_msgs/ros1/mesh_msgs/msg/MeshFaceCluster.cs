/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshFaceCluster : IDeserializable<MeshFaceCluster>, IHasSerializer<MeshFaceCluster>, IMessage
    {
        //Cluster
        [DataMember (Name = "face_indices")] public uint[] FaceIndices;
        //optional
        [DataMember (Name = "label")] public string Label;
    
        public MeshFaceCluster()
        {
            FaceIndices = EmptyArray<uint>.Value;
            Label = "";
        }
        
        public MeshFaceCluster(uint[] FaceIndices, string Label)
        {
            this.FaceIndices = FaceIndices;
            this.Label = Label;
        }
        
        public MeshFaceCluster(ref ReadBuffer b)
        {
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<uint>.Value
                    : new uint[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                FaceIndices = array;
            }
            b.DeserializeString(out Label);
        }
        
        public MeshFaceCluster(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<uint>.Value
                    : new uint[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                FaceIndices = array;
            }
            b.DeserializeString(out Label);
        }
        
        public MeshFaceCluster RosDeserialize(ref ReadBuffer b) => new MeshFaceCluster(ref b);
        
        public MeshFaceCluster RosDeserialize(ref ReadBuffer2 b) => new MeshFaceCluster(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(FaceIndices);
            b.Serialize(Label);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(FaceIndices);
            b.Serialize(Label);
        }
        
        public void RosValidate()
        {
            if (FaceIndices is null) BuiltIns.ThrowNullReference();
            if (Label is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + 4 * FaceIndices.Length + WriteBuffer.GetStringSize(Label);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // FaceIndices length
            c += 4 * FaceIndices.Length;
            c = WriteBuffer2.AddLength(c, Label);
            return c;
        }
    
        public const string MessageType = "mesh_msgs/MeshFaceCluster";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9e0f40b9dcf1de10d00e57182c9d138f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1N2ziktLkkt4irNzCsxNoqOVUhLTE6Nz8xLyUxOLeZSzi8oyczPS8zhKi4pysxLV8hJ" +
                "TErN4eICAKZztFU3AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshFaceCluster> CreateSerializer() => new Serializer();
        public Deserializer<MeshFaceCluster> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshFaceCluster>
        {
            public override void RosSerialize(MeshFaceCluster msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshFaceCluster msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshFaceCluster msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshFaceCluster msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<MeshFaceCluster>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshFaceCluster msg) => msg = new MeshFaceCluster(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshFaceCluster msg) => msg = new MeshFaceCluster(ref b);
        }
    }
}
