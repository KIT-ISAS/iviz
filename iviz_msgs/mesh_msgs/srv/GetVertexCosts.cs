using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class GetVertexCosts : IService
    {
        /// <summary> Request message. </summary>
        public GetVertexCostsRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetVertexCostsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetVertexCosts()
        {
            Request = new GetVertexCostsRequest();
            Response = new GetVertexCostsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetVertexCosts(GetVertexCostsRequest request)
        {
            Request = request;
            Response = new GetVertexCostsResponse();
        }
        
        public IService Create() => new GetVertexCosts();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "mesh_msgs/GetVertexCosts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "d0fc06ce39b58884e8cdf147765b9d6b";
    }

    public sealed class GetVertexCostsRequest : IRequest
    {
        public string uuid;
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexCostsRequest()
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

    public sealed class GetVertexCostsResponse : IResponse
    {
        public mesh_msgs.MeshVertexCostsStamped mesh_vertex_costs_stamped;
    
        /// <summary> Constructor for empty message. </summary>
        public GetVertexCostsResponse()
        {
            mesh_vertex_costs_stamped = new mesh_msgs.MeshVertexCostsStamped();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            mesh_vertex_costs_stamped.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            mesh_vertex_costs_stamped.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += mesh_vertex_costs_stamped.RosMessageLength;
                return size;
            }
        }
    }
}
