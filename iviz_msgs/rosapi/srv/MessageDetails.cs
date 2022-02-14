using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class MessageDetails : IService
    {
        /// Request message.
        [DataMember] public MessageDetailsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public MessageDetailsResponse Response { get; set; }
        
        /// Empty constructor.
        public MessageDetails()
        {
            Request = new MessageDetailsRequest();
            Response = new MessageDetailsResponse();
        }
        
        /// Setter constructor.
        public MessageDetails(MessageDetailsRequest request)
        {
            Request = request;
            Response = new MessageDetailsResponse();
        }
        
        IService IService.Create() => new MessageDetails();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/MessageDetails";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class MessageDetailsRequest : IRequest<MessageDetails, MessageDetailsResponse>, IDeserializable<MessageDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        /// Constructor for empty message.
        public MessageDetailsRequest()
        {
            Type = "";
        }
        
        /// Explicit constructor.
        public MessageDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// Constructor with buffer.
        public MessageDetailsRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MessageDetailsRequest(ref b);
        
        public MessageDetailsRequest RosDeserialize(ref ReadBuffer b) => new MessageDetailsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetStringSize(Type);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class MessageDetailsResponse : IResponse, IDeserializable<MessageDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs;
    
        /// Constructor for empty message.
        public MessageDetailsResponse()
        {
            Typedefs = System.Array.Empty<TypeDef>();
        }
        
        /// Explicit constructor.
        public MessageDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        /// Constructor with buffer.
        public MessageDetailsResponse(ref ReadBuffer b)
        {
            Typedefs = b.DeserializeArray<TypeDef>();
            for (int i = 0; i < Typedefs.Length; i++)
            {
                Typedefs[i] = new TypeDef(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MessageDetailsResponse(ref b);
        
        public MessageDetailsResponse RosDeserialize(ref ReadBuffer b) => new MessageDetailsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Typedefs);
        }
        
        public void RosValidate()
        {
            if (Typedefs is null) throw new System.NullReferenceException(nameof(Typedefs));
            for (int i = 0; i < Typedefs.Length; i++)
            {
                if (Typedefs[i] is null) throw new System.NullReferenceException($"{nameof(Typedefs)}[{i}]");
                Typedefs[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Typedefs);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
