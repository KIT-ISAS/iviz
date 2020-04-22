namespace Iviz.Msgs.rosbridge_library
{
    public class TestArrayRequest : IService
    {
        public sealed class Request : IRequest
        {
            public int[] @int;
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = TestArrayRequest.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = TestArrayRequest.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = TestArrayRequest.DependenciesBase64;
        
            public IResponse CreateResponse() => new Response();
        
            public bool IsResponseType<T>()
            {
                return typeof(T).Equals(typeof(Response));
            }
        
            public int GetLength()
            {
                int size = 4;
                size += 4 * @int.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Request()
            {
                @int = new int[0];
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out @int, ref ptr, end, 0);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(@int, ref ptr, end, 0);
            }
        }

        public sealed class Response : IResponse
        {
        
            public int GetLength() => 0;
        
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "rosbridge_library/TestArrayRequest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "3d7cfb7e4aa0844868966efa8a264398";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACsvMKzE2io5VyMwr4eXS1dXl5QIAVi7OQxIAAAA=";
            
    }

}
