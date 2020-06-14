using System.Runtime.Serialization;

namespace Iviz.Msgs.Rosapi
{
    [DataContract (Name = "rosapi/Topics")]
    public sealed class Topics : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public TopicsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public TopicsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public Topics()
        {
            Request = new TopicsRequest();
            Response = new TopicsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public Topics(TopicsRequest request)
        {
            Request = request;
            Response = new TopicsResponse();
        }
        
        IService IService.Create() => new Topics();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TopicsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TopicsResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "rosapi/Topics";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "d966d98fc333fa1f3135af765eac1ba8";
    }

    public sealed class TopicsRequest : IRequest
    {
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsRequest()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsRequest(Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TopicsRequest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 0;
    }

    public sealed class TopicsResponse : IResponse
    {
        [DataMember (Name = "topics")] public string[] Topics_ { get; set; }
        [DataMember (Name = "types")] public string[] Types { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicsResponse()
        {
            Topics_ = System.Array.Empty<string>();
            Types = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicsResponse(string[] Topics_, string[] Types)
        {
            this.Topics_ = Topics_;
            this.Types = Types;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicsResponse(Buffer b)
        {
            Topics_ = b.DeserializeStringArray();
            Types = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TopicsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(Topics_, 0);
            b.SerializeArray(Types, 0);
        }
        
        public void RosValidate()
        {
            if (Topics_ is null) throw new System.NullReferenceException();
            for (int i = 0; i < Topics_.Length; i++)
            {
                if (Topics_[i] is null) throw new System.NullReferenceException();
            }
            if (Types is null) throw new System.NullReferenceException();
            for (int i = 0; i < Types.Length; i++)
            {
                if (Types[i] is null) throw new System.NullReferenceException();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 4 * Topics_.Length;
                for (int i = 0; i < Topics_.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Topics_[i]);
                }
                size += 4 * Types.Length;
                for (int i = 0; i < Types.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Types[i]);
                }
                return size;
            }
        }
    }
}
