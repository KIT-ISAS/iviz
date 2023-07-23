using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class SearchParam : IService<SearchParamRequest, SearchParamResponse>
    {
        /// Request message.
        [DataMember] public SearchParamRequest Request;
        
        /// Response message.
        [DataMember] public SearchParamResponse Response;
        
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
        
        public const string ServiceType = "rosapi_msgs/SearchParam";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "dfadc39f113c1cc6d7759508d8461d5a";
        
        public IService Generate() => new SearchParam();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SearchParamRequest : IRequest<SearchParam, SearchParamResponse>, IDeserializable<SearchParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
    
        public SearchParamRequest()
        {
            Name = "";
        }
        
        public SearchParamRequest(string Name)
        {
            this.Name = Name;
        }
        
        public SearchParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
        }
        
        public SearchParamRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
        }
        
        public SearchParamRequest RosDeserialize(ref ReadBuffer b) => new SearchParamRequest(ref b);
        
        public SearchParamRequest RosDeserialize(ref ReadBuffer2 b) => new SearchParamRequest(ref b);
    
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
    public sealed class SearchParamResponse : IResponse, IDeserializable<SearchParamResponse>
    {
        [DataMember (Name = "global_name")] public string GlobalName;
    
        public SearchParamResponse()
        {
            GlobalName = "";
        }
        
        public SearchParamResponse(string GlobalName)
        {
            this.GlobalName = GlobalName;
        }
        
        public SearchParamResponse(ref ReadBuffer b)
        {
            GlobalName = b.DeserializeString();
        }
        
        public SearchParamResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            GlobalName = b.DeserializeString();
        }
        
        public SearchParamResponse RosDeserialize(ref ReadBuffer b) => new SearchParamResponse(ref b);
        
        public SearchParamResponse RosDeserialize(ref ReadBuffer2 b) => new SearchParamResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(GlobalName);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(GlobalName);
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
                size += WriteBuffer.GetStringSize(GlobalName);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, GlobalName);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
