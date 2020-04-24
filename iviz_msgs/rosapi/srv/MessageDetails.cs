namespace Iviz.Msgs.rosapi
{
    public class MessageDetails : IService
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
            public TypeDef[] typedefs;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                typedefs = System.Array.Empty<TypeDef>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.DeserializeArray(out typedefs, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.SerializeArray(typedefs, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 4;
                for (int i = 0; i < typedefs.Length; i++)
                {
                    size += typedefs[i].GetLength();
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "rosapi/MessageDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public MessageDetails()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public MessageDetails(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new MessageDetails();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
