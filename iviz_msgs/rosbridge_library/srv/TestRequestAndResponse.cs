using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestRequestAndResponse")]
    public sealed class TestRequestAndResponse : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestRequestAndResponseRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestRequestAndResponseResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestRequestAndResponse()
        {
            Request = new TestRequestAndResponseRequest();
            Response = new TestRequestAndResponseResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestRequestAndResponse";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "491d316f183df11876531749005b31d1";
    }

    [DataContract]
    public sealed class TestRequestAndResponseRequest : IRequest, IDeserializable<TestRequestAndResponseRequest>
    {
        [DataMember (Name = "data")] public int Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestAndResponseRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestAndResponseRequest(int Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestAndResponseRequest(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestAndResponseRequest(ref b);
        }
        
        TestRequestAndResponseRequest IDeserializable<TestRequestAndResponseRequest>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestAndResponseRequest(ref b);
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

    [DataContract]
    public sealed class TestRequestAndResponseResponse : IResponse, IDeserializable<TestRequestAndResponseResponse>
    {
        [DataMember (Name = "data")] public int Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestAndResponseResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestAndResponseResponse(int Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestAndResponseResponse(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestAndResponseResponse(ref b);
        }
        
        TestRequestAndResponseResponse IDeserializable<TestRequestAndResponseResponse>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestAndResponseResponse(ref b);
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
