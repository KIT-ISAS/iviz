using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestNestedService : IService
    {
        /// <summary> Request message. </summary>
        public TestNestedServiceRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public TestNestedServiceResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestNestedService()
        {
            Request = new TestNestedServiceRequest();
            Response = new TestNestedServiceResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestNestedService(TestNestedServiceRequest request)
        {
            Request = request;
            Response = new TestNestedServiceResponse();
        }
        
        public IService Create() => new TestNestedService();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestNestedServiceRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestNestedServiceResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosbridge_library/TestNestedService";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "063d2b71e58b5225a457d4ee09dab6f6";
    }

    public sealed class TestNestedServiceRequest : IRequest
    {
        //request definition
        public geometry_msgs.Pose pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestNestedServiceRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestNestedServiceRequest(geometry_msgs.Pose pose)
        {
            this.pose = pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestNestedServiceRequest(Buffer b)
        {
            this.pose = new geometry_msgs.Pose(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestNestedServiceRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.pose.Serialize(b);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 56;
    }

    public sealed class TestNestedServiceResponse : IResponse
    {
        //response definition
        public std_msgs.Float64 data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestNestedServiceResponse()
        {
            data = new std_msgs.Float64();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestNestedServiceResponse(std_msgs.Float64 data)
        {
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestNestedServiceResponse(Buffer b)
        {
            this.data = new std_msgs.Float64(b);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestNestedServiceResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.data.Serialize(b);
        }
        
        public void Validate()
        {
            if (data is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }
}
