using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/SetCameraInfo")]
    public sealed class SetCameraInfo : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetCameraInfoRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetCameraInfoResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetCameraInfo()
        {
            Request = new SetCameraInfoRequest();
            Response = new SetCameraInfoResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "sensor_msgs/SetCameraInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "bef1df590ed75ed1f393692395e15482";
    }

    [DataContract]
    public sealed class SetCameraInfoRequest : IRequest, IDeserializable<SetCameraInfoRequest>
    {
        // This service requests that a camera stores the given CameraInfo 
        // as that camera's calibration information.
        //
        // The width and height in the camera_info field should match what the
        // camera is currently outputting on its camera_info topic, and the camera
        // will assume that the region of the imager that is being referred to is
        // the region that the camera is currently capturing.
        [DataMember (Name = "camera_info")] public SensorMsgs.CameraInfo CameraInfo { get; set; } // The camera_info to store
    
        /// <summary> Constructor for empty message. </summary>
        public SetCameraInfoRequest()
        {
            CameraInfo = new SensorMsgs.CameraInfo();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetCameraInfoRequest(SensorMsgs.CameraInfo CameraInfo)
        {
            this.CameraInfo = CameraInfo;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetCameraInfoRequest(ref Buffer b)
        {
            CameraInfo = new SensorMsgs.CameraInfo(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetCameraInfoRequest(ref b);
        }
        
        SetCameraInfoRequest IDeserializable<SetCameraInfoRequest>.RosDeserialize(ref Buffer b)
        {
            return new SetCameraInfoRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            CameraInfo.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (CameraInfo is null) throw new System.NullReferenceException(nameof(CameraInfo));
            CameraInfo.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += CameraInfo.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class SetCameraInfoResponse : IResponse, IDeserializable<SetCameraInfoResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // True if the call succeeded
        [DataMember (Name = "status_message")] public string StatusMessage { get; set; } // Used to give details about success
    
        /// <summary> Constructor for empty message. </summary>
        public SetCameraInfoResponse()
        {
            StatusMessage = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetCameraInfoResponse(bool Success, string StatusMessage)
        {
            this.Success = Success;
            this.StatusMessage = StatusMessage;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetCameraInfoResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            StatusMessage = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetCameraInfoResponse(ref b);
        }
        
        SetCameraInfoResponse IDeserializable<SetCameraInfoResponse>.RosDeserialize(ref Buffer b)
        {
            return new SetCameraInfoResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(StatusMessage);
        }
        
        public void RosValidate()
        {
            if (StatusMessage is null) throw new System.NullReferenceException(nameof(StatusMessage));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(StatusMessage);
                return size;
            }
        }
    }
}
