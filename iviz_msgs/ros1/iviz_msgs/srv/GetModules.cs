using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "iviz_msgs/GetModules";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "854d12ba02315a7b73d8ac45d1a68e74";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModulesRequest : IRequest<GetModules, GetModulesResponse>, IDeserializableRos1<GetModulesRequest>
    {
        // Gets a list of modules
    
        /// Constructor for empty message.
        public GetModulesRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetModulesRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetModulesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static GetModulesRequest? singleton;
        public static GetModulesRequest Singleton => singleton ??= new GetModulesRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModulesResponse : IResponse, IDeserializableRos1<GetModulesResponse>
    {
        /// <summary> List of module configurations in JSON encoding </summary>
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
        public GetModulesResponse(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Configs);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetModulesResponse(ref b);
        
        public GetModulesResponse RosDeserialize(ref ReadBuffer b) => new GetModulesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Configs);
        }
        
        public void RosValidate()
        {
            if (Configs is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Configs.Length; i++)
            {
                if (Configs[i] is null) BuiltIns.ThrowNullReference(nameof(Configs), i);
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Configs);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
