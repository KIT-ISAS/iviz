using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/GetParamNames")]
    public sealed class GetParamNames : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetParamNamesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetParamNamesResponse Response { get; set; }
        
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
        
        IService IService.Create() => new GetParamNames();
        
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
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/GetParamNames";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "dc7ae3609524b18034e49294a4ce670e";
    }

    public sealed class GetParamNamesRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetParamNamesResponse : IResponse
    {
        [DataMember (Name = "names")] public string[] Names { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamNamesResponse()
        {
            Names = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetParamNamesResponse(string[] Names)
        {
            this.Names = Names;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetParamNamesResponse(ref Buffer b)
        {
            Names = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetParamNamesResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Names, 0);
        }
        
        public void RosValidate()
        {
            if (Names is null) throw new System.NullReferenceException(nameof(Names));
            for (int i = 0; i < Names.Length; i++)
            {
                if (Names[i] is null) throw new System.NullReferenceException($"{nameof(Names)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Names.Length;
                foreach (string s in Names)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    }
}
