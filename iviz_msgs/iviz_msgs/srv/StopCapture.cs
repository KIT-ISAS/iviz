using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/StopCapture")]
    public sealed class StopCapture : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public StopCaptureRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public StopCaptureResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public StopCapture()
        {
            Request = StopCaptureRequest.Singleton;
            Response = new StopCaptureResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public StopCapture(StopCaptureRequest request)
        {
            Request = request;
            Response = new StopCaptureResponse();
        }
        
        IService IService.Create() => new StopCapture();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (StopCaptureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (StopCaptureResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/StopCapture";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "937c9679a518e3a18d831e57125ea522";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StopCaptureRequest : IRequest<StopCapture, StopCaptureResponse>, IDeserializable<StopCaptureRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public StopCaptureRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public StopCaptureRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        StopCaptureRequest IDeserializable<StopCaptureRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly StopCaptureRequest Singleton = new StopCaptureRequest();
    
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
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class StopCaptureResponse : IResponse, IDeserializable<StopCaptureResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
    
        /// <summary> Constructor for empty message. </summary>
        public StopCaptureResponse()
        {
            Message = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public StopCaptureResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public StopCaptureResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new StopCaptureResponse(ref b);
        }
        
        StopCaptureResponse IDeserializable<StopCaptureResponse>.RosDeserialize(ref Buffer b)
        {
            return new StopCaptureResponse(ref b);
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
    
        public override string ToString() => Extensions.ToString(this);
    }
}
