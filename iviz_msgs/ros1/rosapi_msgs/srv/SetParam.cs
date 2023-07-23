using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class SetParam : IService<SetParamRequest, SetParamResponse>
    {
        /// Request message.
        [DataMember] public SetParamRequest Request;
        
        /// Response message.
        [DataMember] public SetParamResponse Response;
        
        /// Empty constructor.
        public SetParam()
        {
            Request = new SetParamRequest();
            Response = SetParamResponse.Singleton;
        }
        
        /// Setter constructor.
        public SetParam(SetParamRequest request)
        {
            Request = request;
            Response = SetParamResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetParamResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/SetParam";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "bc6ccc4a57f61779c8eaae61e9f422e0";
        
        public IService Generate() => new SetParam();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParamRequest : IRequest<SetParam, SetParamResponse>, IDeserializable<SetParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public string Value;
    
        public SetParamRequest()
        {
            Name = "";
            Value = "";
        }
        
        public SetParamRequest(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        public SetParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Value = b.DeserializeString();
        }
        
        public SetParamRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
            b.Align4();
            Value = b.DeserializeString();
        }
        
        public SetParamRequest RosDeserialize(ref ReadBuffer b) => new SetParamRequest(ref b);
        
        public SetParamRequest RosDeserialize(ref ReadBuffer2 b) => new SetParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Name);
            b.Align4();
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Value);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Value);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParamResponse : IResponse, IDeserializable<SetParamResponse>
    {
    
        public SetParamResponse()
        {
        }
        
        public SetParamResponse(ref ReadBuffer b)
        {
        }
        
        public SetParamResponse(ref ReadBuffer2 b)
        {
        }
        
        public SetParamResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public SetParamResponse RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static SetParamResponse? singleton;
        public static SetParamResponse Singleton => singleton ??= new SetParamResponse();
    
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
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
