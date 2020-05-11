using System.Runtime.Serialization;

namespace Iviz.Msgs.std_srvs
{
    public sealed class Trigger : IService
    {
        /// <summary> Request message. </summary>
        public TriggerRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public TriggerResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Trigger()
        {
            Request = new TriggerRequest();
            Response = new TriggerResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Trigger(TriggerRequest request)
        {
            Request = request;
            Response = new TriggerResponse();
        }
        
        public IService Create() => new Trigger();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TriggerRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TriggerResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "std_srvs/Trigger";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "937c9679a518e3a18d831e57125ea522";
    }

    public sealed class TriggerRequest : Internal.EmptyRequest
    {
    }

    public sealed class TriggerResponse : IResponse
    {
        public bool success { get; set; } // indicate successful run of triggered service
        public string message { get; set; } // informational, e.g. for error messages
    
        /// <summary> Constructor for empty message. </summary>
        public TriggerResponse()
        {
            message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TriggerResponse(bool success, string message)
        {
            this.success = success;
            this.message = message ?? throw new System.ArgumentNullException(nameof(message));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TriggerResponse(Buffer b)
        {
            this.success = b.Deserialize<bool>();
            this.message = b.DeserializeString();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TriggerResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.success);
            b.Serialize(this.message);
        }
        
        public void Validate()
        {
            if (message is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(message);
                return size;
            }
        }
    }
}
