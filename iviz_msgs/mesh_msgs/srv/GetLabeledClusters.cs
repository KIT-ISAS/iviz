using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetLabeledClusters : IService
    {
        /// <summary> Request message. </summary>
        public GetLabeledClustersRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public GetLabeledClustersResponse Response { get; set; }
        
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
        
        public IService Create() => new GetLabeledClusters();
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetLabeledClusters";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "d4165053db3e9b1ffe9db49f0702734c";
    }

    public sealed class GetLabeledClustersRequest : IRequest
    {
        public string uuid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetLabeledClustersRequest()
        {
            uuid = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetLabeledClustersRequest(string uuid)
        {
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetLabeledClustersRequest(Buffer b)
        {
            this.uuid = BuiltIns.DeserializeString(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetLabeledClustersRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.uuid, b);
        }
        
        public void Validate()
        {
            if (uuid is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(uuid);
                return size;
            }
        }
    }

    public sealed class GetLabeledClustersResponse : IResponse
    {
        public MeshFaceCluster[] clusters { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetLabeledClustersResponse()
        {
            clusters = System.Array.Empty<MeshFaceCluster>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetLabeledClustersResponse(MeshFaceCluster[] clusters)
        {
            this.clusters = clusters ?? throw new System.ArgumentNullException(nameof(clusters));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetLabeledClustersResponse(Buffer b)
        {
            this.clusters = BuiltIns.DeserializeArray<MeshFaceCluster>(b, 0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetLabeledClustersResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.SerializeArray(this.clusters, b, 0);
        }
        
        public void Validate()
        {
            if (clusters is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                for (int i = 0; i < clusters.Length; i++)
                {
                    size += clusters[i].RosMessageLength;
                }
                return size;
            }
        }
    }
}
