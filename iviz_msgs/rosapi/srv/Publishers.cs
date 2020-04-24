namespace Iviz.Msgs.rosapi
{
    public class Publishers : IService
    {
        public sealed class Request : IRequest
        {
            public string topic;
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                topic = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out topic, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(topic, ref ptr, end);
            }
        
            public int GetLength()
            {
                int size = 4;
                size += topic.Length;
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public string[] publishers;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                publishers = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out publishers, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(publishers, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 8;
                for (int i = 0; i < publishers.Length; i++)
                {
                    size += publishers[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/Publishers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "cb37f09944e7ba1fc08ee38f7a94291d";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public Publishers()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public Publishers(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new Publishers();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
