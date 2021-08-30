using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestMultipleResponseFields")]
    public sealed class TestMultipleResponseFields : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestMultipleResponseFieldsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestMultipleResponseFieldsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestMultipleResponseFields()
        {
            Request = TestMultipleResponseFieldsRequest.Singleton;
            Response = new TestMultipleResponseFieldsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestMultipleResponseFields(TestMultipleResponseFieldsRequest request)
        {
            Request = request;
            Response = new TestMultipleResponseFieldsResponse();
        }
        
        IService IService.Create() => new TestMultipleResponseFields();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestMultipleResponseFields";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestMultipleResponseFieldsRequest : IRequest<TestMultipleResponseFields, TestMultipleResponseFieldsResponse>, IDeserializable<TestMultipleResponseFieldsRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TestMultipleResponseFieldsRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestMultipleResponseFieldsRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        TestMultipleResponseFieldsRequest IDeserializable<TestMultipleResponseFieldsRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly TestMultipleResponseFieldsRequest Singleton = new TestMultipleResponseFieldsRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
    
        /// <summary> Constructor for empty message. </summary>
        public TestMultipleResponseFieldsResponse()
        {
            @string = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestMultipleResponseFieldsResponse(int @int, float @float, string @string, bool @bool)
        {
            this.@int = @int;
            this.@float = @float;
            this.@string = @string;
            this.@bool = @bool;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestMultipleResponseFieldsResponse(ref Buffer b)
        {
            @int = b.Deserialize<int>();
            @float = b.Deserialize<float>();
            @string = b.DeserializeString();
            @bool = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestMultipleResponseFieldsResponse(ref b);
        }
        
        TestMultipleResponseFieldsResponse IDeserializable<TestMultipleResponseFieldsResponse>.RosDeserialize(ref Buffer b)
        {
            return new TestMultipleResponseFieldsResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(@int);
            b.Serialize(@float);
            b.Serialize(@string);
            b.Serialize(@bool);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (@string is null) throw new System.NullReferenceException(nameof(@string));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 13;
                size += BuiltIns.UTF8.GetByteCount(@string);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
