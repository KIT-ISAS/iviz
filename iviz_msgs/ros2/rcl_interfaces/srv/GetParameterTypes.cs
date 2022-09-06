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
            Names = EmptyArray<string>.Value;
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
    public sealed class GetParameterTypesResponse : IResponse, IDeserializable<GetParameterTypesResponse>
    {
        // List of types which is the same length and order as the provided names.
        //
        // The type enum is defined in ParameterType.msg. ParameterType.PARAMETER_NOT_SET
        // indicates that the parameter is not currently set.
        [DataMember (Name = "types")] public byte[] Types;
    
        public GetParameterTypesResponse()
        {
            Types = EmptyArray<byte>.Value;
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
                var array = n == 0
                    ? EmptyArray<byte>.Value
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                Types = array;
            }
        }
        
        public GetParameterTypesResponse(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<byte>.Value
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                Types = array;
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Types.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Types.Length
            size += 1 * Types.Length;
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
