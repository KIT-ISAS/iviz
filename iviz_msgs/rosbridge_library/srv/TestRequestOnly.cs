using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract]
    public sealed class TestRequestOnly : IService
    {
        /// Request message.
        [DataMember] public TestRequestOnlyRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestRequestOnlyResponse Response { get; set; }
        
        /// Empty constructor.
        public TestRequestOnly()
        {
            Request = new TestRequestOnlyRequest();
            Response = TestRequestOnlyResponse.Singleton;
        }
        
        /// Setter constructor.
        public TestRequestOnly(TestRequestOnlyRequest request)
        {
            Request = request;
            Response = TestRequestOnlyResponse.Singleton;
        }
        
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
        
        public const string ServiceType = "rosbridge_library/TestRequestOnly";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "da5909fbe378aeaf85e547e830cc1bb7";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestRequestOnlyRequest : IRequest<TestRequestOnly, TestRequestOnlyResponse>, IDeserializable<TestRequestOnlyRequest>
    {
        [DataMember (Name = "data")] public int Data;
    
        /// Constructor for empty message.
        public TestRequestOnlyRequest()
        {
        }
        
        /// Explicit constructor.
        public TestRequestOnlyRequest(int Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public TestRequestOnlyRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestRequestOnlyRequest(ref b);
        
        public TestRequestOnlyRequest RosDeserialize(ref ReadBuffer b) => new TestRequestOnlyRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestRequestOnlyResponse : IResponse, IDeserializable<TestRequestOnlyResponse>
    {
    
        /// Constructor for empty message.
        public TestRequestOnlyResponse()
        {
        }
        
        /// Constructor with buffer.
        public TestRequestOnlyResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TestRequestOnlyResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static TestRequestOnlyResponse? singleton;
        public static TestRequestOnlyResponse Singleton => singleton ??= new TestRequestOnlyResponse();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
