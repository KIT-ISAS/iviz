using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class GetLabeledClusters : IService
    {
        /// Request message.
        [DataMember] public GetLabeledClustersRequest Request;
        
        /// Response message.
        [DataMember] public GetLabeledClustersResponse Response;
        
        /// Empty constructor.
        public GetLabeledClusters()
        {
            Request = new GetLabeledClustersRequest();
            Response = new GetLabeledClustersResponse();
        }
        
        /// Setter constructor.
        public GetLabeledClusters(GetLabeledClustersRequest request)
        {
            Request = request;
            Response = new GetLabeledClustersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetLabeledClustersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetLabeledClustersResponse)value;
        }
        
        public const string ServiceType = "mesh_msgs/GetLabeledClusters";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "d4165053db3e9b1ffe9db49f0702734c";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetLabeledClustersRequest : IRequest<GetLabeledClusters, GetLabeledClustersResponse>, IDeserializable<GetLabeledClustersRequest>
    {
        [DataMember (Name = "uuid")] public string Uuid;
    
        public GetLabeledClustersRequest()
        {
            Uuid = "";
        }
        
        public GetLabeledClustersRequest(string Uuid)
        {
            this.Uuid = Uuid;
        }
        
        public GetLabeledClustersRequest(ref ReadBuffer b)
        {
            Uuid = b.DeserializeString();
        }
        
        public GetLabeledClustersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Uuid = b.DeserializeString();
        }
        
        public GetLabeledClustersRequest RosDeserialize(ref ReadBuffer b) => new GetLabeledClustersRequest(ref b);
        
        public GetLabeledClustersRequest RosDeserialize(ref ReadBuffer2 b) => new GetLabeledClustersRequest(ref b);
    
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
    public sealed class GetLabeledClustersResponse : IResponse, IDeserializable<GetLabeledClustersResponse>
    {
        [DataMember (Name = "clusters")] public MeshFaceCluster[] Clusters;
    
        public GetLabeledClustersResponse()
        {
            Clusters = EmptyArray<MeshFaceCluster>.Value;
        }
        
        public GetLabeledClustersResponse(MeshFaceCluster[] Clusters)
        {
            this.Clusters = Clusters;
        }
        
        public GetLabeledClustersResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                MeshFaceCluster[] array;
                if (n == 0) array = EmptyArray<MeshFaceCluster>.Value;
                else
                {
                    array = new MeshFaceCluster[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshFaceCluster(ref b);
                    }
                }
                Clusters = array;
            }
        }
        
        public GetLabeledClustersResponse(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                MeshFaceCluster[] array;
                if (n == 0) array = EmptyArray<MeshFaceCluster>.Value;
                else
                {
                    array = new MeshFaceCluster[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MeshFaceCluster(ref b);
                    }
                }
                Clusters = array;
            }
        }
        
        public GetLabeledClustersResponse RosDeserialize(ref ReadBuffer b) => new GetLabeledClustersResponse(ref b);
        
        public GetLabeledClustersResponse RosDeserialize(ref ReadBuffer2 b) => new GetLabeledClustersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Clusters.Length);
            foreach (var t in Clusters)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Clusters.Length);
            foreach (var t in Clusters)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Clusters, nameof(Clusters));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Clusters) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Clusters.Length
            foreach (var msg in Clusters) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
