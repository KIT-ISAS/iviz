namespace Iviz.Msgs.rosapi
{
    public class TopicsForType : IService
    {
        public sealed class Request : IRequest
        {
            public string type;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                type = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out type, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(type, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += type.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public string[] topics;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                topics = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out topics, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(topics, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 8;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += topics[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/TopicsForType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "56f77ff6da756dd27c1ed16ec721072a";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TopicsForType()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicsForType(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new TopicsForType();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
