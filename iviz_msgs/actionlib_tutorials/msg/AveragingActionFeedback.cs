/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingActionFeedback")]
    public sealed class AveragingActionFeedback : IDeserializable<AveragingActionFeedback>, IActionFeedback<AveragingFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public AveragingFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new AveragingFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new AveragingFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingActionFeedback(ref b);
        }
        
        AveragingActionFeedback IDeserializable<AveragingActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new AveragingActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "78a4a09241b1791069223ae7ebd5b16b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WXXMaNxR931+hGR5id2LSJm2aeoYHCsRxx0k8Nu2rR7u67KrVaqk+wPz7nqtd1mCb" +
                "hockDPYikM49Ovdc6X4gqciJKj0yWQTdWKPzu9qX/tVFI81tkCF64dMjG6/IyVLb8j2RymXxj1h0H7LR" +
                "V35lH28vzhFXtVw+tAwHAoSskk6JmoJUMkixaLABXVbkzgytyDDZeklKpF/DZkl+iIXzSnuBd0kWmzBm" +
                "I6LHpNCIoqnraHUhA4mga9pbj5XaCimW0gVdRCMd5jdOacvTF07WxOh4e/o3ki1IXE7PMcd6KmLQILQB" +
                "QuFIegiHH0UWtQ1vXvOCbDBfN2cYUok09MFFqGRgsnS/dOSZp/TniPFDu7khsCEOIYry4iR9d4ehPxUI" +
                "Agq0bIpKnID59SZUjQUgiZV0WuaGGLiAAkB9wYtenO4g2wRtpW228C3iQ4xjYG2Py3s6q5Azw7v3sYSA" +
                "mLh0zUorTM03CaQwmmwQ8J6TbpPxqjZkNnjPGmMSVqWM4Cm9bwqNBCix1qHKfHCMnrJxp1X2jdx4sD4y" +
                "/ojMlnhwfE7wu23RtIPr2afp5acLsX2NxI/4z7aktExU0osNBTZkTqxP0Sa+E6iNjZw71GCHOZ7ML/+a" +
                "iR3Mn/YxOSPROSgLE+bEGh0FfH0zm328ns+mPfDrfWBHBcHasCVSDnvwN3C/D0IuApysA+/ecYLoPtWB" +
                "LTPxP68B/mCSpEJrOFTl0hAj6OC3KCB6MidXo/oMHwWBTjvKt39OJrPZdIfym33KayDLotLEtH0sWIVF" +
                "5HPgOSEOhRn//vnmQRcO8/MzYfImbV3FZMsH7s9GUpG+KA27wjcog4XUJjo6RO9m9sdsssNvJH55Ss/R" +
                "31SEAw5IBdXE8NguL7/MMadC4kxNmH2wiHMySDDlEwIntbYrabQ6tIHOeX2ljMTb7+C83nq2CakIH8zX" +
                "J69XeDK+unqo5JH49ViCOeGqomcZHqMucvI0W/uk7UK7mi81vj7C7imQmJDa28SuTd59hU0cJzObYq/8" +
                "2gB8bRzwxNXn2/ku1Ej8lgDHditGd3sASShkjUGoFUH2EjDKsO0CPAxuVNItP6L2PGM3rDZLutbYPipH" +
                "2kdHZzYYG9OsUz/CE1EKjuu2v6xApruouMbETnvFSxTlseTeanubBboP2Xe8yi6nWeuAtgXpRPKB0837" +
                "SXcyJF1XGr1Fuo93jpTkDlLcC12m1iV2d8xjnbCeLPsHuyTPAqHFoXqJXBmD1Yzp2+StCaF76K31YEly" +
                "fKQkRrutQscfp0vXXuAoBr3Nfha2LSu7ESvQX0UT0E56L0tqU+OXVOiFLrbFkBj4YYfOvV47AaTqmIoC" +
                "55zGrOE2edyEfPPUhYjkaMj16kljnmVdjyn5IMsWppE85AaqH9QkbT/gTlvRKvsPrEFxBxIMAAA=";
                
    }
}
