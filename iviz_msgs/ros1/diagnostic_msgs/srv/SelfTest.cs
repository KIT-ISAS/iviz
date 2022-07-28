using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [DataContract]
    public sealed class SelfTest : IService
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
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
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
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int c) { }
    
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
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        public SelfTestResponse(string Id, byte Passed, DiagnosticStatus[] Status)
        {
            this.Id = Id;
            this.Passed = Passed;
            this.Status = Status;
        }
        
        public SelfTestResponse(ref ReadBuffer b)
        {
            b.DeserializeString(out Id);
            b.Deserialize(out Passed);
            b.DeserializeArray(out Status);
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        public SelfTestResponse(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Id);
            b.Deserialize(out Passed);
            b.DeserializeArray(out Status);
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new SelfTestResponse(ref b);
        
        public SelfTestResponse RosDeserialize(ref ReadBuffer b) => new SelfTestResponse(ref b);
        
        public SelfTestResponse RosDeserialize(ref ReadBuffer2 b) => new SelfTestResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(Passed);
            b.SerializeArray(Status);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Id);
            b.Serialize(Passed);
            b.SerializeArray(Status);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Status is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] is null) BuiltIns.ThrowNullReference(nameof(Status), i);
                Status[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 9 + WriteBuffer.GetStringSize(Id) + WriteBuffer.GetArraySize(Status);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Id);
            WriteBuffer2.AddLength(ref c, Passed);
            WriteBuffer2.AddLength(ref c, Status);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
