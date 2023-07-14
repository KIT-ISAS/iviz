using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class SelfTest : IService<SelfTestRequest, SelfTestResponse>
    {
        /// Request message.
        [DataMember] public SelfTestRequest Request;
        
        /// Response message.
        [DataMember] public SelfTestResponse Response;
        
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
        
        public const string ServiceType = "diagnostic_msgs/SelfTest";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "ac21b1bab7ab17546986536c22eb34e9";
        
        public IService Generate() => new SelfTest();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SelfTestRequest : IRequest<SelfTest, SelfTestResponse>, IDeserializable<SelfTestRequest>
    {
    
        public SelfTestRequest()
        {
        }
        
        public SelfTestRequest(ref ReadBuffer b)
        {
        }
        
        public SelfTestRequest(ref ReadBuffer2 b)
        {
        }
        
        public SelfTestRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public SelfTestRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static SelfTestRequest? singleton;
        public static SelfTestRequest Singleton => singleton ??= new SelfTestRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SelfTestResponse : IResponse, IDeserializable<SelfTestResponse>
    {
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "passed")] public byte Passed;
        [DataMember (Name = "status")] public DiagnosticStatus[] Status;
    
        public SelfTestResponse()
        {
            Id = "";
            Status = EmptyArray<DiagnosticStatus>.Value;
        }
        
        public SelfTestResponse(string Id, byte Passed, DiagnosticStatus[] Status)
        {
            this.Id = Id;
            this.Passed = Passed;
            this.Status = Status;
        }
        
        public SelfTestResponse(ref ReadBuffer b)
        {
            Id = b.DeserializeString();
            b.Deserialize(out Passed);
            {
                int n = b.DeserializeArrayLength();
                DiagnosticStatus[] array;
                if (n == 0) array = EmptyArray<DiagnosticStatus>.Value;
                else
                {
                    array = new DiagnosticStatus[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new DiagnosticStatus(ref b);
                    }
                }
                Status = array;
            }
        }
        
        public SelfTestResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Id = b.DeserializeString();
            b.Deserialize(out Passed);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                DiagnosticStatus[] array;
                if (n == 0) array = EmptyArray<DiagnosticStatus>.Value;
                else
                {
                    array = new DiagnosticStatus[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new DiagnosticStatus(ref b);
                    }
                }
                Status = array;
            }
        }
        
        public SelfTestResponse RosDeserialize(ref ReadBuffer b) => new SelfTestResponse(ref b);
        
        public SelfTestResponse RosDeserialize(ref ReadBuffer2 b) => new SelfTestResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(Passed);
            b.Serialize(Status.Length);
            foreach (var t in Status)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Id);
            b.Serialize(Passed);
            b.Align4();
            b.Serialize(Status.Length);
            foreach (var t in Status)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Id, nameof(Id));
            BuiltIns.ThrowIfNull(Status, nameof(Status));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 9;
                size += WriteBuffer.GetStringSize(Id);
                foreach (var msg in Status) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size += 1; // Passed
            size = WriteBuffer2.Align4(size);
            size += 4; // Status.Length
            foreach (var msg in Status) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
