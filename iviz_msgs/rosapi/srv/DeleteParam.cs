namespace Iviz.Msgs.rosapi
{
    public class DeleteParam : IService
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
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        
            public int GetLength() => 0;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/DeleteParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "c1f3d28f1b044c871e6eff2e9fc3c667";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public DeleteParam()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public DeleteParam(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new DeleteParam();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
