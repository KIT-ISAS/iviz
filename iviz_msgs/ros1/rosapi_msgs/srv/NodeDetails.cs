using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class NodeDetails : IService<NodeDetailsRequest, NodeDetailsResponse>
    {
        /// Request message.
        [DataMember] public NodeDetailsRequest Request;
        
        /// Response message.
        [DataMember] public NodeDetailsResponse Response;
        
        /// Empty constructor.
        public NodeDetails()
        {
            Request = new NodeDetailsRequest();
            Response = new NodeDetailsResponse();
        }
        
        /// Setter constructor.
        public NodeDetails(NodeDetailsRequest request)
        {
            Request = request;
            Response = new NodeDetailsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (NodeDetailsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (NodeDetailsResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/NodeDetails";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e1d0ced5ab8d5edb5fc09c98eb1d46f6";
        
        public IService Generate() => new NodeDetails();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class NodeDetailsRequest : IRequest<NodeDetails, NodeDetailsResponse>, IDeserializable<NodeDetailsRequest>
    {
        [DataMember (Name = "node")] public string Node;
    
        public NodeDetailsRequest()
        {
            Node = "";
        }
        
        public NodeDetailsRequest(string Node)
        {
            this.Node = Node;
        }
        
        public NodeDetailsRequest(ref ReadBuffer b)
        {
            Node = b.DeserializeString();
        }
        
        public NodeDetailsRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Node = b.DeserializeString();
        }
        
        public NodeDetailsRequest RosDeserialize(ref ReadBuffer b) => new NodeDetailsRequest(ref b);
        
        public NodeDetailsRequest RosDeserialize(ref ReadBuffer2 b) => new NodeDetailsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Node);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Node);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Node, nameof(Node));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Node);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Node);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class NodeDetailsResponse : IResponse, IDeserializable<NodeDetailsResponse>
    {
        [DataMember (Name = "subscribing")] public string[] Subscribing;
        [DataMember (Name = "publishing")] public string[] Publishing;
        [DataMember (Name = "services")] public string[] Services;
    
        public NodeDetailsResponse()
        {
            Subscribing = EmptyArray<string>.Value;
            Publishing = EmptyArray<string>.Value;
            Services = EmptyArray<string>.Value;
        }
        
        public NodeDetailsResponse(string[] Subscribing, string[] Publishing, string[] Services)
        {
            this.Subscribing = Subscribing;
            this.Publishing = Publishing;
            this.Services = Services;
        }
        
        public NodeDetailsResponse(ref ReadBuffer b)
        {
            Subscribing = b.DeserializeStringArray();
            Publishing = b.DeserializeStringArray();
            Services = b.DeserializeStringArray();
        }
        
        public NodeDetailsResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Subscribing = b.DeserializeStringArray();
            b.Align4();
            Publishing = b.DeserializeStringArray();
            b.Align4();
            Services = b.DeserializeStringArray();
        }
        
        public NodeDetailsResponse RosDeserialize(ref ReadBuffer b) => new NodeDetailsResponse(ref b);
        
        public NodeDetailsResponse RosDeserialize(ref ReadBuffer2 b) => new NodeDetailsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Subscribing.Length);
            b.SerializeArray(Subscribing);
            b.Serialize(Publishing.Length);
            b.SerializeArray(Publishing);
            b.Serialize(Services.Length);
            b.SerializeArray(Services);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Subscribing.Length);
            b.SerializeArray(Subscribing);
            b.Align4();
            b.Serialize(Publishing.Length);
            b.SerializeArray(Publishing);
            b.Align4();
            b.Serialize(Services.Length);
            b.SerializeArray(Services);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Subscribing, nameof(Subscribing));
            BuiltIns.ThrowIfNull(Publishing, nameof(Publishing));
            BuiltIns.ThrowIfNull(Services, nameof(Services));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += WriteBuffer.GetArraySize(Subscribing);
                size += WriteBuffer.GetArraySize(Publishing);
                size += WriteBuffer.GetArraySize(Services);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Subscribing);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Publishing);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Services);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
