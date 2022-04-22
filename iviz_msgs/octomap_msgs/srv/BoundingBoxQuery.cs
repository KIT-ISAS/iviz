using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class BoundingBoxQuery : IService
    {
        /// Request message.
        [DataMember] public BoundingBoxQueryRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public BoundingBoxQueryResponse Response { get; set; }
        
        /// Empty constructor.
        public BoundingBoxQuery()
        {
            Request = new BoundingBoxQueryRequest();
            Response = BoundingBoxQueryResponse.Singleton;
        }
        
        /// Setter constructor.
        public BoundingBoxQuery(BoundingBoxQueryRequest request)
        {
            Request = request;
            Response = BoundingBoxQueryResponse.Singleton;
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (BoundingBoxQueryRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (BoundingBoxQueryResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "octomap_msgs/BoundingBoxQuery";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "93aa3d73b866f04880927745f4aab303";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class BoundingBoxQueryRequest : IRequest<BoundingBoxQuery, BoundingBoxQueryResponse>, IDeserializable<BoundingBoxQueryRequest>
    {
        // Clear a region specified by a global axis-aligned bounding box in stored OctoMap
        // minimum corner point of axis-aligned bounding box in global frame
        [DataMember (Name = "min")] public GeometryMsgs.Point Min;
        // maximum corner point of axis-aligned bounding box in global frame
        [DataMember (Name = "max")] public GeometryMsgs.Point Max;
    
        /// Constructor for empty message.
        public BoundingBoxQueryRequest()
        {
        }
        
        /// Explicit constructor.
        public BoundingBoxQueryRequest(in GeometryMsgs.Point Min, in GeometryMsgs.Point Max)
        {
            this.Min = Min;
            this.Max = Max;
        }
        
        /// Constructor with buffer.
        public BoundingBoxQueryRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Min);
            b.Deserialize(out Max);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new BoundingBoxQueryRequest(ref b);
        
        public BoundingBoxQueryRequest RosDeserialize(ref ReadBuffer b) => new BoundingBoxQueryRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Min);
            b.Serialize(in Max);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class BoundingBoxQueryResponse : IResponse, IDeserializable<BoundingBoxQueryResponse>
    {
    
        /// Constructor for empty message.
        public BoundingBoxQueryResponse()
        {
        }
        
        /// Constructor with buffer.
        public BoundingBoxQueryResponse(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public BoundingBoxQueryResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static BoundingBoxQueryResponse? singleton;
        public static BoundingBoxQueryResponse Singleton => singleton ??= new BoundingBoxQueryResponse();
    
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
