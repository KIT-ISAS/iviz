using System.Runtime.Serialization;

namespace Iviz.Msgs.std_srvs
{
    public sealed class Empty : IService
    {
        /// <summary> Request message. </summary>
        public EmptyRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public EmptyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Empty()
        {
            Request = new EmptyRequest();
            Response = new EmptyResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Empty(EmptyRequest request)
        {
            Request = request;
            Response = new EmptyResponse();
        }
        
        public IService Create() => new Empty();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "std_srvs/Empty";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    }

    public sealed class EmptyRequest : Internal.EmptyRequest
    {
    }

    public sealed class EmptyResponse : Internal.EmptyResponse
    {
    }
}
