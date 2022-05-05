using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "moveit_msgs/LoadMap";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "93a4bc4c60dc17e2a69e3fcaaa25d69d";
        
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
            b.DeserializeString(out Filename);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LoadMapRequest(ref b);
        
        public LoadMapRequest RosDeserialize(ref ReadBuffer b) => new LoadMapRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Filename);
        }
        
        public void RosValidate()
        {
            if (Filename is null) BuiltIns.ThrowNullReference();
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
            b.Deserialize(out Success);
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
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
