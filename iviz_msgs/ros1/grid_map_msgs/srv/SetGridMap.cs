using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class SetGridMap : IService
    {
        /// Request message.
        [DataMember] public SetGridMapRequest Request;
        
        /// Response message.
        [DataMember] public SetGridMapResponse Response;
        
        /// Empty constructor.
        public SetGridMap()
        {
            Request = new SetGridMapRequest();
            Response = SetGridMapResponse.Singleton;
        }
        
        /// Setter constructor.
        public SetGridMap(SetGridMapRequest request)
        {
            Request = request;
            Response = SetGridMapResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetGridMapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetGridMapResponse)value;
        }
        
        public const string ServiceType = "grid_map_msgs/SetGridMap";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "4f8e24cfd42bc1470fe765b7516ff7e5";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetGridMapRequest : IRequest<SetGridMap, SetGridMapResponse>, IDeserializable<SetGridMapRequest>
    {
        // map
        [DataMember (Name = "map")] public GridMapMsgs.GridMap Map;
    
        public SetGridMapRequest()
        {
            Map = new GridMapMsgs.GridMap();
        }
        
        public SetGridMapRequest(GridMapMsgs.GridMap Map)
        {
            this.Map = Map;
        }
        
        public SetGridMapRequest(ref ReadBuffer b)
        {
            Map = new GridMapMsgs.GridMap(ref b);
        }
        
        public SetGridMapRequest(ref ReadBuffer2 b)
        {
            Map = new GridMapMsgs.GridMap(ref b);
        }
        
        public SetGridMapRequest RosDeserialize(ref ReadBuffer b) => new SetGridMapRequest(ref b);
        
        public SetGridMapRequest RosDeserialize(ref ReadBuffer2 b) => new SetGridMapRequest(ref b);
    
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

    [DataContract]
    public sealed class SetGridMapResponse : IResponse, IDeserializable<SetGridMapResponse>
    {
    
        public SetGridMapResponse()
        {
        }
        
        public SetGridMapResponse(ref ReadBuffer b)
        {
        }
        
        public SetGridMapResponse(ref ReadBuffer2 b)
        {
        }
        
        public SetGridMapResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public SetGridMapResponse RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static SetGridMapResponse? singleton;
        public static SetGridMapResponse Singleton => singleton ??= new SetGridMapResponse();
    
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
}
