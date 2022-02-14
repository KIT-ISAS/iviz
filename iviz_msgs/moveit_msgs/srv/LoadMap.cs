using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class LoadMap : IService
    {
        /// Request message.
        [DataMember] public LoadMapRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public LoadMapResponse Response { get; set; }
        
        /// Empty constructor.
        public LoadMap()
        {
            Request = new LoadMapRequest();
            Response = new LoadMapResponse();
        }
        
        /// Setter constructor.
        public LoadMap(LoadMapRequest request)
        {
            Request = request;
            Response = new LoadMapResponse();
        }
        
        IService IService.Create() => new LoadMap();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (LoadMapRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (LoadMapResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/LoadMap";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "93a4bc4c60dc17e2a69e3fcaaa25d69d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LoadMapRequest : IRequest<LoadMap, LoadMapResponse>, IDeserializable<LoadMapRequest>
    {
        [DataMember (Name = "filename")] public string Filename;
    
        /// Constructor for empty message.
        public LoadMapRequest()
        {
            Filename = "";
        }
        
        /// Explicit constructor.
        public LoadMapRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        /// Constructor with buffer.
        public LoadMapRequest(ref ReadBuffer b)
        {
            Filename = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LoadMapRequest(ref b);
        
        public LoadMapRequest RosDeserialize(ref ReadBuffer b) => new LoadMapRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    public sealed class LoadMapResponse : IResponse, IDeserializable<LoadMapResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public LoadMapResponse()
        {
        }
        
        /// Explicit constructor.
        public LoadMapResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        public LoadMapResponse(ref ReadBuffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LoadMapResponse(ref b);
        
        public LoadMapResponse RosDeserialize(ref ReadBuffer b) => new LoadMapResponse(ref b);
    
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
