using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract]
    public sealed class GetLoggers : IService
    {
        /// Request message.
        [DataMember] public GetLoggersRequest Request;
        
        /// Response message.
        [DataMember] public GetLoggersResponse Response;
        
        /// Empty constructor.
        public GetLoggers()
        {
            Request = GetLoggersRequest.Singleton;
            Response = new GetLoggersResponse();
        }
        
        /// Setter constructor.
        public GetLoggers(GetLoggersRequest request)
        {
            Request = request;
            Response = new GetLoggersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetLoggersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetLoggersResponse)value;
        }
        
        public const string ServiceType = "roscpp/GetLoggers";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "32e97e85527d4678a8f9279894bb64b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLoggersRequest : IRequest<GetLoggers, GetLoggersResponse>, IDeserializable<GetLoggersRequest>
    {
    
        public GetLoggersRequest()
        {
        }
        
        public GetLoggersRequest(ref ReadBuffer b)
        {
        }
        
        public GetLoggersRequest(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetLoggersRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetLoggersRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetLoggersRequest? singleton;
        public static GetLoggersRequest Singleton => singleton ??= new GetLoggersRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int c) { }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLoggersResponse : IResponse, IDeserializable<GetLoggersResponse>
    {
        [DataMember (Name = "loggers")] public Logger[] Loggers;
    
        public GetLoggersResponse()
        {
            Loggers = System.Array.Empty<Logger>();
        }
        
        public GetLoggersResponse(Logger[] Loggers)
        {
            this.Loggers = Loggers;
        }
        
        public GetLoggersResponse(ref ReadBuffer b)
        {
            b.DeserializeArray(out Loggers);
            for (int i = 0; i < Loggers.Length; i++)
            {
                Loggers[i] = new Logger(ref b);
            }
        }
        
        public GetLoggersResponse(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Loggers);
            for (int i = 0; i < Loggers.Length; i++)
            {
                Loggers[i] = new Logger(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GetLoggersResponse(ref b);
        
        public GetLoggersResponse RosDeserialize(ref ReadBuffer b) => new GetLoggersResponse(ref b);
        
        public GetLoggersResponse RosDeserialize(ref ReadBuffer2 b) => new GetLoggersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Loggers);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Loggers);
        }
        
        public void RosValidate()
        {
            if (Loggers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Loggers.Length; i++)
            {
                if (Loggers[i] is null) BuiltIns.ThrowNullReference(nameof(Loggers), i);
                Loggers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Loggers);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Loggers);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
