using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class GetParameterTypes : IService
    {
        /// Request message.
        [DataMember] public GetParameterTypesRequest Request;
        
        /// Response message.
        [DataMember] public GetParameterTypesResponse Response;
        
        /// Empty constructor.
        public GetParameterTypes()
        {
            Request = new GetParameterTypesRequest();
            Response = new GetParameterTypesResponse();
        }
        
        /// Setter constructor.
        public GetParameterTypes(GetParameterTypesRequest request)
        {
            Request = request;
            Response = new GetParameterTypesResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetParameterTypesRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetParameterTypesResponse)value;
        }
        
        public const string ServiceType = "rcl_interfaces/GetParameterTypes";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "ee0005694d9e1e7c4757ee697be4dcc5";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetParameterTypesRequest : IRequest<GetParameterTypes, GetParameterTypesResponse>, IDeserializable<GetParameterTypesRequest>
    {
        // A list of parameter names.
        // TODO(wjwwood): link to parameter naming rules.
        [DataMember (Name = "names")] public string[] Names;
    
        public GetParameterTypesRequest()
        {
            Names = System.Array.Empty<string>();
        }
        
        public GetParameterTypesRequest(string[] Names)
        {
            this.Names = Names;
        }
        
        public GetParameterTypesRequest(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Names);
        }
        
        public GetParameterTypesRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeStringArray(out Names);
        }
        
        public GetParameterTypesRequest RosDeserialize(ref ReadBuffer b) => new GetParameterTypesRequest(ref b);
        
        public GetParameterTypesRequest RosDeserialize(ref ReadBuffer2 b) => new GetParameterTypesRequest(ref b);
    
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
    public sealed class GetParameterTypesResponse : IResponse, IDeserializable<GetParameterTypesResponse>
    {
        // List of types which is the same length and order as the provided names.
        //
        // The type enum is defined in ParameterType.msg. ParameterType.PARAMETER_NOT_SET
        // indicates that the parameter is not currently set.
        [DataMember (Name = "types")] public byte[] Types;
    
        public GetParameterTypesResponse()
        {
            Types = System.Array.Empty<byte>();
        }
        
        public GetParameterTypesResponse(byte[] Types)
        {
            this.Types = Types;
        }
        
        public GetParameterTypesResponse(ref ReadBuffer b)
        {
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Types = n == 0
                    ? System.Array.Empty<byte>()
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Types[0]), n * 1);
                }
            }
        }
        
        public GetParameterTypesResponse(ref ReadBuffer2 b)
        {
            b.Align4();
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Types = n == 0
                    ? System.Array.Empty<byte>()
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Types[0]), n * 1);
                }
            }
        }
        
        public GetParameterTypesResponse RosDeserialize(ref ReadBuffer b) => new GetParameterTypesResponse(ref b);
        
        public GetParameterTypesResponse RosDeserialize(ref ReadBuffer2 b) => new GetParameterTypesResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Types);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Types);
        }
        
        public void RosValidate()
        {
            if (Types is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Types.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Types length
            c += 1 * Types.Length;
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
