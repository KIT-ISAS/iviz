using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract]
    public sealed class GetLoggers : IService<GetLoggersRequest, GetLoggersResponse>
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
        
        public IService Generate() => new GetLoggers();
        
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
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLoggersResponse : IResponse, IDeserializable<GetLoggersResponse>
    {
        [DataMember (Name = "loggers")] public Logger[] Loggers;
    
        public GetLoggersResponse()
        {
            Loggers = EmptyArray<Logger>.Value;
        }
        
        public GetLoggersResponse(Logger[] Loggers)
        {
            this.Loggers = Loggers;
        }
        
        public GetLoggersResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Logger[] array;
                if (n == 0) array = EmptyArray<Logger>.Value;
                else
                {
                    array = new Logger[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Logger(ref b);
                    }
                }
                Loggers = array;
            }
        }
        
        public GetLoggersResponse(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Logger[] array;
                if (n == 0) array = EmptyArray<Logger>.Value;
                else
                {
                    array = new Logger[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Logger(ref b);
                    }
                }
                Loggers = array;
            }
        }
        
        public GetLoggersResponse RosDeserialize(ref ReadBuffer b) => new GetLoggersResponse(ref b);
        
        public GetLoggersResponse RosDeserialize(ref ReadBuffer2 b) => new GetLoggersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Loggers.Length);
            foreach (var t in Loggers)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Loggers.Length);
            foreach (var t in Loggers)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Loggers, nameof(Loggers));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Loggers) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Loggers.Length
            foreach (var msg in Loggers) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
