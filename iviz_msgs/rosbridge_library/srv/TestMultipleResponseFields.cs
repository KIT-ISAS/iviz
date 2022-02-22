using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class TestMultipleResponseFields : IService
    {
        /// Request message.
        [DataMember] public TestMultipleResponseFieldsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestMultipleResponseFieldsResponse Response { get; set; }
        
        /// Empty constructor.
        public TestMultipleResponseFields()
        {
            Request = TestMultipleResponseFieldsRequest.Singleton;
            Response = new TestMultipleResponseFieldsResponse();
        }
        
        /// Setter constructor.
        public TestMultipleResponseFields(TestMultipleResponseFieldsRequest request)
        {
            Request = request;
            Response = new TestMultipleResponseFieldsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestMultipleResponseFieldsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestMultipleResponseFieldsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/TestMultipleResponseFields";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestMultipleResponseFieldsRequest : IRequest<TestMultipleResponseFields, TestMultipleResponseFieldsResponse>, IDeserializable<TestMultipleResponseFieldsRequest>
    {
    
        /// Constructor for empty message.
        public TestMultipleResponseFieldsRequest()
        {
        }
        
        /// Constructor with buffer.
        public TestMultipleResponseFieldsRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TestMultipleResponseFieldsRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly TestMultipleResponseFieldsRequest Singleton = new TestMultipleResponseFieldsRequest();
    
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
    public sealed class TestMultipleResponseFieldsResponse : IResponse, IDeserializable<TestMultipleResponseFieldsResponse>
    {
        [DataMember (Name = "int")] public int @int;
        [DataMember (Name = "float")] public float @float;
        [DataMember (Name = "string")] public string @string;
        [DataMember (Name = "bool")] public bool @bool;
    
        /// Constructor for empty message.
        public TestMultipleResponseFieldsResponse()
        {
            @string = "";
        }
        
        /// Explicit constructor.
        public TestMultipleResponseFieldsResponse(int @int, float @float, string @string, bool @bool)
        {
            this.@int = @int;
            this.@float = @float;
            this.@string = @string;
            this.@bool = @bool;
        }
        
        /// Constructor with buffer.
        public TestMultipleResponseFieldsResponse(ref ReadBuffer b)
        {
            @int = b.Deserialize<int>();
            @float = b.Deserialize<float>();
            @string = b.DeserializeString();
            @bool = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestMultipleResponseFieldsResponse(ref b);
        
        public TestMultipleResponseFieldsResponse RosDeserialize(ref ReadBuffer b) => new TestMultipleResponseFieldsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(@int);
            b.Serialize(@float);
            b.Serialize(@string);
            b.Serialize(@bool);
        }
        
        public void RosValidate()
        {
            if (@string is null) throw new System.NullReferenceException(nameof(@string));
        }
    
        public int RosMessageLength => 13 + BuiltIns.GetStringSize(@string);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
