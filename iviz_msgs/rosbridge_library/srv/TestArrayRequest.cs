using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestArrayRequest")]
    public sealed class TestArrayRequest : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TestArrayRequestRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TestArrayRequestResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public TestArrayRequest()
        {
            Request = new TestArrayRequestRequest();
            Response = TestArrayRequestResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestArrayRequest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "3d7cfb7e4aa0844868966efa8a264398";
    }

    [DataContract]
    public sealed class TestArrayRequestRequest : IRequest<TestArrayRequest, TestArrayRequestResponse>, IDeserializable<TestArrayRequestRequest>
    {
        [DataMember (Name = "int")] public int[] @int { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestArrayRequestRequest()
        {
            @int = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestArrayRequestRequest(int[] @int)
        {
            this.@int = @int;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestArrayRequestRequest(ref Buffer b)
        {
            @int = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestArrayRequestRequest(ref b);
        }
        
        TestArrayRequestRequest IDeserializable<TestArrayRequestRequest>.RosDeserialize(ref Buffer b)
        {
            return new TestArrayRequestRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(@int, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (@int is null) throw new System.NullReferenceException(nameof(@int));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * @int.Length;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class TestArrayRequestResponse : IResponse, IDeserializable<TestArrayRequestResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TestArrayRequestResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestArrayRequestResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        TestArrayRequestResponse IDeserializable<TestArrayRequestResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly TestArrayRequestResponse Singleton = new TestArrayRequestResponse();
    
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
    }
}
