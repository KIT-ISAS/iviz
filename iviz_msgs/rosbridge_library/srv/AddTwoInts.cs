using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/AddTwoInts")]
    public sealed class AddTwoInts : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public AddTwoIntsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public AddTwoIntsResponse Response { get; set; }
        
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
        
        IService IService.Create() => new AddTwoInts();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (AddTwoIntsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (AddTwoIntsResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosbridge_library/AddTwoInts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "6a2e34150c00229791cc89ff309fff21";
    }

    [DataContract]
    public sealed class AddTwoIntsRequest : IRequest<AddTwoInts, AddTwoIntsResponse>, IDeserializable<AddTwoIntsRequest>
    {
        [DataMember (Name = "a")] public long A { get; set; }
        [DataMember (Name = "b")] public long B { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddTwoIntsRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddTwoIntsRequest(long A, long B)
        {
            this.A = A;
            this.B = B;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AddTwoIntsRequest(ref Buffer b)
        {
            A = b.Deserialize<long>();
            B = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AddTwoIntsRequest(ref b);
        }
        
        AddTwoIntsRequest IDeserializable<AddTwoIntsRequest>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    }

    [DataContract]
    public sealed class AddTwoIntsResponse : IResponse, IDeserializable<AddTwoIntsResponse>
    {
        [DataMember (Name = "sum")] public long Sum { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddTwoIntsResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddTwoIntsResponse(long Sum)
        {
            this.Sum = Sum;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AddTwoIntsResponse(ref Buffer b)
        {
            Sum = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AddTwoIntsResponse(ref b);
        }
        
        AddTwoIntsResponse IDeserializable<AddTwoIntsResponse>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Sum);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    }
}
