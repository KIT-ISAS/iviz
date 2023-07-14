using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class GetActionServers : IService<GetActionServersRequest, GetActionServersResponse>
    {
        /// Request message.
        [DataMember] public GetActionServersRequest Request;
        
        /// Response message.
        [DataMember] public GetActionServersResponse Response;
        
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
        
        public const string ServiceType = "rosapi_msgs/GetActionServers";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "46807ba271844ac5ba4730a47556b236";
        
        public IService Generate() => new GetActionServers();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetActionServersRequest : IRequest<GetActionServers, GetActionServersResponse>, IDeserializable<GetActionServersRequest>
    {
    
        public GetActionServersRequest()
        {
        }
        
        public GetActionServersRequest(ref ReadBuffer b)
        {
        }
        
        public GetActionServersRequest(ref ReadBuffer2 b)
        {
        }
        
        public GetActionServersRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetActionServersRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetActionServersRequest? singleton;
        public static GetActionServersRequest Singleton => singleton ??= new GetActionServersRequest();
    
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
    public sealed class GetActionServersResponse : IResponse, IDeserializable<GetActionServersResponse>
    {
        [DataMember (Name = "action_servers")] public string[] ActionServers;
    
        public GetActionServersResponse()
        {
            ActionServers = EmptyArray<string>.Value;
        }
        
        public GetActionServersResponse(string[] ActionServers)
        {
            this.ActionServers = ActionServers;
        }
        
        public GetActionServersResponse(ref ReadBuffer b)
        {
            ActionServers = b.DeserializeStringArray();
        }
        
        public GetActionServersResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            ActionServers = b.DeserializeStringArray();
        }
        
        public GetActionServersResponse RosDeserialize(ref ReadBuffer b) => new GetActionServersResponse(ref b);
        
        public GetActionServersResponse RosDeserialize(ref ReadBuffer2 b) => new GetActionServersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ActionServers.Length);
            b.SerializeArray(ActionServers);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(ActionServers.Length);
            b.SerializeArray(ActionServers);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(ActionServers, nameof(ActionServers));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(ActionServers);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ActionServers);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
