using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract (Name = RosServiceType)]
    public sealed class SetLoggerLevel : IService
    {
        /// Request message.
        [DataMember] public SetLoggerLevelRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public SetLoggerLevelResponse Response { get; set; }
        
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
        
        IService IService.Create() => new SetLoggerLevel();
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "roscpp/SetLoggerLevel";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "51da076440d78ca1684d36c868df61ea";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetLoggerLevelRequest : IRequest<SetLoggerLevel, SetLoggerLevelResponse>, IDeserializable<SetLoggerLevelRequest>
    {
        [DataMember (Name = "logger")] public string Logger;
        [DataMember (Name = "level")] public string Level;
    
        /// Constructor for empty message.
        public SetLoggerLevelRequest()
        {
            Logger = string.Empty;
            Level = string.Empty;
        }
        
        /// Explicit constructor.
        public SetLoggerLevelRequest(string Logger, string Level)
        {
            this.Logger = Logger;
            this.Level = Level;
        }
        
        /// Constructor with buffer.
        internal SetLoggerLevelRequest(ref Buffer b)
        {
            Logger = b.DeserializeString();
            Level = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new SetLoggerLevelRequest(ref b);
        
        SetLoggerLevelRequest IDeserializable<SetLoggerLevelRequest>.RosDeserialize(ref Buffer b) => new SetLoggerLevelRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Logger);
            b.Serialize(Level);
        }
        
        public void RosValidate()
        {
            if (Logger is null) throw new System.NullReferenceException(nameof(Logger));
            if (Level is null) throw new System.NullReferenceException(nameof(Level));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Logger) + BuiltIns.GetStringSize(Level);
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetLoggerLevelResponse : IResponse, IDeserializable<SetLoggerLevelResponse>
    {
    
        /// Constructor for empty message.
        public SetLoggerLevelResponse()
        {
        }
        
        /// Constructor with buffer.
        internal SetLoggerLevelResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        SetLoggerLevelResponse IDeserializable<SetLoggerLevelResponse>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly SetLoggerLevelResponse Singleton = new SetLoggerLevelResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
