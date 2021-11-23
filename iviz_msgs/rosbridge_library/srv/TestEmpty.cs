using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class TestEmpty : IService
    {
        /// Request message.
        [DataMember] public TestEmptyRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestEmptyResponse Response { get; set; }
        
        /// Empty constructor.
        public TestEmpty()
        {
            Request = TestEmptyRequest.Singleton;
            Response = TestEmptyResponse.Singleton;
        }
        
        /// Setter constructor.
        public TestEmpty(TestEmptyRequest request)
        {
            Request = request;
            Response = TestEmptyResponse.Singleton;
        }
        
        IService IService.Create() => new TestEmpty();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestEmptyRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestEmptyResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/TestEmpty";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestEmptyRequest : IRequest<TestEmpty, TestEmptyResponse>, IDeserializable<TestEmptyRequest>
    {
    
        /// Constructor for empty message.
        public TestEmptyRequest()
        {
        }
        
        /// Constructor with buffer.
        internal TestEmptyRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        TestEmptyRequest IDeserializable<TestEmptyRequest>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly TestEmptyRequest Singleton = new TestEmptyRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestEmptyResponse : IResponse, IDeserializable<TestEmptyResponse>
    {
    
        /// Constructor for empty message.
        public TestEmptyResponse()
        {
        }
        
        /// Constructor with buffer.
        internal TestEmptyResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        TestEmptyResponse IDeserializable<TestEmptyResponse>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly TestEmptyResponse Singleton = new TestEmptyResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
