using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetLabeledClusters : IService
    {
        /// <summary> Request message. </summary>
        public GetLabeledClustersRequest Request { get; }
        
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
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
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
        public string uuid;
    
        /// <summary> Constructor for empty message. </summary>
        public GetLabeledClustersRequest()
        {
            uuid = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out uuid, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(uuid, ref ptr, end);
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
        public MeshFaceCluster[] clusters;
    
        /// <summary> Constructor for empty message. </summary>
        public GetLabeledClustersResponse()
        {
            clusters = System.Array.Empty<MeshFaceCluster>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out clusters, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(clusters, ref ptr, end, 0);
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
