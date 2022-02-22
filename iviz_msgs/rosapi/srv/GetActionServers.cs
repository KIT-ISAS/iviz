using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetActionServers : IService
    {
        /// Request message.
        [DataMember] public GetActionServersRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetActionServersResponse Response { get; set; }
        
        /// Empty constructor.
        public GetActionServers()
        {
            Request = GetActionServersRequest.Singleton;
            Response = new GetActionServersResponse();
        }
        
        /// Setter constructor.
        public GetActionServers(GetActionServersRequest request)
        {
            Request = request;
            Response = new GetActionServersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetActionServersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetActionServersResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/GetActionServers";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "46807ba271844ac5ba4730a47556b236";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetActionServersRequest : IRequest<GetActionServers, GetActionServersResponse>, IDeserializable<GetActionServersRequest>
    {
    
        /// Constructor for empty message.
        public GetActionServersRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetActionServersRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetActionServersRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly GetActionServersRequest Singleton = new GetActionServersRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetActionServersResponse : IResponse, IDeserializable<GetActionServersResponse>
    {
        [DataMember (Name = "action_servers")] public string[] ActionServers;
    
        /// Constructor for empty message.
        public GetActionServersResponse()
        {
            ActionServers = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public GetActionServersResponse(string[] ActionServers)
        {
            this.ActionServers = ActionServers;
        }
        
        /// Constructor with buffer.
        public GetActionServersResponse(ref ReadBuffer b)
        {
            ActionServers = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetActionServersResponse(ref b);
        
        public GetActionServersResponse RosDeserialize(ref ReadBuffer b) => new GetActionServersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(ActionServers);
        }
        
        public void RosValidate()
        {
            if (ActionServers is null) throw new System.NullReferenceException(nameof(ActionServers));
            for (int i = 0; i < ActionServers.Length; i++)
            {
                if (ActionServers[i] is null) throw new System.NullReferenceException($"{nameof(ActionServers)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(ActionServers);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
