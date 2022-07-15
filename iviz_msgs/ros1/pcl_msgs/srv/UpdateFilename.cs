using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class UpdateFilename : IService
    {
        /// Request message.
        [DataMember] public UpdateFilenameRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public UpdateFilenameResponse Response { get; set; }
        
        /// Empty constructor.
        public UpdateFilename()
        {
            Request = new UpdateFilenameRequest();
            Response = new UpdateFilenameResponse();
        }
        
        /// Setter constructor.
        public UpdateFilename(UpdateFilenameRequest request)
        {
            Request = request;
            Response = new UpdateFilenameResponse();
        }
        
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
        
        public const string ServiceType = "pcl_msgs/UpdateFilename";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "93a4bc4c60dc17e2a69e3fcaaa25d69d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class UpdateFilenameRequest : IRequest<UpdateFilename, UpdateFilenameResponse>, IDeserializable<UpdateFilenameRequest>
    {
        [DataMember (Name = "filename")] public string Filename;
    
        /// Constructor for empty message.
        public UpdateFilenameRequest()
        {
            Filename = "";
        }
        
        /// Explicit constructor.
        public UpdateFilenameRequest(string Filename)
        {
            this.Filename = Filename;
        }
        
        /// Constructor with buffer.
        public UpdateFilenameRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Filename);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UpdateFilenameRequest(ref b);
        
        public UpdateFilenameRequest RosDeserialize(ref ReadBuffer b) => new UpdateFilenameRequest(ref b);
    
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
    public sealed class UpdateFilenameResponse : IResponse, IDeserializable<UpdateFilenameResponse>
    {
        [DataMember (Name = "success")] public bool Success;
    
        /// Constructor for empty message.
        public UpdateFilenameResponse()
        {
        }
        
        /// Explicit constructor.
        public UpdateFilenameResponse(bool Success)
        {
            this.Success = Success;
        }
        
        /// Constructor with buffer.
        public UpdateFilenameResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UpdateFilenameResponse(ref b);
        
        public UpdateFilenameResponse RosDeserialize(ref ReadBuffer b) => new UpdateFilenameResponse(ref b);
    
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
