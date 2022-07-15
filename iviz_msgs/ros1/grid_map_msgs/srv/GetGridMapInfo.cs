using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class GetGridMapInfo : IService
    {
        /// Request message.
        [DataMember] public GetGridMapInfoRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetGridMapInfoResponse Response { get; set; }
        
        /// Empty constructor.
        public GetGridMapInfo()
        {
            Request = GetGridMapInfoRequest.Singleton;
            Response = new GetGridMapInfoResponse();
        }
        
        /// Setter constructor.
        public GetGridMapInfo(GetGridMapInfoRequest request)
        {
            Request = request;
            Response = new GetGridMapInfoResponse();
        }
        
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
        
        public const string ServiceType = "grid_map_msgs/GetGridMapInfo";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "a0be1719725f7fd7b490db4d64321ff2";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGridMapInfoRequest : IRequest<GetGridMapInfo, GetGridMapInfoResponse>, IDeserializable<GetGridMapInfoRequest>
    {
    
        /// Constructor for empty message.
        public GetGridMapInfoRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetGridMapInfoRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetGridMapInfoRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static GetGridMapInfoRequest? singleton;
        public static GetGridMapInfoRequest Singleton => singleton ??= new GetGridMapInfoRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGridMapInfoResponse : IResponse, IDeserializable<GetGridMapInfoResponse>
    {
        // Grid map info
        [DataMember (Name = "info")] public GridMapMsgs.GridMapInfo Info;
    
        /// Constructor for empty message.
        public GetGridMapInfoResponse()
        {
            Info = new GridMapMsgs.GridMapInfo();
        }
        
        /// Explicit constructor.
        public GetGridMapInfoResponse(GridMapMsgs.GridMapInfo Info)
        {
            this.Info = Info;
        }
        
        /// Constructor with buffer.
        public GetGridMapInfoResponse(ref ReadBuffer b)
        {
            Info = new GridMapMsgs.GridMapInfo(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetGridMapInfoResponse(ref b);
        
        public GetGridMapInfoResponse RosDeserialize(ref ReadBuffer b) => new GetGridMapInfoResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Info.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Info is null) BuiltIns.ThrowNullReference();
            Info.RosValidate();
        }
    
        public int RosMessageLength => 0 + Info.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
