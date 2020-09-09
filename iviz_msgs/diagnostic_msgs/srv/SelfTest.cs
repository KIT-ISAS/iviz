using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract (Name = "diagnostic_msgs/SelfTest")]
    public sealed class SelfTest : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SelfTestRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SelfTestResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SelfTest()
        {
            Request = new SelfTestRequest();
            Response = new SelfTestResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "diagnostic_msgs/SelfTest";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "ac21b1bab7ab17546986536c22eb34e9";
    }

    public sealed class SelfTestRequest : Internal.EmptyRequest
    {
    }

    public sealed class SelfTestResponse : IResponse
    {
        [DataMember (Name = "id")] public string Id { get; set; }
        [DataMember (Name = "passed")] public byte Passed { get; set; }
        [DataMember (Name = "status")] public DiagnosticStatus[] Status { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SelfTestResponse()
        {
            Id = "";
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SelfTestResponse(string Id, byte Passed, DiagnosticStatus[] Status)
        {
            this.Id = Id;
            this.Passed = Passed;
            this.Status = Status;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SelfTestResponse(Buffer b)
        {
            Id = b.DeserializeString();
            Passed = b.Deserialize<byte>();
            Status = b.DeserializeArray<DiagnosticStatus>();
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(b);
            }
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new SelfTestResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
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
    
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += BuiltIns.UTF8.GetByteCount(Id);
                foreach (var i in Status)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    }
}
