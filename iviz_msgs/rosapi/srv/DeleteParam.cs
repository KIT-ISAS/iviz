using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/DeleteParam")]
    public sealed class DeleteParam : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public DeleteParamRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public DeleteParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public DeleteParam()
        {
            Request = new DeleteParamRequest();
            Response = DeleteParamResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/DeleteParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "c1f3d28f1b044c871e6eff2e9fc3c667";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteParamRequest : IRequest<DeleteParam, DeleteParamResponse>, IDeserializable<DeleteParamRequest>
    {
        [DataMember (Name = "name")] public string Name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DeleteParamRequest()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public DeleteParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DeleteParamRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DeleteParamRequest(ref b);
        }
        
        DeleteParamRequest IDeserializable<DeleteParamRequest>.RosDeserialize(ref Buffer b)
        {
            return new DeleteParamRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Name);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DeleteParamResponse : IResponse, IDeserializable<DeleteParamResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public DeleteParamResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DeleteParamResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        DeleteParamResponse IDeserializable<DeleteParamResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly DeleteParamResponse Singleton = new DeleteParamResponse();
    
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
    
        public override string ToString() => Extensions.ToString(this);
    }
}
