using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    [DataContract]
    public sealed class SetGridMap : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetGridMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetGridMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetGridMap()
        {
            Request = new SetGridMapRequest();
            Response = new SetGridMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetGridMap(SetGridMapRequest request)
        {
            Request = request;
            Response = new SetGridMapResponse();
        }
        
        IService IService.Create() => new SetGridMap();
        
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "grid_map_msgs/SetGridMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "4f8e24cfd42bc1470fe765b7516ff7e5";
    }

    public sealed class SetGridMapRequest : IRequest
    {
        // map
        [DataMember] public grid_map_msgs.GridMap map { get; set; }
        
    
        /// <summary> Constructor for empty message. </summary>
        public SetGridMapRequest()
        {
            map = new grid_map_msgs.GridMap();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetGridMapRequest(grid_map_msgs.GridMap map)
        {
            this.map = map ?? throw new System.ArgumentNullException(nameof(map));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetGridMapRequest(Buffer b)
        {
            this.map = new grid_map_msgs.GridMap(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetGridMapRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.map);
        }
        
        public void Validate()
        {
            if (map is null) throw new System.NullReferenceException();
            map.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += map.RosMessageLength;
                return size;
            }
        }
    }

    public sealed class SetGridMapResponse : Internal.EmptyResponse
    {
    }
}
