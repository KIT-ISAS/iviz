using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ListParameters : IService<ListParametersRequest, ListParametersResponse>
    {
        /// Request message.
        [DataMember] public ListParametersRequest Request;
        
        /// Response message.
        [DataMember] public ListParametersResponse Response;
        
        /// Empty constructor.
        public ListParameters()
        {
            Request = new ListParametersRequest();
            Response = new ListParametersResponse();
        }
        
        /// Setter constructor.
        public ListParameters(ListParametersRequest request)
        {
            Request = request;
            Response = new ListParametersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ListParametersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ListParametersResponse)value;
        }
        
        public const string ServiceType = "rcl_interfaces/ListParameters";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "d064321287c33b91a0820c3036dc0b87";
        
        public IService Generate() => new ListParameters();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ListParametersRequest : IRequest<ListParameters, ListParametersResponse>, IDeserializable<ListParametersRequest>
    {
        // Recursively get parameters with unlimited depth.
        public const ulong DEPTH_RECURSIVE = 0;
        // The list of parameter prefixes to query.
        [DataMember (Name = "prefixes")] public string[] Prefixes;
        // Relative depth from given prefixes to return.
        //
        // Use DEPTH_RECURSIVE to get the recursive parameters and prefixes for each prefix.
        [DataMember (Name = "depth")] public ulong Depth;
    
        public ListParametersRequest()
        {
            Prefixes = EmptyArray<string>.Value;
        }
        
        public ListParametersRequest(string[] Prefixes, ulong Depth)
        {
            this.Prefixes = Prefixes;
            this.Depth = Depth;
        }
        
        public ListParametersRequest(ref ReadBuffer b)
        {
            Prefixes = b.DeserializeStringArray();
            b.Deserialize(out Depth);
        }
        
        public ListParametersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Prefixes = b.DeserializeStringArray();
            b.Align8();
            b.Deserialize(out Depth);
        }
        
        public ListParametersRequest RosDeserialize(ref ReadBuffer b) => new ListParametersRequest(ref b);
        
        public ListParametersRequest RosDeserialize(ref ReadBuffer2 b) => new ListParametersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Prefixes.Length);
            b.SerializeArray(Prefixes);
            b.Serialize(Depth);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Prefixes.Length);
            b.SerializeArray(Prefixes);
            b.Align8();
            b.Serialize(Depth);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Prefixes, nameof(Prefixes));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += WriteBuffer.GetArraySize(Prefixes);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Prefixes);
            size = WriteBuffer2.Align8(size);
            size += 8; // Depth
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ListParametersResponse : IResponse, IDeserializable<ListParametersResponse>
    {
        // The list of parameter names and their prefixes.
        [DataMember (Name = "result")] public ListParametersResult Result;
    
        public ListParametersResponse()
        {
            Result = new ListParametersResult();
        }
        
        public ListParametersResponse(ListParametersResult Result)
        {
            this.Result = Result;
        }
        
        public ListParametersResponse(ref ReadBuffer b)
        {
            Result = new ListParametersResult(ref b);
        }
        
        public ListParametersResponse(ref ReadBuffer2 b)
        {
            Result = new ListParametersResult(ref b);
        }
        
        public ListParametersResponse RosDeserialize(ref ReadBuffer b) => new ListParametersResponse(ref b);
        
        public ListParametersResponse RosDeserialize(ref ReadBuffer2 b) => new ListParametersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Result.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Result, nameof(Result));
            Result.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Result.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Result.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
