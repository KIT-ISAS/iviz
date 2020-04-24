namespace Iviz.Msgs.tf2_msgs
{
    public class FrameGraph : IService
    {
        public sealed class Request : IRequest
        {
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public int GetLength() => 0;
        }

        public sealed class Response : IResponse
        {
            public string frame_yaml;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                frame_yaml = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out frame_yaml, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(frame_yaml, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += frame_yaml.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        internal const string _ServiceType = "tf2_msgs/FrameGraph";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "437ea58e9463815a0d511c7326b686b0";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public FrameGraph()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public FrameGraph(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new FrameGraph();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
