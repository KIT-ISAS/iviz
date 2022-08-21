using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetGeometry : IService
    {
        /// Request message.
        [DataMember] public GetGeometryRequest Request;
        
        /// Response message.
        [DataMember] public GetGeometryResponse Response;
        
        /// Empty constructor.
        public GetGeometry()
        {
            Request = new GetGeometryRequest();
            Response = new GetGeometryResponse();
        }
        
        /// Setter constructor.
        public GetGeometry(GetGeometryRequest request)
        {
            Request = request;
            Response = new GetGeometryResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetGeometryRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetGeometryResponse)value;
        }
        
        public const string ServiceType = "mesh_msgs/GetGeometry";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "e21c42f8a3978429fcbcd1c03ddeb4e3";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGeometryRequest : IRequest<GetGeometry, GetGeometryResponse>, IDeserializable<GetGeometryRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        public GetGeometryRequest()
        {
            Uuid = "";
        }
        
        public GetGeometryRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public GetGeometryRequest(ref ReadBuffer b)
        {
            b.DeserializeString(out Uuid);
        }
        
        public GetGeometryRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Uuid);
        }
        
        public GetGeometryRequest RosDeserialize(ref ReadBuffer b) => new GetGeometryRequest(ref b);
        
        public GetGeometryRequest RosDeserialize(ref ReadBuffer2 b) => new GetGeometryRequest(ref b);
    
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
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Uuid);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetGeometryResponse : IResponse, IDeserializable<GetGeometryResponse>
    {
        [DataMember (Name = "mesh_geometry_stamped")] public MeshMsgs.MeshGeometryStamped MeshGeometryStamped;
    
        public GetGeometryResponse()
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped();
        }
        
        public GetGeometryResponse(MeshMsgs.MeshGeometryStamped MeshGeometryStamped)
        {
            this.MeshGeometryStamped = MeshGeometryStamped;
        }
        
        public GetGeometryResponse(ref ReadBuffer b)
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped(ref b);
        }
        
        public GetGeometryResponse(ref ReadBuffer2 b)
        {
            MeshGeometryStamped = new MeshMsgs.MeshGeometryStamped(ref b);
        }
        
        public GetGeometryResponse RosDeserialize(ref ReadBuffer b) => new GetGeometryResponse(ref b);
        
        public GetGeometryResponse RosDeserialize(ref ReadBuffer2 b) => new GetGeometryResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            MeshGeometryStamped.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            MeshGeometryStamped.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (MeshGeometryStamped is null) BuiltIns.ThrowNullReference();
            MeshGeometryStamped.RosValidate();
        }
    
        public int RosMessageLength => 0 + MeshGeometryStamped.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = MeshGeometryStamped.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}