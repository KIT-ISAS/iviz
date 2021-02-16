using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestRequestOnly")]
    public sealed class TestRequestOnly : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestRequestOnlyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestRequestOnlyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestRequestOnly()
        {
            Request = new TestRequestOnlyRequest();
            Response = TestRequestOnlyResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
        public TestRequestOnly(TestRequestOnlyRequest request)
        {
            Request = request;
            Response = TestRequestOnlyResponse.Singleton;
        }
        
        IService IService.Create() => new TestRequestOnly();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestRequestOnly";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    }

    [DataContract]
    public sealed class TestRequestOnlyRequest : IRequest<TestRequestOnly, TestRequestOnlyResponse>, IDeserializable<TestRequestOnlyRequest>
    {
        [DataMember (Name = "data")] public int Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestOnlyRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestOnlyRequest(int Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestOnlyRequest(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestOnlyRequest(ref b);
        }
        
        TestRequestOnlyRequest IDeserializable<TestRequestOnlyRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class TestRequestOnlyResponse : IResponse, IDeserializable<TestRequestOnlyResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestOnlyResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestOnlyResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        TestRequestOnlyResponse IDeserializable<TestRequestOnlyResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly TestRequestOnlyResponse Singleton = new();
    
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
