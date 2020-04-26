using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class AddTwoInts : IService
    {
        /// <summary> Request message. </summary>
        public AddTwoIntsRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public AddTwoIntsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public AddTwoInts()
        {
            Request = new AddTwoIntsRequest();
            Response = new AddTwoIntsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public AddTwoInts(AddTwoIntsRequest request)
        {
            Request = request;
            Response = new AddTwoIntsResponse();
        }
        
        public IService Create() => new AddTwoInts();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        public const string RosServiceType = "rosbridge_library/AddTwoInts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        public const string RosMd5Sum = "6a2e34150c00229791cc89ff309fff21";
    }

    public sealed class AddTwoIntsRequest : IRequest
    {
        public long a;
        public long b;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out a, ref ptr, end);
            BuiltIns.Deserialize(out b, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(a, ref ptr, end);
            BuiltIns.Serialize(b, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 16;
    }

    public sealed class AddTwoIntsResponse : IResponse
    {
        public long sum;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out sum, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(sum, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }
}
