using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServiceType_ : IService<ServiceTypeRequest, ServiceTypeResponse>
    {
        /// Request message.
        [DataMember] public ServiceTypeRequest Request;
        
        /// Response message.
        [DataMember] public ServiceTypeResponse Response;
        
        /// Empty constructor.
        public ServiceType_()
        {
            Request = new ServiceTypeRequest();
            Response = new ServiceTypeResponse();
        }
        
        /// Setter constructor.
        public ServiceType_(ServiceTypeRequest request)
        {
            Request = request;
            Response = new ServiceTypeResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceTypeRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceTypeResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/ServiceType";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "0e24a2dcdf70e483afc092a35a1f15f7";
        
        public IService Generate() => new ServiceType_();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceTypeRequest : IRequest<ServiceType_, ServiceTypeResponse>, IDeserializable<ServiceTypeRequest>
    {
        [DataMember (Name = "service")] public string Service;
    
        public ServiceTypeRequest()
        {
            Service = "";
        }
        
        public ServiceTypeRequest(string Service)
        {
            this.Service = Service;
        }
        
        public ServiceTypeRequest(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ServiceTypeRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Service = b.DeserializeString();
        }
        
        public ServiceTypeRequest RosDeserialize(ref ReadBuffer b) => new ServiceTypeRequest(ref b);
        
        public ServiceTypeRequest RosDeserialize(ref ReadBuffer2 b) => new ServiceTypeRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Service);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Service);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Service, nameof(Service));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Service);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Service);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceTypeResponse : IResponse, IDeserializable<ServiceTypeResponse>
    {
        [DataMember (Name = "type")] public string Type;
    
        public ServiceTypeResponse()
        {
            Type = "";
        }
        
        public ServiceTypeResponse(string Type)
        {
            this.Type = Type;
        }
        
        public ServiceTypeResponse(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ServiceTypeResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public ServiceTypeResponse RosDeserialize(ref ReadBuffer b) => new ServiceTypeResponse(ref b);
        
        public ServiceTypeResponse RosDeserialize(ref ReadBuffer2 b) => new ServiceTypeResponse(ref b);
    
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
}
