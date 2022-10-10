using System.Runtime.CompilerServices;
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
        
        public const int Ros2FixedMessageLength = 0;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModulesResponse : IResponse, IDeserializable<GetModulesResponse>
    {
        /// <summary> List of module configurations in JSON encoding </summary>
        [DataMember (Name = "configs")] public string[] Configs;
    
        public GetModulesResponse()
        {
            Configs = EmptyArray<string>.Value;
        }
        
        public GetModulesResponse(string[] Configs)
        {
            this.Configs = Configs;
        }
        
        public GetModulesResponse(ref ReadBuffer b)
        {
            Configs = b.DeserializeStringArray();
        }
        
        public GetModulesResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Configs = b.DeserializeStringArray();
        }
        
        public GetModulesResponse RosDeserialize(ref ReadBuffer b) => new GetModulesResponse(ref b);
        
        public GetModulesResponse RosDeserialize(ref ReadBuffer2 b) => new GetModulesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Configs.Length);
            b.SerializeArray(Configs);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Configs.Length);
            b.SerializeArray(Configs);
        }
        
        public void RosValidate()
        {
            if (Configs is null) BuiltIns.ThrowNullReference(nameof(Configs));
            for (int i = 0; i < Configs.Length; i++)
            {
                if (Configs[i] is null) BuiltIns.ThrowNullReference(nameof(Configs), i);
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Configs);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Configs);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
