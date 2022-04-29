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
        public TestArrayRequestRequest(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out @int);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestArrayRequestRequest(ref b);
        
        public TestArrayRequestRequest RosDeserialize(ref ReadBuffer b) => new TestArrayRequestRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(@int);
        }
        
        public void RosValidate()
        {
            if (@int is null) BuiltIns.ThrowNullReference();
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
        public TestArrayRequestResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TestArrayRequestResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static TestArrayRequestResponse? singleton;
        public static TestArrayRequestResponse Singleton => singleton ??= new TestArrayRequestResponse();
    
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
