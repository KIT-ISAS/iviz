using System.Runtime.Serialization;

namespace Iviz.Msgs.rosapi
{
    public sealed class MessageDetails : IService
    {
        /// <summary> Request message. </summary>
        public MessageDetailsRequest Request { get; }
        
        /// <summary> Response message. </summary>
        public MessageDetailsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public MessageDetails()
        {
            Request = new MessageDetailsRequest();
            Response = new MessageDetailsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public MessageDetails(MessageDetailsRequest request)
        {
            Request = request;
            Response = new MessageDetailsResponse();
        }
        
        public IService Create() => new MessageDetails();
        
        IRequest IService.Request => Request;
        
        IResponse IService.Response => Response;
        
        public string ErrorMessage { get; set; }
        
        [IgnoreDataMember]
        public string RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve]
        public const string RosServiceType = "rosapi/MessageDetails";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public const string RosMd5Sum = "f9c88144f6f6bd888dd99d4e0411905d";
    }

    public sealed class MessageDetailsRequest : IRequest
    {
        public string type;
    
        /// <summary> Constructor for empty message. </summary>
        public MessageDetailsRequest()
        {
            type = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out type, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(type, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(type);
                return size;
            }
        }
    }

    public sealed class MessageDetailsResponse : IResponse
    {
        public TypeDef[] typedefs;
    
        /// <summary> Constructor for empty message. </summary>
        public MessageDetailsResponse()
        {
            typedefs = System.Array.Empty<TypeDef>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out typedefs, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(typedefs, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                for (int i = 0; i < typedefs.Length; i++)
                {
                    size += typedefs[i].RosMessageLength;
                }
                return size;
            }
        }
    }
}
