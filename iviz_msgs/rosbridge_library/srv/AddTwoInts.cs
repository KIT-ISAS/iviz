namespace Iviz.Msgs.rosbridge_library
{
    public class AddTwoInts : IService
    {
        public sealed class Request : IRequest
        {
            public long a;
            public long b;
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = AddTwoInts.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = AddTwoInts.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = AddTwoInts.DependenciesBase64;
        
            public IResponse CreateResponse() => new Response();
        
            public bool IsResponseType<T>()
            {
                return typeof(T).Equals(typeof(Response));
            }
        
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
        public const string MessageType = "rosbridge_library/AddTwoInts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "6a2e34150c00229791cc89ff309fff21";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsvMKzEzUUjkygTTSVy6urpQdnFpLhcvFwBzZU9BIAAAAA==";
            
    }

}
