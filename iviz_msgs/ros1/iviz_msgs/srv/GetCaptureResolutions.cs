using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "iviz_msgs/GetCaptureResolutions";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e375c70c9e7caf58991e78dd0f791c3a";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetCaptureResolutionsRequest : IRequest<GetCaptureResolutions, GetCaptureResolutionsResponse>, IDeserializable<GetCaptureResolutionsRequest>
    {
    
        public GetCaptureResolutionsRequest()
        {
        }
        
        public GetCaptureResolutionsRequest(ref ReadBuffer b)
        {
        }
        
        public GetCaptureResolutionsRequest(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetCaptureResolutionsRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetCaptureResolutionsRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetCaptureResolutionsRequest? singleton;
        public static GetCaptureResolutionsRequest Singleton => singleton ??= new GetCaptureResolutionsRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int c) { }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetCaptureResolutionsResponse : IResponse, IDeserializable<GetCaptureResolutionsResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
        [DataMember (Name = "resolutions")] public Vector2i[] Resolutions;
    
        public GetCaptureResolutionsResponse()
        {
            Message = "";
            Resolutions = System.Array.Empty<Vector2i>();
        }
        
        public GetCaptureResolutionsResponse(bool Success, string Message, Vector2i[] Resolutions)
        {
            this.Success = Success;
            this.Message = Message;
            this.Resolutions = Resolutions;
        }
        
        public GetCaptureResolutionsResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
            b.DeserializeArray(out Resolutions);
            for (int i = 0; i < Resolutions.Length; i++)
            {
                Resolutions[i] = new Vector2i(ref b);
            }
        }
        
        public GetCaptureResolutionsResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
            b.DeserializeArray(out Resolutions);
            for (int i = 0; i < Resolutions.Length; i++)
            {
                Resolutions[i] = new Vector2i(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetCaptureResolutionsResponse(ref b);
        
        public GetCaptureResolutionsResponse RosDeserialize(ref ReadBuffer b) => new GetCaptureResolutionsResponse(ref b);
        
        public GetCaptureResolutionsResponse RosDeserialize(ref ReadBuffer2 b) => new GetCaptureResolutionsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.SerializeArray(Resolutions);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.SerializeArray(Resolutions);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
            if (Resolutions is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Resolutions.Length; i++)
            {
                if (Resolutions[i] is null) BuiltIns.ThrowNullReference(nameof(Resolutions), i);
                Resolutions[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 9 + WriteBuffer.GetStringSize(Message) + 8 * Resolutions.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Success);
            WriteBuffer2.AddLength(ref c, Message);
            WriteBuffer2.AddLength(ref c, Resolutions);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
