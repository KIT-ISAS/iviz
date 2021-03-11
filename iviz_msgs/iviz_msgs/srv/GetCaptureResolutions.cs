using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/GetCaptureResolutions")]
    public sealed class GetCaptureResolutions : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetCaptureResolutionsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetCaptureResolutionsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetCaptureResolutions()
        {
            Request = GetCaptureResolutionsRequest.Singleton;
            Response = new GetCaptureResolutionsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetCaptureResolutions(GetCaptureResolutionsRequest request)
        {
            Request = request;
            Response = new GetCaptureResolutionsResponse();
        }
        
        IService IService.Create() => new GetCaptureResolutions();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetCaptureResolutionsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetCaptureResolutionsResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/GetCaptureResolutions";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "c1a4ef517e74fbd385d235c2bacbd3b7";
    }

    [DataContract]
    public sealed class GetCaptureResolutionsRequest : IRequest<GetCaptureResolutions, GetCaptureResolutionsResponse>, IDeserializable<GetCaptureResolutionsRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public GetCaptureResolutionsRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetCaptureResolutionsRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        GetCaptureResolutionsRequest IDeserializable<GetCaptureResolutionsRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly GetCaptureResolutionsRequest Singleton = new GetCaptureResolutionsRequest();
    
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
    }

    [DataContract]
    public sealed class GetCaptureResolutionsResponse : IResponse, IDeserializable<GetCaptureResolutionsResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
        [DataMember (Name = "resolutions_xy")] public int[] ResolutionsXy { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetCaptureResolutionsResponse()
        {
            Message = string.Empty;
            ResolutionsXy = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetCaptureResolutionsResponse(bool Success, string Message, int[] ResolutionsXy)
        {
            this.Success = Success;
            this.Message = Message;
            this.ResolutionsXy = ResolutionsXy;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetCaptureResolutionsResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            ResolutionsXy = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetCaptureResolutionsResponse(ref b);
        }
        
        GetCaptureResolutionsResponse IDeserializable<GetCaptureResolutionsResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetCaptureResolutionsResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.SerializeStructArray(ResolutionsXy, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (ResolutionsXy is null) throw new System.NullReferenceException(nameof(ResolutionsXy));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += BuiltIns.UTF8.GetByteCount(Message);
                size += 4 * ResolutionsXy.Length;
                return size;
            }
        }
    }
}
