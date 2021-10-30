using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/Publishers")]
    public sealed class Publishers : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public PublishersRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public PublishersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Publishers()
        {
            Request = new PublishersRequest();
            Response = new PublishersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Publishers(PublishersRequest request)
        {
            Request = request;
            Response = new PublishersResponse();
        }
        
        IService IService.Create() => new Publishers();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (PublishersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (PublishersResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/Publishers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "cb37f09944e7ba1fc08ee38f7a94291d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class PublishersRequest : IRequest<Publishers, PublishersResponse>, IDeserializable<PublishersRequest>
    {
        [DataMember (Name = "topic")] public string Topic;
    
        /// <summary> Constructor for empty message. </summary>
        public PublishersRequest()
        {
            Topic = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public PublishersRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PublishersRequest(ref Buffer b)
        {
            Topic = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PublishersRequest(ref b);
        }
        
        PublishersRequest IDeserializable<PublishersRequest>.RosDeserialize(ref Buffer b)
        {
            return new PublishersRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Topic);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Topic is null) throw new System.NullReferenceException(nameof(Topic));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Topic);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class PublishersResponse : IResponse, IDeserializable<PublishersResponse>
    {
        [DataMember (Name = "publishers")] public string[] Publishers_;
    
        /// <summary> Constructor for empty message. </summary>
        public PublishersResponse()
        {
            Publishers_ = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PublishersResponse(string[] Publishers_)
        {
            this.Publishers_ = Publishers_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PublishersResponse(ref Buffer b)
        {
            Publishers_ = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PublishersResponse(ref b);
        }
        
        PublishersResponse IDeserializable<PublishersResponse>.RosDeserialize(ref Buffer b)
        {
            return new PublishersResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Publishers_, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Publishers_ is null) throw new System.NullReferenceException(nameof(Publishers_));
            for (int i = 0; i < Publishers_.Length; i++)
            {
                if (Publishers_[i] is null) throw new System.NullReferenceException($"{nameof(Publishers_)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Publishers_);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
