using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestResponseOnly")]
    public sealed class TestResponseOnly : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestResponseOnlyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestResponseOnlyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestResponseOnly()
        {
            Request = new TestResponseOnlyRequest();
            Response = new TestResponseOnlyResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestResponseOnly";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    }

    public sealed class TestResponseOnlyRequest : Internal.EmptyRequest
    {
    }

    public sealed class TestResponseOnlyResponse : IResponse, IDeserializable<TestResponseOnlyResponse>
    {
        [DataMember (Name = "data")] public int Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestResponseOnlyResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestResponseOnlyResponse(int Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestResponseOnlyResponse(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestResponseOnlyResponse(ref b);
        }
        
        TestResponseOnlyResponse IDeserializable<TestResponseOnlyResponse>.RosDeserialize(ref Buffer b)
        {
            return new TestResponseOnlyResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
