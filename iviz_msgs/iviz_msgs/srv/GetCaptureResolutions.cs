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
        [Preserve] public const string RosMd5Sum = "e375c70c9e7caf58991e78dd0f791c3a";
        
        public override string ToString() => Extensions.ToString(this);
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
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetCaptureResolutionsResponse : IResponse, IDeserializable<GetCaptureResolutionsResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
        [DataMember (Name = "resolutions")] public Vector2i[] Resolutions { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetCaptureResolutionsResponse()
        {
            Message = string.Empty;
            Resolutions = System.Array.Empty<Vector2i>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetCaptureResolutionsResponse(bool Success, string Message, Vector2i[] Resolutions)
        {
            this.Success = Success;
            this.Message = Message;
            this.Resolutions = Resolutions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetCaptureResolutionsResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Resolutions = b.DeserializeArray<Vector2i>();
            for (int i = 0; i < Resolutions.Length; i++)
            {
                Resolutions[i] = new Vector2i(ref b);
            }
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
            b.SerializeArray(Resolutions, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (Resolutions is null) throw new System.NullReferenceException(nameof(Resolutions));
            for (int i = 0; i < Resolutions.Length; i++)
            {
                if (Resolutions[i] is null) throw new System.NullReferenceException($"{nameof(Resolutions)}[{i}]");
                Resolutions[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 9;
                size += BuiltIns.UTF8.GetByteCount(Message);
                size += 8 * Resolutions.Length;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
