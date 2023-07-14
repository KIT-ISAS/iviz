using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class TopicsAndRawTypes : IService<TopicsAndRawTypesRequest, TopicsAndRawTypesResponse>
    {
        /// Request message.
        [DataMember] public TopicsAndRawTypesRequest Request;
        
        /// Response message.
        [DataMember] public TopicsAndRawTypesResponse Response;
        
        /// Empty constructor.
        public TopicsAndRawTypes()
        {
            Request = TopicsAndRawTypesRequest.Singleton;
            Response = new TopicsAndRawTypesResponse();
        }
        
        /// Setter constructor.
        public TopicsAndRawTypes(TopicsAndRawTypesRequest request)
        {
            Request = request;
            Response = new TopicsAndRawTypesResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicsAndRawTypesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicsAndRawTypesResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/TopicsAndRawTypes";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e1432466c8f64316723276ba07c59d12";
        
        public IService Generate() => new TopicsAndRawTypes();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class TopicsAndRawTypesRequest : IRequest<TopicsAndRawTypes, TopicsAndRawTypesResponse>, IDeserializable<TopicsAndRawTypesRequest>
    {
    
        public TopicsAndRawTypesRequest()
        {
        }
        
        public TopicsAndRawTypesRequest(ref ReadBuffer b)
        {
        }
        
        public TopicsAndRawTypesRequest(ref ReadBuffer2 b)
        {
        }
        
        public TopicsAndRawTypesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public TopicsAndRawTypesRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static TopicsAndRawTypesRequest? singleton;
        public static TopicsAndRawTypesRequest Singleton => singleton ??= new TopicsAndRawTypesRequest();
    
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
    public sealed class TopicsAndRawTypesResponse : IResponse, IDeserializable<TopicsAndRawTypesResponse>
    {
        [DataMember (Name = "topics")] public string[] Topics;
        [DataMember (Name = "types")] public string[] Types;
        [DataMember (Name = "typedefs_full_text")] public string[] TypedefsFullText;
    
        public TopicsAndRawTypesResponse()
        {
            Topics = EmptyArray<string>.Value;
            Types = EmptyArray<string>.Value;
            TypedefsFullText = EmptyArray<string>.Value;
        }
        
        public TopicsAndRawTypesResponse(string[] Topics, string[] Types, string[] TypedefsFullText)
        {
            this.Topics = Topics;
            this.Types = Types;
            this.TypedefsFullText = TypedefsFullText;
        }
        
        public TopicsAndRawTypesResponse(ref ReadBuffer b)
        {
            Topics = b.DeserializeStringArray();
            Types = b.DeserializeStringArray();
            TypedefsFullText = b.DeserializeStringArray();
        }
        
        public TopicsAndRawTypesResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Topics = b.DeserializeStringArray();
            b.Align4();
            Types = b.DeserializeStringArray();
            b.Align4();
            TypedefsFullText = b.DeserializeStringArray();
        }
        
        public TopicsAndRawTypesResponse RosDeserialize(ref ReadBuffer b) => new TopicsAndRawTypesResponse(ref b);
        
        public TopicsAndRawTypesResponse RosDeserialize(ref ReadBuffer2 b) => new TopicsAndRawTypesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Topics.Length);
            b.SerializeArray(Topics);
            b.Serialize(Types.Length);
            b.SerializeArray(Types);
            b.Serialize(TypedefsFullText.Length);
            b.SerializeArray(TypedefsFullText);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Topics.Length);
            b.SerializeArray(Topics);
            b.Align4();
            b.Serialize(Types.Length);
            b.SerializeArray(Types);
            b.Align4();
            b.Serialize(TypedefsFullText.Length);
            b.SerializeArray(TypedefsFullText);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Topics, nameof(Topics));
            BuiltIns.ThrowIfNull(Types, nameof(Types));
            BuiltIns.ThrowIfNull(TypedefsFullText, nameof(TypedefsFullText));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += WriteBuffer.GetArraySize(Topics);
                size += WriteBuffer.GetArraySize(Types);
                size += WriteBuffer.GetArraySize(TypedefsFullText);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Topics);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Types);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, TypedefsFullText);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
