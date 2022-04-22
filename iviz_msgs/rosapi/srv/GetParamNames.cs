using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetParamNames : IService
    {
        /// Request message.
        [DataMember] public GetParamNamesRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetParamNamesResponse Response { get; set; }
        
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
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosapi/GetParamNames";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "dc7ae3609524b18034e49294a4ce670e";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParamNamesRequest : IRequest<GetParamNames, GetParamNamesResponse>, IDeserializable<GetParamNamesRequest>
    {
    
        /// Constructor for empty message.
        public GetParamNamesRequest()
        {
        }
        
        /// Constructor with buffer.
        public GetParamNamesRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public GetParamNamesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static GetParamNamesRequest? singleton;
        public static GetParamNamesRequest Singleton => singleton ??= new GetParamNamesRequest();
    
        public void RosSerialize(ref WriteBuffer b)
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

    [DataContract]
    public sealed class GetParamNamesResponse : IResponse, IDeserializable<GetParamNamesResponse>
    {
        [DataMember (Name = "names")] public string[] Names;
    
        /// Constructor for empty message.
        public GetParamNamesResponse()
        {
            Names = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public GetParamNamesResponse(string[] Names)
        {
            this.Names = Names;
        }
        
        /// Constructor with buffer.
        public GetParamNamesResponse(ref ReadBuffer b)
        {
            Names = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetParamNamesResponse(ref b);
        
        public GetParamNamesResponse RosDeserialize(ref ReadBuffer b) => new GetParamNamesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Names);
        }
        
        public void RosValidate()
        {
            if (Names is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Names.Length; i++)
            {
                if (Names[i] is null) BuiltIns.ThrowNullReference($"{nameof(Names)}[{i}]");
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Names);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
