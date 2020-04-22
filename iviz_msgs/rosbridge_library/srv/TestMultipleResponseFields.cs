namespace Iviz.Msgs.rosbridge_library
{
    public class TestMultipleResponseFields : IService
    {
        public sealed class Request : IRequest
        {
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = TestMultipleResponseFields.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = TestMultipleResponseFields.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = TestMultipleResponseFields.DependenciesBase64;
        
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
            public int @int;
            public float @float;
            public string @string;
            public bool @bool;
        
            public int GetLength()
            {
                int size = 13;
                size += @string.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                @string = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out @int, ref ptr, end);
                BuiltIns.Deserialize(out @float, ref ptr, end);
                BuiltIns.Deserialize(out @string, ref ptr, end);
                BuiltIns.Deserialize(out @bool, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(@int, ref ptr, end);
                BuiltIns.Serialize(@float, ref ptr, end);
                BuiltIns.Serialize(@string, ref ptr, end);
                BuiltIns.Serialize(@bool, ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "rosbridge_library/TestMultipleResponseFields";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACtPV1eXlyswrMTZSAJK8XGk5+YkgDpjm5SouKcrMS1eAULxcSfn5OQoggpcLAORK02Q5" +
            "AAAA";
            
    }

}
