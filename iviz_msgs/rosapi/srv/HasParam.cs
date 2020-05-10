using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class HasParam : IService
    {
        /// <summary> Request message. </summary>
        public HasParamRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public HasParamResponse Response { get; set; }
        
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
        
        public IService Create() => new HasParam();
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/HasParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "ed3df286bd6dff9b961770f577454ea9";
    }

    public sealed class HasParamRequest : IRequest
    {
        public string name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public HasParamRequest()
        {
            name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public HasParamRequest(string name)
        {
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal HasParamRequest(Buffer b)
        {
            this.name = b.DeserializeString();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new HasParamRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.name);
        }
        
        public void Validate()
        {
            if (name is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(name);
                return size;
            }
        }
    }

    public sealed class HasParamResponse : IResponse
    {
        public bool exists { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public HasParamResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public HasParamResponse(bool exists)
        {
            this.exists = exists;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal HasParamResponse(Buffer b)
        {
            this.exists = b.Deserialize<bool>();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new HasParamResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.exists);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 1;
    }
}
