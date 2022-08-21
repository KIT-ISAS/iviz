using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class DescribeParameters : IService
    {
        /// Request message.
        [DataMember] public DescribeParametersRequest Request;
        
        /// Response message.
        [DataMember] public DescribeParametersResponse Response;
        
        /// Empty constructor.
        public DescribeParameters()
        {
            Request = new DescribeParametersRequest();
            Response = new DescribeParametersResponse();
        }
        
        /// Setter constructor.
        public DescribeParameters(DescribeParametersRequest request)
        {
            Request = request;
            Response = new DescribeParametersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (DescribeParametersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (DescribeParametersResponse)value;
        }
        
        public const string ServiceType = "rcl_interfaces/DescribeParameters";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "585bd903c45022c2d4608266d2877f64";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DescribeParametersRequest : IRequest<DescribeParameters, DescribeParametersResponse>, IDeserializable<DescribeParametersRequest>
    {
        // A list of parameters of which to get the descriptor.
        [DataMember (Name = "names")] public string[] Names;
    
        public DescribeParametersRequest()
        {
            Names = System.Array.Empty<string>();
        }
        
        public DescribeParametersRequest(string[] Names)
        {
            this.Names = Names;
        }
        
        public DescribeParametersRequest(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Names);
        }
        
        public DescribeParametersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeStringArray(out Names);
        }
        
        public DescribeParametersRequest RosDeserialize(ref ReadBuffer b) => new DescribeParametersRequest(ref b);
        
        public DescribeParametersRequest RosDeserialize(ref ReadBuffer2 b) => new DescribeParametersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Names);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Names);
        }
        
        public void RosValidate()
        {
            if (Names is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Names.Length; i++)
            {
                if (Names[i] is null) BuiltIns.ThrowNullReference(nameof(Names), i);
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Names);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Names);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DescribeParametersResponse : IResponse, IDeserializable<DescribeParametersResponse>
    {
        // A list of the descriptors of all parameters requested in the same order
        // as they were requested. This list has the same length as the list of
        // parameters requested.
        [DataMember (Name = "descriptors")] public ParameterDescriptor[] Descriptors;
    
        public DescribeParametersResponse()
        {
            Descriptors = System.Array.Empty<ParameterDescriptor>();
        }
        
        public DescribeParametersResponse(ParameterDescriptor[] Descriptors)
        {
            this.Descriptors = Descriptors;
        }
        
        public DescribeParametersResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Descriptors = n == 0
                    ? System.Array.Empty<ParameterDescriptor>()
                    : new ParameterDescriptor[n];
                for (int i = 0; i < n; i++)
                {
                    Descriptors[i] = new ParameterDescriptor(ref b);
                }
            }
        }
        
        public DescribeParametersResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Descriptors = n == 0
                    ? System.Array.Empty<ParameterDescriptor>()
                    : new ParameterDescriptor[n];
                for (int i = 0; i < n; i++)
                {
                    Descriptors[i] = new ParameterDescriptor(ref b);
                }
            }
        }
        
        public DescribeParametersResponse RosDeserialize(ref ReadBuffer b) => new DescribeParametersResponse(ref b);
        
        public DescribeParametersResponse RosDeserialize(ref ReadBuffer2 b) => new DescribeParametersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Descriptors.Length);
            foreach (var t in Descriptors)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Descriptors.Length);
            foreach (var t in Descriptors)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Descriptors is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Descriptors.Length; i++)
            {
                if (Descriptors[i] is null) BuiltIns.ThrowNullReference(nameof(Descriptors), i);
                Descriptors[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Descriptors);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Descriptors.Length
            foreach (var t in Descriptors)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
