using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract (Name = "pcl_msgs/UpdateFilename")]
    public sealed class UpdateFilename : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public UpdateFilenameRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public UpdateFilenameResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public UpdateFilename()
        {
            Request = new UpdateFilenameRequest();
            Response = new UpdateFilenameResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public UpdateFilename(UpdateFilenameRequest request)
        {
            Request = request;
            Response = new UpdateFilenameResponse();
        }
        
        IService IService.Create() => new UpdateFilename();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (UpdateFilenameRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (UpdateFilenameResponse)value;
        }
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "pcl_msgs/UpdateFilename";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "93a4bc4c60dc17e2a69e3fcaaa25d69d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateFilenameRequest : IRequest<UpdateFilename, UpdateFilenameResponse>, IDeserializable<UpdateFilenameRequest>
    {
        [DataMember (Name = "filename")] public string Filename;
    
        /// <summary> Constructor for empty message. </summary>
        public UpdateFilenameRequest()
        {
            Filename = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public UpdateFilenameRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UpdateFilenameRequest(ref Buffer b)
        {
            Filename = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UpdateFilenameRequest(ref b);
        }
        
        UpdateFilenameRequest IDeserializable<UpdateFilenameRequest>.RosDeserialize(ref Buffer b)
        {
            return new UpdateFilenameRequest(ref b);
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Filename);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateFilenameResponse : IResponse, IDeserializable<UpdateFilenameResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// <summary> Constructor for empty message. </summary>
        public UpdateFilenameResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public UpdateFilenameResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UpdateFilenameResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UpdateFilenameResponse(ref b);
        }
        
        UpdateFilenameResponse IDeserializable<UpdateFilenameResponse>.RosDeserialize(ref Buffer b)
        {
            return new UpdateFilenameResponse(ref b);
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
