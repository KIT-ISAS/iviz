using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract (Name = "std_srvs/SetBool")]
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "std_srvs/SetBool";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "09fb03525b03e7ea1fd3992bafd87e16";
    }

    [DataContract]
    public sealed class SetBoolRequest : IRequest, IDeserializable<SetBoolRequest>
    {
        [DataMember (Name = "data")] public bool Data { get; set; } // e.g. for hardware enabling / disabling
    
        /// <summary> Constructor for empty message. </summary>
        public SetBoolRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetBoolRequest(bool Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetBoolRequest(ref Buffer b)
        {
            Data = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetBoolRequest(ref b);
        }
        
        SetBoolRequest IDeserializable<SetBoolRequest>.RosDeserialize(ref Buffer b)
        {
            return new SetBoolRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class SetBoolResponse : IResponse, IDeserializable<SetBoolResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; } // indicate successful run of triggered service
        [DataMember (Name = "message")] public string Message { get; set; } // informational, e.g. for error messages
    
        /// <summary> Constructor for empty message. </summary>
        public SetBoolResponse()
        {
            Message = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public SetBoolResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public SetBoolResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new SetBoolResponse(ref b);
        }
        
        SetBoolResponse IDeserializable<SetBoolResponse>.RosDeserialize(ref Buffer b)
        {
            return new SetBoolResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(Message);
                return size;
            }
        }
    }
}
