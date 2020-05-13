using System.Runtime.Serialization;

namespace Iviz.Msgs.std_srvs
{
    [DataContract]
    public sealed class SetBool : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public SetBoolRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public SetBoolResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SetBool()
        {
            Request = new SetBoolRequest();
            Response = new SetBoolResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SetBool(SetBoolRequest request)
        {
            Request = request;
            Response = new SetBoolResponse();
        }
        
        IService IService.Create() => new SetBool();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetBoolRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetBoolResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "std_srvs/SetBool";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "09fb03525b03e7ea1fd3992bafd87e16";
    }

    public sealed class SetBoolRequest : IRequest
    {
        [DataMember] public bool data { get; set; } // e.g. for hardware enabling / disabling
    
        /// <summary> Constructor for empty message. </summary>
        public SetBoolRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetBoolRequest(bool data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetBoolRequest(Buffer b)
        {
            this.data = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetBoolRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 1;
    }

    public sealed class SetBoolResponse : IResponse
    {
        [DataMember] public bool success { get; set; } // indicate successful run of triggered service
        [DataMember] public string message { get; set; } // informational, e.g. for error messages
    
        /// <summary> Constructor for empty message. </summary>
        public SetBoolResponse()
        {
            message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetBoolResponse(bool success, string message)
        {
            this.success = success;
            this.message = message ?? throw new System.ArgumentNullException(nameof(message));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SetBoolResponse(Buffer b)
        {
            this.success = b.Deserialize<bool>();
            this.message = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new SetBoolResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.success);
            b.Serialize(this.message);
        }
        
        public void Validate()
        {
            if (message is null) throw new System.NullReferenceException();
        }
    
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
