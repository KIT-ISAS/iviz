using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
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
        
        IService IService.Create() => new GetFramePose();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/GetFramePose";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "80ebc02508d7723ac1b22636270c4ba6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFramePoseRequest : IRequest<GetFramePose, GetFramePoseResponse>, IDeserializable<GetFramePoseRequest>
    {
        // Gets the absolute pose of a TF frame w.r.t. the map frame
        [DataMember (Name = "frames")] public string[] Frames; // Frame ids
    
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
        internal GetFramePoseRequest(ref Buffer b)
        {
            Frames = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetFramePoseRequest(ref b);
        
        GetFramePoseRequest IDeserializable<GetFramePoseRequest>.RosDeserialize(ref Buffer b) => new GetFramePoseRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Frames, 0);
        }
        
        public void RosValidate()
        {
            if (Frames is null) throw new System.NullReferenceException(nameof(Frames));
            for (int i = 0; i < Frames.Length; i++)
            {
                if (Frames[i] is null) throw new System.NullReferenceException($"{nameof(Frames)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Frames);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFramePoseResponse : IResponse, IDeserializable<GetFramePoseResponse>
    {
        [DataMember (Name = "is_valid")] public bool[] IsValid; // Whether the entry is valid
        [DataMember (Name = "poses")] public GeometryMsgs.Pose[] Poses; // The absolute poses
    
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
        internal GetFramePoseResponse(ref Buffer b)
        {
            IsValid = b.DeserializeStructArray<bool>();
            Poses = b.DeserializeStructArray<GeometryMsgs.Pose>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetFramePoseResponse(ref b);
        
        GetFramePoseResponse IDeserializable<GetFramePoseResponse>.RosDeserialize(ref Buffer b) => new GetFramePoseResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(IsValid, 0);
            b.SerializeStructArray(Poses, 0);
        }
        
        public void RosValidate()
        {
            if (IsValid is null) throw new System.NullReferenceException(nameof(IsValid));
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
        }
    
        public int RosMessageLength => 8 + IsValid.Length + 56 * Poses.Length;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
