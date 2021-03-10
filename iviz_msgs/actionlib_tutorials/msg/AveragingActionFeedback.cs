/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingActionFeedback")]
    public sealed class AveragingActionFeedback : IDeserializable<AveragingActionFeedback>, IActionFeedback<AveragingFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public AveragingFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = AveragingFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingFeedback Feedback)
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
            Feedback = AveragingFeedback.Singleton;
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71W23IaRxB936+YKh4spSyc2InjqIoHAlgmJdsqieRVNbvT7E6yO0vmAuLvc3r2Akgi" +
                "5sE2hcRt5vSZ06d7+gNJRVYU8SWRmde1KXV6X7ncvbqqZXnnpQ9OuPiSjNdkZa5N/p5IpTL7RyzbN8no" +
                "Kz+Sj3dXl4irGi4fGoYDAUJGSatERV4q6aVY1jiAzguyFyWtqWSy1YqUiL/67YrcEBsXhXYCz5wMDlGW" +
                "WxEcFvlaZHVVBaMz6Ul4XdHBfuzURkixktbrLJTSYn1tlTa8fGllRYyOp6N/A5mMxHx6iTXGURa8BqEt" +
                "EDJL0kE4/CiSoI1/85o3JIPFpr7AR8qRhj648IX0TJYeVpYc85TuEjF+aA43BDbEIURRTpzF7+7x0Z0L" +
                "BAEFWtVZIc7A/Gbri9oAkMRaWi3Tkhg4gwJAfcGbXpzvIZsIbaSpO/gGcRfjFFjT4/KZLgrkrOTTu5BD" +
                "QCxc2XqtFZam2wiSlZqMF/CelXab8K4mZDJ4zxpjEXbFjOBVOldnGglQYqN9kThvGT1m416r5Bu58Wh9" +
                "JPwWmc3xwvE5we+6omk+3Mw+TeefrkT3GIkf8Z9tSXGbKKQTW/JsyJRYn6xJfCtQExs5t6jBFnM8Wcz/" +
                "mok9zJ8OMTkjwVooCxOmxBqdBHxzO5t9vFnMpj3w60NgSxnB2rAlUg578Ddwv/NCLj2crD2f3nKC6CHW" +
                "gckT8T+PAf5gkqhCYzhU5aokRtDedSggerYgW6H6Sm4Fns5bynd/Tiaz2XSP8ptDyhsgy6zQxLRdyFiF" +
                "ZeA+8JwQx8KMf/98u9OFw/z8TJi0jkdXIdpyx/3ZSCrQF6VhV7gaZbCUugyWjtG7nf0xm+zxG4lfntKz" +
                "9Ddl/ogDYkHVwT+2y8svc0wpk+ipEbMPFtAnvQRT7hDo1NqsZanVsQO0zusrZSTefgfn9dYztY9FuDNf" +
                "n7xe4cn4+npXySPx66kEU8JVRc8yPEVd5ORptg5Jm6W2FV9qfH34/S4QmZA6OMS+Td59hUOcJjOb4qD8" +
                "mgB8bRzxxPXnu8U+1Ej8FgHHphOjvT2AJBSyxiDUiCB7CRhl2EwBDgYvVdQtPaH2HGPXrDZLutE4PipH" +
                "mketMxmMy7LexHmEF6IULNdtf1mBTHtRcY2JvfGKtyhKQ86zVXebeXrwyXe8yubTpHFAM4K0IjnP6ebz" +
                "xDsZkm4Kjdki3sd7LSW6gxTPQvM4uoT2jnmsE/aTYf/glORYIIw4VK2Qq7LEbsZ0TfI2hNA9dGc9WJIs" +
                "t5TIaH9UaPmju7TjBVox6G0Ps9CNrOxG7MB8FUqPcdI5mVOTGreiTC911hVDZOCGLTrPes0CkKpCLAr0" +
                "OY1Vwy55PIR889T5gORoyPXqyWCO6P8B/If1wdwLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
