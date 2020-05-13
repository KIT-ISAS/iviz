using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
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
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "sensor_msgs/SetCameraInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "bef1df590ed75ed1f393692395e15482";
    }

    public sealed class SetCameraInfoRequest : IRequest
    {
        // This service requests that a camera stores the given CameraInfo 
        // as that camera's calibration information.
        //
        // The width and height in the camera_info field should match what the
        // camera is currently outputting on its camera_info topic, and the camera
        // will assume that the region of the imager that is being referred to is
        // the region that the camera is currently capturing.
        
        [DataMember] public sensor_msgs.CameraInfo camera_info { get; set; } // The camera_info to store
    
        /// <summary> Constructor for empty message. </summary>
        public SetCameraInfoRequest()
        {
            camera_info = new sensor_msgs.CameraInfo();
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetCameraInfoRequest(sensor_msgs.CameraInfo camera_info)
        {
            this.camera_info = camera_info ?? throw new System.ArgumentNullException(nameof(camera_info));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetCameraInfoRequest(Buffer b)
        {
            this.camera_info = new sensor_msgs.CameraInfo(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetCameraInfoRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.camera_info);
        }
        
        public void Validate()
        {
            if (camera_info is null) throw new System.NullReferenceException();
            camera_info.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += camera_info.RosMessageLength;
                return size;
            }
        }
    }

    public sealed class SetCameraInfoResponse : IResponse
    {
        [DataMember] public bool success { get; set; } // True if the call succeeded
        [DataMember] public string status_message { get; set; } // Used to give details about success
    
        /// <summary> Constructor for empty message. </summary>
        public SetCameraInfoResponse()
        {
            status_message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetCameraInfoResponse(bool success, string status_message)
        {
            this.success = success;
            this.status_message = status_message ?? throw new System.ArgumentNullException(nameof(status_message));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetCameraInfoResponse(Buffer b)
        {
            this.success = b.Deserialize<bool>();
            this.status_message = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetCameraInfoResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.success);
            b.Serialize(this.status_message);
        }
        
        public void Validate()
        {
            if (status_message is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(status_message);
                return size;
            }
        }
    }
}
