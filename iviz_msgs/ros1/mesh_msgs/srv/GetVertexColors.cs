using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetVertexColors : IService
    {
        /// Request message.
        [DataMember] public GetVertexColorsRequest Request;
        
        /// Response message.
        [DataMember] public GetVertexColorsResponse Response;
        
        /// Empty constructor.
        public GetVertexColors()
        {
            Request = new GetVertexColorsRequest();
            Response = new GetVertexColorsResponse();
        }
        
        /// Setter constructor.
        public GetVertexColors(GetVertexColorsRequest request)
        {
            Request = request;
            Response = new GetVertexColorsResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetVertexColorsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetVertexColorsResponse)value;
        }
        
        public const string ServiceType = "mesh_msgs/GetVertexColors";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "f9925939094ed9c8a413184db9bca5b3";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetVertexColorsRequest : IRequest<GetVertexColors, GetVertexColorsResponse>, IDeserializable<GetVertexColorsRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        public GetVertexColorsRequest()
        {
            Uuid = "";
        }
        
        public GetVertexColorsRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public GetVertexColorsRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetVertexColorsRequest(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetVertexColorsRequest RosDeserialize(ref ReadBuffer b) => new GetVertexColorsRequest(ref b);
        
        public GetVertexColorsRequest RosDeserialize(ref ReadBuffer2 b) => new GetVertexColorsRequest(ref b);
    
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
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.AddLength(c, Uuid);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetVertexColorsResponse : IResponse, IDeserializable<GetVertexColorsResponse>
    {
        [DataMember (Name = "mesh_vertex_colors_stamped")] public MeshMsgs.MeshVertexColorsStamped MeshVertexColorsStamped;
    
        public GetVertexColorsResponse()
        {
            MeshVertexColorsStamped = new MeshMsgs.MeshVertexColorsStamped();
        }
        
        public GetVertexColorsResponse(MeshMsgs.MeshVertexColorsStamped MeshVertexColorsStamped)
        {
            this.MeshVertexColorsStamped = MeshVertexColorsStamped;
        }
        
        public GetVertexColorsResponse(ref ReadBuffer b)
        {
            MeshVertexColorsStamped = new MeshMsgs.MeshVertexColorsStamped(ref b);
        }
        
        public GetVertexColorsResponse(ref ReadBuffer2 b)
        {
            MeshVertexColorsStamped = new MeshMsgs.MeshVertexColorsStamped(ref b);
        }
        
        public GetVertexColorsResponse RosDeserialize(ref ReadBuffer b) => new GetVertexColorsResponse(ref b);
        
        public GetVertexColorsResponse RosDeserialize(ref ReadBuffer2 b) => new GetVertexColorsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MeshVertexColorsStamped.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            MeshVertexColorsStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshVertexColorsStamped is null) BuiltIns.ThrowNullReference();
            MeshVertexColorsStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshVertexColorsStamped.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = MeshVertexColorsStamped.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
