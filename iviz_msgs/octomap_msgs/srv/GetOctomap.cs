using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetOctomap : IService
    {
        /// Request message.
        [DataMember] public GetOctomapRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetOctomapResponse Response { get; set; }
        
        /// Empty constructor.
        public GetOctomap()
        {
            Request = GetOctomapRequest.Singleton;
            Response = new GetOctomapResponse();
        }
        
        /// Setter constructor.
        public GetOctomap(GetOctomapRequest request)
        {
            Request = request;
            Response = new GetOctomapResponse();
        }
        
        IService IService.Create() => new GetOctomap();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetOctomapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetOctomapResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "octomap_msgs/GetOctomap";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "be9d2869d24fe40d6bc21ac21f6bb2c5";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetOctomapRequest : IRequest<GetOctomap, GetOctomapResponse>, IDeserializable<GetOctomapRequest>
    {
        // Get the map as a octomap
    
        /// Constructor for empty message.
        public GetOctomapRequest()
        {
        }
        
        /// Constructor with buffer.
        internal GetOctomapRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetOctomapRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly GetOctomapRequest Singleton = new GetOctomapRequest();
    
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
    public sealed class GetOctomapResponse : IResponse, IDeserializable<GetOctomapResponse>
    {
        [DataMember (Name = "map")] public OctomapMsgs.Octomap Map;
    
        /// Constructor for empty message.
        public GetOctomapResponse()
        {
            Map = new OctomapMsgs.Octomap();
        }
        
        /// Explicit constructor.
        public GetOctomapResponse(OctomapMsgs.Octomap Map)
        {
            this.Map = Map;
        }
        
        /// Constructor with buffer.
        internal GetOctomapResponse(ref ReadBuffer b)
        {
            Map = new OctomapMsgs.Octomap(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetOctomapResponse(ref b);
        
        public GetOctomapResponse RosDeserialize(ref ReadBuffer b) => new GetOctomapResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
