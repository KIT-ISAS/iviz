using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/HasParam")]
    public sealed class HasParam : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public HasParamRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public HasParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public HasParam()
        {
            Request = new HasParamRequest();
            Response = new HasParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public HasParam(HasParamRequest request)
        {
            Request = request;
            Response = new HasParamResponse();
        }
        
        IService IService.Create() => new HasParam();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/HasParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "ed3df286bd6dff9b961770f577454ea9";
    }

    [DataContract]
    public sealed class HasParamRequest : IRequest<HasParam, HasParamResponse>, IDeserializable<HasParamRequest>
    {
        [DataMember (Name = "name")] public string Name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public HasParamRequest()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public HasParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public HasParamRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new HasParamRequest(ref b);
        }
        
        HasParamRequest IDeserializable<HasParamRequest>.RosDeserialize(ref Buffer b)
        {
            return new HasParamRequest(ref b);
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
    }

    [DataContract]
    public sealed class HasParamResponse : IResponse, IDeserializable<HasParamResponse>
    {
        [DataMember (Name = "exists")] public bool Exists { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public HasParamResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public HasParamResponse(bool Exists)
        {
            this.Exists = Exists;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public HasParamResponse(ref Buffer b)
        {
            Exists = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new HasParamResponse(ref b);
        }
        
        HasParamResponse IDeserializable<HasParamResponse>.RosDeserialize(ref Buffer b)
        {
            return new HasParamResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Exists);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
