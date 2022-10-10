using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class LaunchDialog : IService
    {
        /// Request message.
        [DataMember] public LaunchDialogRequest Request;
        
        /// Response message.
        [DataMember] public LaunchDialogResponse Response;
        
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
        
        public const string ServiceType = "iviz_msgs/LaunchDialog";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "3e28f33adf4bc180b0c552d7317b5aa7";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LaunchDialogRequest : IRequest<LaunchDialog, LaunchDialogResponse>, IDeserializable<LaunchDialogRequest>
    {
        [DataMember (Name = "dialog")] public IvizMsgs.Dialog Dialog;
    
        public LaunchDialogRequest()
        {
            Dialog = new IvizMsgs.Dialog();
        }
        
        public LaunchDialogRequest(IvizMsgs.Dialog Dialog)
        {
            this.Dialog = Dialog;
        }
        
        public LaunchDialogRequest(ref ReadBuffer b)
        {
            Dialog = new IvizMsgs.Dialog(ref b);
        }
        
        public LaunchDialogRequest(ref ReadBuffer2 b)
        {
            Dialog = new IvizMsgs.Dialog(ref b);
        }
        
        public LaunchDialogRequest RosDeserialize(ref ReadBuffer b) => new LaunchDialogRequest(ref b);
        
        public LaunchDialogRequest RosDeserialize(ref ReadBuffer2 b) => new LaunchDialogRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Dialog.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Dialog.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Dialog is null) BuiltIns.ThrowNullReference(nameof(Dialog));
            Dialog.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Dialog.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Dialog.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class LaunchDialogResponse : IResponse, IDeserializable<LaunchDialogResponse>
    {
        [DataMember (Name = "success")] public bool Success;
        [DataMember (Name = "message")] public string Message;
        [DataMember (Name = "feedback")] public IvizMsgs.Feedback Feedback;
    
        public LaunchDialogResponse()
        {
            Message = "";
            Feedback = new IvizMsgs.Feedback();
        }
        
        public LaunchDialogResponse(bool Success, string Message, IvizMsgs.Feedback Feedback)
        {
            this.Success = Success;
            this.Message = Message;
            this.Feedback = Feedback;
        }
        
        public LaunchDialogResponse(ref ReadBuffer b)
        {
            b.Deserialize(out Success);
            Message = b.DeserializeString();
            Feedback = new IvizMsgs.Feedback(ref b);
        }
        
        public LaunchDialogResponse(ref ReadBuffer2 b)
        {
            b.Deserialize(out Success);
            b.Align4();
            Message = b.DeserializeString();
            Feedback = new IvizMsgs.Feedback(ref b);
        }
        
        public LaunchDialogResponse RosDeserialize(ref ReadBuffer b) => new LaunchDialogResponse(ref b);
        
        public LaunchDialogResponse RosDeserialize(ref ReadBuffer2 b) => new LaunchDialogResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Success);
            b.Serialize(Message);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Success);
            b.Align4();
            b.Serialize(Message);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Message is null) BuiltIns.ThrowNullReference(nameof(Message));
            if (Feedback is null) BuiltIns.ThrowNullReference(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(Message);
                size += Feedback.RosMessageLength;
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
            size = Feedback.AddRos2MessageLength(size);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}
