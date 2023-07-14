using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class SetParametersAtomically : IService<SetParametersAtomicallyRequest, SetParametersAtomicallyResponse>
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
        
        public IService Generate() => new SetParametersAtomically();
        
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
                Parameter[] array;
                if (n == 0) array = EmptyArray<Parameter>.Value;
                else
                {
                    array = new Parameter[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Parameter(ref b);
                    }
                }
                Parameters = array;
            }
        }
        
        public SetParametersAtomicallyRequest(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Parameter[] array;
                if (n == 0) array = EmptyArray<Parameter>.Value;
                else
                {
                    array = new Parameter[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Parameter(ref b);
                    }
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
            b.Align4();
            b.Serialize(Parameters.Length);
            foreach (var t in Parameters)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Parameters, nameof(Parameters));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Parameters) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
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
