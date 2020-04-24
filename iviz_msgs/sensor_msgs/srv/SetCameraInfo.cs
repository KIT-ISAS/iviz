namespace Iviz.Msgs.sensor_msgs
{
    public class SetCameraInfo : IService
    {
        public sealed class Request : IRequest
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
            public Request()
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
        
            public int GetLength()
            {
                int size = 0;
                size += camera_info.GetLength();
                return size;
            }
        }

        public sealed class Response : IResponse
        {
            public bool success; // True if the call succeeded
            public string status_message; // Used to give details about success
        
            /// <summary> Constructor for empty message. </summary>
            public Response()
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
        
            public int GetLength()
            {
                int size = 5;
                size += status_message.Length;
                return size;
            }
        }
        
        /// <summary> Full ROS name of this service. </summary>
        public const string _ServiceType = "sensor_msgs/SetCameraInfo";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string _Md5Sum = "bef1df590ed75ed1f393692395e15482";
        
        /// <summary> Request message. </summary>
        public readonly Request request;
        
        /// <summary> Response message. </summary>
        public Response response;
        
        /// <summary> Empty constructor. </summary>
        public SetCameraInfo()
        {
            request = new Request();
            response = new Response();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetCameraInfo(Request request)
        {
            this.request = request;
            response = new Response();
        }
        
        public IService Create() => new SetCameraInfo();
        
        IRequest IService.Request => request;
        
        IResponse IService.Response => response;
        
        public string ErrorMessage { get; set; }
    }

}
