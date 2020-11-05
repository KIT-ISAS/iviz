using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/MessageDetails")]
    public sealed class MessageDetails : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public MessageDetailsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public MessageDetailsResponse Response { get; set; }
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/MessageDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
    }

    public sealed class MessageDetailsRequest : IRequest, IDeserializable<MessageDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MessageDetailsRequest()
        {
            Type = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public MessageDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MessageDetailsRequest(ref Buffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MessageDetailsRequest(ref b);
        }
        
        MessageDetailsRequest IDeserializable<MessageDetailsRequest>.RosDeserialize(ref Buffer b)
        {
            return new MessageDetailsRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Type);
                return size;
            }
        }
    }

    public sealed class MessageDetailsResponse : IResponse, IDeserializable<MessageDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MessageDetailsResponse()
        {
            Typedefs = System.Array.Empty<TypeDef>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MessageDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MessageDetailsResponse(ref Buffer b)
        {
            Typedefs = b.DeserializeArray<TypeDef>();
            for (int i = 0; i < Typedefs.Length; i++)
            {
                Typedefs[i] = new TypeDef(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MessageDetailsResponse(ref b);
        }
        
        MessageDetailsResponse IDeserializable<MessageDetailsResponse>.RosDeserialize(ref Buffer b)
        {
            return new MessageDetailsResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Typedefs, 0);
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Typedefs)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    }
}
