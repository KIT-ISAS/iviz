using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class TestNestedService : IService
    {
        /// Request message.
        [DataMember] public TestNestedServiceRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestNestedServiceResponse Response { get; set; }
        
        /// Empty constructor.
        public TestNestedService()
        {
            Request = new TestNestedServiceRequest();
            Response = new TestNestedServiceResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/TestNestedService";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "063d2b71e58b5225a457d4ee09dab6f6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestNestedServiceRequest : IRequest<TestNestedService, TestNestedServiceResponse>, IDeserializable<TestNestedServiceRequest>
    {
        //request definition
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
    
        /// Constructor for empty message.
        public TestNestedServiceRequest()
        {
        }
        
        /// Explicit constructor.
        public TestNestedServiceRequest(in GeometryMsgs.Pose Pose)
        {
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        internal TestNestedServiceRequest(ref Buffer b)
        {
            b.Deserialize(out Pose);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestNestedServiceRequest(ref b);
        
        TestNestedServiceRequest IDeserializable<TestNestedServiceRequest>.RosDeserialize(ref Buffer b) => new TestNestedServiceRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Pose);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestNestedServiceResponse : IResponse, IDeserializable<TestNestedServiceResponse>
    {
        //response definition
        [DataMember (Name = "data")] public StdMsgs.Float64 Data;
    
        /// Constructor for empty message.
        public TestNestedServiceResponse()
        {
            Data = new StdMsgs.Float64();
        }
        
        /// Explicit constructor.
        public TestNestedServiceResponse(StdMsgs.Float64 Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal TestNestedServiceResponse(ref Buffer b)
        {
            Data = new StdMsgs.Float64(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestNestedServiceResponse(ref b);
        
        TestNestedServiceResponse IDeserializable<TestNestedServiceResponse>.RosDeserialize(ref Buffer b) => new TestNestedServiceResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Data.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
            Data.RosValidate();
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
