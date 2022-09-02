using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class SetParametersAtomically : IService
    {
        /// Request message.
        [DataMember] public SetParametersAtomicallyRequest Request;
        
        /// Response message.
        [DataMember] public SetParametersAtomicallyResponse Response;
        
        /// Empty constructor.
        public SetParametersAtomically()
        {
            Request = new SetParametersAtomicallyRequest();
            Response = new SetParametersAtomicallyResponse();
        }
        
        /// Setter constructor.
        public SetParametersAtomically(SetParametersAtomicallyRequest request)
        {
            Request = request;
            Response = new SetParametersAtomicallyResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (SetParametersAtomicallyRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (SetParametersAtomicallyResponse)value;
        }
        
        public const string ServiceType = "rcl_interfaces/SetParametersAtomically";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "b212f806e7a333d89be2051122e406bf";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParametersAtomicallyRequest : IRequest<SetParametersAtomically, SetParametersAtomicallyResponse>, IDeserializable<SetParametersAtomicallyRequest>
    {
        // A list of parameters to set atomically.
        //
        // This call will either set all values, or none of the values.
        [DataMember (Name = "parameters")] public Parameter[] Parameters;
    
        public SetParametersAtomicallyRequest()
        {
            Parameters = EmptyArray<Parameter>.Value;
        }
        
        public SetParametersAtomicallyRequest(Parameter[] Parameters)
        {
            this.Parameters = Parameters;
        }
        
        public SetParametersAtomicallyRequest(ref ReadBuffer b)
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
        
        public SetParametersAtomicallyRequest(ref ReadBuffer2 b)
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
        
        public SetParametersAtomicallyRequest RosDeserialize(ref ReadBuffer b) => new SetParametersAtomicallyRequest(ref b);
        
        public SetParametersAtomicallyRequest RosDeserialize(ref ReadBuffer2 b) => new SetParametersAtomicallyRequest(ref b);
    
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
            for (int i = 0; i < Parameters.Length; i++)
            {
                c = Parameters[i].AddRos2MessageLength(c);
            }
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class SetParametersAtomicallyResponse : IResponse, IDeserializable<SetParametersAtomicallyResponse>
    {
        // Indicates whether setting all of the parameters succeeded or not and why.
        [DataMember (Name = "result")] public SetParametersResult Result;
    
        public SetParametersAtomicallyResponse()
        {
            Result = new SetParametersResult();
        }
        
        public SetParametersAtomicallyResponse(SetParametersResult Result)
        {
            this.Result = Result;
        }
        
        public SetParametersAtomicallyResponse(ref ReadBuffer b)
        {
            Result = new SetParametersResult(ref b);
        }
        
        public SetParametersAtomicallyResponse(ref ReadBuffer2 b)
        {
            Result = new SetParametersResult(ref b);
        }
        
        public SetParametersAtomicallyResponse RosDeserialize(ref ReadBuffer b) => new SetParametersAtomicallyResponse(ref b);
        
        public SetParametersAtomicallyResponse RosDeserialize(ref ReadBuffer2 b) => new SetParametersAtomicallyResponse(ref b);
    
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
            if (Result is null) BuiltIns.ThrowNullReference();
            Result.RosValidate();
        }
    
        public int RosMessageLength => 0 + Result.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Result.AddRos2MessageLength(c);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
