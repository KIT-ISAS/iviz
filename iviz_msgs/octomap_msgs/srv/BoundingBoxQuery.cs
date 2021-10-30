using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract (Name = "octomap_msgs/BoundingBoxQuery")]
    public sealed class BoundingBoxQuery : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public BoundingBoxQueryRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public BoundingBoxQueryResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public BoundingBoxQuery()
        {
            Request = new BoundingBoxQueryRequest();
            Response = BoundingBoxQueryResponse.Singleton;
        }
        
        /// <summary> Setter constructor. </summary>
        public BoundingBoxQuery(BoundingBoxQueryRequest request)
        {
            Request = request;
            Response = BoundingBoxQueryResponse.Singleton;
        }
        
        IService IService.Create() => new BoundingBoxQuery();
        
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "octomap_msgs/BoundingBoxQuery";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
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
    
        /// <summary> Constructor for empty message. </summary>
        public BoundingBoxQueryRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoundingBoxQueryRequest(in GeometryMsgs.Point Min, in GeometryMsgs.Point Max)
        {
            this.Min = Min;
            this.Max = Max;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal BoundingBoxQueryRequest(ref Buffer b)
        {
            b.Deserialize(out Min);
            b.Deserialize(out Max);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BoundingBoxQueryRequest(ref b);
        }
        
        BoundingBoxQueryRequest IDeserializable<BoundingBoxQueryRequest>.RosDeserialize(ref Buffer b)
        {
            return new BoundingBoxQueryRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Min);
            b.Serialize(Max);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Constructor for empty message. </summary>
        public BoundingBoxQueryResponse()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal BoundingBoxQueryResponse(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        BoundingBoxQueryResponse IDeserializable<BoundingBoxQueryResponse>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly BoundingBoxQueryResponse Singleton = new BoundingBoxQueryResponse();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void Dispose()
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
