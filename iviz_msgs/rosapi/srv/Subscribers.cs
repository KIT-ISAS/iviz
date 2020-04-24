namespace Iviz.Msgs.rosapi
{
    public class Subscribers : IService
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
            public string[] subscribers;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                subscribers = System.Array.Empty<string>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out subscribers, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(subscribers, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 8;
                for (int i = 0; i < subscribers.Length; i++)
                {
                    size += subscribers[i].Length;
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/Subscribers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "cb387b68f5b29bc1456398ee8476b973";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public Subscribers()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public Subscribers(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new Subscribers();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
