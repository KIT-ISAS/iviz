using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract (Name = "dynamic_reconfigure/Reconfigure")]
    public sealed class Reconfigure : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ReconfigureRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ReconfigureResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Reconfigure()
        {
            Request = new ReconfigureRequest();
            Response = new ReconfigureResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Reconfigure(ReconfigureRequest request)
        {
            Request = request;
            Response = new ReconfigureResponse();
        }
        
        IService IService.Create() => new Reconfigure();
        
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
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "dynamic_reconfigure/Reconfigure";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "bb125d226a21982a4a98760418dc2672";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ReconfigureRequest : IRequest<Reconfigure, ReconfigureResponse>, IDeserializable<ReconfigureRequest>
    {
        [DataMember (Name = "config")] public Config Config;
    
        /// <summary> Constructor for empty message. </summary>
        public ReconfigureRequest()
        {
            Config = new Config();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ReconfigureRequest(Config Config)
        {
            this.Config = Config;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ReconfigureRequest(ref Buffer b)
        {
            Config = new Config(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ReconfigureRequest(ref b);
        }
        
        ReconfigureRequest IDeserializable<ReconfigureRequest>.RosDeserialize(ref Buffer b)
        {
            return new ReconfigureRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Config.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Config is null) throw new System.NullReferenceException(nameof(Config));
            Config.RosValidate();
        }
    
        public int RosMessageLength => 0 + Config.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ReconfigureResponse : IResponse, IDeserializable<ReconfigureResponse>
    {
        [DataMember (Name = "config")] public Config Config;
    
        /// <summary> Constructor for empty message. </summary>
        public ReconfigureResponse()
        {
            Config = new Config();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ReconfigureResponse(Config Config)
        {
            this.Config = Config;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ReconfigureResponse(ref Buffer b)
        {
            Config = new Config(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ReconfigureResponse(ref b);
        }
        
        ReconfigureResponse IDeserializable<ReconfigureResponse>.RosDeserialize(ref Buffer b)
        {
            return new ReconfigureResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Config.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Config is null) throw new System.NullReferenceException(nameof(Config));
            Config.RosValidate();
        }
    
        public int RosMessageLength => 0 + Config.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
