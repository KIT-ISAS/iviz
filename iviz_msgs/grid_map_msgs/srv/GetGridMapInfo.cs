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
            Request = GetGridMapInfoRequest.Singleton;
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "grid_map_msgs/GetGridMapInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "a0be1719725f7fd7b490db4d64321ff2";
    }

    [DataContract]
    public sealed class GetGridMapInfoRequest : IRequest<GetGridMapInfo, GetGridMapInfoResponse>, IDeserializable<GetGridMapInfoRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public GetGridMapInfoRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetGridMapInfoRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        GetGridMapInfoRequest IDeserializable<GetGridMapInfoRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly GetGridMapInfoRequest Singleton = new GetGridMapInfoRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class GetGridMapInfoResponse : IResponse, IDeserializable<GetGridMapInfoResponse>
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
        public GetGridMapInfoResponse(ref Buffer b)
        {
            Info = new GridMapMsgs.GridMapInfo(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetGridMapInfoResponse(ref b);
        }
        
        GetGridMapInfoResponse IDeserializable<GetGridMapInfoResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetGridMapInfoResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Info.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Info is null) throw new System.NullReferenceException(nameof(Info));
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
