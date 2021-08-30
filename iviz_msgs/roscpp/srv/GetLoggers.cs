using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract (Name = "roscpp/GetLoggers")]
    public sealed class GetLoggers : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetLoggersRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetLoggersResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetLoggers()
        {
            Request = GetLoggersRequest.Singleton;
            Response = new GetLoggersResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetLoggers(GetLoggersRequest request)
        {
            Request = request;
            Response = new GetLoggersResponse();
        }
        
        IService IService.Create() => new GetLoggers();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "roscpp/GetLoggers";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "32e97e85527d4678a8f9279894bb64b0";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLoggersRequest : IRequest<GetLoggers, GetLoggersResponse>, IDeserializable<GetLoggersRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public GetLoggersRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetLoggersRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        GetLoggersRequest IDeserializable<GetLoggersRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly GetLoggersRequest Singleton = new GetLoggersRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
    
        /// <summary> Constructor for empty message. </summary>
        public GetLoggersResponse()
        {
            Loggers = System.Array.Empty<Logger>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetLoggersResponse(Logger[] Loggers)
        {
            this.Loggers = Loggers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetLoggersResponse(ref Buffer b)
        {
            Loggers = b.DeserializeArray<Logger>();
            for (int i = 0; i < Loggers.Length; i++)
            {
                Loggers[i] = new Logger(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetLoggersResponse(ref b);
        }
        
        GetLoggersResponse IDeserializable<GetLoggersResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetLoggersResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Loggers, 0);
        }
        
        public void Dispose()
        {
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Loggers)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
