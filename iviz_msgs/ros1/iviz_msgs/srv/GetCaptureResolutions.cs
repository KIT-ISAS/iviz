using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetCaptureResolutions : IService
    {
        /// Request message.
        [DataMember] public GetCaptureResolutionsRequest Request;
        
        /// Response message.
        [DataMember] public GetCaptureResolutionsResponse Response;
        
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
        
        public int AddRos2MessageLength(int c) => c;
    
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
            Resolutions = EmptyArray<Vector2i>.Value;
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
            {
                int n = b.DeserializeArrayLength();
                Resolutions = n == 0
                    ? EmptyArray<Vector2i>.Value
                    : new Vector2i[n];
                for (int i = 0; i < n; i++)
                {
                    Resolutions[i] = new Vector2i(ref b);
                }
            }
        }
        
        public GetCaptureResolutionsResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            b.DeserializeString(out Message);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Resolutions = n == 0
                    ? EmptyArray<Vector2i>.Value
                    : new Vector2i[n];
                for (int i = 0; i < n; i++)
                {
                    Resolutions[i] = new Vector2i(ref b);
                }
            }
        }
        
        public GetCaptureResolutionsResponse RosDeserialize(ref ReadBuffer b) => new GetCaptureResolutionsResponse(ref b);
        
        public GetCaptureResolutionsResponse RosDeserialize(ref ReadBuffer2 b) => new GetCaptureResolutionsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Resolutions.Length);
            foreach (var t in Resolutions)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            b.Serialize(Resolutions.Length);
            foreach (var t in Resolutions)
            {
                t.RosSerialize(ref b);
            }
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Message);
            c = WriteBuffer2.Align4(c);
            c += 4; // Resolutions length
            c += 8 * Resolutions.Length;
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
