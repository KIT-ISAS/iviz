using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract (Name = "roscpp/SetLoggerLevel")]
    public sealed class SetLoggerLevel : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetLoggerLevelRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetLoggerLevelResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetLoggerLevel()
        {
            Request = new SetLoggerLevelRequest();
            Response = SetLoggerLevelResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "roscpp/SetLoggerLevel";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "51da076440d78ca1684d36c868df61ea";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetLoggerLevelRequest : IRequest<SetLoggerLevel, SetLoggerLevelResponse>, IDeserializable<SetLoggerLevelRequest>
    {
        [DataMember (Name = "logger")] public string Logger { get; set; }
        [DataMember (Name = "level")] public string Level { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public SetLoggerLevelRequest()
        {
            Logger = string.Empty;
            Level = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetLoggerLevelRequest(string Logger, string Level)
        {
            this.Logger = Logger;
            this.Level = Level;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetLoggerLevelRequest(ref Buffer b)
        {
            Logger = b.DeserializeString();
            Level = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetLoggerLevelRequest(ref b);
        }
        
        SetLoggerLevelRequest IDeserializable<SetLoggerLevelRequest>.RosDeserialize(ref Buffer b)
        {
            return new SetLoggerLevelRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Logger);
            b.Serialize(Level);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Logger is null) throw new System.NullReferenceException(nameof(Logger));
            if (Level is null) throw new System.NullReferenceException(nameof(Level));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Logger);
                size += BuiltIns.UTF8.GetByteCount(Level);
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetLoggerLevelResponse : IResponse, IDeserializable<SetLoggerLevelResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public SetLoggerLevelResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetLoggerLevelResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        SetLoggerLevelResponse IDeserializable<SetLoggerLevelResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly SetLoggerLevelResponse Singleton = new SetLoggerLevelResponse();
    
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
}
