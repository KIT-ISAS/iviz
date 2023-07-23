using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class ServiceResponseDetails : IService<ServiceResponseDetailsRequest, ServiceResponseDetailsResponse>
    {
        /// Request message.
        [DataMember] public ServiceResponseDetailsRequest Request;
        
        /// Response message.
        [DataMember] public ServiceResponseDetailsResponse Response;
        
        /// Empty constructor.
        public ServiceResponseDetails()
        {
            Request = new ServiceResponseDetailsRequest();
            Response = new ServiceResponseDetailsResponse();
        }
        
        /// Setter constructor.
        public ServiceResponseDetails(ServiceResponseDetailsRequest request)
        {
            Request = request;
            Response = new ServiceResponseDetailsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ServiceResponseDetailsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ServiceResponseDetailsResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/ServiceResponseDetails";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "f9c88144f6f6bd888dd99d4e0411905d";
        
        public IService Generate() => new ServiceResponseDetails();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ServiceResponseDetailsRequest : IRequest<ServiceResponseDetails, ServiceResponseDetailsResponse>, IDeserializable<ServiceResponseDetailsRequest>
    {
        [DataMember (Name = "type")] public string Type;
    
        public ServiceResponseDetailsRequest()
        {
            Type = "";
        }
        
        public ServiceResponseDetailsRequest(string Type)
        {
            this.Type = Type;
        }
        
        public ServiceResponseDetailsRequest(ref ReadBuffer b)
        {
            Type = b.DeserializeString();
        }
        
        public ServiceResponseDetailsRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Type = b.DeserializeString();
        }
        
        public ServiceResponseDetailsRequest RosDeserialize(ref ReadBuffer b) => new ServiceResponseDetailsRequest(ref b);
        
        public ServiceResponseDetailsRequest RosDeserialize(ref ReadBuffer2 b) => new ServiceResponseDetailsRequest(ref b);
    
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

    [DataContract]
    public sealed class ServiceResponseDetailsResponse : IResponse, IDeserializable<ServiceResponseDetailsResponse>
    {
        [DataMember (Name = "typedefs")] public TypeDef[] Typedefs;
    
        public ServiceResponseDetailsResponse()
        {
            Typedefs = EmptyArray<TypeDef>.Value;
        }
        
        public ServiceResponseDetailsResponse(TypeDef[] Typedefs)
        {
            this.Typedefs = Typedefs;
        }
        
        public ServiceResponseDetailsResponse(ref ReadBuffer b)
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
        
        public ServiceResponseDetailsResponse(ref ReadBuffer2 b)
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
        
        public ServiceResponseDetailsResponse RosDeserialize(ref ReadBuffer b) => new ServiceResponseDetailsResponse(ref b);
        
        public ServiceResponseDetailsResponse RosDeserialize(ref ReadBuffer2 b) => new ServiceResponseDetailsResponse(ref b);
    
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
            foreach (var msg in Typedefs) msg.RosValidate();
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
