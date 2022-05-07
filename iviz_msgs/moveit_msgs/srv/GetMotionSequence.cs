using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "moveit_msgs/GetMotionSequence";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "da3890ab96dfd351e68c0fd2615116bd";
        
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
        public GetMotionSequenceRequest(ref ReadBuffer b)
        {
            Request = new MotionSequenceRequest(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMotionSequenceRequest(ref b);
        
        public GetMotionSequenceRequest RosDeserialize(ref ReadBuffer b) => new GetMotionSequenceRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Request.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Request is null) BuiltIns.ThrowNullReference();
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
        public GetMotionSequenceResponse(ref ReadBuffer b)
        {
            Response = new MotionSequenceResponse(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMotionSequenceResponse(ref b);
        
        public GetMotionSequenceResponse RosDeserialize(ref ReadBuffer b) => new GetMotionSequenceResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Response.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Response is null) BuiltIns.ThrowNullReference();
            Response.RosValidate();
        }
    
        public int RosMessageLength => 0 + Response.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
