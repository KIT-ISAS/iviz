using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class DeleteParam : IService
    {
        /// <summary> Request message. </summary>
        public DeleteParamRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public DeleteParamResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public DeleteParam()
        {
            Request = new DeleteParamRequest();
            Response = new DeleteParamResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public DeleteParam(DeleteParamRequest request)
        {
            Request = request;
            Response = new DeleteParamResponse();
        }
        
        public IService Create() => new DeleteParam();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosapi/DeleteParam";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "c1f3d28f1b044c871e6eff2e9fc3c667";
    }

    public sealed class DeleteParamRequest : IRequest
    {
        public string name;
    
        /// <summary> Constructor for empty message. </summary>
        public DeleteParamRequest()
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
                size += BuiltIns.UTF8.GetByteCount(name);
                return size;
            }
        }
    }

    public sealed class DeleteParamResponse : Internal.EmptyResponse
    {
    }
}
