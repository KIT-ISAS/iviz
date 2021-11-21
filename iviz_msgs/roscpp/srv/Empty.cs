using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract (Name = "roscpp/Empty")]
    public sealed class Empty : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public EmptyRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public EmptyResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Empty()
        {
            Request = EmptyRequest.Singleton;
            Response = EmptyResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
        public Empty(EmptyRequest request)
        {
            Request = request;
            Response = EmptyResponse.Singleton;
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "roscpp/Empty";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class EmptyRequest : IRequest<Empty, EmptyResponse>, IDeserializable<EmptyRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public EmptyRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal EmptyRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        EmptyRequest IDeserializable<EmptyRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly EmptyRequest Singleton = new EmptyRequest();
    
        public void RosSerialize(ref Buffer b)
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
    public sealed class EmptyResponse : IResponse, IDeserializable<EmptyResponse>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public EmptyResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal EmptyResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        EmptyResponse IDeserializable<EmptyResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly EmptyResponse Singleton = new EmptyResponse();
    
        public void RosSerialize(ref Buffer b)
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
}
