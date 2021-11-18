/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapAction")]
    public sealed class GetMapAction : IDeserializable<GetMapAction>,
		IAction<GetMapActionGoal, GetMapActionFeedback, GetMapActionResult>
    {
        [DataMember (Name = "action_goal")] public GetMapActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public GetMapActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public GetMapActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapAction()
        {
            ActionGoal = new GetMapActionGoal();
            ActionResult = new GetMapActionResult();
            ActionFeedback = new GetMapActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapAction(GetMapActionGoal ActionGoal, GetMapActionResult ActionResult, GetMapActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetMapAction(ref Buffer b)
        {
            ActionGoal = new GetMapActionGoal(ref b);
            ActionResult = new GetMapActionResult(ref b);
            ActionFeedback = new GetMapActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapAction(ref b);
        }
        
        GetMapAction IDeserializable<GetMapAction>.RosDeserialize(ref Buffer b)
        {
            return new GetMapAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e611ad23fbf237c031b7536416dc7cd7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1YW08bRxR+X4n/MBIPgcp2yKVpGokHFxxCFRIKTl8QQuPdY+8kuzvOzCzG/fX9zsxe" +
                "vGAaqwq2nHgv5/Kd+xlOyJ3J+TB2ShcnWmZC+subGa6jk5WXF2TLzNWvjb/rELwnSiYy/laTTKv7nejw" +
                "J392orPLk3eikLc3uZ3Z5/dt2Ik+kEzIiNT/RAFQpiYVOUhOjwVbeKOSygZvOz96MrjWJUF/ALcT7YpL" +
                "J4tEmkTk5GQinRRTDdRqlpLpZ3RLGbhkPqdE+LduOSc7AOM4VVbgO6OCjMyypSgtiJwWsc7zslCxdCSc" +
                "yqnDD05VCCnm0jgVl5k0oNcmUQWTT43MiaXja+l7SUVM4vT4HWgKS3HpFAAtISE2JK0qZngpolIV7tVL" +
                "Zoh2xwvdxy3N4PtGuXCpdAyW7uZIG8Yp7Tvo+CUYN4BseIegJbFizz+7wa3dF1ACCDTXcSr2gPx86VJd" +
                "QCCJW2mUnGTEgmN4AFKfMdOz/RXJDJvzpNC1+CCx1bGJ2KKRyzb1U8QsY+ttOYMDQTg3+lYlIJ0svZA4" +
                "U1Q4gYQz0iwj5goqo9337GMQgctHBL/SWh0rBCARC+XSyDrD0n00OD+fLCHXlgWn5Rg2hNDZVJdZghtt" +
                "GHVIKYFwLlKFmHg7uGjEQlphOGcs7OAcOvUh91kJr8ii0oY4m1tkxyKlQignYCtZzlukBuVz9JcsAzfL" +
                "tCFxFgTVjWgxIZQIIIiYjJMIHiNadXGFXyV1WOBhwENkdOtqUTcnIEvAEdoZytBaOSMfB2HnFKupioOB" +
                "FQI7qKRzjQQCgMpL64BMoPBANahDCKpoW90v9L3t9towEjbotuhzrkTx+Z+q4VbzJHh+mzkewOxE98YA" +
                "97G3NcRwcz76dHz66UTUn0NxgP9DfvmkSJH1S0IWa44+8i0O/a3qA52Ur2QOj8anf4/EiswXXZnceEpj" +
                "0EDQayfEebSR4POL0ejsfDw6bgS/7Ao2FBM6OLovOhu6YJPTQk4dwodqhPWGi4zufLsvZlEL9OFnF/9Q" +
                "Pt4Loa9i+MwzYgnK2VoKgO6NyeQYMhlPPEf7FeTLL0dHo9HxCuRXXcjcVWScKkxCNKEyZi9MSx536xzx" +
                "mJrhH58vWr+wmtdr1Ey0Nz0pfem22NdqSkr6oWs4K6xGX5pKlZXoWY/Auxj9OTpawXcofn0Iz9BXin3/" +
                "WweH+5Uu3f106f0Y44RiiSbtZTbKSqwD3Fv9IMRCoopbmaGhPmJAlXlNpRyKN1vIvCb1Cu18EbbJ1wSv" +
                "8fDR8OPHtpIPxW+bAqzGzTqEm3gXMXkYrS7oYqpMzrsbD7smDH79YCSUdIxYTZO3P8GIzdzMSdEpv6CA" +
                "t6NHcuLj58vxqqhD8bsXOGwWgWpJgiSRIGoshIITZOMClsIDF5fVJsJ+m2xQe5Zla/Y2u3ShYP6aNQTL" +
                "wjDL9MKv3UyIUjDdRUHCZ74j+J1gZZgxS0KTcjZjN1ZEju7c9mZ+PX+bx5/juJwjNMsTg3LN5fzpkXRU" +
                "huUR0TLEqz5iwMvXyz6mbAWoxweQsDwSOju2uCxbpeYgoH9N5ERlyi2FnkKkrnUMou6uIRDAMxydjuuj" +
                "E7NDSwTnNM9VMdX1Voh3fu/2MIxe9HP5FWw4ApHphepoYr130DvYHwjRWAgZLTReWyUXVTg3GFlgb7w6" +
                "6L04OLgG05fiW6EXWHGt6L8YRFwTV9de9RayY8X6JiKpRvVMcGyLvUNMLn0dYOxVPSpOpUFxkFHYcuF3" +
                "/7AT3dqJ988Awa3cIzKNqCRhG8ezG76/8Qt6635EWmdhtF7lzzn+19EUhHyIbN+BAVYgEAkiccVU9ro+" +
                "avqHFUFKOCy7+xThaaVUG4USrS1iCFd5T+BrZMKhqs/SPowks/5CG/hqjjNjxQRBPk99RtQRh6BBNCMM" +
                "eGeWwe/nnsWre7IgP9TIIR62NRTiCuDeAICd4mSFs4qMqednJB4n1XsVcgAdHqhr3oGIzjX82BBEf5Xo" +
                "hKbwclu6aGs2AkyTxxiYvJpUnaI2AebgrxoedcfikFlvXou75mrZXP2zLQta/61tkB2vdvHz3ffW+1y3" +
                "6IH/bVR9tdjyafB98/e+/3UerNmbo/m20Le4d6J/Ad3lDMEOFQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
