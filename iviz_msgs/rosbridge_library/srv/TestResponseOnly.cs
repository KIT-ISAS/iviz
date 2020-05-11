using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestResponseOnly : IService
    {
        /// <summary> Request message. </summary>
        public TestResponseOnlyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public TestResponseOnlyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestResponseOnly()
        {
            Request = new TestResponseOnlyRequest();
            Response = new TestResponseOnlyResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestResponseOnly(TestResponseOnlyRequest request)
        {
            Request = request;
            Response = new TestResponseOnlyResponse();
        }
        
        public IService Create() => new TestResponseOnly();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestResponseOnlyRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestResponseOnlyResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosbridge_library/TestResponseOnly";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    }

    public sealed class TestResponseOnlyRequest : Internal.EmptyRequest
    {
    }

    public sealed class TestResponseOnlyResponse : IResponse
    {
        public int data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestResponseOnlyResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestResponseOnlyResponse(int data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestResponseOnlyResponse(Buffer b)
        {
            this.data = b.Deserialize<int>();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestResponseOnlyResponse(b);
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
}
