using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract (Name = "grid_map_msgs/GetGridMapInfo")]
    public sealed class GetGridMapInfo : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetGridMapInfoRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetGridMapInfoResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetGridMapInfo()
        {
            Request = new GetGridMapInfoRequest();
            Response = new GetGridMapInfoResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetGridMapInfo(GetGridMapInfoRequest request)
        {
            Request = request;
            Response = new GetGridMapInfoResponse();
        }
        
        IService IService.Create() => new GetGridMapInfo();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetGridMapInfoRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetGridMapInfoResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "grid_map_msgs/GetGridMapInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "a0be1719725f7fd7b490db4d64321ff2";
    }

    public sealed class GetGridMapInfoRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetGridMapInfoResponse : IResponse
    {
        // Grid map info
        [DataMember (Name = "info")] public GridMapMsgs.GridMapInfo Info { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapInfoResponse()
        {
            Info = new GridMapMsgs.GridMapInfo();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetGridMapInfoResponse(GridMapMsgs.GridMapInfo Info)
        {
            this.Info = Info;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetGridMapInfoResponse(Buffer b)
        {
            Info = new GridMapMsgs.GridMapInfo(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GetGridMapInfoResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Info.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Info is null) throw new System.NullReferenceException();
            Info.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Info.RosMessageLength;
                return size;
            }
        }
    }
}
