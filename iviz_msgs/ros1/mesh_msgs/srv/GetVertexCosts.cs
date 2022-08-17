using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetVertexCosts : IService
    {
        /// Request message.
        [DataMember] public GetVertexCostsRequest Request;
        
        /// Response message.
        [DataMember] public GetVertexCostsResponse Response;
        
        /// Empty constructor.
        public GetVertexCosts()
        {
            Request = new GetVertexCostsRequest();
            Response = new GetVertexCostsResponse();
        }
        
        /// Setter constructor.
        public GetVertexCosts(GetVertexCostsRequest request)
        {
            Request = request;
            Response = new GetVertexCostsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetVertexCostsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetVertexCostsResponse)value;
        }
        
        public const string ServiceType = "mesh_msgs/GetVertexCosts";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "d0fc06ce39b58884e8cdf147765b9d6b";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetVertexCostsRequest : IRequest<GetVertexCosts, GetVertexCostsResponse>, IDeserializable<GetVertexCostsRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        public GetVertexCostsRequest()
        {
            Uuid = "";
        }
        
        public GetVertexCostsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public GetVertexCostsRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetVertexCostsRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetVertexCostsRequest RosDeserialize(ref ReadBuffer b) => new GetVertexCostsRequest(ref b);
        
        public GetVertexCostsRequest RosDeserialize(ref ReadBuffer2 b) => new GetVertexCostsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uuid);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.AddLength(c, Uuid);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetVertexCostsResponse : IResponse, IDeserializable<GetVertexCostsResponse>
    {
        [DataMember (Name = "mesh_vertex_costs_stamped")] public MeshMsgs.MeshVertexCostsStamped MeshVertexCostsStamped;
    
        public GetVertexCostsResponse()
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped();
        }
        
        public GetVertexCostsResponse(MeshMsgs.MeshVertexCostsStamped MeshVertexCostsStamped)
        {
            this.MeshVertexCostsStamped = MeshVertexCostsStamped;
        }
        
        public GetVertexCostsResponse(ref ReadBuffer b)
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped(ref b);
        }
        
        public GetVertexCostsResponse(ref ReadBuffer2 b)
        {
            MeshVertexCostsStamped = new MeshMsgs.MeshVertexCostsStamped(ref b);
        }
        
        public GetVertexCostsResponse RosDeserialize(ref ReadBuffer b) => new GetVertexCostsResponse(ref b);
        
        public GetVertexCostsResponse RosDeserialize(ref ReadBuffer2 b) => new GetVertexCostsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MeshVertexCostsStamped.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            MeshVertexCostsStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshVertexCostsStamped is null) BuiltIns.ThrowNullReference();
            MeshVertexCostsStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshVertexCostsStamped.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = MeshVertexCostsStamped.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
