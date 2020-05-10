using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class AddTwoInts : IService
    {
        /// <summary> Request message. </summary>
        public AddTwoIntsRequest Request { get; set; }
        
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
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosbridge_library/AddTwoInts";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "6a2e34150c00229791cc89ff309fff21";
    }

    public sealed class AddTwoIntsRequest : IRequest
    {
        public long a { get; set; }
        public long b { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddTwoIntsRequest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddTwoIntsRequest(long a, long b)
        {
            this.a = a;
            this.b = b;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddTwoIntsRequest(Buffer b)
        {
            this.a = b.Deserialize<long>();
            this.b = b.Deserialize<long>();
        }
        
        public IRequest Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new AddTwoIntsRequest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.a);
            b.Serialize(this.b);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 16;
    }

    public sealed class AddTwoIntsResponse : IResponse
    {
        public long sum { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AddTwoIntsResponse()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public AddTwoIntsResponse(long sum)
        {
            this.sum = sum;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AddTwoIntsResponse(Buffer b)
        {
            this.sum = b.Deserialize<long>();
        }
        
        public IResponse Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new AddTwoIntsResponse(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.sum);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    }
}
