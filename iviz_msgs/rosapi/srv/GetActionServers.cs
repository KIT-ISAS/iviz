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
            Request = GetActionServersRequest.Singleton;
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/GetActionServers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "46807ba271844ac5ba4730a47556b236";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetActionServersRequest : IRequest<GetActionServers, GetActionServersResponse>, IDeserializable<GetActionServersRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public GetActionServersRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetActionServersRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        GetActionServersRequest IDeserializable<GetActionServersRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly GetActionServersRequest Singleton = new GetActionServersRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
        public GetActionServersResponse(ref Buffer b)
        {
            ActionServers = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetActionServersResponse(ref b);
        }
        
        GetActionServersResponse IDeserializable<GetActionServersResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetActionServersResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(ActionServers, 0);
        }
        
        public void Dispose()
        {
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
                foreach (string s in ActionServers)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
