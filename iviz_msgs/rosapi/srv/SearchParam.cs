using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class SearchParam : IService
    {
        /// <summary> Request message. </summary>
        public SearchParamRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public SearchParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SearchParam()
        {
            Request = new SearchParamRequest();
            Response = new SearchParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SearchParam(SearchParamRequest request)
        {
            Request = request;
            Response = new SearchParamResponse();
        }
        
        public IService Create() => new SearchParam();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SearchParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SearchParamResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/SearchParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "dfadc39f113c1cc6d7759508d8461d5a";
    }

    public sealed class SearchParamRequest : IRequest
    {
        public string name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SearchParamRequest()
        {
            name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SearchParamRequest(string name)
        {
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SearchParamRequest(Buffer b)
        {
            this.name = b.DeserializeString();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SearchParamRequest(b);
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

    public sealed class SearchParamResponse : IResponse
    {
        public string global_name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SearchParamResponse()
        {
            global_name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SearchParamResponse(string global_name)
        {
            this.global_name = global_name ?? throw new System.ArgumentNullException(nameof(global_name));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SearchParamResponse(Buffer b)
        {
            this.global_name = b.DeserializeString();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new SearchParamResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.global_name);
        }
        
        public void Validate()
        {
            if (global_name is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(global_name);
                return size;
            }
        }
    }
}
