using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class SetParameters : IService
    {
        /// Request message.
        [DataMember] public SetParametersRequest Request;
        
        /// Response message.
        [DataMember] public SetParametersResponse Response;
        
        /// Empty constructor.
        public SetParameters()
        {
            Request = new SetParametersRequest();
            Response = new SetParametersResponse();
        }
        
        /// Setter constructor.
        public SetParameters(SetParametersRequest request)
        {
            Request = request;
            Response = new SetParametersResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetParametersRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetParametersResponse)value;
        }
        
        public const string ServiceType = "rcl_interfaces/SetParameters";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "8f82da95ed562e7d91a7f87d70671d92";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParametersRequest : IRequest<SetParameters, SetParametersResponse>, IDeserializable<SetParametersRequest>
    {
        // A list of parameters to set.
        [DataMember (Name = "parameters")] public Parameter[] Parameters;
    
        public SetParametersRequest()
        {
            Parameters = System.Array.Empty<Parameter>();
        }
        
        public SetParametersRequest(Parameter[] Parameters)
        {
            this.Parameters = Parameters;
        }
        
        public SetParametersRequest(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Parameters = n == 0
                    ? System.Array.Empty<Parameter>()
                    : new Parameter[n];
                for (int i = 0; i < n; i++)
                {
                    Parameters[i] = new Parameter(ref b);
                }
            }
        }
        
        public SetParametersRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Parameters = n == 0
                    ? System.Array.Empty<Parameter>()
                    : new Parameter[n];
                for (int i = 0; i < n; i++)
                {
                    Parameters[i] = new Parameter(ref b);
                }
            }
        }
        
        public SetParametersRequest RosDeserialize(ref ReadBuffer b) => new SetParametersRequest(ref b);
        
        public SetParametersRequest RosDeserialize(ref ReadBuffer2 b) => new SetParametersRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Parameters.Length);
            foreach (var t in Parameters)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Parameters.Length);
            foreach (var t in Parameters)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Parameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Parameters.Length; i++)
            {
                if (Parameters[i] is null) BuiltIns.ThrowNullReference(nameof(Parameters), i);
                Parameters[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Parameters);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Parameters.Length
            foreach (var t in Parameters)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParametersResponse : IResponse, IDeserializable<SetParametersResponse>
    {
        // Indicates whether setting each parameter succeeded or not and why.
        [DataMember (Name = "results")] public SetParametersResult[] Results;
    
        public SetParametersResponse()
        {
            Results = System.Array.Empty<SetParametersResult>();
        }
        
        public SetParametersResponse(SetParametersResult[] Results)
        {
            this.Results = Results;
        }
        
        public SetParametersResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Results = n == 0
                    ? System.Array.Empty<SetParametersResult>()
                    : new SetParametersResult[n];
                for (int i = 0; i < n; i++)
                {
                    Results[i] = new SetParametersResult(ref b);
                }
            }
        }
        
        public SetParametersResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Results = n == 0
                    ? System.Array.Empty<SetParametersResult>()
                    : new SetParametersResult[n];
                for (int i = 0; i < n; i++)
                {
                    Results[i] = new SetParametersResult(ref b);
                }
            }
        }
        
        public SetParametersResponse RosDeserialize(ref ReadBuffer b) => new SetParametersResponse(ref b);
        
        public SetParametersResponse RosDeserialize(ref ReadBuffer2 b) => new SetParametersResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Results is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) BuiltIns.ThrowNullReference(nameof(Results), i);
                Results[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Results);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Results.Length
            foreach (var t in Results)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}