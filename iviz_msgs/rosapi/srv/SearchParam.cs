using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class SearchParam : IService
    {
        /// Request message.
        [DataMember] public SearchParamRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SearchParamResponse Response { get; set; }
        
        /// Empty constructor.
        public SearchParam()
        {
            Request = new SearchParamRequest();
            Response = new SearchParamResponse();
        }
        
        /// Setter constructor.
        public SearchParam(SearchParamRequest request)
        {
            Request = request;
            Response = new SearchParamResponse();
        }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/SearchParam";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "dfadc39f113c1cc6d7759508d8461d5a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SearchParamRequest : IRequest<SearchParam, SearchParamResponse>, IDeserializable<SearchParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
    
        /// Constructor for empty message.
        public SearchParamRequest()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public SearchParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        /// Constructor with buffer.
        public SearchParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SearchParamRequest(ref b);
        
        public SearchParamRequest RosDeserialize(ref ReadBuffer b) => new SearchParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Name);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SearchParamResponse : IResponse, IDeserializable<SearchParamResponse>
    {
        [DataMember (Name = "global_name")] public string GlobalName;
    
        /// Constructor for empty message.
        public SearchParamResponse()
        {
            GlobalName = "";
        }
        
        /// Explicit constructor.
        public SearchParamResponse(string GlobalName)
        {
            this.GlobalName = GlobalName;
        }
        
        /// Constructor with buffer.
        public SearchParamResponse(ref ReadBuffer b)
        {
            GlobalName = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SearchParamResponse(ref b);
        
        public SearchParamResponse RosDeserialize(ref ReadBuffer b) => new SearchParamResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(GlobalName);
        }
        
        public void RosValidate()
        {
            if (GlobalName is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(GlobalName);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
