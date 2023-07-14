using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class Subscribers : IService<SubscribersRequest, SubscribersResponse>
    {
        /// Request message.
        [DataMember] public SubscribersRequest Request;
        
        /// Response message.
        [DataMember] public SubscribersResponse Response;
        
        /// Empty constructor.
        public Subscribers()
        {
            Request = new SubscribersRequest();
            Response = new SubscribersResponse();
        }
        
        /// Setter constructor.
        public Subscribers(SubscribersRequest request)
        {
            Request = request;
            Response = new SubscribersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SubscribersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SubscribersResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/Subscribers";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "cb387b68f5b29bc1456398ee8476b973";
        
        public IService Generate() => new Subscribers();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SubscribersRequest : IRequest<Subscribers, SubscribersResponse>, IDeserializable<SubscribersRequest>
    {
        [DataMember (Name = "topic")] public string Topic;
    
        public SubscribersRequest()
        {
            Topic = "";
        }
        
        public SubscribersRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        public SubscribersRequest(ref ReadBuffer b)
        {
            Topic = b.DeserializeString();
        }
        
        public SubscribersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Topic = b.DeserializeString();
        }
        
        public SubscribersRequest RosDeserialize(ref ReadBuffer b) => new SubscribersRequest(ref b);
        
        public SubscribersRequest RosDeserialize(ref ReadBuffer2 b) => new SubscribersRequest(ref b);
    
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
    public sealed class SubscribersResponse : IResponse, IDeserializable<SubscribersResponse>
    {
        [DataMember (Name = "subscribers")] public string[] Subscribers_;
    
        public SubscribersResponse()
        {
            Subscribers_ = EmptyArray<string>.Value;
        }
        
        public SubscribersResponse(string[] Subscribers_)
        {
            this.Subscribers_ = Subscribers_;
        }
        
        public SubscribersResponse(ref ReadBuffer b)
        {
            Subscribers_ = b.DeserializeStringArray();
        }
        
        public SubscribersResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Subscribers_ = b.DeserializeStringArray();
        }
        
        public SubscribersResponse RosDeserialize(ref ReadBuffer b) => new SubscribersResponse(ref b);
        
        public SubscribersResponse RosDeserialize(ref ReadBuffer2 b) => new SubscribersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Subscribers_.Length);
            b.SerializeArray(Subscribers_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Subscribers_.Length);
            b.SerializeArray(Subscribers_);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Subscribers_, nameof(Subscribers_));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Subscribers_);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Subscribers_);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
