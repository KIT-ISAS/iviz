using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class MessageDetails : IService<MessageDetailsRequest, MessageDetailsResponse>
    {
        /// Request message.
        [DataMember] public MessageDetailsRequest Request;
        
        /// Response message.
        [DataMember] public MessageDetailsResponse Response;
        
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
        
        public const string ServiceType = "rosapi_msgs/MessageDetails";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "f9c88144f6f6bd888dd99d4e0411905d";
        
        public IService Generate() => new MessageDetails();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class MessageDetailsRequest : IRequest<MessageDetails, MessageDetailsResponse>, IDeserializable<MessageDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        public MessageDetailsRequest()
        {
            Type = "";
        }
        
        public MessageDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        public MessageDetailsRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public MessageDetailsRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public MessageDetailsRequest RosDeserialize(ref ReadBuffer b) => new MessageDetailsRequest(ref b);
        
        public MessageDetailsRequest RosDeserialize(ref ReadBuffer2 b) => new MessageDetailsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Type);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Type, nameof(Type));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Type);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Type);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class MessageDetailsResponse : IResponse, IDeserializable<MessageDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs;
    
        public MessageDetailsResponse()
        {
            Typedefs = EmptyArray<TypeDef>.Value;
        }
        
        public MessageDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        public MessageDetailsResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                TypeDef[] array;
                if (n == 0) array = EmptyArray<TypeDef>.Value;
                else
                {
                    array = new TypeDef[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new TypeDef(ref b);
                    }
                }
                Typedefs = array;
            }
        }
        
        public MessageDetailsResponse(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                TypeDef[] array;
                if (n == 0) array = EmptyArray<TypeDef>.Value;
                else
                {
                    array = new TypeDef[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new TypeDef(ref b);
                    }
                }
                Typedefs = array;
            }
        }
        
        public MessageDetailsResponse RosDeserialize(ref ReadBuffer b) => new MessageDetailsResponse(ref b);
        
        public MessageDetailsResponse RosDeserialize(ref ReadBuffer2 b) => new MessageDetailsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Typedefs.Length);
            foreach (var t in Typedefs)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Typedefs.Length);
            foreach (var t in Typedefs)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Typedefs, nameof(Typedefs));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Typedefs) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Typedefs.Length
            foreach (var msg in Typedefs) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
