using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract]
    public sealed class GetOctomap : IService
    {
        /// Request message.
        [DataMember] public GetOctomapRequest Request;
        
        /// Response message.
        [DataMember] public GetOctomapResponse Response;
        
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
        
        public const string ServiceType = "octomap_msgs/GetOctomap";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "be9d2869d24fe40d6bc21ac21f6bb2c5";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetOctomapRequest : IRequest<GetOctomap, GetOctomapResponse>, IDeserializable<GetOctomapRequest>
    {
        // Get the map as a octomap
    
        public GetOctomapRequest()
        {
        }
        
        public GetOctomapRequest(ref ReadBuffer b)
        {
        }
        
        public GetOctomapRequest(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetOctomapRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetOctomapRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetOctomapRequest? singleton;
        public static GetOctomapRequest Singleton => singleton ??= new GetOctomapRequest();
    
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
        
        public void AddRos2MessageLength(ref int c) { }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetOctomapResponse : IResponse, IDeserializable<GetOctomapResponse>
    {
        [DataMember (Name = "map")] public OctomapMsgs.Octomap Map;
    
        public GetOctomapResponse()
        {
            Map = new OctomapMsgs.Octomap();
        }
        
        public GetOctomapResponse(OctomapMsgs.Octomap Map)
        {
            this.Map = Map;
        }
        
        public GetOctomapResponse(ref ReadBuffer b)
        {
            Map = new OctomapMsgs.Octomap(ref b);
        }
        
        public GetOctomapResponse(ref ReadBuffer2 b)
        {
            Map = new OctomapMsgs.Octomap(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetOctomapResponse(ref b);
        
        public GetOctomapResponse RosDeserialize(ref ReadBuffer b) => new GetOctomapResponse(ref b);
        
        public GetOctomapResponse RosDeserialize(ref ReadBuffer2 b) => new GetOctomapResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) BuiltIns.ThrowNullReference();
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Map.AddRos2MessageLength(ref c);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
