using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class GetParamNames : IService
    {
        /// <summary> Request message. </summary>
        public GetParamNamesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        public GetParamNamesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetParamNames()
        {
            Request = new GetParamNamesRequest();
            Response = new GetParamNamesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public GetParamNames(GetParamNamesRequest request)
        {
            Request = request;
            Response = new GetParamNamesResponse();
        }
        
        public IService Create() => new GetParamNames();
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/GetParamNames";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "dc7ae3609524b18034e49294a4ce670e";
    }

    public sealed class GetParamNamesRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetParamNamesResponse : IResponse
    {
        public string[] names { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamNamesResponse()
        {
            names = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamNamesResponse(string[] names)
        {
            this.names = names ?? throw new System.ArgumentNullException(nameof(names));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetParamNamesResponse(Buffer b)
        {
            this.names = b.DeserializeStringArray(0);
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GetParamNamesResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.names, 0);
        }
        
        public void Validate()
        {
            if (names is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * names.Length;
                for (int i = 0; i < names.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(names[i]);
                }
                return size;
            }
        }
    }
}
