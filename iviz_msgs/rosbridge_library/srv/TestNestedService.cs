using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract]
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
        
        public const string ServiceType = "rosbridge_library/TestNestedService";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "063d2b71e58b5225a457d4ee09dab6f6";
        
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
        public TestNestedServiceRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Pose);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestNestedServiceRequest(ref b);
        
        public TestNestedServiceRequest RosDeserialize(ref ReadBuffer b) => new TestNestedServiceRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Pose);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 56;
        
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
        public TestNestedServiceResponse(ref ReadBuffer b)
        {
            Data = new StdMsgs.Float64(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestNestedServiceResponse(ref b);
        
        public TestNestedServiceResponse RosDeserialize(ref ReadBuffer b) => new TestNestedServiceResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Data.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Data is null) BuiltIns.ThrowNullReference();
            Data.RosValidate();
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
