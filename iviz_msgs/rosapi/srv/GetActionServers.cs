using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/GetActionServers")]
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
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetActionServersRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class GetActionServersResponse : IResponse
    {
        [DataMember (Name = "action_servers")] public string[] ActionServers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetActionServersResponse()
        {
            ActionServers = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetActionServersResponse(string[] ActionServers)
        {
            this.ActionServers = ActionServers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetActionServersResponse(Buffer b)
        {
            ActionServers = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetActionServersResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(ActionServers, 0);
        }
        
        public void RosValidate()
        {
            if (ActionServers is null) throw new System.NullReferenceException(nameof(ActionServers));
            for (int i = 0; i < ActionServers.Length; i++)
            {
                if (ActionServers[i] is null) throw new System.NullReferenceException($"{nameof(ActionServers)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * ActionServers.Length;
                for (int i = 0; i < ActionServers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(ActionServers[i]);
                }
                return size;
            }
        }
    }
}
