using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class SetCameraInfo : IService
    {
        /// <summary> Request message. </summary>
        public SetCameraInfoRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public SetCameraInfoResponse Response { get; set; }
        
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
        
        public IService Create() => new SetCameraInfo();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "sensor_msgs/SetCameraInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "bef1df590ed75ed1f393692395e15482";
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
        
        public sensor_msgs.CameraInfo camera_info; // The camera_info to store
    
        /// <summary> Constructor for empty message. </summary>
        public SetCameraInfoRequest()
        {
            camera_info = new sensor_msgs.CameraInfo();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            camera_info.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            camera_info.Serialize(ref ptr, end);
        }
    
        [IgnoreDataMember]
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
        public bool success; // True if the call succeeded
        public string status_message; // Used to give details about success
    
        /// <summary> Constructor for empty message. </summary>
        public SetCameraInfoResponse()
        {
            status_message = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out success, ref ptr, end);
            BuiltIns.Deserialize(out status_message, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(success, ref ptr, end);
            BuiltIns.Serialize(status_message, ref ptr, end);
        }
    
        [IgnoreDataMember]
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
