using System.Runtime.CompilerServices;
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
                var array = n == 0
                    ? EmptyArray<Logger>.Value
                    : new Logger[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Logger(ref b);
                }
                Loggers = array;
            }
        }
        
        public GetLoggersResponse(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Logger>.Value
                    : new Logger[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Logger(ref b);
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
            b.Serialize(Loggers.Length);
            foreach (var t in Loggers)
            {
                t.RosSerialize(ref b);
            }
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Loggers.Length
            foreach (var t in Loggers)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
