using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class SetGridMap : IService<SetGridMapRequest, SetGridMapResponse>
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
        
        public IService Generate() => new SetGridMap();
        
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
            Map.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Map.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Map.AddRos2MessageLength(size);
            return size;
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
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
