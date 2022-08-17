using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract]
    public sealed class SetLoggerLevel : IService
    {
        /// Request message.
        [DataMember] public SetLoggerLevelRequest Request;
        
        /// Response message.
        [DataMember] public SetLoggerLevelResponse Response;
        
        /// Empty constructor.
        public SetLoggerLevel()
        {
            Request = new SetLoggerLevelRequest();
            Response = SetLoggerLevelResponse.Singleton;
        }
        
        /// Setter constructor.
        public SetLoggerLevel(SetLoggerLevelRequest request)
        {
            Request = request;
            Response = SetLoggerLevelResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetLoggerLevelRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetLoggerLevelResponse)value;
        }
        
        public const string ServiceType = "roscpp/SetLoggerLevel";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "51da076440d78ca1684d36c868df61ea";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetLoggerLevelRequest : IRequest<SetLoggerLevel, SetLoggerLevelResponse>, IDeserializable<SetLoggerLevelRequest>
    {
        [DataMember (Name = "logger")] public string Logger;
        [DataMember (Name = "level")] public string Level;
    
        public SetLoggerLevelRequest()
        {
            Logger = "";
            Level = "";
        }
        
        public SetLoggerLevelRequest(string Logger, string Level)
        {
            this.Logger = Logger;
            this.Level = Level;
        }
        
        public SetLoggerLevelRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Logger);
            b.DeserializeString(out Level);
        }
        
        public SetLoggerLevelRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Logger);
            b.DeserializeString(out Level);
        }
        
        public SetLoggerLevelRequest RosDeserialize(ref ReadBuffer b) => new SetLoggerLevelRequest(ref b);
        
        public SetLoggerLevelRequest RosDeserialize(ref ReadBuffer2 b) => new SetLoggerLevelRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Logger);
            b.Serialize(Level);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Logger);
            b.Serialize(Level);
        }
        
        public void RosValidate()
        {
            if (Logger is null) BuiltIns.ThrowNullReference();
            if (Level is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(Logger) + WriteBuffer.GetStringSize(Level);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.AddLength(c, Logger);
            c = WriteBuffer2.AddLength(c, Level);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetLoggerLevelResponse : IResponse, IDeserializable<SetLoggerLevelResponse>
    {
    
        public SetLoggerLevelResponse()
        {
        }
        
        public SetLoggerLevelResponse(ref ReadBuffer b)
        {
        }
        
        public SetLoggerLevelResponse(ref ReadBuffer2 b)
        {
        }
        
        public SetLoggerLevelResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public SetLoggerLevelResponse RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static SetLoggerLevelResponse? singleton;
        public static SetLoggerLevelResponse Singleton => singleton ??= new SetLoggerLevelResponse();
    
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
}
