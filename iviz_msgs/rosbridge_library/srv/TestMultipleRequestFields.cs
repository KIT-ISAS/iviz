using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestMultipleRequestFields")]
    public sealed class TestMultipleRequestFields : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestMultipleRequestFieldsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestMultipleRequestFieldsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestMultipleRequestFields()
        {
            Request = new TestMultipleRequestFieldsRequest();
            Response = TestMultipleRequestFieldsResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
        public TestMultipleRequestFields(TestMultipleRequestFieldsRequest request)
        {
            Request = request;
            Response = TestMultipleRequestFieldsResponse.Singleton;
        }
        
        IService IService.Create() => new TestMultipleRequestFields();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestMultipleRequestFields";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestMultipleRequestFieldsRequest : IRequest<TestMultipleRequestFields, TestMultipleRequestFieldsResponse>, IDeserializable<TestMultipleRequestFieldsRequest>
    {
        [DataMember (Name = "int")] public int @int { get; set; }
        [DataMember (Name = "float")] public float @float { get; set; }
        [DataMember (Name = "string")] public string @string { get; set; }
        [DataMember (Name = "bool")] public bool @bool { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestMultipleRequestFieldsRequest()
        {
            @string = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestMultipleRequestFieldsRequest(int @int, float @float, string @string, bool @bool)
        {
            this.@int = @int;
            this.@float = @float;
            this.@string = @string;
            this.@bool = @bool;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestMultipleRequestFieldsRequest(ref Buffer b)
        {
            @int = b.Deserialize<int>();
            @float = b.Deserialize<float>();
            @string = b.DeserializeString();
            @bool = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestMultipleRequestFieldsRequest(ref b);
        }
        
        TestMultipleRequestFieldsRequest IDeserializable<TestMultipleRequestFieldsRequest>.RosDeserialize(ref Buffer b)
        {
            return new TestMultipleRequestFieldsRequest(ref b);
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

    [DataContract]
    public sealed class TestMultipleRequestFieldsResponse : IResponse, IDeserializable<TestMultipleRequestFieldsResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TestMultipleRequestFieldsResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestMultipleRequestFieldsResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        TestMultipleRequestFieldsResponse IDeserializable<TestMultipleRequestFieldsResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly TestMultipleRequestFieldsResponse Singleton = new TestMultipleRequestFieldsResponse();
    
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
}
