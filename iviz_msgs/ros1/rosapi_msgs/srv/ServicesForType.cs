using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServicesForType : IService<ServicesForTypeRequest, ServicesForTypeResponse>
    {
        /// Request message.
        [DataMember] public ServicesForTypeRequest Request;
        
        /// Response message.
        [DataMember] public ServicesForTypeResponse Response;
        
        /// Empty constructor.
        public ServicesForType()
        {
            Request = new ServicesForTypeRequest();
            Response = new ServicesForTypeResponse();
        }
        
        /// Setter constructor.
        public ServicesForType(ServicesForTypeRequest request)
        {
            Request = request;
            Response = new ServicesForTypeResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServicesForTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServicesForTypeResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/ServicesForType";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "93e9fe8ae5a9136008e260fe510bd2b0";
        
        public IService Generate() => new ServicesForType();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServicesForTypeRequest : IRequest<ServicesForType, ServicesForTypeResponse>, IDeserializable<ServicesForTypeRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        public ServicesForTypeRequest()
        {
            Type = "";
        }
        
        public ServicesForTypeRequest(string Type)
        {
            this.Type = Type;
        }
        
        public ServicesForTypeRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ServicesForTypeRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public ServicesForTypeRequest RosDeserialize(ref ReadBuffer b) => new ServicesForTypeRequest(ref b);
        
        public ServicesForTypeRequest RosDeserialize(ref ReadBuffer2 b) => new ServicesForTypeRequest(ref b);
    
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
    public sealed class ServicesForTypeResponse : IResponse, IDeserializable<ServicesForTypeResponse>
    {
        [DataMember (Name = "services")] public string[] Services;
    
        public ServicesForTypeResponse()
        {
            Services = EmptyArray<string>.Value;
        }
        
        public ServicesForTypeResponse(string[] Services)
        {
            this.Services = Services;
        }
        
        public ServicesForTypeResponse(ref ReadBuffer b)
        {
            Services = b.DeserializeStringArray();
        }
        
        public ServicesForTypeResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Services = b.DeserializeStringArray();
        }
        
        public ServicesForTypeResponse RosDeserialize(ref ReadBuffer b) => new ServicesForTypeResponse(ref b);
        
        public ServicesForTypeResponse RosDeserialize(ref ReadBuffer2 b) => new ServicesForTypeResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Services.Length);
            b.SerializeArray(Services);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Services.Length);
            b.SerializeArray(Services);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Services, nameof(Services));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Services);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Services);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
