/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapActionFeedback : IDeserializable<GetMapActionFeedback>, IActionFeedback<GetMapFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public GetMapFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public GetMapActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = GetMapFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public GetMapActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public GetMapActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = GetMapFeedback.Singleton;
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMapActionFeedback(ref b);
        
        public GetMapActionFeedback RosDeserialize(ref ReadBuffer b) => new GetMapActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Feedback is null) BuiltIns.ThrowNullReference();
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/GetMapActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVptkzZJPaODKimOMnbisdVePSC5ItGCIIsPyfr3fQtSlORY" +
                "jQ5JNLL1Bbx9ePt2se9J5mRFGV8SmXlVG63Sh8oV7qerWup7L31wwsWX5Ir8jWzeEeWpzP4Ry+5NMvrK" +
                "j+Tm/uoSQfOWyPuW3kCAjcmlzUVFXubSS7GswV4VJdkLTSvSzLRqKBfxV79pyA2xcVEqJ/AsyJCVWm9E" +
                "cFjka5HVVRWMyqQn4VVFB/uxUxkhRSOtV1nQ0mJ9bXNlePnSyooYHU9H/wYyGYn59BJrjKMseAVCGyBk" +
                "lqRTpsCPIgnK+FcveUMyWKzrC3ykAjnogwtfSs9k6bGx5JindJeI8UN7uCGwIQ4hSu7EWfzuAR/duUAQ" +
                "UKCmzkpxBua3G1/WBoAkVtIqmWpi4AwKAPUFb3pxvodsIrSRpt7Ct4i7GKfAmh6Xz3RRImeaT+9CAQGx" +
                "sLH1SuVYmm4iSKYVGS9gPCvtJuFdbchk8I41xiLsihnBq3SuzhQSkIu18mXivGX0mI0HlSffyI1HiyPh" +
                "t8hsgReOzwl+u62Y9sPt7ON0/vFKbB8j8TP+sy0pbhOldGJDng2ZEuuTtYnvBGpjI+d2hTpoMceTxfyv" +
                "mdjD/OUQkzMSrIWyMGFKrNFJwLd3s9nN7WI27YFfHgJbygjWhi2RctiDv4H7nRdy6eFk5fn0lhNEj7EO" +
                "TJGI/3kM8AeTRBVaw6EqG02MoLzbooDo2YJsherT3Ao8nXeU7/+cTGaz6R7lV4eU10CWWamIabuQsQrL" +
                "wH3gOSGOhRn/8elupwuH+fWZMGkdj56HaMsd92cj5YG+KA27wtUog6VUOlg6Ru9u9mE22eM3Er99Ts/S" +
                "35T5Iw6IBVUH/9QuP36ZY0qZRE+NmH2wgD7pJZhyh0CnVmYltcqPHaBzXl8pI/H6Ozivt56pfSzCnfn6" +
                "5PUKT8bX17tKHok3pxJMCVcVPcvwFHWRk8+zdUjaLJWt+FLj68Pvd4HIhPKDQ+zb5O1XOMRpMrMpDsqv" +
                "DcDXxhFPXH+6X+xDjcTvEXBstmJ0tweQRI6sMQi1IsheAkYZtlOAg8F1HnVLT6g9x9g1q82SrhWOj8qR" +
                "5knrTAZjret1nEd4IUrBct32lxXIdBcV15jYm614S05pKAqWsVvk6dEn3/Eqm0+T1gHtCNKJ5Dynm88T" +
                "72RIui4VZot4H++1lOgOynkWmsfRJXR3zFOdsJ8M+wenJMcCYcShqkGutMZuxnRt8taE0D301nqwJFlu" +
                "KZHR/qjQ8Ud36cYLtGLQ2xxmYTuyshuxA/NV0B7jpHOyoDY1rqFMLVW2LYbIwA07dJ712gUgVYVYFOhz" +
                "CquG2+TxEPKNUmfkqkvawSieJP8BJ45xX8oLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
