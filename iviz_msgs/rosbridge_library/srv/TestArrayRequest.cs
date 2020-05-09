using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestArrayRequest : IService
    {
        /// <summary> Request message. </summary>
        public TestArrayRequestRequest Request { get; set; }
        
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
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestArrayRequestRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestArrayRequestResponse)value;
        }
        
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
        public int[] @int { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestArrayRequestRequest()
        {
            @int = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestArrayRequestRequest(int[] @int)
        {
            this.@int = @int ?? throw new System.ArgumentNullException(nameof(@int));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestArrayRequestRequest(Buffer b)
        {
            this.@int = BuiltIns.DeserializeStructArray<int>(b, 0);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestArrayRequestRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.@int, b, 0);
        }
        
        public void Validate()
        {
            if (@int is null) throw new System.NullReferenceException();
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
