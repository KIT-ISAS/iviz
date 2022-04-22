using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class QueryPlannerInterfaces : IService
    {
        /// Request message.
        [DataMember] public QueryPlannerInterfacesRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public QueryPlannerInterfacesResponse Response { get; set; }
        
        /// Empty constructor.
        public QueryPlannerInterfaces()
        {
            Request = QueryPlannerInterfacesRequest.Singleton;
            Response = new QueryPlannerInterfacesResponse();
        }
        
        /// Setter constructor.
        public QueryPlannerInterfaces(QueryPlannerInterfacesRequest request)
        {
            Request = request;
            Response = new QueryPlannerInterfacesResponse();
        }
        
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
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "moveit_msgs/QueryPlannerInterfaces";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "acd3317a4c5631f33127fb428209db05";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class QueryPlannerInterfacesRequest : IRequest<QueryPlannerInterfaces, QueryPlannerInterfacesResponse>, IDeserializable<QueryPlannerInterfacesRequest>
    {
    
        /// Constructor for empty message.
        public QueryPlannerInterfacesRequest()
        {
        }
        
        /// Constructor with buffer.
        public QueryPlannerInterfacesRequest(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public QueryPlannerInterfacesRequest RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static QueryPlannerInterfacesRequest? singleton;
        public static QueryPlannerInterfacesRequest Singleton => singleton ??= new QueryPlannerInterfacesRequest();
    
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
    public sealed class QueryPlannerInterfacesResponse : IResponse, IDeserializable<QueryPlannerInterfacesResponse>
    {
        // The planning instances that could be used in the benchmark
        [DataMember (Name = "planner_interfaces")] public PlannerInterfaceDescription[] PlannerInterfaces;
    
        /// Constructor for empty message.
        public QueryPlannerInterfacesResponse()
        {
            PlannerInterfaces = System.Array.Empty<PlannerInterfaceDescription>();
        }
        
        /// Explicit constructor.
        public QueryPlannerInterfacesResponse(PlannerInterfaceDescription[] PlannerInterfaces)
        {
            this.PlannerInterfaces = PlannerInterfaces;
        }
        
        /// Constructor with buffer.
        public QueryPlannerInterfacesResponse(ref ReadBuffer b)
        {
            PlannerInterfaces = b.DeserializeArray<PlannerInterfaceDescription>();
            for (int i = 0; i < PlannerInterfaces.Length; i++)
            {
                PlannerInterfaces[i] = new PlannerInterfaceDescription(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new QueryPlannerInterfacesResponse(ref b);
        
        public QueryPlannerInterfacesResponse RosDeserialize(ref ReadBuffer b) => new QueryPlannerInterfacesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(PlannerInterfaces);
        }
        
        public void RosValidate()
        {
            if (PlannerInterfaces is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < PlannerInterfaces.Length; i++)
            {
                if (PlannerInterfaces[i] is null) BuiltIns.ThrowNullReference($"{nameof(PlannerInterfaces)}[{i}]");
                PlannerInterfaces[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(PlannerInterfaces);
    
        public override string ToString() => Extensions.ToString(this);
    }
}
