using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetVertexColors : IService<GetVertexColorsRequest, GetVertexColorsResponse>
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
        
        public IService Generate() => new GetVertexColors();
        
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
            Uuid = b.DeserializeString();
        }
        
        public GetVertexColorsRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Uuid = b.DeserializeString();
        }
        
        public GetVertexColorsRequest RosDeserialize(ref ReadBuffer b) => new GetVertexColorsRequest(ref b);
        
        public GetVertexColorsRequest RosDeserialize(ref ReadBuffer2 b) => new GetVertexColorsRequest(ref b);
    
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
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Uuid);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
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
            BuiltIns.ThrowIfNull(MeshVertexColorsStamped, nameof(MeshVertexColorsStamped));
            MeshVertexColorsStamped.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += MeshVertexColorsStamped.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = MeshVertexColorsStamped.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
