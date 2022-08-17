using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class GetGridMapInfo : IService
    {
        /// Request message.
        [DataMember] public GetGridMapInfoRequest Request;
        
        /// Response message.
        [DataMember] public GetGridMapInfoResponse Response;
        
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
    
        public GetGridMapInfoRequest()
        {
        }
        
        public GetGridMapInfoRequest(ref ReadBuffer b)
        {
        }
        
        public GetGridMapInfoRequest(ref ReadBuffer2 b)
        {
        }
        
        public GetGridMapInfoRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetGridMapInfoRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetGridMapInfoRequest? singleton;
        public static GetGridMapInfoRequest Singleton => singleton ??= new GetGridMapInfoRequest();
    
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
    public sealed class GetGridMapInfoResponse : IResponse, IDeserializable<GetGridMapInfoResponse>
    {
        // Grid map info
        [DataMember (Name = "info")] public GridMapMsgs.GridMapInfo Info;
    
        public GetGridMapInfoResponse()
        {
            Info = new GridMapMsgs.GridMapInfo();
        }
        
        public GetGridMapInfoResponse(GridMapMsgs.GridMapInfo Info)
        {
            this.Info = Info;
        }
        
        public GetGridMapInfoResponse(ref ReadBuffer b)
        {
            Info = new GridMapMsgs.GridMapInfo(ref b);
        }
        
        public GetGridMapInfoResponse(ref ReadBuffer2 b)
        {
            Info = new GridMapMsgs.GridMapInfo(ref b);
        }
        
        public GetGridMapInfoResponse RosDeserialize(ref ReadBuffer b) => new GetGridMapInfoResponse(ref b);
        
        public GetGridMapInfoResponse RosDeserialize(ref ReadBuffer2 b) => new GetGridMapInfoResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Info.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Info.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Info is null) BuiltIns.ThrowNullReference();
            Info.RosValidate();
        }
    
        public int RosMessageLength => 0 + Info.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Info.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
