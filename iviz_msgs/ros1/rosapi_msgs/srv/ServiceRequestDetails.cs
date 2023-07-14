using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServiceRequestDetails : IService<ServiceRequestDetailsRequest, ServiceRequestDetailsResponse>
    {
        /// Request message.
        [DataMember] public ServiceRequestDetailsRequest Request;
        
        /// Response message.
        [DataMember] public ServiceRequestDetailsResponse Response;
        
        /// Empty constructor.
        public ServiceRequestDetails()
        {
            Request = new ServiceRequestDetailsRequest();
            Response = new ServiceRequestDetailsResponse();
        }
        
        /// Setter constructor.
        public ServiceRequestDetails(ServiceRequestDetailsRequest request)
        {
            Request = request;
            Response = new ServiceRequestDetailsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceRequestDetailsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceRequestDetailsResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/ServiceRequestDetails";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "f9c88144f6f6bd888dd99d4e0411905d";
        
        public IService Generate() => new ServiceRequestDetails();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceRequestDetailsRequest : IRequest<ServiceRequestDetails, ServiceRequestDetailsResponse>, IDeserializable<ServiceRequestDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        public ServiceRequestDetailsRequest()
        {
            Type = "";
        }
        
        public ServiceRequestDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        public ServiceRequestDetailsRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ServiceRequestDetailsRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public ServiceRequestDetailsRequest RosDeserialize(ref ReadBuffer b) => new ServiceRequestDetailsRequest(ref b);
        
        public ServiceRequestDetailsRequest RosDeserialize(ref ReadBuffer2 b) => new ServiceRequestDetailsRequest(ref b);
    
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
    public sealed class ServiceRequestDetailsResponse : IResponse, IDeserializable<ServiceRequestDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs;
    
        public ServiceRequestDetailsResponse()
        {
            Typedefs = EmptyArray<TypeDef>.Value;
        }
        
        public ServiceRequestDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        public ServiceRequestDetailsResponse(ref ReadBuffer b)
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
        
        public ServiceRequestDetailsResponse(ref ReadBuffer2 b)
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
        
        public ServiceRequestDetailsResponse RosDeserialize(ref ReadBuffer b) => new ServiceRequestDetailsResponse(ref b);
        
        public ServiceRequestDetailsResponse RosDeserialize(ref ReadBuffer2 b) => new ServiceRequestDetailsResponse(ref b);
    
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
