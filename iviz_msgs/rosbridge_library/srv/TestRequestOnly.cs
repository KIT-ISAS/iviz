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
            Response = new TestRequestOnlyResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestRequestOnly(TestRequestOnlyRequest request)
        {
            Request = request;
            Response = new TestRequestOnlyResponse();
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestRequestOnly";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    }

    public sealed class TestRequestOnlyRequest : IRequest, IDeserializable<TestRequestOnlyRequest>
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
        internal TestRequestOnlyRequest(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestOnlyRequest(ref b);
        }
        
        TestRequestOnlyRequest IDeserializable<TestRequestOnlyRequest>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestOnlyRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 4;
    }

    public sealed class TestRequestOnlyResponse : Internal.EmptyResponse
    {
    }
}
