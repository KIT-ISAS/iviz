/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsAction")]
    public sealed class TwoIntsAction : IDeserializable<TwoIntsAction>,
		IAction<TwoIntsActionGoal, TwoIntsActionFeedback, TwoIntsActionResult>
    {
        [DataMember (Name = "action_goal")] public TwoIntsActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TwoIntsActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TwoIntsActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsAction()
        {
            ActionGoal = new TwoIntsActionGoal();
            ActionResult = new TwoIntsActionResult();
            ActionFeedback = new TwoIntsActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsAction(TwoIntsActionGoal ActionGoal, TwoIntsActionResult ActionResult, TwoIntsActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsAction(ref Buffer b)
        {
            ActionGoal = new TwoIntsActionGoal(ref b);
            ActionResult = new TwoIntsActionResult(ref b);
            ActionFeedback = new TwoIntsActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsAction(ref b);
        }
        
        TwoIntsAction IDeserializable<TwoIntsAction>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6d1aa538c4bd6183a2dfb7fcac41ee50";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXW/bNhR9F+D/QCAPTYYm3dqu7QL4wUuc1EPaBom314CSriVuEumRlB3/+51LSrLd" +
                "OKgxtDYcy7Iuzz33+2a6NBPt3SjzyuhrIyshw9eHAt+T6ebTO3JN5bvnNtxtS1wR5anM/ulkZu39IBl+" +
                "59cg+XR/fd7qqVT66okdg+QjyZysKMMl6UUfale4VywyuRRs5YPKOzOCA/i3H8bY+TwSiOwGyZG491Ln" +
                "0uaiJi9z6aWYGdBWRUn2tKIFVTgl6znlIjz1qzm5MxyclsoJvAvSZGVVrUTjIOSNyExdN1pl0pPwqqat" +
                "8ziptJBiLq1XWVNJC3ljc6VZfGZlTYyOt6N/G9IZicnlOWS0o6zxCoRWQMgsSad0gYciaZT2b17zgeQI" +
                "vjzFLRVwfq9c+FJ6JkuPc6QO85TuHDp+isadARveIWjJnTgOvz3g1p0IKAEFmpusFMdgfrvypdEAJLGQ" +
                "Vsm0IgbO4AGgvuBDL042kJn2udBSmw4+Iq517AOre1y26bREzCq23jUFHAjBuTULlUM0XQWQrFKkvUDG" +
                "WWlXCZ+KKpOjK/YxhHAqRARX6ZzJFAKQi6XyZeK8ZfQQDU7QH19Cm3XBaTmFDTF0rjRNlePGWGYdU0og" +
                "nMtSISbBDi4asZROWM4ZBzs4hyYh5CEr4RWpW22Is10gO5YlaaG8gK3kOG+RGlTP0WOqCqcZ08XEWRJU" +
                "99AiJZQIKIiMrJcIHjPadHHLX+VdWOBh0ENkzNrVoutPYJbjRGxpKEPnZEEhDsLNKVMzlUUDWwburEXn" +
                "GokCIFU3zoOZQOFB6qwLIaSSwzXA2PoS1N+7t0K21/TQDTiOij1aMHqfb1CQ4dJ14XbQxHAcMvEjm0Hy" +
                "1XDg5vah4xhvbsefLyefr0X3Goqf8RmTLmRKiVJYEVLbcEogCbPY9NrmsFUHLeboYjr5ayw2MH/ZxuRu" +
                "1FiLroIGnBIn117At3fj8afb6fiyB369DWwpI7R1tGS0O7TGPtGFnHnEDyUK6y1XHj2GGaCLZE306esI" +
                "f6ip4IXYbDGR5hUxgvKuQwHR4ynZGpOn4jHo6aSlfP/nxcV4fLlB+c02ZW41MisVxiM6U5OxF2YNz8Bd" +
                "jnhOzej3L3drv7CatzvUpCaYnjehntfcd2rKG/qmazgrnEGzmklVNWhkz9C7G/8xvtjgNxS/PqVn6W/K" +
                "QlPcRYebmGn81+ny8tscU8okOnfA7JU12BG44YbpiC1F6YWs0GWfMaDNvL5ShuLdATKvTz1tfCjCdfL1" +
                "wes9fDG6uVlX8lC835dgO4N2MdzHu4jJ02htk9YzZWte6HgC9mEIOwkzoXzLiM00+fAdjNjPzZwUW+UX" +
                "FfDK9ExO3Hy5n25CDcVvAXDUbwft5gQkkSNqDELRCbJ3AaPwFMbXdj1hv6V71J5jbMPeZpcuFczfsZtg" +
                "gxhVlVmGXZwFUQp2e3uQ8FnoCGFR2JhmfCSntCkKdmMr5OnRH3IR6EZw3ABcUx96B7jq//n7f1tAd77f" +
                "0g5nwJr6IPkPhc/nKyEPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
