using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class SaveMap : IService
    {
        /// Request message.
        [DataMember] public SaveMapRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SaveMapResponse Response { get; set; }
        
        /// Empty constructor.
        public SaveMap()
        {
            Request = new SaveMapRequest();
            Response = new SaveMapResponse();
        }
        
        /// Setter constructor.
        public SaveMap(SaveMapRequest request)
        {
            Request = request;
            Response = new SaveMapResponse();
        }
        
        IService IService.Create() => new SaveMap();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SaveMapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SaveMapResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/SaveMap";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "93a4bc4c60dc17e2a69e3fcaaa25d69d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SaveMapRequest : IRequest<SaveMap, SaveMapResponse>, IDeserializable<SaveMapRequest>
    {
        [DataMember (Name = "filename")] public string Filename;
    
        /// Constructor for empty message.
        public SaveMapRequest()
        {
            Filename = string.Empty;
        }
        
        /// Explicit constructor.
        public SaveMapRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        /// Constructor with buffer.
        internal SaveMapRequest(ref Buffer b)
        {
            Filename = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SaveMapRequest(ref b);
        
        SaveMapRequest IDeserializable<SaveMapRequest>.RosDeserialize(ref Buffer b) => new SaveMapRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Filename);
        }
        
        public void RosValidate()
        {
            if (Filename is null) throw new System.NullReferenceException(nameof(Filename));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Filename);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SaveMapResponse : IResponse, IDeserializable<SaveMapResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public SaveMapResponse()
        {
        }
        
        /// Explicit constructor.
        public SaveMapResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        internal SaveMapResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SaveMapResponse(ref b);
        
        SaveMapResponse IDeserializable<SaveMapResponse>.RosDeserialize(ref Buffer b) => new SaveMapResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
