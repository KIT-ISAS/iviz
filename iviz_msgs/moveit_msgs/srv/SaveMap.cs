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
            Filename = "";
        }
        
        /// Explicit constructor.
        public SaveMapRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        /// Constructor with buffer.
        public SaveMapRequest(ref ReadBuffer b)
        {
            Filename = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SaveMapRequest(ref b);
        
        public SaveMapRequest RosDeserialize(ref ReadBuffer b) => new SaveMapRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Filename);
        }
        
        public void RosValidate()
        {
            if (Filename is null) BuiltIns.ThrowNullReference(nameof(Filename));
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
        public SaveMapResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SaveMapResponse(ref b);
        
        public SaveMapResponse RosDeserialize(ref ReadBuffer b) => new SaveMapResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
