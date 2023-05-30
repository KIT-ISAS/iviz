using System.Runtime.CompilerServices;
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
            Uuid = b.DeserializeString();
        }
        
        public GetGeometryRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Uuid = b.DeserializeString();
        }
        
        public GetGeometryRequest RosDeserialize(ref ReadBuffer b) => new GetGeometryRequest(ref b);
        
        public GetGeometryRequest RosDeserialize(ref ReadBuffer2 b) => new GetGeometryRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Uuid);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Uuid, nameof(Uuid));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Uuid);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            return size;
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
            BuiltIns.ThrowIfNull(MeshGeometryStamped, nameof(MeshGeometryStamped));
            MeshGeometryStamped.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += MeshGeometryStamped.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = MeshGeometryStamped.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
