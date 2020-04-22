namespace Iviz.Msgs.tf2_msgs
{
    public class FrameGraph : IService
    {
        public sealed class Request : IRequest
        {
        
            /// <summary> Full ROS name of the parent service. </summary>
            public const string MessageType = FrameGraph.MessageType;
        
            /// <summary> MD5 hash of a compact representation of the parent service. </summary>
            public const string Md5Sum = FrameGraph.Md5Sum;
        
            /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
            public const string DependenciesBase64 = FrameGraph.DependenciesBase64;
        
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
            public string frame_yaml;
        
            public int GetLength()
            {
                int size = 4;
                size += frame_yaml.Length;
                return size;
            }
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
            {
                frame_yaml = "";
            }
            
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Deserialize(out frame_yaml, ref ptr, end);
            }
        
            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
                BuiltIns.Serialize(frame_yaml, ref ptr, end);
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string MessageType = "tf2_msgs/FrameGraph";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string Md5Sum = "437ea58e9463815a0d511c7326b686b0";
        
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAACtPV1eUqLinKzEtXSCtKzE2Nr0zMzeHi5QIAyjqj3BgAAAA=";
            
    }

}
