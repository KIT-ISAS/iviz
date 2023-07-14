using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class GetParam : IService<GetParamRequest, GetParamResponse>
    {
        /// Request message.
        [DataMember] public GetParamRequest Request;
        
        /// Response message.
        [DataMember] public GetParamResponse Response;
        
        /// Empty constructor.
        public GetParam()
        {
            Request = new GetParamRequest();
            Response = new GetParamResponse();
        }
        
        /// Setter constructor.
        public GetParam(GetParamRequest request)
        {
            Request = request;
            Response = new GetParamResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetParamRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetParamResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/GetParam";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "4e1e229c08fb70602243287505293619";
        
        public IService Generate() => new GetParam();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParamRequest : IRequest<GetParam, GetParamResponse>, IDeserializable<GetParamRequest>
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "default_value")] public string DefaultValue;
    
        public GetParamRequest()
        {
            Name = "";
            DefaultValue = "";
        }
        
        public GetParamRequest(string Name, string DefaultValue)
        {
            this.Name = Name;
            this.DefaultValue = DefaultValue;
        }
        
        public GetParamRequest(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            DefaultValue = b.DeserializeString();
        }
        
        public GetParamRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
            b.Align4();
            DefaultValue = b.DeserializeString();
        }
        
        public GetParamRequest RosDeserialize(ref ReadBuffer b) => new GetParamRequest(ref b);
        
        public GetParamRequest RosDeserialize(ref ReadBuffer2 b) => new GetParamRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(DefaultValue);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Name);
            b.Align4();
            b.Serialize(DefaultValue);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Name, nameof(Name));
            BuiltIns.ThrowIfNull(DefaultValue, nameof(DefaultValue));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(DefaultValue);
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
            size = WriteBuffer2.AddLength(size, DefaultValue);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParamResponse : IResponse, IDeserializable<GetParamResponse>
    {
        [DataMember (Name = "value")] public string Value;
    
        public GetParamResponse()
        {
            Value = "";
        }
        
        public GetParamResponse(string Value)
        {
            this.Value = Value;
        }
        
        public GetParamResponse(ref ReadBuffer b)
        {
            Value = b.DeserializeString();
        }
        
        public GetParamResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Value = b.DeserializeString();
        }
        
        public GetParamResponse RosDeserialize(ref ReadBuffer b) => new GetParamResponse(ref b);
        
        public GetParamResponse RosDeserialize(ref ReadBuffer2 b) => new GetParamResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Value);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Value, nameof(Value));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Value);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Value);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
