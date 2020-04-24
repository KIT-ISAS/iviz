namespace Iviz.Msgs.diagnostic_msgs
{
    public class SelfTest : IService
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
            public string id;
            public byte passed;
            public DiagnosticStatus[] status;
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                id = "";
                status = System.Array.Empty<DiagnosticStatus>();
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out id, ref ptr, end);
                BuiltIns.Deserialize(out passed, ref ptr, end);
                BuiltIns.DeserializeArray(out status, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(id, ref ptr, end);
                BuiltIns.Serialize(passed, ref ptr, end);
                BuiltIns.SerializeArray(status, ref ptr, end, 0);
            }
        
            public int GetLength()
            {
                int size = 9;
                size += id.Length;
                for (int i = 0; i < status.Length; i++)
                {
                    size += status[i].GetLength();
                }
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string ServiceType = "diagnostic_msgs/SelfTest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "ac21b1bab7ab17546986536c22eb34e9";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SelfTest()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public SelfTest(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new SelfTest();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
