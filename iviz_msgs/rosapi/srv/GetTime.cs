namespace Iviz.Msgs.rosapi
{
    public class GetTime : IService
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
            public time time;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out time, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(time, ref ptr, end);
            }
        
            public int GetLength() => 8;
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/GetTime";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "556a4fb76023a469987922359d08a844";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public GetTime()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetTime(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new GetTime();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
