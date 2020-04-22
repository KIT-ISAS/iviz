namespace Iviz.Msgs.rosbridge_library
{
    public class AddTwoInts : IService
    {
        public sealed class Request : IRequest
        {
            public long a;
            public long b;
        
            public int GetLength() => 16;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out a, ref ptr, end);
                BuiltIns.Deserialize(out b, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(a, ref ptr, end);
                BuiltIns.Serialize(b, ref ptr, end);
            }
        
            public Response Call(IServiceCaller caller)
            {
                AddTwoInts s = new AddTwoInts(this);
                caller.Call(s);
                return s.response;
            }
        }

        public sealed class Response : IResponse
        {
            public long sum;
        
            public int GetLength() => 8;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out sum, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(sum, ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "rosbridge_library/AddTwoInts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "6a2e34150c00229791cc89ff309fff21";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsvMKzEzUUjkygTTSVy6urpQdnFpLhcvFwBzZU9BIAAAAA==";
            
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public AddTwoInts()
        {
            request = new Request();
        }
        
        /// <summary> Setter constructor. </summary>
        public AddTwoInts(Request request)
        {
            this.request = request;
        }
        
        public IResponse CreateResponse() => new Response();
        
        public IRequest GetRequest() => request;
        
        public void SetResponse(IResponse response)
        {
            this.response = (Response)response;
        }
    }

}
