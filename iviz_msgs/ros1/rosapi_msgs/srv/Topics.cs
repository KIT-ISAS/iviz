using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class Topics : IService<TopicsRequest, TopicsResponse>
    {
        /// Request message.
        [DataMember] public TopicsRequest Request;
        
        /// Response message.
        [DataMember] public TopicsResponse Response;
        
        /// Empty constructor.
        public Topics()
        {
            Request = TopicsRequest.Singleton;
            Response = new TopicsResponse();
        }
        
        /// Setter constructor.
        public Topics(TopicsRequest request)
        {
            Request = request;
            Response = new TopicsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicsResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/Topics";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "d966d98fc333fa1f3135af765eac1ba8";
        
        public IService Generate() => new Topics();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsRequest : IRequest<Topics, TopicsResponse>, IDeserializable<TopicsRequest>
    {
    
        public TopicsRequest()
        {
        }
        
        public TopicsRequest(ref ReadBuffer b)
        {
        }
        
        public TopicsRequest(ref ReadBuffer2 b)
        {
        }
        
        public TopicsRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public TopicsRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static TopicsRequest? singleton;
        public static TopicsRequest Singleton => singleton ??= new TopicsRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsResponse : IResponse, IDeserializable<TopicsResponse>
    {
        [DataMember (Name = "topics")] public string[] Topics_;
        [DataMember (Name = "types")] public string[] Types;
    
        public TopicsResponse()
        {
            Topics_ = EmptyArray<string>.Value;
            Types = EmptyArray<string>.Value;
        }
        
        public TopicsResponse(string[] Topics_, string[] Types)
        {
            this.Topics_ = Topics_;
            this.Types = Types;
        }
        
        public TopicsResponse(ref ReadBuffer b)
        {
            Topics_ = b.DeserializeStringArray();
            Types = b.DeserializeStringArray();
        }
        
        public TopicsResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Topics_ = b.DeserializeStringArray();
            b.Align4();
            Types = b.DeserializeStringArray();
        }
        
        public TopicsResponse RosDeserialize(ref ReadBuffer b) => new TopicsResponse(ref b);
        
        public TopicsResponse RosDeserialize(ref ReadBuffer2 b) => new TopicsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Topics_.Length);
            b.SerializeArray(Topics_);
            b.Serialize(Types.Length);
            b.SerializeArray(Types);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Topics_.Length);
            b.SerializeArray(Topics_);
            b.Align4();
            b.Serialize(Types.Length);
            b.SerializeArray(Types);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Topics_, nameof(Topics_));
            BuiltIns.ThrowIfNull(Types, nameof(Types));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetArraySize(Topics_);
                size += WriteBuffer.GetArraySize(Types);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Topics_);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Types);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
