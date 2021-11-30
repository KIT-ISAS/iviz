using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class TestArrayRequest : IService
    {
        /// Request message.
        [DataMember] public TestArrayRequestRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public TestArrayRequestResponse Response { get; set; }
        
        /// Empty constructor.
        public TestArrayRequest()
        {
            Request = new TestArrayRequestRequest();
            Response = TestArrayRequestResponse.Singleton;
        }
        
        /// Setter constructor.
        public TestArrayRequest(TestArrayRequestRequest request)
        {
            Request = request;
            Response = TestArrayRequestResponse.Singleton;
        }
        
        IService IService.Create() => new TestArrayRequest();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TestArrayRequestRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TestArrayRequestResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/TestArrayRequest";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "3d7cfb7e4aa0844868966efa8a264398";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestArrayRequestRequest : IRequest<TestArrayRequest, TestArrayRequestResponse>, IDeserializable<TestArrayRequestRequest>
    {
        [DataMember (Name = "int")] public int[] @int;
    
        /// Constructor for empty message.
        public TestArrayRequestRequest()
        {
            @int = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public TestArrayRequestRequest(int[] @int)
        {
            this.@int = @int;
        }
        
        /// Constructor with buffer.
        internal TestArrayRequestRequest(ref Buffer b)
        {
            @int = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestArrayRequestRequest(ref b);
        
        TestArrayRequestRequest IDeserializable<TestArrayRequestRequest>.RosDeserialize(ref Buffer b) => new TestArrayRequestRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(@int);
        }
        
        public void RosValidate()
        {
            if (@int is null) throw new System.NullReferenceException(nameof(@int));
        }
    
        public int RosMessageLength => 4 + 4 * @int.Length;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TestArrayRequestResponse : IResponse, IDeserializable<TestArrayRequestResponse>
    {
    
        /// Constructor for empty message.
        public TestArrayRequestResponse()
        {
        }
        
        /// Constructor with buffer.
        internal TestArrayRequestResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        TestArrayRequestResponse IDeserializable<TestArrayRequestResponse>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly TestArrayRequestResponse Singleton = new TestArrayRequestResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
