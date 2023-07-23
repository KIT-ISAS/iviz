using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class DescribeParameters : IService<DescribeParametersRequest, DescribeParametersResponse>
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
        
        public IService Generate() => new DescribeParameters();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class DescribeParametersRequest : IRequest<DescribeParameters, DescribeParametersResponse>, IDeserializable<DescribeParametersRequest>
    {
        // A list of parameters of which to get the descriptor.
        [DataMember (Name = "names")] public string[] Names;
    
        public DescribeParametersRequest()
        {
            Names = EmptyArray<string>.Value;
        }
        
        public DescribeParametersRequest(string[] Names)
        {
            this.Names = Names;
        }
        
        public DescribeParametersRequest(ref ReadBuffer b)
        {
            Names = b.DeserializeStringArray();
        }
        
        public DescribeParametersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Names = b.DeserializeStringArray();
        }
        
        public DescribeParametersRequest RosDeserialize(ref ReadBuffer b) => new DescribeParametersRequest(ref b);
        
        public DescribeParametersRequest RosDeserialize(ref ReadBuffer2 b) => new DescribeParametersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Names.Length);
            b.SerializeArray(Names);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Names.Length);
            b.SerializeArray(Names);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Names, nameof(Names));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Names);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Names);
            return size;
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
            Descriptors = EmptyArray<ParameterDescriptor>.Value;
        }
        
        public DescribeParametersResponse(ParameterDescriptor[] Descriptors)
        {
            this.Descriptors = Descriptors;
        }
        
        public DescribeParametersResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                ParameterDescriptor[] array;
                if (n == 0) array = EmptyArray<ParameterDescriptor>.Value;
                else
                {
                    array = new ParameterDescriptor[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ParameterDescriptor(ref b);
                    }
                }
                Descriptors = array;
            }
        }
        
        public DescribeParametersResponse(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                ParameterDescriptor[] array;
                if (n == 0) array = EmptyArray<ParameterDescriptor>.Value;
                else
                {
                    array = new ParameterDescriptor[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ParameterDescriptor(ref b);
                    }
                }
                Descriptors = array;
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
            b.Align4();
            b.Serialize(Descriptors.Length);
            foreach (var t in Descriptors)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Descriptors, nameof(Descriptors));
            foreach (var msg in Descriptors) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Descriptors) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Descriptors.Length
            foreach (var msg in Descriptors) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
