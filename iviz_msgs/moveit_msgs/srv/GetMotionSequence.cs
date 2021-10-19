using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/GetMotionSequence")]
    public sealed class GetMotionSequence : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public GetMotionSequenceRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public GetMotionSequenceResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public GetMotionSequence()
        {
            Request = new GetMotionSequenceRequest();
            Response = new GetMotionSequenceResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/GetMotionSequence";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "da3890ab96dfd351e68c0fd2615116bd";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionSequenceRequest : IRequest<GetMotionSequence, GetMotionSequenceResponse>, IDeserializable<GetMotionSequenceRequest>
    {
        // Planning request with a list of motion commands
        [DataMember (Name = "request")] public MotionSequenceRequest Request;
    
        /// <summary> Constructor for empty message. </summary>
        public GetMotionSequenceRequest()
        {
            Request = new MotionSequenceRequest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMotionSequenceRequest(MotionSequenceRequest Request)
        {
            this.Request = Request;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMotionSequenceRequest(ref Buffer b)
        {
            Request = new MotionSequenceRequest(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMotionSequenceRequest(ref b);
        }
        
        GetMotionSequenceRequest IDeserializable<GetMotionSequenceRequest>.RosDeserialize(ref Buffer b)
        {
            return new GetMotionSequenceRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Request.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Request is null) throw new System.NullReferenceException(nameof(Request));
            Request.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Request.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetMotionSequenceResponse : IResponse, IDeserializable<GetMotionSequenceResponse>
    {
        // Response to the planning request
        [DataMember (Name = "response")] public MotionSequenceResponse Response;
    
        /// <summary> Constructor for empty message. </summary>
        public GetMotionSequenceResponse()
        {
            Response = new MotionSequenceResponse();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMotionSequenceResponse(MotionSequenceResponse Response)
        {
            this.Response = Response;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMotionSequenceResponse(ref Buffer b)
        {
            Response = new MotionSequenceResponse(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMotionSequenceResponse(ref b);
        }
        
        GetMotionSequenceResponse IDeserializable<GetMotionSequenceResponse>.RosDeserialize(ref Buffer b)
        {
            return new GetMotionSequenceResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Response.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Response is null) throw new System.NullReferenceException(nameof(Response));
            Response.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Response.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
