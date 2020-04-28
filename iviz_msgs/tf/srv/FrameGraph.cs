using System.Runtime.Serialization;

namespace Iviz.Msgs.tf
{
    public sealed class FrameGraph : IService
    {
        /// <summary> Request message. </summary>
        public FrameGraphRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public FrameGraphResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public FrameGraph()
        {
            Request = new FrameGraphRequest();
            Response = new FrameGraphResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public FrameGraph(FrameGraphRequest request)
        {
            Request = request;
            Response = new FrameGraphResponse();
        }
        
        public IService Create() => new FrameGraph();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "tf/FrameGraph";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "c4af9ac907e58e906eb0b6e3c58478c0";
    }

    public sealed class FrameGraphRequest : Internal.EmptyRequest
    {
    }

    public sealed class FrameGraphResponse : IResponse
    {
        public string dot_graph;
    
        /// <summary> Constructor for empty message. </summary>
        public FrameGraphResponse()
        {
            dot_graph = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out dot_graph, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(dot_graph, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(dot_graph);
                return size;
            }
        }
    }
}
