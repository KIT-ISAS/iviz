using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    [DataContract]
    public sealed class TestNestedService : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestNestedServiceRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestNestedServiceResponse Response { get; set; }
        
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
        
        IService IService.Create() => new TestNestedService();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestNestedService";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "063d2b71e58b5225a457d4ee09dab6f6";
    }

    public sealed class TestNestedServiceRequest : IRequest
    {
        //request definition
        [DataMember] public geometry_msgs.Pose pose { get; set; }
    
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
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestNestedServiceRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.pose);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 56;
    }

    public sealed class TestNestedServiceResponse : IResponse
    {
        //response definition
        [DataMember] public std_msgs.Float64 data { get; set; }
    
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
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestNestedServiceResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
            if (data is null) throw new System.NullReferenceException();
            data.Validate();
        }
    
        public int RosMessageLength => 8;
    }
}
