using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract (Name = RosServiceType)]
    public sealed class Reconfigure : IService
    {
        /// Request message.
        [DataMember] public ReconfigureRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public ReconfigureResponse Response { get; set; }
        
        /// Empty constructor.
        public Reconfigure()
        {
            Request = new ReconfigureRequest();
            Response = new ReconfigureResponse();
        }
        
        /// Setter constructor.
        public Reconfigure(ReconfigureRequest request)
        {
            Request = request;
            Response = new ReconfigureResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ReconfigureRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ReconfigureResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "dynamic_reconfigure/Reconfigure";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "bb125d226a21982a4a98760418dc2672";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ReconfigureRequest : IRequest<Reconfigure, ReconfigureResponse>, IDeserializable<ReconfigureRequest>
    {
        [DataMember (Name = "config")] public Config Config;
    
        /// Constructor for empty message.
        public ReconfigureRequest()
        {
            Config = new Config();
        }
        
        /// Explicit constructor.
        public ReconfigureRequest(Config Config)
        {
            this.Config = Config;
        }
        
        /// Constructor with buffer.
        public ReconfigureRequest(ref ReadBuffer b)
        {
            Config = new Config(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ReconfigureRequest(ref b);
        
        public ReconfigureRequest RosDeserialize(ref ReadBuffer b) => new ReconfigureRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Config.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Config is null) BuiltIns.ThrowNullReference(nameof(Config));
            Config.RosValidate();
        }
    
        public int RosMessageLength => 0 + Config.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ReconfigureResponse : IResponse, IDeserializable<ReconfigureResponse>
    {
        [DataMember (Name = "config")] public Config Config;
    
        /// Constructor for empty message.
        public ReconfigureResponse()
        {
            Config = new Config();
        }
        
        /// Explicit constructor.
        public ReconfigureResponse(Config Config)
        {
            this.Config = Config;
        }
        
        /// Constructor with buffer.
        public ReconfigureResponse(ref ReadBuffer b)
        {
            Config = new Config(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ReconfigureResponse(ref b);
        
        public ReconfigureResponse RosDeserialize(ref ReadBuffer b) => new ReconfigureResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Config.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Config is null) BuiltIns.ThrowNullReference(nameof(Config));
            Config.RosValidate();
        }
    
        public int RosMessageLength => 0 + Config.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
