using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = "moveit_msgs/QueryPlannerInterfaces")]
    public sealed class QueryPlannerInterfaces : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public QueryPlannerInterfacesRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public QueryPlannerInterfacesResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public QueryPlannerInterfaces()
        {
            Request = QueryPlannerInterfacesRequest.Singleton;
            Response = new QueryPlannerInterfacesResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public QueryPlannerInterfaces(QueryPlannerInterfacesRequest request)
        {
            Request = request;
            Response = new QueryPlannerInterfacesResponse();
        }
        
        IService IService.Create() => new QueryPlannerInterfaces();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (QueryPlannerInterfacesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (QueryPlannerInterfacesResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "moveit_msgs/QueryPlannerInterfaces";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "acd3317a4c5631f33127fb428209db05";
    }

    [DataContract]
    public sealed class QueryPlannerInterfacesRequest : IRequest, IDeserializable<QueryPlannerInterfacesRequest>
    {
    
        /// <summary> Constructor for empty message. </summary>
        public QueryPlannerInterfacesRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public QueryPlannerInterfacesRequest(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        QueryPlannerInterfacesRequest IDeserializable<QueryPlannerInterfacesRequest>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly QueryPlannerInterfacesRequest Singleton = new QueryPlannerInterfacesRequest();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class QueryPlannerInterfacesResponse : IResponse, IDeserializable<QueryPlannerInterfacesResponse>
    {
        // The planning instances that could be used in the benchmark
        [DataMember (Name = "planner_interfaces")] public PlannerInterfaceDescription[] PlannerInterfaces { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public QueryPlannerInterfacesResponse()
        {
            PlannerInterfaces = System.Array.Empty<PlannerInterfaceDescription>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public QueryPlannerInterfacesResponse(PlannerInterfaceDescription[] PlannerInterfaces)
        {
            this.PlannerInterfaces = PlannerInterfaces;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public QueryPlannerInterfacesResponse(ref Buffer b)
        {
            PlannerInterfaces = b.DeserializeArray<PlannerInterfaceDescription>();
            for (int i = 0; i < PlannerInterfaces.Length; i++)
            {
                PlannerInterfaces[i] = new PlannerInterfaceDescription(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new QueryPlannerInterfacesResponse(ref b);
        }
        
        QueryPlannerInterfacesResponse IDeserializable<QueryPlannerInterfacesResponse>.RosDeserialize(ref Buffer b)
        {
            return new QueryPlannerInterfacesResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(PlannerInterfaces, 0);
        }
        
        public void RosValidate()
        {
            if (PlannerInterfaces is null) throw new System.NullReferenceException(nameof(PlannerInterfaces));
            for (int i = 0; i < PlannerInterfaces.Length; i++)
            {
                if (PlannerInterfaces[i] is null) throw new System.NullReferenceException($"{nameof(PlannerInterfaces)}[{i}]");
                PlannerInterfaces[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in PlannerInterfaces)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    }
}
