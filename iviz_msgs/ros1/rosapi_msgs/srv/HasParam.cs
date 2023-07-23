using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class HasParam : IService<HasParamRequest, HasParamResponse>
    {
        /// Request message.
        [DataMember] public HasParamRequest Request;
        
        /// Response message.
        [DataMember] public HasParamResponse Response;
        
        /// Empty constructor.
        public HasParam()
        {
            Request = new HasParamRequest();
            Response = new HasParamResponse();
        }
        
        /// Setter constructor.
        public HasParam(HasParamRequest request)
        {
            Request = request;
            Response = new HasParamResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (HasParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (HasParamResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/HasParam";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "ed3df286bd6dff9b961770f577454ea9";
        
        public IService Generate() => new HasParam();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class HasParamRequest : IRequest<HasParam, HasParamResponse>, IDeserializable<HasParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
    
        public HasParamRequest()
        {
            Name = "";
        }
        
        public HasParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        public HasParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
        }
        
        public HasParamRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
        }
        
        public HasParamRequest RosDeserialize(ref ReadBuffer b) => new HasParamRequest(ref b);
        
        public HasParamRequest RosDeserialize(ref ReadBuffer2 b) => new HasParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Name);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class HasParamResponse : IResponse, IDeserializable<HasParamResponse>
    {
        [DataMember (Name = "exists")] public bool Exists;
    
        public HasParamResponse()
        {
        }
        
        public HasParamResponse(bool Exists)
        {
            this.Exists = Exists;
        }
        
        public HasParamResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Exists);
        }
        
        public HasParamResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Exists);
        }
        
        public HasParamResponse RosDeserialize(ref ReadBuffer b) => new HasParamResponse(ref b);
        
        public HasParamResponse RosDeserialize(ref ReadBuffer2 b) => new HasParamResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Exists);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Exists);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 1;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Exists
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
