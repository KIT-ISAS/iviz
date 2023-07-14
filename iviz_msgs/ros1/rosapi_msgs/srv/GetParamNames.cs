using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RosapiMsgs
{
    [DataContract]
    public sealed class GetParamNames : IService<GetParamNamesRequest, GetParamNamesResponse>
    {
        /// Request message.
        [DataMember] public GetParamNamesRequest Request;
        
        /// Response message.
        [DataMember] public GetParamNamesResponse Response;
        
        /// Empty constructor.
        public GetParamNames()
        {
            Request = GetParamNamesRequest.Singleton;
            Response = new GetParamNamesResponse();
        }
        
        /// Setter constructor.
        public GetParamNames(GetParamNamesRequest request)
        {
            Request = request;
            Response = new GetParamNamesResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetParamNamesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetParamNamesResponse)value;
        }
        
        public const string ServiceType = "rosapi_msgs/GetParamNames";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "dc7ae3609524b18034e49294a4ce670e";
        
        public IService Generate() => new GetParamNames();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParamNamesRequest : IRequest<GetParamNames, GetParamNamesResponse>, IDeserializable<GetParamNamesRequest>
    {
    
        public GetParamNamesRequest()
        {
        }
        
        public GetParamNamesRequest(ref ReadBuffer b)
        {
        }
        
        public GetParamNamesRequest(ref ReadBuffer2 b)
        {
        }
        
        public GetParamNamesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetParamNamesRequest RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetParamNamesRequest? singleton;
        public static GetParamNamesRequest Singleton => singleton ??= new GetParamNamesRequest();
    
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

    [DataContract]
    public sealed class GetParamNamesResponse : IResponse, IDeserializable<GetParamNamesResponse>
    {
        [DataMember (Name = "names")] public string[] Names;
    
        public GetParamNamesResponse()
        {
            Names = EmptyArray<string>.Value;
        }
        
        public GetParamNamesResponse(string[] Names)
        {
            this.Names = Names;
        }
        
        public GetParamNamesResponse(ref ReadBuffer b)
        {
            Names = b.DeserializeStringArray();
        }
        
        public GetParamNamesResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            Names = b.DeserializeStringArray();
        }
        
        public GetParamNamesResponse RosDeserialize(ref ReadBuffer b) => new GetParamNamesResponse(ref b);
        
        public GetParamNamesResponse RosDeserialize(ref ReadBuffer2 b) => new GetParamNamesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Names.Length);
            b.SerializeArray(Names);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Names.Length);
            b.SerializeArray(Names);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Names, nameof(Names));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Names);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Names);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
