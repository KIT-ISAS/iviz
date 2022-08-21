using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class CaptureScreenshot : IService
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
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Compress
            return c;
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
            Data = System.Array.Empty<byte>();
        }
        
        public CaptureScreenshotResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Width);
            b.Deserialize(out Height);
            b.Deserialize(out Bpp);
            unsafe
            {
                Intrinsics = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref Intrinsics[0]), 9 * 8);
            }
            b.Deserialize(out Pose);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Data = n == 0
                    ? System.Array.Empty<byte>()
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Data[0]), n * 1);
                }
            }
        }
        
        public CaptureScreenshotResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            b.DeserializeString(out Message);
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Width);
            b.Deserialize(out Height);
            b.Deserialize(out Bpp);
            b.Align8();
            unsafe
            {
                Intrinsics = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref Intrinsics[0]), 9 * 8);
            }
            b.Deserialize(out Pose);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Data = n == 0
                    ? System.Array.Empty<byte>()
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Data[0]), n * 1);
                }
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
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            Header.RosSerialize(ref b);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(Bpp);
            b.SerializeStructArray(Intrinsics, 9);
            b.Serialize(in Pose);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
            if (Intrinsics is null) BuiltIns.ThrowNullReference();
            if (Intrinsics.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(Intrinsics.Length, 9);
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 149;
                size += WriteBuffer.GetStringSize(Message);
                size += Header.RosMessageLength;
                size += Data.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Message);
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Width
            c += 4; // Height
            c += 4; // Bpp
            c = WriteBuffer2.Align8(c);
            c += 8 * 9; // Intrinsics
            c += 56; // Pose
            c += 4; // Data length
            c += 1 * Data.Length;
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
