namespace Iviz.Msgs.rosapi
{
    public class TopicType : IService
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
            public string type;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/TopicType";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "0d30b3f53a0fd5036523a7141e524ddf";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public TopicType()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public TopicType(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new TopicType();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
