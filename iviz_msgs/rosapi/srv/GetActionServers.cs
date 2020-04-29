using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class GetActionServers : IService
    {
        /// <summary> Request message. </summary>
        public GetActionServersRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public GetActionServersResponse Response { get; set; }
        
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
        
        public IService Create() => new GetActionServers();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/GetActionServers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "46807ba271844ac5ba4730a47556b236";
    }

    public sealed class GetActionServersRequest : IRequest
    {
        
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 0;
    }

    public sealed class GetActionServersResponse : IResponse
    {
        public string[] action_servers;
    
        /// <summary> Constructor for empty message. </summary>
        public GetActionServersResponse()
        {
            action_servers = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out action_servers, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(action_servers, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
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
