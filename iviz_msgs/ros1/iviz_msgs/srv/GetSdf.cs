using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetSdf : IService
    {
        /// Request message.
        [DataMember] public GetSdfRequest Request;
        
        /// Response message.
        [DataMember] public GetSdfResponse Response;
        
        /// Empty constructor.
        public GetSdf()
        {
            Request = new GetSdfRequest();
            Response = new GetSdfResponse();
        }
        
        /// Setter constructor.
        public GetSdf(GetSdfRequest request)
        {
            Request = request;
            Response = new GetSdfResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetSdfRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetSdfResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/GetSdf";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "4268e0641c7ff6b587e46790f433e3ba";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetSdfRequest : IRequest<GetSdf, GetSdfResponse>, IDeserializable<GetSdfRequest>
    {
        // Retrieves a scene, which can contain one or multiple 3D models and lights
        /// <summary> Uri of the file. Example: package://some_package/file.world </summary>
        [DataMember (Name = "uri")] public string Uri;
    
        public GetSdfRequest()
        {
            Uri = "";
        }
        
        public GetSdfRequest(string Uri)
        {
            this.Uri = Uri;
        }
        
        public GetSdfRequest(ref ReadBuffer b)
        {
            Uri = b.DeserializeString();
        }
        
        public GetSdfRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Uri = b.DeserializeString();
        }
        
        public GetSdfRequest RosDeserialize(ref ReadBuffer b) => new GetSdfRequest(ref b);
        
        public GetSdfRequest RosDeserialize(ref ReadBuffer2 b) => new GetSdfRequest(ref b);
    
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Uri);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
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
    public sealed class GetSdfResponse : IResponse, IDeserializable<GetSdfResponse>
    {
        /// <summary> Whether the retrieval succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> The scene </summary>
        [DataMember (Name = "scene")] public Scene Scene;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        public GetSdfResponse()
        {
            Scene = new Scene();
            Message = "";
        }
        
        public GetSdfResponse(bool Success, Scene Scene, string Message)
        {
            this.Success = Success;
            this.Scene = Scene;
            this.Message = Message;
        }
        
        public GetSdfResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Scene = new Scene(ref b);
            Message = b.DeserializeString();
        }
        
        public GetSdfResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            Scene = new Scene(ref b);
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public GetSdfResponse RosDeserialize(ref ReadBuffer b) => new GetSdfResponse(ref b);
        
        public GetSdfResponse RosDeserialize(ref ReadBuffer2 b) => new GetSdfResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            Scene.RosSerialize(ref b);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            Scene.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Scene, nameof(Scene));
            Scene.RosValidate();
            BuiltIns.ThrowIfNull(Message, nameof(Message));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += Scene.RosMessageLength;
                size += WriteBuffer.GetStringSize(Message);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Success
            size = Scene.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
