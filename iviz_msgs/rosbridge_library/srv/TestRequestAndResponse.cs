using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class TestRequestAndResponse : IService
    {
        /// Request message.
        [DataMember] public TestRequestAndResponseRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestRequestAndResponseResponse Response { get; set; }
        
        /// Empty constructor.
        public TestRequestAndResponse()
        {
            Request = new TestRequestAndResponseRequest();
            Response = new TestRequestAndResponseResponse();
        }
        
        /// Setter constructor.
        public TestRequestAndResponse(TestRequestAndResponseRequest request)
        {
            Request = request;
            Response = new TestRequestAndResponseResponse();
        }
        
        IService IService.Create() => new TestRequestAndResponse();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestRequestAndResponseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestRequestAndResponseResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/TestRequestAndResponse";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "491d316f183df11876531749005b31d1";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestRequestAndResponseRequest : IRequest<TestRequestAndResponse, TestRequestAndResponseResponse>, IDeserializable<TestRequestAndResponseRequest>
    {
        [DataMember (Name = "data")] public int Data;
    
        /// Constructor for empty message.
        public TestRequestAndResponseRequest()
        {
        }
        
        /// Explicit constructor.
        public TestRequestAndResponseRequest(int Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal TestRequestAndResponseRequest(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestRequestAndResponseRequest(ref b);
        
        TestRequestAndResponseRequest IDeserializable<TestRequestAndResponseRequest>.RosDeserialize(ref Buffer b) => new TestRequestAndResponseRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestRequestAndResponseResponse : IResponse, IDeserializable<TestRequestAndResponseResponse>
    {
        [DataMember (Name = "data")] public int Data;
    
        /// Constructor for empty message.
        public TestRequestAndResponseResponse()
        {
        }
        
        /// Explicit constructor.
        public TestRequestAndResponseResponse(int Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal TestRequestAndResponseResponse(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestRequestAndResponseResponse(ref b);
        
        TestRequestAndResponseResponse IDeserializable<TestRequestAndResponseResponse>.RosDeserialize(ref Buffer b) => new TestRequestAndResponseResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
