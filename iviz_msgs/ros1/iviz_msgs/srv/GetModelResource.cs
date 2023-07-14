using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetModelResource : IService<GetModelResourceRequest, GetModelResourceResponse>
    {
        /// Request message.
        [DataMember] public GetModelResourceRequest Request;
        
        /// Response message.
        [DataMember] public GetModelResourceResponse Response;
        
        /// Empty constructor.
        public GetModelResource()
        {
            Request = new GetModelResourceRequest();
            Response = new GetModelResourceResponse();
        }
        
        /// Setter constructor.
        public GetModelResource(GetModelResourceRequest request)
        {
            Request = request;
            Response = new GetModelResourceResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetModelResourceRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetModelResourceResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/GetModelResource";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "a67de8e71bc8e03882d5d86e64000b51";
        
        public IService Generate() => new GetModelResource();
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelResourceRequest : IRequest<GetModelResource, GetModelResourceResponse>, IDeserializable<GetModelResourceRequest>
    {
        // Retrieves a 3D model, and converts it into a format that can be used in iviz
        /// <summary> Uri of the file. Example: package://some_package/file.dae </summary>
        [DataMember (Name = "uri")] public string Uri;
    
        public GetModelResourceRequest()
        {
            Uri = "";
        }
        
        public GetModelResourceRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        public GetModelResourceRequest(ref ReadBuffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public GetModelResourceRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Uri = b.DeserializeString();
        }
        
        public GetModelResourceRequest RosDeserialize(ref ReadBuffer b) => new GetModelResourceRequest(ref b);
        
        public GetModelResourceRequest RosDeserialize(ref ReadBuffer2 b) => new GetModelResourceRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Uri, nameof(Uri));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Uri);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uri);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetModelResourceResponse : IResponse, IDeserializable<GetModelResourceResponse>
    {
        /// <summary> Whether the retrieval succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> The 3D model </summary>
        [DataMember (Name = "model")] public Model Model;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        public GetModelResourceResponse()
        {
            Model = new Model();
            Message = "";
        }
        
        public GetModelResourceResponse(bool Success, Model Model, string Message)
        {
            this.Success = Success;
            this.Model = Model;
            this.Message = Message;
        }
        
        public GetModelResourceResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Model = new Model(ref b);
            Message = b.DeserializeString();
        }
        
        public GetModelResourceResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            Model = new Model(ref b);
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public GetModelResourceResponse RosDeserialize(ref ReadBuffer b) => new GetModelResourceResponse(ref b);
        
        public GetModelResourceResponse RosDeserialize(ref ReadBuffer2 b) => new GetModelResourceResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            Model.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            Model.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Model, nameof(Model));
            Model.RosValidate();
            BuiltIns.ThrowIfNull(Message, nameof(Message));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += Model.RosMessageLength;
                size += WriteBuffer.GetStringSize(Message);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Success
            size = Model.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
