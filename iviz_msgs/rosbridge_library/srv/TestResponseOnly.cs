namespace Iviz.Msgs.rosbridge_library
{
    public class TestResponseOnly : IService
    {
        public sealed class Request : IRequest
        {
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = TestResponseOnly.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = TestResponseOnly.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = TestResponseOnly.DependenciesBase64;
        
            public IResponse CreateResponse() => new Response();
        
            public bool IsResponseType<T>()
            {
                return typeof(T).Equals(typeof(Response));
            }
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        }

        public sealed class Response : IResponse
        {
            public int data;
        
            public int GetLength() => 4;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out data, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(data, ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "rosbridge_library/TestResponseOnly";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACtPV1eXlyswrMTZSSEksSeTlAgBuTv+KEQAAAA==";
            
    }

}
