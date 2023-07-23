using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class TopicsForType : IService<TopicsForTypeRequest, TopicsForTypeResponse>
    {
        /// Request message.
        [DataMember] public TopicsForTypeRequest Request;
        
        /// Response message.
        [DataMember] public TopicsForTypeResponse Response;
        
        /// Empty constructor.
        public TopicsForType()
        {
            Request = new TopicsForTypeRequest();
            Response = new TopicsForTypeResponse();
        }
        
        /// Setter constructor.
        public TopicsForType(TopicsForTypeRequest request)
        {
            Request = request;
            Response = new TopicsForTypeResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicsForTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicsForTypeResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/TopicsForType";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "56f77ff6da756dd27c1ed16ec721072a";
        
        public IService Generate() => new TopicsForType();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsForTypeRequest : IRequest<TopicsForType, TopicsForTypeResponse>, IDeserializable<TopicsForTypeRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        public TopicsForTypeRequest()
        {
            Type = "";
        }
        
        public TopicsForTypeRequest(string Type)
        {
            this.Type = Type;
        }
        
        public TopicsForTypeRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public TopicsForTypeRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public TopicsForTypeRequest RosDeserialize(ref ReadBuffer b) => new TopicsForTypeRequest(ref b);
        
        public TopicsForTypeRequest RosDeserialize(ref ReadBuffer2 b) => new TopicsForTypeRequest(ref b);
    
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

    [DataContract]
    public sealed class TopicsForTypeResponse : IResponse, IDeserializable<TopicsForTypeResponse>
    {
        [DataMember (Name = "topics")] public string[] Topics;
    
        public TopicsForTypeResponse()
        {
            Topics = EmptyArray<string>.Value;
        }
        
        public TopicsForTypeResponse(string[] Topics)
        {
            this.Topics = Topics;
        }
        
        public TopicsForTypeResponse(ref ReadBuffer b)
        {
            Topics = b.DeserializeStringArray();
        }
        
        public TopicsForTypeResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Topics = b.DeserializeStringArray();
        }
        
        public TopicsForTypeResponse RosDeserialize(ref ReadBuffer b) => new TopicsForTypeResponse(ref b);
        
        public TopicsForTypeResponse RosDeserialize(ref ReadBuffer2 b) => new TopicsForTypeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Topics.Length);
            b.SerializeArray(Topics);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Topics.Length);
            b.SerializeArray(Topics);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Topics, nameof(Topics));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Topics);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Topics);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
