using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetCaptureResolutions : IService
    {
        /// Request message.
        [DataMember] public GetCaptureResolutionsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetCaptureResolutionsResponse Response { get; set; }
        
        /// Empty constructor.
        public GetCaptureResolutions()
        {
            Request = GetCaptureResolutionsRequest.Singleton;
            Response = new GetCaptureResolutionsResponse();
        }
        
        /// Setter constructor.
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/GetCaptureResolutions";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "e375c70c9e7caf58991e78dd0f791c3a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetCaptureResolutionsRequest : IRequest<GetCaptureResolutions, GetCaptureResolutionsResponse>, IDeserializable<GetCaptureResolutionsRequest>
    {
    
        /// Constructor for empty message.
        public GetCaptureResolutionsRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetCaptureResolutionsRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetCaptureResolutionsRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly GetCaptureResolutionsRequest Singleton = new GetCaptureResolutionsRequest();
    
        public void RosSerialize(ref WriteBuffer b)
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
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
        [DataMember (Name = "resolutions")] public Vector2i[] Resolutions;
    
        /// Constructor for empty message.
        public GetCaptureResolutionsResponse()
        {
            Message = string.Empty;
            Resolutions = System.Array.Empty<Vector2i>();
        }
        
        /// Explicit constructor.
        public GetCaptureResolutionsResponse(bool Success, string Message, Vector2i[] Resolutions)
        {
            this.Success = Success;
            this.Message = Message;
            this.Resolutions = Resolutions;
        }
        
        /// Constructor with buffer.
        public GetCaptureResolutionsResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Resolutions = b.DeserializeArray<Vector2i>();
            for (int i = 0; i < Resolutions.Length; i++)
            {
                Resolutions[i] = new Vector2i(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetCaptureResolutionsResponse(ref b);
        
        public GetCaptureResolutionsResponse RosDeserialize(ref ReadBuffer b) => new GetCaptureResolutionsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.SerializeArray(Resolutions);
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
    
        public int RosMessageLength => 9 + BuiltIns.GetStringSize(Message) + 8 * Resolutions.Length;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
