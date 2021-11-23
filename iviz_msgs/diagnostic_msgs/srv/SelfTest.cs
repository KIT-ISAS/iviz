using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SelfTest : IService
    {
        /// Request message.
        [DataMember] public SelfTestRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SelfTestResponse Response { get; set; }
        
        /// Empty constructor.
        public SelfTest()
        {
            Request = SelfTestRequest.Singleton;
            Response = new SelfTestResponse();
        }
        
        /// Setter constructor.
        public SelfTest(SelfTestRequest request)
        {
            Request = request;
            Response = new SelfTestResponse();
        }
        
        IService IService.Create() => new SelfTest();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SelfTestRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SelfTestResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "diagnostic_msgs/SelfTest";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "ac21b1bab7ab17546986536c22eb34e9";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SelfTestRequest : IRequest<SelfTest, SelfTestResponse>, IDeserializable<SelfTestRequest>
    {
    
        /// Constructor for empty message.
        public SelfTestRequest()
        {
        }
        
        /// Constructor with buffer.
        internal SelfTestRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        SelfTestRequest IDeserializable<SelfTestRequest>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly SelfTestRequest Singleton = new SelfTestRequest();
    
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

    [DataContract]
    public sealed class SelfTestResponse : IResponse, IDeserializable<SelfTestResponse>
    {
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "passed")] public byte Passed;
        [DataMember (Name = "status")] public DiagnosticStatus[] Status;
    
        /// Constructor for empty message.
        public SelfTestResponse()
        {
            Id = string.Empty;
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// Explicit constructor.
        public SelfTestResponse(string Id, byte Passed, DiagnosticStatus[] Status)
        {
            this.Id = Id;
            this.Passed = Passed;
            this.Status = Status;
        }
        
        /// Constructor with buffer.
        internal SelfTestResponse(ref Buffer b)
        {
            Id = b.DeserializeString();
            Passed = b.Deserialize<byte>();
            Status = b.DeserializeArray<DiagnosticStatus>();
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SelfTestResponse(ref b);
        
        SelfTestResponse IDeserializable<SelfTestResponse>.RosDeserialize(ref Buffer b) => new SelfTestResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            b.Serialize(Passed);
            b.SerializeArray(Status, 0);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] is null) throw new System.NullReferenceException($"{nameof(Status)}[{i}]");
                Status[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 9 + BuiltIns.GetStringSize(Id) + BuiltIns.GetArraySize(Status);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
