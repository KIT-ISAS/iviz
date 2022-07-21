using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract]
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
        
        public const string ServiceType = "octomap_msgs/BoundingBoxQuery";
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "93aa3d73b866f04880927745f4aab303";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class BoundingBoxQueryRequest : IRequest<BoundingBoxQuery, BoundingBoxQueryResponse>, IDeserializableRos1<BoundingBoxQueryRequest>
    {
        // Clear a region specified by a global axis-aligned bounding box in stored OctoMap
        // minimum corner point of axis-aligned bounding box in global frame
        [DataMember (Name = "min")] public GeometryMsgs.Point Min;
        // maximum corner point of axis-aligned bounding box in global frame
        [DataMember (Name = "max")] public GeometryMsgs.Point Max;
    
        public BoundingBoxQueryRequest()
        {
        }
        
        public BoundingBoxQueryRequest(in GeometryMsgs.Point Min, in GeometryMsgs.Point Max)
        {
            this.Min = Min;
            this.Max = Max;
        }
        
        public BoundingBoxQueryRequest(ref ReadBuffer b)
        {
            b.Deserialize(out Min);
            b.Deserialize(out Max);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new BoundingBoxQueryRequest(ref b);
        
        public BoundingBoxQueryRequest RosDeserialize(ref ReadBuffer b) => new BoundingBoxQueryRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Min);
            b.Serialize(in Max);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class BoundingBoxQueryResponse : IResponse, IDeserializableRos1<BoundingBoxQueryResponse>
    {
    
        public BoundingBoxQueryResponse()
        {
        }
        
        public BoundingBoxQueryResponse(ref ReadBuffer b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public BoundingBoxQueryResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static BoundingBoxQueryResponse? singleton;
        public static BoundingBoxQueryResponse Singleton => singleton ??= new BoundingBoxQueryResponse();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
