using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class DeleteParam : IService<DeleteParamRequest, DeleteParamResponse>
    {
        /// Request message.
        [DataMember] public DeleteParamRequest Request;
        
        /// Response message.
        [DataMember] public DeleteParamResponse Response;
        
        /// Empty constructor.
        public DeleteParam()
        {
            Request = new DeleteParamRequest();
            Response = DeleteParamResponse.Singleton;
        }
        
        /// Setter constructor.
        public DeleteParam(DeleteParamRequest request)
        {
            Request = request;
            Response = DeleteParamResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (DeleteParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (DeleteParamResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/DeleteParam";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "c1f3d28f1b044c871e6eff2e9fc3c667";
        
        public IService Generate() => new DeleteParam();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteParamRequest : IRequest<DeleteParam, DeleteParamResponse>, IDeserializable<DeleteParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
    
        public DeleteParamRequest()
        {
            Name = "";
        }
        
        public DeleteParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        public DeleteParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
        }
        
        public DeleteParamRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
        }
        
        public DeleteParamRequest RosDeserialize(ref ReadBuffer b) => new DeleteParamRequest(ref b);
        
        public DeleteParamRequest RosDeserialize(ref ReadBuffer2 b) => new DeleteParamRequest(ref b);
    
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
    public sealed class DeleteParamResponse : IResponse, IDeserializable<DeleteParamResponse>
    {
    
        public DeleteParamResponse()
        {
        }
        
        public DeleteParamResponse(ref ReadBuffer b)
        {
        }
        
        public DeleteParamResponse(ref ReadBuffer2 b)
        {
        }
        
        public DeleteParamResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public DeleteParamResponse RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static DeleteParamResponse? singleton;
        public static DeleteParamResponse Singleton => singleton ??= new DeleteParamResponse();
    
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
}
