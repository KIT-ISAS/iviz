namespace Iviz.Msgs.rosapi
{
    public class GetActionServers : IService
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
            public string[] action_servers;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                action_servers = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out action_servers, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(action_servers, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 8;
                for (int i = 0; i < action_servers.Length; i++)
                {
                    size += action_servers[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/GetActionServers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "46807ba271844ac5ba4730a47556b236";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetActionServers()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetActionServers(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new GetActionServers();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
