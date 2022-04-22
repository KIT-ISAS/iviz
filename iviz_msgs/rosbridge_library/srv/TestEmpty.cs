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
        public TestEmptyRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TestEmptyRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static TestEmptyRequest? singleton;
        public static TestEmptyRequest Singleton => singleton ??= new TestEmptyRequest();
    
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
    public sealed class TestEmptyResponse : IResponse, IDeserializable<TestEmptyResponse>
    {
    
        /// Constructor for empty message.
        public TestEmptyResponse()
        {
        }
        
        /// Constructor with buffer.
        public TestEmptyResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TestEmptyResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static TestEmptyResponse? singleton;
        public static TestEmptyResponse Singleton => singleton ??= new TestEmptyResponse();
    
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
}
