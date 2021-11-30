using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = RosServiceType)]
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
        
        IService IService.Create() => new GetLabeledClusters();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "mesh_msgs/GetLabeledClusters";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "d4165053db3e9b1ffe9db49f0702734c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLabeledClustersRequest : IRequest<GetLabeledClusters, GetLabeledClustersResponse>, IDeserializable<GetLabeledClustersRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        /// Constructor for empty message.
        public GetLabeledClustersRequest()
        {
            Uuid = string.Empty;
        }
        
        /// Explicit constructor.
        public GetLabeledClustersRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        internal GetLabeledClustersRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetLabeledClustersRequest(ref b);
        
        GetLabeledClustersRequest IDeserializable<GetLabeledClustersRequest>.RosDeserialize(ref Buffer b) => new GetLabeledClustersRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Uuid);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLabeledClustersResponse : IResponse, IDeserializable<GetLabeledClustersResponse>
    {
        [DataMember (Name = "clusters")] public MeshFaceCluster[] Clusters;
    
        /// Constructor for empty message.
        public GetLabeledClustersResponse()
        {
            Clusters = System.Array.Empty<MeshFaceCluster>();
        }
        
        /// Explicit constructor.
        public GetLabeledClustersResponse(MeshFaceCluster[] Clusters)
        {
            this.Clusters = Clusters;
        }
        
        /// Constructor with buffer.
        internal GetLabeledClustersResponse(ref Buffer b)
        {
            Clusters = b.DeserializeArray<MeshFaceCluster>();
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshFaceCluster(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetLabeledClustersResponse(ref b);
        
        GetLabeledClustersResponse IDeserializable<GetLabeledClustersResponse>.RosDeserialize(ref Buffer b) => new GetLabeledClustersResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Clusters);
        }
        
        public void RosValidate()
        {
            if (Clusters is null) throw new System.NullReferenceException(nameof(Clusters));
            for (int i = 0; i < Clusters.Length; i++)
            {
                if (Clusters[i] is null) throw new System.NullReferenceException($"{nameof(Clusters)}[{i}]");
                Clusters[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Clusters);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
