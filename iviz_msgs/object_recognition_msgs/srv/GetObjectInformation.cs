using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [DataContract (Name = "object_recognition_msgs/GetObjectInformation")]
    public sealed class GetObjectInformation : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetObjectInformationRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetObjectInformationResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetObjectInformation()
        {
            Request = new GetObjectInformationRequest();
            Response = new GetObjectInformationResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetObjectInformation(GetObjectInformationRequest request)
        {
            Request = request;
            Response = new GetObjectInformationResponse();
        }
        
        IService IService.Create() => new GetObjectInformation();
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "object_recognition_msgs/GetObjectInformation";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "dd7d344324fd86c32836f4fe1bc7b322";
    }

    [DataContract]
    public sealed class GetObjectInformationRequest : IRequest<GetObjectInformation, GetObjectInformationResponse>, IDeserializable<GetObjectInformationRequest>
    {
        // Retrieve extra data from the DB for a given object
        // The type of the object to retrieve info from
        [DataMember (Name = "type")] public ObjectRecognitionMsgs.ObjectType Type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetObjectInformationRequest()
        {
            Type = new ObjectRecognitionMsgs.ObjectType();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetObjectInformationRequest(ObjectRecognitionMsgs.ObjectType Type)
        {
            this.Type = Type;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetObjectInformationRequest(ref Buffer b)
        {
            Type = new ObjectRecognitionMsgs.ObjectType(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetObjectInformationRequest(ref b);
        }
        
        GetObjectInformationRequest IDeserializable<GetObjectInformationRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Type.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
            Type.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Type.RosMessageLength;
                return size;
            }
        }
    }

    [DataContract]
    public sealed class GetObjectInformationResponse : IResponse, IDeserializable<GetObjectInformationResponse>
    {
        // Extra object info 
        [DataMember (Name = "information")] public ObjectRecognitionMsgs.ObjectInformation Information { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetObjectInformationResponse()
        {
            Information = new ObjectRecognitionMsgs.ObjectInformation();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetObjectInformationResponse(ObjectRecognitionMsgs.ObjectInformation Information)
        {
            this.Information = Information;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetObjectInformationResponse(ref Buffer b)
        {
            Information = new ObjectRecognitionMsgs.ObjectInformation(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetObjectInformationResponse(ref b);
        }
        
        GetObjectInformationResponse IDeserializable<GetObjectInformationResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Information.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Information is null) throw new System.NullReferenceException(nameof(Information));
            Information.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Information.RosMessageLength;
                return size;
            }
        }
    }
}
