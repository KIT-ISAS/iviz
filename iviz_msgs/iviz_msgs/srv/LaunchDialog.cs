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
        
        IService IService.Create() => new LaunchDialog();
        
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
        [Preserve] public const string RosMd5Sum = "106553f64c8ef760e23ed6e9e0dea9e7";
        
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
        internal LaunchDialogRequest(ref Buffer b)
        {
            Dialog = new IvizMsgs.Dialog(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LaunchDialogRequest(ref b);
        
        LaunchDialogRequest IDeserializable<LaunchDialogRequest>.RosDeserialize(ref Buffer b) => new LaunchDialogRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Dialog.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Dialog is null) throw new System.NullReferenceException(nameof(Dialog));
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
            Message = string.Empty;
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
        internal LaunchDialogResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Feedback = new IvizMsgs.Feedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LaunchDialogResponse(ref b);
        
        LaunchDialogResponse IDeserializable<LaunchDialogResponse>.RosDeserialize(ref Buffer b) => new LaunchDialogResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(Message) + Feedback.RosMessageLength;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
