using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract (Name = "std_srvs/Empty")]
    public sealed class Empty : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public EmptyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public EmptyResponse Response { get; set; }
        
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
        
        IService IService.Create() => new Empty();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (EmptyRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (EmptyResponse)value;
        }
        
        /// <summary>
        /// An error message in case the call fails.
        /// If the provider sets this to non-null, the ok byte is set to false, and the error message is sent instead of the response.
        /// </summary>
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "std_srvs/Empty";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    }

    public sealed class EmptyRequest : Internal.EmptyRequest
    {
    }

    public sealed class EmptyResponse : Internal.EmptyResponse
    {
    }
}
