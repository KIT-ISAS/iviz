using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class TopicType : IService<TopicTypeRequest, TopicTypeResponse>
    {
        /// Request message.
        [DataMember] public TopicTypeRequest Request;
        
        /// Response message.
        [DataMember] public TopicTypeResponse Response;
        
        /// Empty constructor.
        public TopicType()
        {
            Request = new TopicTypeRequest();
            Response = new TopicTypeResponse();
        }
        
        /// Setter constructor.
        public TopicType(TopicTypeRequest request)
        {
            Request = request;
            Response = new TopicTypeResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicTypeResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/TopicType";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "0d30b3f53a0fd5036523a7141e524ddf";
        
        public IService Generate() => new TopicType();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicTypeRequest : IRequest<TopicType, TopicTypeResponse>, IDeserializable<TopicTypeRequest>
    {
        [DataMember (Name = "topic")] public string Topic;
    
        public TopicTypeRequest()
        {
            Topic = "";
        }
        
        public TopicTypeRequest(string Topic)
        {
            this.Topic = Topic;
        }
        
        public TopicTypeRequest(ref ReadBuffer b)
        {
            Topic = b.DeserializeString();
        }
        
        public TopicTypeRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Topic = b.DeserializeString();
        }
        
        public TopicTypeRequest RosDeserialize(ref ReadBuffer b) => new TopicTypeRequest(ref b);
        
        public TopicTypeRequest RosDeserialize(ref ReadBuffer2 b) => new TopicTypeRequest(ref b);
    
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
    public sealed class TopicTypeResponse : IResponse, IDeserializable<TopicTypeResponse>
    {
        [DataMember (Name = "type")] public string Type;
    
        public TopicTypeResponse()
        {
            Type = "";
        }
        
        public TopicTypeResponse(string Type)
        {
            this.Type = Type;
        }
        
        public TopicTypeResponse(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public TopicTypeResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public TopicTypeResponse RosDeserialize(ref ReadBuffer b) => new TopicTypeResponse(ref b);
        
        public TopicTypeResponse RosDeserialize(ref ReadBuffer2 b) => new TopicTypeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Type, nameof(Type));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Type);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Type);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
