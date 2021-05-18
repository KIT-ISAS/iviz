using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/LaunchDialog")]
    public sealed class LaunchDialog : IService
    {
        /// <summary> Request message. </summary>
        [DataMember] public LaunchDialogRequest Request { get; set; }
        
        /// <summary> Response message. </summary>
        [DataMember] public LaunchDialogResponse Response { get; set; }
        
        /// <summary> Empty constructor. </summary>
        public LaunchDialog()
        {
            Request = new LaunchDialogRequest();
            Response = new LaunchDialogResponse();
        }
        
        /// <summary> Setter constructor. </summary>
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
        
        public void Dispose()
        {
            Request.Dispose();
            Response.Dispose();
        }
        
        string IService.RosType => RosServiceType;
        
        /// <summary> Full ROS name of this service. </summary>
        [Preserve] public const string RosServiceType = "iviz_msgs/LaunchDialog";
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve] public const string RosMd5Sum = "106553f64c8ef760e23ed6e9e0dea9e7";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LaunchDialogRequest : IRequest<LaunchDialog, LaunchDialogResponse>, IDeserializable<LaunchDialogRequest>
    {
        [DataMember (Name = "dialog")] public IvizMsgs.Dialog Dialog { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LaunchDialogRequest()
        {
            Dialog = new IvizMsgs.Dialog();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LaunchDialogRequest(IvizMsgs.Dialog Dialog)
        {
            this.Dialog = Dialog;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LaunchDialogRequest(ref Buffer b)
        {
            Dialog = new IvizMsgs.Dialog(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LaunchDialogRequest(ref b);
        }
        
        LaunchDialogRequest IDeserializable<LaunchDialogRequest>.RosDeserialize(ref Buffer b)
        {
            return new LaunchDialogRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Dialog.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Dialog is null) throw new System.NullReferenceException(nameof(Dialog));
            Dialog.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Dialog.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LaunchDialogResponse : IResponse, IDeserializable<LaunchDialogResponse>
    {
        [DataMember (Name = "success")] public bool Success { get; set; }
        [DataMember (Name = "message")] public string Message { get; set; }
        [DataMember (Name = "feedback")] public IvizMsgs.Feedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LaunchDialogResponse()
        {
            Message = string.Empty;
            Feedback = new IvizMsgs.Feedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LaunchDialogResponse(bool Success, string Message, IvizMsgs.Feedback Feedback)
        {
            this.Success = Success;
            this.Message = Message;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LaunchDialogResponse(ref Buffer b)
        {
            Success = b.Deserialize<bool>();
            Message = b.DeserializeString();
            Feedback = new IvizMsgs.Feedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LaunchDialogResponse(ref b);
        }
        
        LaunchDialogResponse IDeserializable<LaunchDialogResponse>.RosDeserialize(ref Buffer b)
        {
            return new LaunchDialogResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            Feedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Message is null) throw new System.NullReferenceException(nameof(Message));
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(Message);
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
