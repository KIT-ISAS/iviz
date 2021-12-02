/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FibonacciAction : IDeserializable<FibonacciAction>,
		IAction<FibonacciActionGoal, FibonacciActionFeedback, FibonacciActionResult>
    {
        [DataMember (Name = "action_goal")] public FibonacciActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public FibonacciActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public FibonacciActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public FibonacciAction()
        {
            ActionGoal = new FibonacciActionGoal();
            ActionResult = new FibonacciActionResult();
            ActionFeedback = new FibonacciActionFeedback();
        }
        
        /// Explicit constructor.
        public FibonacciAction(FibonacciActionGoal ActionGoal, FibonacciActionResult ActionResult, FibonacciActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal FibonacciAction(ref Buffer b)
        {
            ActionGoal = new FibonacciActionGoal(ref b);
            ActionResult = new FibonacciActionResult(ref b);
            ActionFeedback = new FibonacciActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new FibonacciAction(ref b);
        
        FibonacciAction IDeserializable<FibonacciAction>.RosDeserialize(ref Buffer b) => new FibonacciAction(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "f59df5767bf7634684781c92598b2406";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXXU8jNxR9n19hiYeFqrDtbj+2SHlIIVAqdhdB2peqQh7PzYxbj53aHkL+fc/1fCSB" +
                "IKJqKRFsMpvr43O/zr2c6dxZqZQeq6idPXfSCJk+3pb4nJ1tfn9NoTGxt/Dp6aHNGVGRS/V3bzXrnrPR" +
                "F35lH2/Oj7tbjM5vYxOd19KEt1u8yn4hWZAXVXrLVqfqUIa3bHFxKtjlW12sPErxSIF4GfIhFi2Bll22" +
                "J26itIX0hagpykJGKWYOrHVZkT80dEcGh2Q9p0Kkb+NyTuEIB6eVDgI/JVny0pilaAKMohPK1XVjtZKR" +
                "RNQ1bZzHSW2FFHPpo1aNkR72zhfasvnMy5oYHT+B/mnIKhIXp8ewsYFUEzUILYGgPMmgbYkvRdZoG9+/" +
                "4wPZ3nThDvFIJWI/XC5iJSOTpfs5ioh5ynCMO75qnTsCNoJDuKUIYj/93y0ew4HAJaBAc6cqsQ/mV8tY" +
                "OQtAEncS2c8NMbBCBID6hg+9OVhDZtrHwkrrevgWcXXHLrB2wGWfDivkzLD3oSkRQBjOvbvTBUzzZQJR" +
                "RpONAgXnpV9mfKq9Mts74xjDCKdSRvAuQ3BKIwGFWOhYZSF6Rk/Z4Pp88VZaa4pUWh1ZESrXmAIPzjPl" +
                "tp4EcrmoNBKSnOB2EQsZhOeCCXCCC+gi5TuVJEIibXcZkuzvUBqLiqzQUcBRCly0qAuq55AaY3CaMUNb" +
                "NQvC1QO0yAn9AQpCkY8SmWNG6/Ht+OuizwnCC3pIi1vFWfQiBWYFTrTKhh4MQZaUkiDCnJSeadU62DEI" +
                "Rx06N0hrAFJ1EyKYCXQdrI76/HHmXkMFk/5lbVOitaEzr0BifYA8L8aQwdigN9PbSo+7+dMNnpf24gGb" +
                "7MGUYJn70FNsH64mn04vPp2L/jUS3+DftgJT2VToiyWhzh3XBypStfLXycRGU3SY45Ppxe8TsYb57SYm" +
                "61LjPfQFUpwTV9pOwFfXk8nHq+nkdAB+twnsSREEHuIM4YNIDlUv5CwifehXeO+5Dek+TQNbZiuij197" +
                "+EWDpSi0sovZNDfECDqGHgVE96fka8wgwwMx0kFH+ea3k5PJ5HSN8vtNyqw7UlUagxIy1SiOwqzhabgt" +
                "EE9dM/758/UqLnzNd1uuyV1yvWhSc6+4b72paOjZ0HBVBAflmkltGqjaE/SuJ79OTtb4jcT3j+l5+otU" +
                "UshtdFjRXBMflsvXz3PMSUnIeMIcLmuwLbD6pjmJfUXbO2kguU840FXe0Ckj8cP/UHlD6VkXUxOuim9I" +
                "3hDhk/Hl5aqTR+LHXQl2A2kbw12ii5w8ztYmaTvTvubVjsfhkIa0nTATKjacWC+TD1/Aid3CzEWx0X7t" +
                "Bbw8PVETl59vputQI/FTAhwPq0K3QwFJFMgag1AbBDmEgFF4JONjt6tw3PIdei8wtuNoc0gXGu5vWVSw" +
                "ToyNcYu0lbMhWsFvrhISMUuKkLaGtWHGRwrKm7LkMHZGke7j62wF3Shu94I//hz2+9dbDs76vxT/63rQ" +
                "Awzr3Gv4MnjxOLLZv33d+u9vDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
