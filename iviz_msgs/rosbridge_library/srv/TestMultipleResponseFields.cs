using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    [DataContract]
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
        [DataMember] public int @int { get; set; }
        [DataMember] public float @float { get; set; }
        [DataMember] public string @string { get; set; }
        [DataMember] public bool @bool { get; set; }
    
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
            this.@string = @string ?? throw new System.ArgumentNullException(nameof(@string));
            this.@bool = @bool;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestMultipleResponseFieldsResponse(Buffer b)
        {
            this.@int = b.Deserialize<int>();
            this.@float = b.Deserialize<float>();
            this.@string = b.DeserializeString();
            this.@bool = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TestMultipleResponseFieldsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.@int);
            b.Serialize(this.@float);
            b.Serialize(this.@string);
            b.Serialize(this.@bool);
        }
        
        public void Validate()
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
