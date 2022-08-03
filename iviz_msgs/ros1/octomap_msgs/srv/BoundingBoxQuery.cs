using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract]
    public sealed class BoundingBoxQuery : IService
    {
        /// Request message.
        [DataMember] public BoundingBoxQueryRequest Request;
        
        /// Response message.
        [DataMember] public BoundingBoxQueryResponse Response;
        
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
    public sealed class BoundingBoxQueryRequest : IRequest<BoundingBoxQuery, BoundingBoxQueryResponse>, IDeserializable<BoundingBoxQueryRequest>
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
        
        public BoundingBoxQueryRequest(ref ReadBuffer2 b)
        {
            b.Deserialize(out Min);
            b.Deserialize(out Max);
        }
        
        public BoundingBoxQueryRequest RosDeserialize(ref ReadBuffer b) => new BoundingBoxQueryRequest(ref b);
        
        public BoundingBoxQueryRequest RosDeserialize(ref ReadBuffer2 b) => new BoundingBoxQueryRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Min);
            b.Serialize(in Max);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in Min);
            b.Serialize(in Max);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Min);
            WriteBuffer2.AddLength(ref c, Max);
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class BoundingBoxQueryResponse : IResponse, IDeserializable<BoundingBoxQueryResponse>
    {
    
        public BoundingBoxQueryResponse()
        {
        }
        
        public BoundingBoxQueryResponse(ref ReadBuffer b)
        {
        }
        
        public BoundingBoxQueryResponse(ref ReadBuffer2 b)
        {
        }
        
        public BoundingBoxQueryResponse RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public BoundingBoxQueryResponse RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static BoundingBoxQueryResponse? singleton;
        public static BoundingBoxQueryResponse Singleton => singleton ??= new BoundingBoxQueryResponse();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int _) { }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
