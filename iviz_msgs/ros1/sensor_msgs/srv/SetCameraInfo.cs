using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class SetCameraInfo : IService
    {
        /// Request message.
        [DataMember] public SetCameraInfoRequest Request;
        
        /// Response message.
        [DataMember] public SetCameraInfoResponse Response;
        
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
        
        public const string ServiceType = "sensor_msgs/SetCameraInfo";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "bef1df590ed75ed1f393692395e15482";
        
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
        /// <summary> The camera_info to store </summary>
        [DataMember (Name = "camera_info")] public SensorMsgs.CameraInfo CameraInfo;
    
        public SetCameraInfoRequest()
        {
            CameraInfo = new SensorMsgs.CameraInfo();
        }
        
        public SetCameraInfoRequest(SensorMsgs.CameraInfo CameraInfo)
        {
            this.CameraInfo = CameraInfo;
        }
        
        public SetCameraInfoRequest(ref ReadBuffer b)
        {
            CameraInfo = new SensorMsgs.CameraInfo(ref b);
        }
        
        public SetCameraInfoRequest(ref ReadBuffer2 b)
        {
            CameraInfo = new SensorMsgs.CameraInfo(ref b);
        }
        
        public SetCameraInfoRequest RosDeserialize(ref ReadBuffer b) => new SetCameraInfoRequest(ref b);
        
        public SetCameraInfoRequest RosDeserialize(ref ReadBuffer2 b) => new SetCameraInfoRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            CameraInfo.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            CameraInfo.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (CameraInfo is null) BuiltIns.ThrowNullReference();
            CameraInfo.RosValidate();
        }
    
        public int RosMessageLength => 0 + CameraInfo.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = CameraInfo.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetCameraInfoResponse : IResponse, IDeserializable<SetCameraInfoResponse>
    {
        /// <summary> True if the call succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> Used to give details about success </summary>
        [DataMember (Name = "status_message")] public string StatusMessage;
    
        public SetCameraInfoResponse()
        {
            StatusMessage = "";
        }
        
        public SetCameraInfoResponse(bool Success, string StatusMessage)
        {
            this.Success = Success;
            this.StatusMessage = StatusMessage;
        }
        
        public SetCameraInfoResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out StatusMessage);
        }
        
        public SetCameraInfoResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            b.DeserializeString(out StatusMessage);
        }
        
        public SetCameraInfoResponse RosDeserialize(ref ReadBuffer b) => new SetCameraInfoResponse(ref b);
        
        public SetCameraInfoResponse RosDeserialize(ref ReadBuffer2 b) => new SetCameraInfoResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(StatusMessage);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Serialize(StatusMessage);
        }
        
        public void RosValidate()
        {
            if (StatusMessage is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + WriteBuffer.GetStringSize(StatusMessage);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = WriteBuffer2.AddLength(c, StatusMessage);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
