using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class Nodes : IService
    {
        /// <summary> Request message. </summary>
        public NodesRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public NodesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Nodes()
        {
            Request = new NodesRequest();
            Response = new NodesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Nodes(NodesRequest request)
        {
            Request = request;
            Response = new NodesResponse();
        }
        
        public IService Create() => new Nodes();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/Nodes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "3d07bfda1268b4f76b16b7ba8a82665d";
    }

    public sealed class NodesRequest : IRequest
    {
        
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 0;
    }

    public sealed class NodesResponse : IResponse
    {
        public string[] nodes;
    
        /// <summary> Constructor for empty message. </summary>
        public NodesResponse()
        {
            nodes = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out nodes, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(nodes, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * nodes.Length;
                for (int i = 0; i < nodes.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(nodes[i]);
                }
                return size;
            }
        }
    }
}
