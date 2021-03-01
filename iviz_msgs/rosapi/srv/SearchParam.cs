using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/SearchParam")]
    public sealed class SearchParam : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SearchParamRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SearchParamResponse Response { get; set; }
        
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
        
        IService IService.Create() => new SearchParam();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/SearchParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "dfadc39f113c1cc6d7759508d8461d5a";
    }

    [DataContract]
    public sealed class SearchParamRequest : IRequest<SearchParam, SearchParamResponse>, IDeserializable<SearchParamRequest>
    {
        [DataMember (Name = "name")] public string Name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SearchParamRequest()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public SearchParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SearchParamRequest(ref Buffer b)
        {
            Name = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SearchParamRequest(ref b);
        }
        
        SearchParamRequest IDeserializable<SearchParamRequest>.RosDeserialize(ref Buffer b)
        {
            return new SearchParamRequest(ref b);
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
    public sealed class SearchParamResponse : IResponse, IDeserializable<SearchParamResponse>
    {
        [DataMember (Name = "global_name")] public string GlobalName { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SearchParamResponse()
        {
            GlobalName = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public SearchParamResponse(string GlobalName)
        {
            this.GlobalName = GlobalName;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SearchParamResponse(ref Buffer b)
        {
            GlobalName = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SearchParamResponse(ref b);
        }
        
        SearchParamResponse IDeserializable<SearchParamResponse>.RosDeserialize(ref Buffer b)
        {
            return new SearchParamResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(GlobalName);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (GlobalName is null) throw new System.NullReferenceException(nameof(GlobalName));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(GlobalName);
                return size;
            }
        }
    }
}
