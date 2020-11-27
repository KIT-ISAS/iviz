using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/GetLabeledClusters")]
    public sealed class GetLabeledClusters : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetLabeledClustersRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetLabeledClustersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetLabeledClusters()
        {
            Request = new GetLabeledClustersRequest();
            Response = new GetLabeledClustersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetLabeledClusters";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d4165053db3e9b1ffe9db49f0702734c";
    }

    [DataContract]
    public sealed class GetLabeledClustersRequest : IRequest, IDeserializable<GetLabeledClustersRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetLabeledClustersRequest()
        {
            Uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetLabeledClustersRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetLabeledClustersRequest(ref Buffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetLabeledClustersRequest(ref b);
        }
        
        GetLabeledClustersRequest IDeserializable<GetLabeledClustersRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetLabeledClustersRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GetLabeledClustersResponse : IResponse, IDeserializable<GetLabeledClustersResponse>
    {
        [DataMember (Name = "clusters")] public MeshFaceCluster[] Clusters { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetLabeledClustersResponse()
        {
            Clusters = System.Array.Empty<MeshFaceCluster>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetLabeledClustersResponse(MeshFaceCluster[] Clusters)
        {
            this.Clusters = Clusters;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetLabeledClustersResponse(ref Buffer b)
        {
            Clusters = b.DeserializeArray<MeshFaceCluster>();
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i] = new MeshFaceCluster(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetLabeledClustersResponse(ref b);
        }
        
        GetLabeledClustersResponse IDeserializable<GetLabeledClustersResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetLabeledClustersResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Clusters, 0);
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Clusters)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    }
}
