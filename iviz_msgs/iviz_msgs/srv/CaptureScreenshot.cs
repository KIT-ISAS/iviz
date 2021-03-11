using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/CaptureScreenshot")]
    public sealed class CaptureScreenshot : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public CaptureScreenshotRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public CaptureScreenshotResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public CaptureScreenshot()
        {
            Request = new CaptureScreenshotRequest();
            Response = new CaptureScreenshotResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public CaptureScreenshot(CaptureScreenshotRequest request)
        {
            Request = request;
            Response = new CaptureScreenshotResponse();
        }
        
        IService IService.Create() => new CaptureScreenshot();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (CaptureScreenshotRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (CaptureScreenshotResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/CaptureScreenshot";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "4bff2d0367d9773d843a6d061102db78";
    }

    [DataContract]
    public sealed class CaptureScreenshotRequest : IRequest<CaptureScreenshot, CaptureScreenshotResponse>, IDeserializable<CaptureScreenshotRequest>
    {
        [DataMember (Name = "compress")] public bool Compress { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public CaptureScreenshotRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public CaptureScreenshotRequest(bool Compress)
        {
            this.Compress = Compress;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public CaptureScreenshotRequest(ref Buffer b)
        {
            Compress = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CaptureScreenshotRequest(ref b);
        }
        
        CaptureScreenshotRequest IDeserializable<CaptureScreenshotRequest>.RosDeserialize(ref Buffer b)
        {
            return new CaptureScreenshotRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Compress);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class CaptureScreenshotResponse : IResponse, IDeserializable<CaptureScreenshotResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
        [DataMember (Name = "width")] public int Width { get; set; }
        [DataMember (Name = "height")] public int Height { get; set; }
        [DataMember (Name = "bpp")] public int Bpp { get; set; }
        [DataMember (Name = "intrinsics")] public double[] Intrinsics { get; set; }
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        [DataMember (Name = "data")] public byte[] Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public CaptureScreenshotResponse()
        {
            Message = string.Empty;
            Intrinsics = System.Array.Empty<double>();
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public CaptureScreenshotResponse(bool Success, string Message, int Width, int Height, int Bpp, double[] Intrinsics, in GeometryMsgs.Pose Pose, byte[] Data)
        {
            this.Success = Success;
            this.Message = Message;
            this.Width = Width;
            this.Height = Height;
            this.Bpp = Bpp;
            this.Intrinsics = Intrinsics;
            this.Pose = Pose;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public CaptureScreenshotResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Width = b.Deserialize<int>();
            Height = b.Deserialize<int>();
            Bpp = b.Deserialize<int>();
            Intrinsics = b.DeserializeStructArray<double>();
            Pose = new GeometryMsgs.Pose(ref b);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CaptureScreenshotResponse(ref b);
        }
        
        CaptureScreenshotResponse IDeserializable<CaptureScreenshotResponse>.RosDeserialize(ref Buffer b)
        {
            return new CaptureScreenshotResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(Bpp);
            b.SerializeStructArray(Intrinsics, 0);
            Pose.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (Intrinsics is null) throw new System.NullReferenceException(nameof(Intrinsics));
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 81;
                size += BuiltIns.UTF8.GetByteCount(Message);
                size += 8 * Intrinsics.Length;
                size += 1 * Data.Length;
                return size;
            }
        }
    }
}
