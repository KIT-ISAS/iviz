using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestNestedService")]
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
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestNestedServiceRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestNestedServiceRequest(in GeometryMsgs.Pose Pose)
        {
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestNestedServiceRequest(Buffer b)
        {
            Pose = new GeometryMsgs.Pose(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestNestedServiceRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Pose);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 56;
    }

    public sealed class TestNestedServiceResponse : IResponse
    {
        //response definition
        [DataMember (Name = "data")] public StdMsgs.Float64 Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestNestedServiceResponse()
        {
            Data = new StdMsgs.Float64();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestNestedServiceResponse(StdMsgs.Float64 Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestNestedServiceResponse(Buffer b)
        {
            Data = new StdMsgs.Float64(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestNestedServiceResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Data);
        }
        
        public void Validate()
        {
            if (Data is null) throw new System.NullReferenceException();
            Data.Validate();
        }
    
        public int RosMessageLength => 8;
    }
}
