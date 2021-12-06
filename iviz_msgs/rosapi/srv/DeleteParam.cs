using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class DeleteParam : IService
    {
        /// Request message.
        [DataMember] public DeleteParamRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public DeleteParamResponse Response { get; set; }
        
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
        
        IService IService.Create() => new DeleteParam();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/DeleteParam";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "c1f3d28f1b044c871e6eff2e9fc3c667";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteParamRequest : IRequest<DeleteParam, DeleteParamResponse>, IDeserializable<DeleteParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
    
        /// Constructor for empty message.
        public DeleteParamRequest()
        {
            Name = string.Empty;
        }
        
        /// Explicit constructor.
        public DeleteParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        /// Constructor with buffer.
        internal DeleteParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new DeleteParamRequest(ref b);
        
        public DeleteParamRequest RosDeserialize(ref ReadBuffer b) => new DeleteParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Name);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteParamResponse : IResponse, IDeserializable<DeleteParamResponse>
    {
    
        /// Constructor for empty message.
        public DeleteParamResponse()
        {
        }
        
        /// Constructor with buffer.
        internal DeleteParamResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public DeleteParamResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly DeleteParamResponse Singleton = new DeleteParamResponse();
    
        public void RosSerialize(ref WriteBuffer b)
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
}
