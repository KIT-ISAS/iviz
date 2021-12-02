using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class CaptureScreenshot : IService
    {
        /// Request message.
        [DataMember] public CaptureScreenshotRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public CaptureScreenshotResponse Response { get; set; }
        
        /// Empty constructor.
        public CaptureScreenshot()
        {
            Request = new CaptureScreenshotRequest();
            Response = new CaptureScreenshotResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/CaptureScreenshot";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "3846b8955f5006a6c3a2585f806a8d1c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class CaptureScreenshotRequest : IRequest<CaptureScreenshot, CaptureScreenshotResponse>, IDeserializable<CaptureScreenshotRequest>
    {
        [DataMember (Name = "compress")] public bool Compress;
    
        /// Constructor for empty message.
        public CaptureScreenshotRequest()
        {
        }
        
        /// Explicit constructor.
        public CaptureScreenshotRequest(bool Compress)
        {
            this.Compress = Compress;
        }
        
        /// Constructor with buffer.
        internal CaptureScreenshotRequest(ref Buffer b)
        {
            Compress = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new CaptureScreenshotRequest(ref b);
        
        CaptureScreenshotRequest IDeserializable<CaptureScreenshotRequest>.RosDeserialize(ref Buffer b) => new CaptureScreenshotRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Compress);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class CaptureScreenshotResponse : IResponse, IDeserializable<CaptureScreenshotResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "width")] public int Width;
        [DataMember (Name = "height")] public int Height;
        [DataMember (Name = "bpp")] public int Bpp;
        [DataMember (Name = "intrinsics")] public double[/*9*/] Intrinsics;
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "data")] public byte[] Data;
    
        /// Constructor for empty message.
        public CaptureScreenshotResponse()
        {
            Message = string.Empty;
            Intrinsics = new double[9];
            Data = System.Array.Empty<byte>();
        }
        
        /// Explicit constructor.
        public CaptureScreenshotResponse(bool Success, string Message, in StdMsgs.Header Header, int Width, int Height, int Bpp, double[] Intrinsics, in GeometryMsgs.Pose Pose, byte[] Data)
        {
            this.Success = Success;
            this.Message = Message;
            this.Header = Header;
            this.Width = Width;
            this.Height = Height;
            this.Bpp = Bpp;
            this.Intrinsics = Intrinsics;
            this.Pose = Pose;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal CaptureScreenshotResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            StdMsgs.Header.Deserialize(ref b, out Header);
            Width = b.Deserialize<int>();
            Height = b.Deserialize<int>();
            Bpp = b.Deserialize<int>();
            Intrinsics = b.DeserializeStructArray<double>(9);
            b.Deserialize(out Pose);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new CaptureScreenshotResponse(ref b);
        
        CaptureScreenshotResponse IDeserializable<CaptureScreenshotResponse>.RosDeserialize(ref Buffer b) => new CaptureScreenshotResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            Header.RosSerialize(ref b);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(Bpp);
            b.SerializeStructArray(Intrinsics, 9);
            b.Serialize(ref Pose);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (Intrinsics is null) throw new System.NullReferenceException(nameof(Intrinsics));
            if (Intrinsics.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(Intrinsics), Intrinsics.Length, 9);
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 149;
                size += BuiltIns.GetStringSize(Message);
                size += Header.RosMessageLength;
                size += Data.Length;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
