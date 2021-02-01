using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestEmpty")]
    public sealed class TestEmpty : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestEmptyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestEmptyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestEmpty()
        {
            Request = TestEmptyRequest.Singleton;
            Response = TestEmptyResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestEmpty";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    }

    [DataContract]
    public sealed class TestEmptyRequest : IRequest<TestEmpty, TestEmptyResponse>, IDeserializable<TestEmptyRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TestEmptyRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestEmptyRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        TestEmptyRequest IDeserializable<TestEmptyRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly TestEmptyRequest Singleton = new TestEmptyRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class TestEmptyResponse : IResponse, IDeserializable<TestEmptyResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TestEmptyResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestEmptyResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        TestEmptyResponse IDeserializable<TestEmptyResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly TestEmptyResponse Singleton = new TestEmptyResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
