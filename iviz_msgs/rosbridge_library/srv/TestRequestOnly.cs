using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestRequestOnly : IService
    {
        /// <summary> Request message. </summary>
        public TestRequestOnlyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public TestRequestOnlyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestRequestOnly()
        {
            Request = new TestRequestOnlyRequest();
            Response = new TestRequestOnlyResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestRequestOnly(TestRequestOnlyRequest request)
        {
            Request = request;
            Response = new TestRequestOnlyResponse();
        }
        
        public IService Create() => new TestRequestOnly();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestRequestOnlyRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestRequestOnlyResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosbridge_library/TestRequestOnly";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    }

    public sealed class TestRequestOnlyRequest : IRequest
    {
        public int data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestOnlyRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestOnlyRequest(int data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestRequestOnlyRequest(Buffer b)
        {
            this.data = b.Deserialize<int>();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestRequestOnlyRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 4;
    }

    public sealed class TestRequestOnlyResponse : Internal.EmptyResponse
    {
    }
}
