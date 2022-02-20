using System.Runtime.Serialization;

namespace Iviz.Msgs.StdSrvs
{
    [DataContract (Name = RosServiceType)]
    public sealed class Empty : IService
    {
        /// Request message.
        [DataMember] public EmptyRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public EmptyResponse Response { get; set; }
        
        /// Empty constructor.
        public Empty()
        {
            Request = EmptyRequest.Singleton;
            Response = EmptyResponse.Singleton;
        }
        
        /// Setter constructor.
        public Empty(EmptyRequest request)
        {
            Request = request;
            Response = EmptyResponse.Singleton;
        }
        
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
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "std_srvs/Empty";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class EmptyRequest : IRequest<Empty, EmptyResponse>, IDeserializable<EmptyRequest>
    {
    
        /// Constructor for empty message.
        public EmptyRequest()
        {
        }
        
        /// Constructor with buffer.
        public EmptyRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public EmptyRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly EmptyRequest Singleton = new EmptyRequest();
    
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
    public sealed class EmptyResponse : IResponse, IDeserializable<EmptyResponse>
    {
    
        /// Constructor for empty message.
        public EmptyResponse()
        {
        }
        
        /// Constructor with buffer.
        public EmptyResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public EmptyResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly EmptyResponse Singleton = new EmptyResponse();
    
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
}
