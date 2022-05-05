using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetFramePose : IService
    {
        /// Request message.
        [DataMember] public GetFramePoseRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetFramePoseResponse Response { get; set; }
        
        /// Empty constructor.
        public GetFramePose()
        {
            Request = new GetFramePoseRequest();
            Response = new GetFramePoseResponse();
        }
        
        /// Setter constructor.
        public GetFramePose(GetFramePoseRequest request)
        {
            Request = request;
            Response = new GetFramePoseResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetFramePoseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetFramePoseResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/GetFramePose";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "80ebc02508d7723ac1b22636270c4ba6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFramePoseRequest : IRequest<GetFramePose, GetFramePoseResponse>, IDeserializable<GetFramePoseRequest>
    {
        // Gets the absolute pose of a TF frame w.r.t. the map frame
        /// <summary> Frame ids </summary>
        [DataMember (Name = "frames")] public string[] Frames;
    
        /// Constructor for empty message.
        public GetFramePoseRequest()
        {
            Frames = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public GetFramePoseRequest(string[] Frames)
        {
            this.Frames = Frames;
        }
        
        /// Constructor with buffer.
        public GetFramePoseRequest(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Frames);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetFramePoseRequest(ref b);
        
        public GetFramePoseRequest RosDeserialize(ref ReadBuffer b) => new GetFramePoseRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Frames);
        }
        
        public void RosValidate()
        {
            if (Frames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Frames.Length; i++)
            {
                if (Frames[i] is null) BuiltIns.ThrowNullReference(nameof(Frames), i);
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Frames);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFramePoseResponse : IResponse, IDeserializable<GetFramePoseResponse>
    {
        /// <summary> Whether the entry is valid </summary>
        [DataMember (Name = "is_valid")] public bool[] IsValid;
        /// <summary> The absolute poses </summary>
        [DataMember (Name = "poses")] public GeometryMsgs.Pose[] Poses;
    
        /// Constructor for empty message.
        public GetFramePoseResponse()
        {
            IsValid = System.Array.Empty<bool>();
            Poses = System.Array.Empty<GeometryMsgs.Pose>();
        }
        
        /// Explicit constructor.
        public GetFramePoseResponse(bool[] IsValid, GeometryMsgs.Pose[] Poses)
        {
            this.IsValid = IsValid;
            this.Poses = Poses;
        }
        
        /// Constructor with buffer.
        public GetFramePoseResponse(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out IsValid);
            b.DeserializeStructArray(out Poses);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetFramePoseResponse(ref b);
        
        public GetFramePoseResponse RosDeserialize(ref ReadBuffer b) => new GetFramePoseResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(IsValid);
            b.SerializeStructArray(Poses);
        }
        
        public void RosValidate()
        {
            if (IsValid is null) BuiltIns.ThrowNullReference();
            if (Poses is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + IsValid.Length + 56 * Poses.Length;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
