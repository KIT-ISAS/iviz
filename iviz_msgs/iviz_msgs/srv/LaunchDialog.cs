using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = RosServiceType)]
    public sealed class LaunchDialog : IService
    {
        /// Request message.
        [DataMember] public LaunchDialogRequest Request { get; set; }
        
        /// Response message.
        [DataMember] public LaunchDialogResponse Response { get; set; }
        
        /// Empty constructor.
        public LaunchDialog()
        {
            Request = new LaunchDialogRequest();
            Response = new LaunchDialogResponse();
        }
        
        /// Setter constructor.
        public LaunchDialog(LaunchDialogRequest request)
        {
            Request = request;
            Response = new LaunchDialogResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (LaunchDialogRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (LaunchDialogResponse)value;
        }
        
        string IService.RosType => RosServiceType;
        
        /// Full ROS name of this service.
        [Preserve] public const string RosServiceType = "iviz_msgs/LaunchDialog";
        
        /// MD5 hash of a compact representation of the service.
        [Preserve] public const string RosMd5Sum = "ec7ed08dc865a51d9dc5312b8351aa02";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LaunchDialogRequest : IRequest<LaunchDialog, LaunchDialogResponse>, IDeserializable<LaunchDialogRequest>
    {
        [DataMember (Name = "dialog")] public IvizMsgs.Dialog Dialog;
    
        /// Constructor for empty message.
        public LaunchDialogRequest()
        {
            Dialog = new IvizMsgs.Dialog();
        }
        
        /// Explicit constructor.
        public LaunchDialogRequest(IvizMsgs.Dialog Dialog)
        {
            this.Dialog = Dialog;
        }
        
        /// Constructor with buffer.
        public LaunchDialogRequest(ref ReadBuffer b)
        {
            Dialog = new IvizMsgs.Dialog(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LaunchDialogRequest(ref b);
        
        public LaunchDialogRequest RosDeserialize(ref ReadBuffer b) => new LaunchDialogRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Dialog.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Dialog is null) BuiltIns.ThrowNullReference();
            Dialog.RosValidate();
        }
    
        public int RosMessageLength => 0 + Dialog.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LaunchDialogResponse : IResponse, IDeserializable<LaunchDialogResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
        [DataMember (Name = "feedback")] public IvizMsgs.Feedback Feedback;
    
        /// Constructor for empty message.
        public LaunchDialogResponse()
        {
            Message = "";
            Feedback = new IvizMsgs.Feedback();
        }
        
        /// Explicit constructor.
        public LaunchDialogResponse(bool Success, string Message, IvizMsgs.Feedback Feedback)
        {
            this.Success = Success;
            this.Message = Message;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public LaunchDialogResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
            Feedback = new IvizMsgs.Feedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LaunchDialogResponse(ref b);
        
        public LaunchDialogResponse RosDeserialize(ref ReadBuffer b) => new LaunchDialogResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference();
            if (Feedback is null) BuiltIns.ThrowNullReference();
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Message) + Feedback.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
