using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetModules : IService
    {
        /// Request message.
        [DataMember] public GetModulesRequest Request;
        
        /// Response message.
        [DataMember] public GetModulesResponse Response;
        
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
    public sealed class GetModulesRequest : IRequest<GetModules, GetModulesResponse>, IDeserializable<GetModulesRequest>
    {
        // Gets a list of modules
    
        public GetModulesRequest()
        {
        }
        
        public GetModulesRequest(ref ReadBuffer b)
        {
        }
        
        public GetModulesRequest(ref ReadBuffer2 b)
        {
        }
        
        public GetModulesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetModulesRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetModulesRequest? singleton;
        public static GetModulesRequest Singleton => singleton ??= new GetModulesRequest();
    
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
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int _) { }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModulesResponse : IResponse, IDeserializable<GetModulesResponse>
    {
        /// <summary> List of module configurations in JSON encoding </summary>
        [DataMember (Name = "configs")] public string[] Configs;
    
        public GetModulesResponse()
        {
            Configs = System.Array.Empty<string>();
        }
        
        public GetModulesResponse(string[] Configs)
        {
            this.Configs = Configs;
        }
        
        public GetModulesResponse(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Configs);
        }
        
        public GetModulesResponse(ref ReadBuffer2 b)
        {
            b.DeserializeStringArray(out Configs);
        }
        
        public GetModulesResponse RosDeserialize(ref ReadBuffer b) => new GetModulesResponse(ref b);
        
        public GetModulesResponse RosDeserialize(ref ReadBuffer2 b) => new GetModulesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Configs);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
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
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Configs);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
