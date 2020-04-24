namespace Iviz.Msgs.rosapi
{
    public class SearchParam : IService
    {
        public sealed class Request : IRequest
        {
            public string name;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                name = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out name, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(name, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += name.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public string global_name;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                global_name = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out global_name, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(global_name, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += global_name.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/SearchParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "dfadc39f113c1cc6d7759508d8461d5a";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SearchParam()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public SearchParam(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new SearchParam();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
