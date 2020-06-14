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
            Request = new TestMultipleResponseFieldsRequest();
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestMultipleResponseFields";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "6cce9fb727dd0f31d504d7d198a1f4ef";
    }

    public sealed class TestMultipleResponseFieldsRequest : Internal.EmptyRequest
    {
    }

    public sealed class TestMultipleResponseFieldsResponse : IResponse
    {
        [DataMember (Name = "int")] public int @int { get; set; }
        [DataMember (Name = "float")] public float @float { get; set; }
        [DataMember (Name = "string")] public string @string { get; set; }
        [DataMember (Name = "bool")] public bool @bool { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestMultipleResponseFieldsResponse()
        {
            @string = "";
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
        internal TestMultipleResponseFieldsResponse(Buffer b)
        {
            @int = b.Deserialize<int>();
            @float = b.Deserialize<float>();
            @string = b.DeserializeString();
            @bool = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TestMultipleResponseFieldsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(@int);
            b.Serialize(@float);
            b.Serialize(@string);
            b.Serialize(@bool);
        }
        
        public void RosValidate()
        {
            if (@string is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 13;
                size += BuiltIns.UTF8.GetByteCount(@string);
                return size;
            }
        }
    }
}
