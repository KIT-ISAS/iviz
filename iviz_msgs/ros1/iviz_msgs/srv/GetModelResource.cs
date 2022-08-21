using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetModelResource : IService
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
            b.DeserializeString(out Uri);
        }
        
        public GetModelResourceRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Uri);
        }
        
        public GetModelResourceRequest RosDeserialize(ref ReadBuffer b) => new GetModelResourceRequest(ref b);
        
        public GetModelResourceRequest RosDeserialize(ref ReadBuffer2 b) => new GetModelResourceRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uri);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Uri);
        }
        
        public void RosValidate()
        {
            if (Uri is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Uri);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Uri);
            return c;
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
            b.DeserializeString(out Message);
        }
        
        public GetModelResourceResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            Model = new Model(ref b);
            b.Align4();
            b.DeserializeString(out Message);
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
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            if (Model is null) BuiltIns.ThrowNullReference();
            Model.RosValidate();
            if (Message is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + Model.RosMessageLength + WriteBuffer.GetStringSize(Message);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // Success
            c = Model.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Message);
            return c;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
