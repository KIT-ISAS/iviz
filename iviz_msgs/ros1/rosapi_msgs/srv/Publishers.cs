using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class Publishers : IService<PublishersRequest, PublishersResponse>
    {
        /// Request message.
        [DataMember] public PublishersRequest Request;
        
        /// Response message.
        [DataMember] public PublishersResponse Response;
        
        /// Empty constructor.
        public Publishers()
        {
            Request = new PublishersRequest();
            Response = new PublishersResponse();
        }
        
        /// Setter constructor.
        public Publishers(PublishersRequest request)
        {
            Request = request;
            Response = new PublishersResponse();
        }
        
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
        
        public const string ServiceType = "rosapi_msgs/Publishers";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "cb37f09944e7ba1fc08ee38f7a94291d";
        
        public IService Generate() => new Publishers();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class PublishersRequest : IRequest<Publishers, PublishersResponse>, IDeserializable<PublishersRequest>
    {
        [DataMember (Name = "topic")] public string Topic;
    
        public PublishersRequest()
        {
            Topic = "";
        }
        
        public PublishersRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        public PublishersRequest(ref ReadBuffer b)
        {
            Topic = b.DeserializeString();
        }
        
        public PublishersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Topic = b.DeserializeString();
        }
        
        public PublishersRequest RosDeserialize(ref ReadBuffer b) => new PublishersRequest(ref b);
        
        public PublishersRequest RosDeserialize(ref ReadBuffer2 b) => new PublishersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Topic);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Topic);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Topic, nameof(Topic));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Topic);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Topic);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class PublishersResponse : IResponse, IDeserializable<PublishersResponse>
    {
        [DataMember (Name = "publishers")] public string[] Publishers_;
    
        public PublishersResponse()
        {
            Publishers_ = EmptyArray<string>.Value;
        }
        
        public PublishersResponse(string[] Publishers_)
        {
            this.Publishers_ = Publishers_;
        }
        
        public PublishersResponse(ref ReadBuffer b)
        {
            Publishers_ = b.DeserializeStringArray();
        }
        
        public PublishersResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Publishers_ = b.DeserializeStringArray();
        }
        
        public PublishersResponse RosDeserialize(ref ReadBuffer b) => new PublishersResponse(ref b);
        
        public PublishersResponse RosDeserialize(ref ReadBuffer2 b) => new PublishersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Publishers_.Length);
            b.SerializeArray(Publishers_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Publishers_.Length);
            b.SerializeArray(Publishers_);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Publishers_, nameof(Publishers_));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Publishers_);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Publishers_);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
