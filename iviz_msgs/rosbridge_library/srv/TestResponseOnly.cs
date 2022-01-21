using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class TestResponseOnly : IService
    {
        /// Request message.
        [DataMember] public TestResponseOnlyRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestResponseOnlyResponse Response { get; set; }
        
        /// Empty constructor.
        public TestResponseOnly()
        {
            Request = TestResponseOnlyRequest.Singleton;
            Response = new TestResponseOnlyResponse();
        }
        
        /// Setter constructor.
        public TestResponseOnly(TestResponseOnlyRequest request)
        {
            Request = request;
            Response = new TestResponseOnlyResponse();
        }
        
        IService IService.Create() => new TestResponseOnly();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/TestResponseOnly";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestResponseOnlyRequest : IRequest<TestResponseOnly, TestResponseOnlyResponse>, IDeserializable<TestResponseOnlyRequest>
    {
    
        /// Constructor for empty message.
        public TestResponseOnlyRequest()
        {
        }
        
        /// Constructor with buffer.
        public TestResponseOnlyRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TestResponseOnlyRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly TestResponseOnlyRequest Singleton = new TestResponseOnlyRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestResponseOnlyResponse : IResponse, IDeserializable<TestResponseOnlyResponse>
    {
        [DataMember (Name = "data")] public int Data;
    
        /// Constructor for empty message.
        public TestResponseOnlyResponse()
        {
        }
        
        /// Explicit constructor.
        public TestResponseOnlyResponse(int Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public TestResponseOnlyResponse(ref ReadBuffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestResponseOnlyResponse(ref b);
        
        public TestResponseOnlyResponse RosDeserialize(ref ReadBuffer b) => new TestResponseOnlyResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
