using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class GetParamNames : IService
    {
        /// <summary> Request message. </summary>
        public GetParamNamesRequest Request { get; }
        
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
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/GetParamNames";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "dc7ae3609524b18034e49294a4ce670e";
    }

    public sealed class GetParamNamesRequest : Internal.EmptyRequest
    {
    }

    public sealed class GetParamNamesResponse : IResponse
    {
        public string[] names;
    
        /// <summary> Constructor for empty message. </summary>
        public GetParamNamesResponse()
        {
            names = System.Array.Empty<string>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out names, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(names, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * names.Length;
                for (int i = 0; i < names.Length; i++)
                {
                    size += Encoding.UTF8.GetByteCount(names[i]);
                }
                return size;
            }
        }
    }
}
