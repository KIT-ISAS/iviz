using System.Runtime.CompilerServices;
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
            Parameters = EmptyArray<Parameter>.Value;
        }
        
        public SetParametersRequest(Parameter[] Parameters)
        {
            this.Parameters = Parameters;
        }
        
        public SetParametersRequest(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Parameter>.Value
                    : new Parameter[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Parameter(ref b);
                }
                Parameters = array;
            }
        }
        
        public SetParametersRequest(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Parameter>.Value
                    : new Parameter[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Parameter(ref b);
                }
                Parameters = array;
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Parameters) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Parameters.Length
            foreach (var msg in Parameters) size = msg.AddRos2MessageLength(size);
            return size;
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
            Results = EmptyArray<SetParametersResult>.Value;
        }
        
        public SetParametersResponse(SetParametersResult[] Results)
        {
            this.Results = Results;
        }
        
        public SetParametersResponse(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<SetParametersResult>.Value
                    : new SetParametersResult[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new SetParametersResult(ref b);
                }
                Results = array;
            }
        }
        
        public SetParametersResponse(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<SetParametersResult>.Value
                    : new SetParametersResult[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new SetParametersResult(ref b);
                }
                Results = array;
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Results) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Results.Length
            foreach (var msg in Results) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
