using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestArrayRequest : IService
    {
        /// <summary> Request message. </summary>
        public TestArrayRequestRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public TestArrayRequestResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestArrayRequest()
        {
            Request = new TestArrayRequestRequest();
            Response = new TestArrayRequestResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestArrayRequest(TestArrayRequestRequest request)
        {
            Request = request;
            Response = new TestArrayRequestResponse();
        }
        
        public IService Create() => new TestArrayRequest();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosbridge_library/TestArrayRequest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "3d7cfb7e4aa0844868966efa8a264398";
    }

    public sealed class TestArrayRequestRequest : IRequest
    {
        public int[] @int;
    
        /// <summary> Constructor for empty message. </summary>
        public TestArrayRequestRequest()
        {
            @int = System.Array.Empty<int>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out @int, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(@int, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * @int.Length;
                return size;
            }
        }
    }

    public sealed class TestArrayRequestResponse : Internal.EmptyResponse
    {
    }
}
