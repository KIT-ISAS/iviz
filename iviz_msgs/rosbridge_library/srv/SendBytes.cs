namespace Iviz.Msgs.rosbridge_library
{
    public class SendBytes : IService
    {
        public sealed class Request : IRequest
        {
            public long count;
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = SendBytes.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = SendBytes.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = SendBytes.DependenciesBase64;
        
            public IResponse CreateResponse() => new Response();
        
            public bool IsResponseType<T>()
            {
                return typeof(T).Equals(typeof(Response));
            }
        
            public int GetLength() => 8;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out count, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(count, ref ptr, end);
            }
        }

        public sealed class Response : IResponse
        {
            public string data;
        
            public int GetLength()
            {
                int size = 4;
                size += data.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                data = "";
            }
            
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
        public const string MessageType = "rosbridge_library/SendBytes";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "d875457256decc7436099d9d612ebf8a";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsvMKzEzUUjOL80r4dLV1eUqLinKzEtXSEksSeTi5QIAadcQWR4AAAA=";
            
    }

}
