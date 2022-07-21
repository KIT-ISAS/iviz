using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetLabeledClusters : IService
    {
        /// Request message.
        [DataMember] public GetLabeledClustersRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetLabeledClustersResponse Response { get; set; }
        
        /// Empty constructor.
        public GetLabeledClusters()
        {
            Request = new GetLabeledClustersRequest();
            Response = new GetLabeledClustersResponse();
        }
        
        /// Setter constructor.
        public GetLabeledClusters(GetLabeledClustersRequest request)
        {
            Request = request;
            Response = new GetLabeledClustersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetLabeledClustersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetLabeledClustersResponse)value;
        }
        
        public const string ServiceType = "mesh_msgs/GetLabeledClusters";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "d4165053db3e9b1ffe9db49f0702734c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLabeledClustersRequest : IRequest<GetLabeledClusters, GetLabeledClustersResponse>, IDeserializable<GetLabeledClustersRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        public GetLabeledClustersRequest()
        {
            Uuid = "";
        }
        
        public GetLabeledClustersRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public GetLabeledClustersRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetLabeledClustersRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Uuid);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetLabeledClustersRequest(ref b);
        
        public GetLabeledClustersRequest RosDeserialize(ref ReadBuffer b) => new GetLabeledClustersRequest(ref b);
        
        public GetLabeledClustersRequest RosDeserialize(ref ReadBuffer2 b) => new GetLabeledClustersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uuid);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Uuid);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLabeledClustersResponse : IResponse, IDeserializable<GetLabeledClustersResponse>
    {
        [DataMember (Name = "clusters")] public MeshFaceCluster[] Clusters;
    
        public GetLabeledClustersResponse()
        {
            Clusters = System.Array.Empty<MeshFaceCluster>();
        }
        
        public GetLabeledClustersResponse(MeshFaceCluster[] Clusters)
        {
            this.Clusters = Clusters;
        }
        
        public GetLabeledClustersResponse(ref ReadBuffer b)
        {
            b.DeserializeArray(out Clusters);
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshFaceCluster(ref b);
            }
        }
        
        public GetLabeledClustersResponse(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Clusters);
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshFaceCluster(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetLabeledClustersResponse(ref b);
        
        public GetLabeledClustersResponse RosDeserialize(ref ReadBuffer b) => new GetLabeledClustersResponse(ref b);
        
        public GetLabeledClustersResponse RosDeserialize(ref ReadBuffer2 b) => new GetLabeledClustersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Clusters);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Clusters);
        }
        
        public void RosValidate()
        {
            if (Clusters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Clusters.Length; i++)
            {
                if (Clusters[i] is null) BuiltIns.ThrowNullReference(nameof(Clusters), i);
                Clusters[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Clusters);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Clusters);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
