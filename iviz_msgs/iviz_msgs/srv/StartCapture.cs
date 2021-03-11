using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/StartCapture")]
    public sealed class StartCapture : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public StartCaptureRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public StartCaptureResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public StartCapture()
        {
            Request = new StartCaptureRequest();
            Response = new StartCaptureResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public StartCapture(StartCaptureRequest request)
        {
            Request = request;
            Response = new StartCaptureResponse();
        }
        
        IService IService.Create() => new StartCapture();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (StartCaptureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (StartCaptureResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/StartCapture";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "b242a981d2c0da9273f6509826da0ce2";
    }

    [DataContract]
    public sealed class StartCaptureRequest : IRequest<StartCapture, StartCaptureResponse>, IDeserializable<StartCaptureRequest>
    {
        [DataMember (Name = "resolution_x")] public int ResolutionX { get; set; }
        [DataMember (Name = "resolution_y")] public int ResolutionY { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public StartCaptureRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public StartCaptureRequest(int ResolutionX, int ResolutionY)
        {
            this.ResolutionX = ResolutionX;
            this.ResolutionY = ResolutionY;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public StartCaptureRequest(ref Buffer b)
        {
            ResolutionX = b.Deserialize<int>();
            ResolutionY = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new StartCaptureRequest(ref b);
        }
        
        StartCaptureRequest IDeserializable<StartCaptureRequest>.RosDeserialize(ref Buffer b)
        {
            return new StartCaptureRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ResolutionX);
            b.Serialize(ResolutionY);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class StartCaptureResponse : IResponse, IDeserializable<StartCaptureResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public StartCaptureResponse()
        {
            Message = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public StartCaptureResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public StartCaptureResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new StartCaptureResponse(ref b);
        }
        
        StartCaptureResponse IDeserializable<StartCaptureResponse>.RosDeserialize(ref Buffer b)
        {
            return new StartCaptureResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
