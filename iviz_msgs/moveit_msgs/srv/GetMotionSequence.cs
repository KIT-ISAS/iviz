using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class GetMotionSequence : IService
    {
        /// Request message.
        [DataMember] public GetMotionSequenceRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public GetMotionSequenceResponse Response { get; set; }
        
        /// Empty constructor.
        public GetMotionSequence()
        {
            Request = new GetMotionSequenceRequest();
            Response = new GetMotionSequenceResponse();
        }
        
        /// Setter constructor.
        public GetMotionSequence(GetMotionSequenceRequest request)
        {
            Request = request;
            Response = new GetMotionSequenceResponse();
        }
        
        IService IService.Create() => new GetMotionSequence();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetMotionSequenceRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetMotionSequenceResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/GetMotionSequence";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "da3890ab96dfd351e68c0fd2615116bd";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionSequenceRequest : IRequest<GetMotionSequence, GetMotionSequenceResponse>, IDeserializable<GetMotionSequenceRequest>
    {
        // Planning request with a list of motion commands
        [DataMember (Name = "request")] public MotionSequenceRequest Request;
    
        /// Constructor for empty message.
        public GetMotionSequenceRequest()
        {
            Request = new MotionSequenceRequest();
        }
        
        /// Explicit constructor.
        public GetMotionSequenceRequest(MotionSequenceRequest Request)
        {
            this.Request = Request;
        }
        
        /// Constructor with buffer.
        internal GetMotionSequenceRequest(ref Buffer b)
        {
            Request = new MotionSequenceRequest(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMotionSequenceRequest(ref b);
        
        GetMotionSequenceRequest IDeserializable<GetMotionSequenceRequest>.RosDeserialize(ref Buffer b) => new GetMotionSequenceRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Request.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Request is null) throw new System.NullReferenceException(nameof(Request));
            Request.RosValidate();
        }
    
        public int RosMessageLength => 0 + Request.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionSequenceResponse : IResponse, IDeserializable<GetMotionSequenceResponse>
    {
        // Response to the planning request
        [DataMember (Name = "response")] public MotionSequenceResponse Response;
    
        /// Constructor for empty message.
        public GetMotionSequenceResponse()
        {
            Response = new MotionSequenceResponse();
        }
        
        /// Explicit constructor.
        public GetMotionSequenceResponse(MotionSequenceResponse Response)
        {
            this.Response = Response;
        }
        
        /// Constructor with buffer.
        internal GetMotionSequenceResponse(ref Buffer b)
        {
            Response = new MotionSequenceResponse(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMotionSequenceResponse(ref b);
        
        GetMotionSequenceResponse IDeserializable<GetMotionSequenceResponse>.RosDeserialize(ref Buffer b) => new GetMotionSequenceResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Response.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Response is null) throw new System.NullReferenceException(nameof(Response));
            Response.RosValidate();
        }
    
        public int RosMessageLength => 0 + Response.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
