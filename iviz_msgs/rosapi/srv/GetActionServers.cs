using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    [DataContract]
    public sealed class GetActionServers : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetActionServersRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetActionServersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetActionServers()
        {
            Request = new GetActionServersRequest();
            Response = new GetActionServersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetActionServers(GetActionServersRequest request)
        {
            Request = request;
            Response = new GetActionServersResponse();
        }
        
        IService IService.Create() => new GetActionServers();
        
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/GetActionServers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "46807ba271844ac5ba4730a47556b236";
    }

    public sealed class GetActionServersRequest : IRequest
    {
        
    
        /// <summary> Constructor for empty message. </summary>
        public GetActionServersRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetActionServersRequest(Buffer b)
        {
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetActionServersRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class GetActionServersResponse : IResponse
    {
        [DataMember] public string[] action_servers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetActionServersResponse()
        {
            action_servers = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetActionServersResponse(string[] action_servers)
        {
            this.action_servers = action_servers ?? throw new System.ArgumentNullException(nameof(action_servers));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetActionServersResponse(Buffer b)
        {
            this.action_servers = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new GetActionServersResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.action_servers, 0);
        }
        
        public void Validate()
        {
            if (action_servers is null) throw new System.NullReferenceException();
            for (int i = 0; i < action_servers.Length; i++)
            {
                if (action_servers[i] is null) throw new System.NullReferenceException();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * action_servers.Length;
                for (int i = 0; i < action_servers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(action_servers[i]);
                }
                return size;
            }
        }
    }
}
