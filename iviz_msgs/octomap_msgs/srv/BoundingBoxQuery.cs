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
            Response = new BoundingBoxQueryResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public BoundingBoxQuery(BoundingBoxQueryRequest request)
        {
            Request = request;
            Response = new BoundingBoxQueryResponse();
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
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "octomap_msgs/BoundingBoxQuery";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "93aa3d73b866f04880927745f4aab303";
    }

    public sealed class BoundingBoxQueryRequest : IRequest, IDeserializable<BoundingBoxQueryRequest>
    {
        // Clear a region specified by a global axis-aligned bounding box in stored OctoMap
        // minimum corner point of axis-aligned bounding box in global frame
        [DataMember (Name = "min")] public GeometryMsgs.Point Min { get; set; }
        // maximum corner point of axis-aligned bounding box in global frame
        [DataMember (Name = "max")] public GeometryMsgs.Point Max { get; set; }
    
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
            Min = new GeometryMsgs.Point(ref b);
            Max = new GeometryMsgs.Point(ref b);
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
            Min.RosSerialize(ref b);
            Max.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

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
            return new BoundingBoxQueryResponse(ref b);
        }
        
        BoundingBoxQueryResponse IDeserializable<BoundingBoxQueryResponse>.RosDeserialize(ref Buffer b)
        {
            return new BoundingBoxQueryResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
