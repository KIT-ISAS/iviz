using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ResetModule : IService
    {
        /// Request message.
        [DataMember] public ResetModuleRequest Request;
        
        /// Response message.
        [DataMember] public ResetModuleResponse Response;
        
        /// Empty constructor.
        public ResetModule()
        {
            Request = new ResetModuleRequest();
            Response = new ResetModuleResponse();
        }
        
        /// Setter constructor.
        public ResetModule(ResetModuleRequest request)
        {
            Request = request;
            Response = new ResetModuleResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (ResetModuleRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (ResetModuleResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/ResetModule";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "7b2e77c05fb1342786184d949a9f06ed";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ResetModuleRequest : IRequest<ResetModule, ResetModuleResponse>, IDeserializable<ResetModuleRequest>
    {
        // Resets a module. What this entails depends on the specific module.
        /// <summary> Id of the module </summary>
        [DataMember (Name = "id")] public string Id;
    
        public ResetModuleRequest()
        {
            Id = "";
        }
        
        public ResetModuleRequest(string Id)
        {
            this.Id = Id;
        }
        
        public ResetModuleRequest(ref ReadBuffer b)
        {
            Id = b.DeserializeString();
        }
        
        public ResetModuleRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            Id = b.DeserializeString();
        }
        
        public ResetModuleRequest RosDeserialize(ref ReadBuffer b) => new ResetModuleRequest(ref b);
        
        public ResetModuleRequest RosDeserialize(ref ReadBuffer2 b) => new ResetModuleRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Id, nameof(Id));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetStringSize(Id);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class ResetModuleResponse : IResponse, IDeserializable<ResetModuleResponse>
    {
        /// <summary> Whether the operation succeeded </summary>
        [DataMember (Name = "success")] public bool Success;
        /// <summary> An error message if success is false </summary>
        [DataMember (Name = "message")] public string Message;
    
        public ResetModuleResponse()
        {
            Message = "";
        }
        
        public ResetModuleResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        
        public ResetModuleResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
        }
        
        public ResetModuleResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
        }
        
        public ResetModuleResponse RosDeserialize(ref ReadBuffer b) => new ResetModuleResponse(ref b);
        
        public ResetModuleResponse RosDeserialize(ref ReadBuffer2 b) => new ResetModuleResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Align4();
            b.Serialize(Message);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Message, nameof(Message));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Message);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Success
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Message);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
