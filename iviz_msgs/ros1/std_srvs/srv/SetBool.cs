using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract]
    public sealed class SetBool : IService
    {
        /// Request message.
        [DataMember] public SetBoolRequest Request;
        
        /// Response message.
        [DataMember] public SetBoolResponse Response;
        
        /// Empty constructor.
        public SetBool()
        {
            Request = new SetBoolRequest();
            Response = new SetBoolResponse();
        }
        
        /// Setter constructor.
        public SetBool(SetBoolRequest request)
        {
            Request = request;
            Response = new SetBoolResponse();
        }
        
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
        
        public const string ServiceType = "std_srvs/SetBool";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "09fb03525b03e7ea1fd3992bafd87e16";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetBoolRequest : IRequest<SetBool, SetBoolResponse>, IDeserializable<SetBoolRequest>
    {
        /// <summary> E.g. for hardware enabling / disabling </summary>
        [DataMember (Name = "data")] public bool Data;
    
        public SetBoolRequest()
        {
        }
        
        public SetBoolRequest(bool Data)
        {
            this.Data = Data;
        }
        
        public SetBoolRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public SetBoolRequest(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public SetBoolRequest RosDeserialize(ref ReadBuffer b) => new SetBoolRequest(ref b);
        
        public SetBoolRequest RosDeserialize(ref ReadBuffer2 b) => new SetBoolRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 1;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Data
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetBoolResponse : IResponse, IDeserializable<SetBoolResponse>
    {
        /// <summary> Indicate successful run of triggered service </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> Informational, e.g. for error messages </summary>
        [DataMember (Name = "message")] public string Message;
    
        public SetBoolResponse()
        {
            Message = "";
        }
        
        public SetBoolResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public SetBoolResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            b.DeserializeString(out Message);
        }
        
        public SetBoolResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            b.DeserializeString(out Message);
        }
        
        public SetBoolResponse RosDeserialize(ref ReadBuffer b) => new SetBoolResponse(ref b);
        
        public SetBoolResponse RosDeserialize(ref ReadBuffer2 b) => new SetBoolResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + WriteBuffer.GetStringSize(Message);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Message);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
