using System.Runtime.Serialization;

namespace Iviz.Msgs.diagnostic_msgs
{
    [DataContract]
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
        [DataMember] public string id { get; set; }
        [DataMember] public byte passed { get; set; }
        [DataMember] public DiagnosticStatus[] status { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SelfTestResponse()
        {
            id = "";
            status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SelfTestResponse(string id, byte passed, DiagnosticStatus[] status)
        {
            this.id = id ?? throw new System.ArgumentNullException(nameof(id));
            this.passed = passed;
            this.status = status ?? throw new System.ArgumentNullException(nameof(status));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SelfTestResponse(Buffer b)
        {
            this.id = b.DeserializeString();
            this.passed = b.Deserialize<byte>();
            this.status = b.DeserializeArray<DiagnosticStatus>();
            for (int i = 0; i < this.status.Length; i++)
            {
                this.status[i] = new DiagnosticStatus(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SelfTestResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.id);
            b.Serialize(this.passed);
            b.SerializeArray(this.status, 0);
        }
        
        public void Validate()
        {
            if (id is null) throw new System.NullReferenceException();
            if (status is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += BuiltIns.UTF8.GetByteCount(id);
                for (int i = 0; i < status.Length; i++)
                {
                    size += status[i].RosMessageLength;
                }
                return size;
            }
        }
    }
}
