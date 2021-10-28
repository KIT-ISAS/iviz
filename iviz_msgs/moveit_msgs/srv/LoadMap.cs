using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/LoadMap")]
    public sealed class LoadMap : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public LoadMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public LoadMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public LoadMap()
        {
            Request = new LoadMapRequest();
            Response = new LoadMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/LoadMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "93a4bc4c60dc17e2a69e3fcaaa25d69d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LoadMapRequest : IRequest<LoadMap, LoadMapResponse>, IDeserializable<LoadMapRequest>
    {
        [DataMember (Name = "filename")] public string Filename;
    
        /// <summary> Constructor for empty message. </summary>
        public LoadMapRequest()
        {
            Filename = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public LoadMapRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LoadMapRequest(ref Buffer b)
        {
            Filename = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LoadMapRequest(ref b);
        }
        
        LoadMapRequest IDeserializable<LoadMapRequest>.RosDeserialize(ref Buffer b)
        {
            return new LoadMapRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Filename);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Constructor for empty message. </summary>
        public LoadMapResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public LoadMapResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LoadMapResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LoadMapResponse(ref b);
        }
        
        LoadMapResponse IDeserializable<LoadMapResponse>.RosDeserialize(ref Buffer b)
        {
            return new LoadMapResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void Dispose()
        {
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
