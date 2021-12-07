using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetModules : IService
    {
        /// Request message.
        [DataMember] public GetModulesRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetModulesResponse Response { get; set; }
        
        /// Empty constructor.
        public GetModules()
        {
            Request = GetModulesRequest.Singleton;
            Response = new GetModulesResponse();
        }
        
        /// Setter constructor.
        public GetModules(GetModulesRequest request)
        {
            Request = request;
            Response = new GetModulesResponse();
        }
        
        IService IService.Create() => new GetModules();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetModulesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetModulesResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/GetModules";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "854d12ba02315a7b73d8ac45d1a68e74";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModulesRequest : IRequest<GetModules, GetModulesResponse>, IDeserializable<GetModulesRequest>
    {
        // Gets a list of modules
    
        /// Constructor for empty message.
        public GetModulesRequest()
        {
        }
        
        /// Constructor with buffer.
        internal GetModulesRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetModulesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly GetModulesRequest Singleton = new GetModulesRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModulesResponse : IResponse, IDeserializable<GetModulesResponse>
    {
        /// List of module configurations in JSON encoding
        [DataMember (Name = "configs")] public string[] Configs;
    
        /// Constructor for empty message.
        public GetModulesResponse()
        {
            Configs = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public GetModulesResponse(string[] Configs)
        {
            this.Configs = Configs;
        }
        
        /// Constructor with buffer.
        internal GetModulesResponse(ref ReadBuffer b)
        {
            Configs = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetModulesResponse(ref b);
        
        public GetModulesResponse RosDeserialize(ref ReadBuffer b) => new GetModulesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Configs);
        }
        
        public void RosValidate()
        {
            if (Configs is null) throw new System.NullReferenceException(nameof(Configs));
            for (int i = 0; i < Configs.Length; i++)
            {
                if (Configs[i] is null) throw new System.NullReferenceException($"{nameof(Configs)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Configs);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
