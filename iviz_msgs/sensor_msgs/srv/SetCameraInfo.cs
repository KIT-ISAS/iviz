using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SetCameraInfo : IService
    {
        /// Request message.
        [DataMember] public SetCameraInfoRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetCameraInfoResponse Response { get; set; }
        
        /// Empty constructor.
        public SetCameraInfo()
        {
            Request = new SetCameraInfoRequest();
            Response = new SetCameraInfoResponse();
        }
        
        /// Setter constructor.
        public SetCameraInfo(SetCameraInfoRequest request)
        {
            Request = request;
            Response = new SetCameraInfoResponse();
        }
        
        IService IService.Create() => new SetCameraInfo();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetCameraInfoRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetCameraInfoResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "sensor_msgs/SetCameraInfo";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "bef1df590ed75ed1f393692395e15482";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetCameraInfoRequest : IRequest<SetCameraInfo, SetCameraInfoResponse>, IDeserializable<SetCameraInfoRequest>
    {
        // This service requests that a camera stores the given CameraInfo 
        // as that camera's calibration information.
        //
        // The width and height in the camera_info field should match what the
        // camera is currently outputting on its camera_info topic, and the camera
        // will assume that the region of the imager that is being referred to is
        // the region that the camera is currently capturing.
        [DataMember (Name = "camera_info")] public SensorMsgs.CameraInfo CameraInfo; // The camera_info to store
    
        /// Constructor for empty message.
        public SetCameraInfoRequest()
        {
            CameraInfo = new SensorMsgs.CameraInfo();
        }
        
        /// Explicit constructor.
        public SetCameraInfoRequest(SensorMsgs.CameraInfo CameraInfo)
        {
            this.CameraInfo = CameraInfo;
        }
        
        /// Constructor with buffer.
        internal SetCameraInfoRequest(ref ReadBuffer b)
        {
            CameraInfo = new SensorMsgs.CameraInfo(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetCameraInfoRequest(ref b);
        
        public SetCameraInfoRequest RosDeserialize(ref ReadBuffer b) => new SetCameraInfoRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            CameraInfo.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (CameraInfo is null) throw new System.NullReferenceException(nameof(CameraInfo));
            CameraInfo.RosValidate();
        }
    
        public int RosMessageLength => 0 + CameraInfo.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetCameraInfoResponse : IResponse, IDeserializable<SetCameraInfoResponse>
    {
        [DataMember (Name = "success")] public bool Success; // True if the call succeeded
        [DataMember (Name = "status_message")] public string StatusMessage; // Used to give details about success
    
        /// Constructor for empty message.
        public SetCameraInfoResponse()
        {
            StatusMessage = string.Empty;
        }
        
        /// Explicit constructor.
        public SetCameraInfoResponse(bool Success, string StatusMessage)
        {
            this.Success = Success;
            this.StatusMessage = StatusMessage;
        }
        
        /// Constructor with buffer.
        internal SetCameraInfoResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
            StatusMessage = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetCameraInfoResponse(ref b);
        
        public SetCameraInfoResponse RosDeserialize(ref ReadBuffer b) => new SetCameraInfoResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(StatusMessage);
        }
        
        public void RosValidate()
        {
            if (StatusMessage is null) throw new System.NullReferenceException(nameof(StatusMessage));
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(StatusMessage);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
