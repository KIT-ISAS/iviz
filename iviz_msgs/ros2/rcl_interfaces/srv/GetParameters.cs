using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class GetParameters : IService
    {
        /// Request message.
        [DataMember] public GetParametersRequest Request;
        
        /// Response message.
        [DataMember] public GetParametersResponse Response;
        
        /// Empty constructor.
        public GetParameters()
        {
            Request = new GetParametersRequest();
            Response = new GetParametersResponse();
        }
        
        /// Setter constructor.
        public GetParameters(GetParametersRequest request)
        {
            Request = request;
            Response = new GetParametersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetParametersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetParametersResponse)value;
        }
        
        public const string ServiceType = "rcl_interfaces/GetParameters";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "8ac544a03a02e4477b9721f3f7c2a9be";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParametersRequest : IRequest<GetParameters, GetParametersResponse>, IDeserializable<GetParametersRequest>
    {
        // TODO(wjwwood): Decide on the rules for grouping, nodes, and parameter "names"
        // in general, then link to that.
        //
        // For more information about parameters and naming rules, see:
        // https://design.ros2.org/articles/ros_parameters.html
        // https://github.com/ros2/design/pull/241
        // A list of parameter names to get.
        [DataMember (Name = "names")] public string[] Names;
    
        public GetParametersRequest()
        {
            Names = EmptyArray<string>.Value;
        }
        
        public GetParametersRequest(string[] Names)
        {
            this.Names = Names;
        }
        
        public GetParametersRequest(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Names);
        }
        
        public GetParametersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeStringArray(out Names);
        }
        
        public GetParametersRequest RosDeserialize(ref ReadBuffer b) => new GetParametersRequest(ref b);
        
        public GetParametersRequest RosDeserialize(ref ReadBuffer2 b) => new GetParametersRequest(ref b);
    
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Names);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
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
    public sealed class GetParametersResponse : IResponse, IDeserializable<GetParametersResponse>
    {
        // List of values which is the same length and order as the provided names. If a
        // parameter was not yet set, the value will have PARAMETER_NOT_SET as the
        // type.
        [DataMember (Name = "values")] public ParameterValue[] Values;
    
        public GetParametersResponse()
        {
            Values = EmptyArray<ParameterValue>.Value;
        }
        
        public GetParametersResponse(ParameterValue[] Values)
        {
            this.Values = Values;
        }
        
        public GetParametersResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ParameterValue>.Value
                    : new ParameterValue[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ParameterValue(ref b);
                }
                Values = array;
            }
        }
        
        public GetParametersResponse(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ParameterValue>.Value
                    : new ParameterValue[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ParameterValue(ref b);
                }
                Values = array;
            }
        }
        
        public GetParametersResponse RosDeserialize(ref ReadBuffer b) => new GetParametersResponse(ref b);
        
        public GetParametersResponse RosDeserialize(ref ReadBuffer2 b) => new GetParametersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Values.Length);
            foreach (var t in Values)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Values.Length);
            foreach (var t in Values)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Values is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] is null) BuiltIns.ThrowNullReference(nameof(Values), i);
                Values[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Values) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Values.Length
            foreach (var msg in Values) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
