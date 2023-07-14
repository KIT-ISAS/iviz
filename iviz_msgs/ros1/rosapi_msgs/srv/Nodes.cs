using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class Nodes : IService<NodesRequest, NodesResponse>
    {
        /// Request message.
        [DataMember] public NodesRequest Request;
        
        /// Response message.
        [DataMember] public NodesResponse Response;
        
        /// Empty constructor.
        public Nodes()
        {
            Request = NodesRequest.Singleton;
            Response = new NodesResponse();
        }
        
        /// Setter constructor.
        public Nodes(NodesRequest request)
        {
            Request = request;
            Response = new NodesResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (NodesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (NodesResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/Nodes";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "3d07bfda1268b4f76b16b7ba8a82665d";
        
        public IService Generate() => new Nodes();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class NodesRequest : IRequest<Nodes, NodesResponse>, IDeserializable<NodesRequest>
    {
    
        public NodesRequest()
        {
        }
        
        public NodesRequest(ref ReadBuffer b)
        {
        }
        
        public NodesRequest(ref ReadBuffer2 b)
        {
        }
        
        public NodesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public NodesRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static NodesRequest? singleton;
        public static NodesRequest Singleton => singleton ??= new NodesRequest();
    
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
    public sealed class NodesResponse : IResponse, IDeserializable<NodesResponse>
    {
        [DataMember (Name = "nodes")] public string[] Nodes_;
    
        public NodesResponse()
        {
            Nodes_ = EmptyArray<string>.Value;
        }
        
        public NodesResponse(string[] Nodes_)
        {
            this.Nodes_ = Nodes_;
        }
        
        public NodesResponse(ref ReadBuffer b)
        {
            Nodes_ = b.DeserializeStringArray();
        }
        
        public NodesResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Nodes_ = b.DeserializeStringArray();
        }
        
        public NodesResponse RosDeserialize(ref ReadBuffer b) => new NodesResponse(ref b);
        
        public NodesResponse RosDeserialize(ref ReadBuffer2 b) => new NodesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Nodes_.Length);
            b.SerializeArray(Nodes_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Nodes_.Length);
            b.SerializeArray(Nodes_);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Nodes_, nameof(Nodes_));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Nodes_);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Nodes_);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
