using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class MessageDetails : IService
    {
        /// <summary> Request message. </summary>
        public MessageDetailsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public MessageDetailsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public MessageDetails()
        {
            Request = new MessageDetailsRequest();
            Response = new MessageDetailsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public MessageDetails(MessageDetailsRequest request)
        {
            Request = request;
            Response = new MessageDetailsResponse();
        }
        
        public IService Create() => new MessageDetails();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (MessageDetailsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (MessageDetailsResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/MessageDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
    }

    public sealed class MessageDetailsRequest : IRequest
    {
        public string type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MessageDetailsRequest()
        {
            type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public MessageDetailsRequest(string type)
        {
            this.type = type ?? throw new System.ArgumentNullException(nameof(type));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MessageDetailsRequest(Buffer b)
        {
            this.type = BuiltIns.DeserializeString(b);
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MessageDetailsRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.type, b);
        }
        
        public void Validate()
        {
            if (type is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(type);
                return size;
            }
        }
    }

    public sealed class MessageDetailsResponse : IResponse
    {
        public TypeDef[] typedefs { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MessageDetailsResponse()
        {
            typedefs = System.Array.Empty<TypeDef>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MessageDetailsResponse(TypeDef[] typedefs)
        {
            this.typedefs = typedefs ?? throw new System.ArgumentNullException(nameof(typedefs));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MessageDetailsResponse(Buffer b)
        {
            this.typedefs = BuiltIns.DeserializeArray<TypeDef>(b, 0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MessageDetailsResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.SerializeArray(this.typedefs, b, 0);
        }
        
        public void Validate()
        {
            if (typedefs is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                for (int i = 0; i < typedefs.Length; i++)
                {
                    size += typedefs[i].RosMessageLength;
                }
                return size;
            }
        }
    }
}
