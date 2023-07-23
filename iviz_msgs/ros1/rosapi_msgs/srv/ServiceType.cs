using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServiceType_ : IService<ServiceType_Request, ServiceType_Response>
    {
        /// Request message.
        [DataMember] public ServiceType_Request Request;
        
        /// Response message.
        [DataMember] public ServiceType_Response Response;
        
        /// Empty constructor.
        public ServiceType_()
        {
            Request = new ServiceType_Request();
            Response = new ServiceType_Response();
        }
        
        /// Setter constructor.
        public ServiceType_(ServiceType_Request request)
        {
            Request = request;
            Response = new ServiceType_Response();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceType_Request)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceType_Response)value;
        }
        
        public const string ServiceType = "rosapi_msgs/ServiceType";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "0e24a2dcdf70e483afc092a35a1f15f7";
        
        public IService Generate() => new ServiceType_();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceType_Request : IRequest<ServiceType_, ServiceType_Response>, IDeserializable<ServiceType_Request>
    {
        [DataMember (Name = "service")] public string Service;
    
        public ServiceType_Request()
        {
            Service = "";
        }
        
        public ServiceType_Request(string Service)
        {
            this.Service = Service;
        }
        
        public ServiceType_Request(ref ReadBuffer b)
        {
            Service = b.DeserializeString();
        }
        
        public ServiceType_Request(ref ReadBuffer2 b)
        {
            b.Align4();
            Service = b.DeserializeString();
        }
        
        public ServiceType_Request RosDeserialize(ref ReadBuffer b) => new ServiceType_Request(ref b);
        
        public ServiceType_Request RosDeserialize(ref ReadBuffer2 b) => new ServiceType_Request(ref b);
    
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
    public sealed class ServiceType_Response : IResponse, IDeserializable<ServiceType_Response>
    {
        [DataMember (Name = "type")] public string Type;
    
        public ServiceType_Response()
        {
            Type = "";
        }
        
        public ServiceType_Response(string Type)
        {
            this.Type = Type;
        }
        
        public ServiceType_Response(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ServiceType_Response(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public ServiceType_Response RosDeserialize(ref ReadBuffer b) => new ServiceType_Response(ref b);
        
        public ServiceType_Response RosDeserialize(ref ReadBuffer2 b) => new ServiceType_Response(ref b);
    
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
