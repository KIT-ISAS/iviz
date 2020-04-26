using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class SearchParam : IService
    {
        /// <summary> Request message. </summary>
        public SearchParamRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public SearchParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public SearchParam()
        {
            Request = new SearchParamRequest();
            Response = new SearchParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public SearchParam(SearchParamRequest request)
        {
            Request = request;
            Response = new SearchParamResponse();
        }
        
        public IService Create() => new SearchParam();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/SearchParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "dfadc39f113c1cc6d7759508d8461d5a";
    }

    public sealed class SearchParamRequest : IRequest
    {
        public string name;
    
        /// <summary> Constructor for empty message. </summary>
        public SearchParamRequest()
        {
            name = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out name, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(name, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Encoding.UTF8.GetByteCount(name);
                return size;
            }
        }
    }

    public sealed class SearchParamResponse : IResponse
    {
        public string global_name;
    
        /// <summary> Constructor for empty message. </summary>
        public SearchParamResponse()
        {
            global_name = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out global_name, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(global_name, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Encoding.UTF8.GetByteCount(global_name);
                return size;
            }
        }
    }
}
