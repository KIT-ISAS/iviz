/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshFaceCluster : IHasSerializer<MeshFaceCluster>, IMessage
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
            {
                int n = b.DeserializeArrayLength();
                uint[] array;
                if (n == 0) array = EmptyArray<uint>.Value;
                else
                {
                    array = new uint[n];
                    b.DeserializeStructArray(array);
                }
                FaceIndices = array;
            }
            Label = b.DeserializeString();
        }
        
        public MeshFaceCluster(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                uint[] array;
                if (n == 0) array = EmptyArray<uint>.Value;
                else
                {
                    array = new uint[n];
                    b.DeserializeStructArray(array);
                }
                FaceIndices = array;
            }
            Label = b.DeserializeString();
        }
        
        public MeshFaceCluster RosDeserialize(ref ReadBuffer b) => new MeshFaceCluster(ref b);
        
        public MeshFaceCluster RosDeserialize(ref ReadBuffer2 b) => new MeshFaceCluster(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(FaceIndices.Length);
            b.SerializeStructArray(FaceIndices);
            b.Serialize(Label);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(FaceIndices.Length);
            b.SerializeStructArray(FaceIndices);
            b.Serialize(Label);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(FaceIndices, nameof(FaceIndices));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += 4 * FaceIndices.Length;
                size += WriteBuffer.GetStringSize(Label);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // FaceIndices.Length
            size += 4 * FaceIndices.Length;
            size = WriteBuffer2.AddLength(size, Label);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshFaceCluster";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9e0f40b9dcf1de10d00e57182c9d138f";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
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
            public override int Ros2MessageLength(MeshFaceCluster msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshFaceCluster msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshFaceCluster>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshFaceCluster msg) => msg = new MeshFaceCluster(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshFaceCluster msg) => msg = new MeshFaceCluster(ref b);
        }
    }
}
