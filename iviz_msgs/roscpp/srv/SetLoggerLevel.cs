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
            Logger = "";
            Level = "";
        }
        
        /// Explicit constructor.
        public SetLoggerLevelRequest(string Logger, string Level)
        {
            this.Logger = Logger;
            this.Level = Level;
        }
        
        /// Constructor with buffer.
        public SetLoggerLevelRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Logger);
            b.DeserializeString(out Level);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new SetLoggerLevelRequest(ref b);
        
        public SetLoggerLevelRequest RosDeserialize(ref ReadBuffer b) => new SetLoggerLevelRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Logger);
            b.Serialize(Level);
        }
        
        public void RosValidate()
        {
            if (Logger is null) BuiltIns.ThrowNullReference();
            if (Level is null) BuiltIns.ThrowNullReference();
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
        public SetLoggerLevelResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public SetLoggerLevelResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static SetLoggerLevelResponse? singleton;
        public static SetLoggerLevelResponse Singleton => singleton ??= new SetLoggerLevelResponse();
    
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
}
