using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = RosServiceType)]
    public sealed class AddTwoInts : IService
    {
        /// Request message.
        [DataMember] public AddTwoIntsRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public AddTwoIntsResponse Response { get; set; }
        
        /// Empty constructor.
        public AddTwoInts()
        {
            Request = new AddTwoIntsRequest();
            Response = new AddTwoIntsResponse();
        }
        
        /// Setter constructor.
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
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "rosbridge_library/AddTwoInts";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "6a2e34150c00229791cc89ff309fff21";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddTwoIntsRequest : IRequest<AddTwoInts, AddTwoIntsResponse>, IDeserializable<AddTwoIntsRequest>
    {
        [DataMember (Name = "a")] public long A;
        [DataMember (Name = "b")] public long B;
    
        /// Constructor for empty message.
        public AddTwoIntsRequest()
        {
        }
        
        /// Explicit constructor.
        public AddTwoIntsRequest(long A, long B)
        {
            this.A = A;
            this.B = B;
        }
        
        /// Constructor with buffer.
        internal AddTwoIntsRequest(ref ReadBuffer b)
        {
            A = b.Deserialize<long>();
            B = b.Deserialize<long>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AddTwoIntsRequest(ref b);
        
        public AddTwoIntsRequest RosDeserialize(ref ReadBuffer b) => new AddTwoIntsRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class AddTwoIntsResponse : IResponse, IDeserializable<AddTwoIntsResponse>
    {
        [DataMember (Name = "sum")] public long Sum;
    
        /// Constructor for empty message.
        public AddTwoIntsResponse()
        {
        }
        
        /// Explicit constructor.
        public AddTwoIntsResponse(long Sum)
        {
            this.Sum = Sum;
        }
        
        /// Constructor with buffer.
        internal AddTwoIntsResponse(ref ReadBuffer b)
        {
            Sum = b.Deserialize<long>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AddTwoIntsResponse(ref b);
        
        public AddTwoIntsResponse RosDeserialize(ref ReadBuffer b) => new AddTwoIntsResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Sum);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
