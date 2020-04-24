namespace Iviz.Msgs.rosapi
{
    public class Topics : IService
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
            public string[] topics;
            public string[] types;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                topics = System.Array.Empty<string>();
                types = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out topics, ref ptr, end, 0);
                BuiltIns.Deserialize(out types, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(topics, ref ptr, end, 0);
                BuiltIns.Serialize(types, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 16;
                for (int i = 0; i < topics.Length; i++)
                {
                    size += topics[i].Length;
                }
                for (int i = 0; i < types.Length; i++)
                {
                    size += types[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/Topics";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "d966d98fc333fa1f3135af765eac1ba8";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public Topics()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public Topics(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new Topics();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
