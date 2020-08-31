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
            Response = new DeleteParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public DeleteParam(DeleteParamRequest request)
        {
            Request = request;
            Response = new DeleteParamResponse();
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/DeleteParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "c1f3d28f1b044c871e6eff2e9fc3c667";
    }

    public sealed class DeleteParamRequest : IRequest
    {
        [DataMember (Name = "name")] public string Name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DeleteParamRequest()
        {
            Name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public DeleteParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DeleteParamRequest(Buffer b)
        {
            Name = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new DeleteParamRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Name);
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
    }

    public sealed class DeleteParamResponse : Internal.EmptyResponse
    {
    }
}
