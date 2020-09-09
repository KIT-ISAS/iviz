using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract (Name = "octomap_msgs/GetOctomap")]
    public sealed class GetOctomap : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetOctomapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetOctomapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetOctomap()
        {
            Request = new GetOctomapRequest();
            Response = new GetOctomapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "octomap_msgs/GetOctomap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "be9d2869d24fe40d6bc21ac21f6bb2c5";
    }

    public sealed class GetOctomapRequest : IRequest
    {
        // Get the map as a octomap
    
        /// <summary> Constructor for empty message. </summary>
        public GetOctomapRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetOctomapRequest(Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetOctomapRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
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

    public sealed class GetOctomapResponse : IResponse
    {
        [DataMember (Name = "map")] public OctomapMsgs.Octomap Map { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetOctomapResponse()
        {
            Map = new OctomapMsgs.Octomap();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetOctomapResponse(OctomapMsgs.Octomap Map)
        {
            this.Map = Map;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetOctomapResponse(Buffer b)
        {
            Map = new OctomapMsgs.Octomap(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetOctomapResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Map.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Map.RosMessageLength;
                return size;
            }
        }
    }
}
