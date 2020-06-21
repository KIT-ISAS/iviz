using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/ListModels")]
    public sealed class ListModels : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public ListModelsRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public ListModelsResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public ListModels()
        {
            Request = new ListModelsRequest();
            Response = new ListModelsResponse();
        }
        
        /// <summary> Setter constructor. </summary>
        public ListModels(ListModelsRequest request)
        {
            Request = request;
            Response = new ListModelsResponse();
        }
        
        IService IService.Create() => new ListModels();
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ListModelsRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ListModelsResponse)value;
        }
        
        public string ErrorMessage { get; set; }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/ListModels";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "0f06eeccd3243d148ef280821e49b2c8";
    }

    public sealed class ListModelsRequest : Internal.EmptyRequest
    {
    }

    public sealed class ListModelsResponse : IResponse
    {
        [DataMember (Name = "uris")] public string[] Uris { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ListModelsResponse()
        {
            Uris = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ListModelsResponse(string[] Uris)
        {
            this.Uris = Uris;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ListModelsResponse(Buffer b)
        {
            Uris = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new ListModelsResponse(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(Uris, 0);
        }
        
        public void RosValidate()
        {
            if (Uris is null) throw new System.NullReferenceException();
            for (int i = 0; i < Uris.Length; i++)
            {
                if (Uris[i] is null) throw new System.NullReferenceException();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Uris.Length;
                for (int i = 0; i < Uris.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(Uris[i]);
                }
                return size;
            }
        }
    }
}
