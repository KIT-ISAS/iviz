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
            Response = new TestArrayRequestResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public TestArrayRequest(TestArrayRequestRequest request)
        {
            Request = request;
            Response = new TestArrayRequestResponse();
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/TestArrayRequest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "3d7cfb7e4aa0844868966efa8a264398";
    }

    public sealed class TestArrayRequestRequest : IRequest, IDeserializable<TestArrayRequestRequest>
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
        internal TestArrayRequestRequest(ref Buffer b)
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

    public sealed class TestArrayRequestResponse : Internal.EmptyResponse
    {
    }
}
