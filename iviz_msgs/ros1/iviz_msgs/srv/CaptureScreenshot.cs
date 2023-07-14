using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class CaptureScreenshot : IService<CaptureScreenshotRequest, CaptureScreenshotResponse>
    {
        /// Request message.
        [DataMember] public CaptureScreenshotRequest Request;
        
        /// Response message.
        [DataMember] public CaptureScreenshotResponse Response;
        
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
        
        public const string ServiceType = "iviz_msgs/CaptureScreenshot";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "3846b8955f5006a6c3a2585f806a8d1c";
        
        public IService Generate() => new CaptureScreenshot();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class CaptureScreenshotRequest : IRequest<CaptureScreenshot, CaptureScreenshotResponse>, IDeserializable<CaptureScreenshotRequest>
    {
        [DataMember (Name = "compress")] public bool Compress;
    
        public CaptureScreenshotRequest()
        {
        }
        
        public CaptureScreenshotRequest(bool Compress)
        {
            this.Compress = Compress;
        }
        
        public CaptureScreenshotRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Compress);
        }
        
        public CaptureScreenshotRequest(ref ReadBuffer2 b)
        {
            b.Deserialize(out Compress);
        }
        
        public CaptureScreenshotRequest RosDeserialize(ref ReadBuffer b) => new CaptureScreenshotRequest(ref b);
        
        public CaptureScreenshotRequest RosDeserialize(ref ReadBuffer2 b) => new CaptureScreenshotRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Compress);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Compress);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 1;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Compress
            return size;
        }
    
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
    
        public CaptureScreenshotResponse()
        {
            Message = "";
            Intrinsics = new double[9];
            Data = EmptyArray<byte>.Value;
        }
        
        public CaptureScreenshotResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Width);
            b.Deserialize(out Height);
            b.Deserialize(out Bpp);
            {
                var array = new double[9];
                b.DeserializeStructArray(array);
                Intrinsics = array;
            }
            b.Deserialize(out Pose);
            {
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                    array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public CaptureScreenshotResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            b.Deserialize(out Width);
            b.Deserialize(out Height);
            b.Deserialize(out Bpp);
            {
                b.Align8();
                var array = new double[9];
                b.DeserializeStructArray(array);
                Intrinsics = array;
            }
            b.Deserialize(out Pose);
            {
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                    array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public CaptureScreenshotResponse RosDeserialize(ref ReadBuffer b) => new CaptureScreenshotResponse(ref b);
        
        public CaptureScreenshotResponse RosDeserialize(ref ReadBuffer2 b) => new CaptureScreenshotResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            Header.RosSerialize(ref b);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(Bpp);
            b.SerializeStructArray(Intrinsics, 9);
            b.Serialize(in Pose);
            b.Serialize(Data.Length);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Align4();
            b.Serialize(Message);
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(Bpp);
            b.Align8();
            b.SerializeStructArray(Intrinsics, 9);
            b.Serialize(in Pose);
            b.Serialize(Data.Length);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Message, nameof(Message));
            BuiltIns.ThrowIfNull(Intrinsics, nameof(Intrinsics));
            BuiltIns.ThrowIfWrongSize(Intrinsics, nameof(Intrinsics), 9);
            BuiltIns.ThrowIfNull(Data, nameof(Data));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 149;
                size += WriteBuffer.GetStringSize(Message);
                size += Header.RosMessageLength;
                size += Data.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Success
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Width
            size += 4; // Height
            size += 4; // Bpp
            size = WriteBuffer2.Align8(size);
            size += 8 * 9; // Intrinsics
            size += 56; // Pose
            size += 4; // Data.Length
            size += 1 * Data.Length;
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
