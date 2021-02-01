using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/SaveMap")]
    public sealed class SaveMap : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SaveMapRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SaveMapResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SaveMap()
        {
            Request = new SaveMapRequest();
            Response = new SaveMapResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/SaveMap";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "93a4bc4c60dc17e2a69e3fcaaa25d69d";
    }

    [DataContract]
    public sealed class SaveMapRequest : IRequest<SaveMap, SaveMapResponse>, IDeserializable<SaveMapRequest>
    {
        [DataMember (Name = "filename")] public string Filename { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SaveMapRequest()
        {
            Filename = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SaveMapRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SaveMapRequest(ref Buffer b)
        {
            Filename = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SaveMapRequest(ref b);
        }
        
        SaveMapRequest IDeserializable<SaveMapRequest>.RosDeserialize(ref Buffer b)
        {
            return new SaveMapRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Filename);
        }
        
        public void RosValidate()
        {
            if (Filename is null) throw new System.NullReferenceException(nameof(Filename));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Filename);
                return size;
            }
        }
    }

    [DataContract]
    public sealed class SaveMapResponse : IResponse, IDeserializable<SaveMapResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SaveMapResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SaveMapResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SaveMapResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SaveMapResponse(ref b);
        }
        
        SaveMapResponse IDeserializable<SaveMapResponse>.RosDeserialize(ref Buffer b)
        {
            return new SaveMapResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
