using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetObjectInformation : IService
    {
        /// Request message.
        [DataMember] public GetObjectInformationRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetObjectInformationResponse Response { get; set; }
        
        /// Empty constructor.
        public GetObjectInformation()
        {
            Request = new GetObjectInformationRequest();
            Response = new GetObjectInformationResponse();
        }
        
        /// Setter constructor.
        public GetObjectInformation(GetObjectInformationRequest request)
        {
            Request = request;
            Response = new GetObjectInformationResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetObjectInformationRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetObjectInformationResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "object_recognition_msgs/GetObjectInformation";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "dd7d344324fd86c32836f4fe1bc7b322";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetObjectInformationRequest : IRequest<GetObjectInformation, GetObjectInformationResponse>, IDeserializable<GetObjectInformationRequest>
    {
        // Retrieve extra data from the DB for a given object
        // The type of the object to retrieve info from
        [DataMember (Name = "type")] public ObjectRecognitionMsgs.ObjectType Type;
    
        /// Constructor for empty message.
        public GetObjectInformationRequest()
        {
            Type = new ObjectRecognitionMsgs.ObjectType();
        }
        
        /// Explicit constructor.
        public GetObjectInformationRequest(ObjectRecognitionMsgs.ObjectType Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        public GetObjectInformationRequest(ref ReadBuffer b)
        {
            Type = new ObjectRecognitionMsgs.ObjectType(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetObjectInformationRequest(ref b);
        
        public GetObjectInformationRequest RosDeserialize(ref ReadBuffer b) => new GetObjectInformationRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Type.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Type is null) BuiltIns.ThrowNullReference();
            Type.RosValidate();
        }
    
        public int RosMessageLength => 0 + Type.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetObjectInformationResponse : IResponse, IDeserializable<GetObjectInformationResponse>
    {
        // Extra object info 
        [DataMember (Name = "information")] public ObjectRecognitionMsgs.ObjectInformation Information;
    
        /// Constructor for empty message.
        public GetObjectInformationResponse()
        {
            Information = new ObjectRecognitionMsgs.ObjectInformation();
        }
        
        /// Explicit constructor.
        public GetObjectInformationResponse(ObjectRecognitionMsgs.ObjectInformation Information)
        {
            this.Information = Information;
        }
        
        /// Constructor with buffer.
        public GetObjectInformationResponse(ref ReadBuffer b)
        {
            Information = new ObjectRecognitionMsgs.ObjectInformation(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetObjectInformationResponse(ref b);
        
        public GetObjectInformationResponse RosDeserialize(ref ReadBuffer b) => new GetObjectInformationResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Information.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Information is null) BuiltIns.ThrowNullReference();
            Information.RosValidate();
        }
    
        public int RosMessageLength => 0 + Information.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
