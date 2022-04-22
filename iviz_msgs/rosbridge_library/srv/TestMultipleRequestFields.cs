using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class TestMultipleRequestFields : IService
    {
        /// Request message.
        [DataMember] public TestMultipleRequestFieldsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestMultipleRequestFieldsResponse Response { get; set; }
        
        /// Empty constructor.
        public TestMultipleRequestFields()
        {
            Request = new TestMultipleRequestFieldsRequest();
            Response = TestMultipleRequestFieldsResponse.Singleton;
        }
        
        /// Setter constructor.
        public TestMultipleRequestFields(TestMultipleRequestFieldsRequest request)
        {
            Request = request;
            Response = TestMultipleRequestFieldsResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestMultipleRequestFieldsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestMultipleRequestFieldsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/TestMultipleRequestFields";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestMultipleRequestFieldsRequest : IRequest<TestMultipleRequestFields, TestMultipleRequestFieldsResponse>, IDeserializable<TestMultipleRequestFieldsRequest>
    {
        [DataMember (Name = "int")] public int @int;
        [DataMember (Name = "float")] public float @float;
        [DataMember (Name = "string")] public string @string;
        [DataMember (Name = "bool")] public bool @bool;
    
        /// Constructor for empty message.
        public TestMultipleRequestFieldsRequest()
        {
            @string = "";
        }
        
        /// Explicit constructor.
        public TestMultipleRequestFieldsRequest(int @int, float @float, string @string, bool @bool)
        {
            this.@int = @int;
            this.@float = @float;
            this.@string = @string;
            this.@bool = @bool;
        }
        
        /// Constructor with buffer.
        public TestMultipleRequestFieldsRequest(ref ReadBuffer b)
        {
            b.Deserialize(out @int);
            b.Deserialize(out @float);
            @string = b.DeserializeString();
            b.Deserialize(out @bool);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestMultipleRequestFieldsRequest(ref b);
        
        public TestMultipleRequestFieldsRequest RosDeserialize(ref ReadBuffer b) => new TestMultipleRequestFieldsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(@int);
            b.Serialize(@float);
            b.Serialize(@string);
            b.Serialize(@bool);
        }
        
        public void RosValidate()
        {
            if (@string is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 13 + BuiltIns.GetStringSize(@string);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestMultipleRequestFieldsResponse : IResponse, IDeserializable<TestMultipleRequestFieldsResponse>
    {
    
        /// Constructor for empty message.
        public TestMultipleRequestFieldsResponse()
        {
        }
        
        /// Constructor with buffer.
        public TestMultipleRequestFieldsResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TestMultipleRequestFieldsResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static TestMultipleRequestFieldsResponse? singleton;
        public static TestMultipleRequestFieldsResponse Singleton => singleton ??= new TestMultipleRequestFieldsResponse();
    
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
