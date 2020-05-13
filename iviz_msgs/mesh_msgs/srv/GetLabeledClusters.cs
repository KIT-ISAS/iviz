using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    [DataContract]
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "mesh_msgs/GetLabeledClusters";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d4165053db3e9b1ffe9db49f0702734c";
    }

    public sealed class GetLabeledClustersRequest : IRequest
    {
        [DataMember] public string uuid { get; set; }
    
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
            this.uuid = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetLabeledClustersRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.uuid);
        }
        
        public void Validate()
        {
            if (uuid is null) throw new System.NullReferenceException();
        }
    
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
        [DataMember] public MeshFaceCluster[] clusters { get; set; }
    
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
            this.clusters = b.DeserializeArray<MeshFaceCluster>();
            for (int i = 0; i < this.clusters.Length; i++)
            {
                this.clusters[i] = new MeshFaceCluster(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetLabeledClustersResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.clusters, 0);
        }
        
        public void Validate()
        {
            if (clusters is null) throw new System.NullReferenceException();
            for (int i = 0; i < clusters.Length; i++)
            {
                if (clusters[i] is null) throw new System.NullReferenceException();
                clusters[i].Validate();
            }
        }
    
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
