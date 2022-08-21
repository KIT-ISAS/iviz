using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMap : IService
    {
        /// Request message.
        [DataMember] public GetMapRequest Request;
        
        /// Response message.
        [DataMember] public GetMapResponse Response;
        
        /// Empty constructor.
        public GetMap()
        {
            Request = GetMapRequest.Singleton;
            Response = new GetMapResponse();
        }
        
        /// Setter constructor.
        public GetMap(GetMapRequest request)
        {
            Request = request;
            Response = new GetMapResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetMapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetMapResponse)value;
        }
        
        public const string ServiceType = "nav_msgs/GetMap";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "6cdd0a18e0aff5b0a3ca2326a89b54ff";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMapRequest : IRequest<GetMap, GetMapResponse>, IDeserializable<GetMapRequest>
    {
        // Get the map as a nav_msgs/OccupancyGrid
    
        public GetMapRequest()
        {
        }
        
        public GetMapRequest(ref ReadBuffer b)
        {
        }
        
        public GetMapRequest(ref ReadBuffer2 b)
        {
        }
        
        public GetMapRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetMapRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetMapRequest? singleton;
        public static GetMapRequest Singleton => singleton ??= new GetMapRequest();
    
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
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMapResponse : IResponse, IDeserializable<GetMapResponse>
    {
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map;
    
        public GetMapResponse()
        {
            Map = new NavMsgs.OccupancyGrid();
        }
        
        public GetMapResponse(NavMsgs.OccupancyGrid Map)
        {
            this.Map = Map;
        }
        
        public GetMapResponse(ref ReadBuffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public GetMapResponse(ref ReadBuffer2 b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public GetMapResponse RosDeserialize(ref ReadBuffer b) => new GetMapResponse(ref b);
        
        public GetMapResponse RosDeserialize(ref ReadBuffer2 b) => new GetMapResponse(ref b);
    
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Map.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}