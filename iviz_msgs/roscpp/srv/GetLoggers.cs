using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetLoggers : IService
    {
        /// Request message.
        [DataMember] public GetLoggersRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetLoggersResponse Response { get; set; }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "roscpp/GetLoggers";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "32e97e85527d4678a8f9279894bb64b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLoggersRequest : IRequest<GetLoggers, GetLoggersResponse>, IDeserializable<GetLoggersRequest>
    {
    
        /// Constructor for empty message.
        public GetLoggersRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetLoggersRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetLoggersRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly GetLoggersRequest Singleton = new GetLoggersRequest();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLoggersResponse : IResponse, IDeserializable<GetLoggersResponse>
    {
        [DataMember (Name = "loggers")] public Logger[] Loggers;
    
        /// Constructor for empty message.
        public GetLoggersResponse()
        {
            Loggers = System.Array.Empty<Logger>();
        }
        
        /// Explicit constructor.
        public GetLoggersResponse(Logger[] Loggers)
        {
            this.Loggers = Loggers;
        }
        
        /// Constructor with buffer.
        public GetLoggersResponse(ref ReadBuffer b)
        {
            Loggers = b.DeserializeArray<Logger>();
            for (int i = 0; i < Loggers.Length; i++)
            {
                Loggers[i] = new Logger(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetLoggersResponse(ref b);
        
        public GetLoggersResponse RosDeserialize(ref ReadBuffer b) => new GetLoggersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Loggers);
        }
        
        public void RosValidate()
        {
            if (Loggers is null) throw new System.NullReferenceException(nameof(Loggers));
            for (int i = 0; i < Loggers.Length; i++)
            {
                if (Loggers[i] is null) throw new System.NullReferenceException($"{nameof(Loggers)}[{i}]");
                Loggers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Loggers);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
