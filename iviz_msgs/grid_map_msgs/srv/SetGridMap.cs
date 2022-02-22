using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SetGridMap : IService
    {
        /// Request message.
        [DataMember] public SetGridMapRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetGridMapResponse Response { get; set; }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "grid_map_msgs/SetGridMap";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "4f8e24cfd42bc1470fe765b7516ff7e5";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetGridMapRequest : IRequest<SetGridMap, SetGridMapResponse>, IDeserializable<SetGridMapRequest>
    {
        // map
        [DataMember (Name = "map")] public GridMapMsgs.GridMap Map;
    
        /// Constructor for empty message.
        public SetGridMapRequest()
        {
            Map = new GridMapMsgs.GridMap();
        }
        
        /// Explicit constructor.
        public SetGridMapRequest(GridMapMsgs.GridMap Map)
        {
            this.Map = Map;
        }
        
        /// Constructor with buffer.
        public SetGridMapRequest(ref ReadBuffer b)
        {
            Map = new GridMapMsgs.GridMap(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetGridMapRequest(ref b);
        
        public SetGridMapRequest RosDeserialize(ref ReadBuffer b) => new SetGridMapRequest(ref b);
    
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

    [DataContract]
    public sealed class SetGridMapResponse : IResponse, IDeserializable<SetGridMapResponse>
    {
    
        /// Constructor for empty message.
        public SetGridMapResponse()
        {
        }
        
        /// Constructor with buffer.
        public SetGridMapResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public SetGridMapResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly SetGridMapResponse Singleton = new SetGridMapResponse();
    
        public void RosSerialize(ref WriteBuffer b)
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
}
