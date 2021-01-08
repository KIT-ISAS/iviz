/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciAction")]
    public sealed class FibonacciAction : IDeserializable<FibonacciAction>,
		IAction<FibonacciActionGoal, FibonacciActionFeedback, FibonacciActionResult>
    {
        [DataMember (Name = "action_goal")] public FibonacciActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public FibonacciActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public FibonacciActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciAction()
        {
            ActionGoal = new FibonacciActionGoal();
            ActionResult = new FibonacciActionResult();
            ActionFeedback = new FibonacciActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciAction(FibonacciActionGoal ActionGoal, FibonacciActionResult ActionResult, FibonacciActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciAction(ref Buffer b)
        {
            ActionGoal = new FibonacciActionGoal(ref b);
            ActionResult = new FibonacciActionResult(ref b);
            ActionFeedback = new FibonacciActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciAction(ref b);
        }
        
        FibonacciAction IDeserializable<FibonacciAction>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciAction(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f59df5767bf7634684781c92598b2406";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXW0/jRhR+t5T/MBIPC1Vh291etkh5SCHQVOwugrQvVYXG4xN7WseTnRkT8u/7nRlf" +
                "4iWoUbWFiGA7OfOd79xPLnRqKqmUniivTXVpZClkuL3LcZ9cDL+/IVeXvpWw4elzmQuiLJXq71Zq0TyP" +
                "kvEXfo2S97eXp42eUqd3vvbGalm61zvsGiW/kMzIiiJckv7Y0uXuNYvMzgVbfaez3qjgEv70f+PvfBYp" +
                "RH6j5EDcelll0mZiSV5m0kuxMCCu84LscUn3VOKUXK4oE+Fbv1mRO+GT80I7gb+cKrKyLDeidpDyRiiz" +
                "XNaVVtKT8HpJAwA+qishxUpar1VdSosDxma6YvmFlUsK+Px29KmmSpGYnZ9CqnKkaq9BagMMZUk6XeX4" +
                "EsK1rvzbN3wCB+drc4xnyhGEjoHwhfTMmB5WSCgmK90pq/kq2ngCeDiJoChz4jB8dodHdySgByxoZVQh" +
                "DkH/euMLUwGRxL1EIqQlMbKCHwD7ig+9OtqGZuqnopKVafEjZK9kH9yqB2azjgsEr2QXuDqHHyG5suZe" +
                "Z5BNNwFFlZoqL5B8VtrNKOFjUSlALtjZEMO5EBtcpXNGaUQiE2vti1HivGUFIS5I11HyDNW1VSYx0xrK" +
                "whWmLjM8GMu8Y3oJRHVdaEQmWMIVJNbSCcvJ42BJSKdZCH1IUbhGVo06hNveI0vWBVVCewFryXESI0Vo" +
                "uUIHKlGPBwHVxQxaE5R34CIlVAxICEXWS8SQOQ0d3RqhszY+cDQ4IkSm97ho+xfYwc8H0BF6INLXyZxC" +
                "PIRbkdILraKZDQt30sCHmokSYLasnQc9gVqE2EkXyxjFl+mSsT8msVhR9dyFXoLH9pTZo1+jTfoaNRsu" +
                "fctuplQM1HMWRuQzSoazJHbBdw3N9ul6+uF89uFStK+x+Ab/Y0aGLCpQLBtC7hvOFmSoit2xaSGDQmlB" +
                "J2fz2e9TsQX67RCUu1ZtLZoPmnVKnHf7IV/fTKfvr+fT8w75zRDZkiLMAHRv9EX00K4MhFx4RBFVDAdY" +
                "rk16CAOjykdJT/Xx6wBv1FxwRGzLGGGrkhhCe9fCgOrhnOwSk6rkwenpqCV9+9vZ2XR6vkX67ZA0NySp" +
                "Co2Jiv5VK3bEouapucsXT+qZ/PzxpncN6/luh57UBOuzOpR7z36nqqymf/cO54Yz6GgLqcsa7e4pgjfT" +
                "X6dnWwzH4vvHBC39RSr0zl2EuM+Z2n+eNF/vwTIlJdHiA2inrcZiwY05DFMsN7q6lyVa8VMmNAnYlcxY" +
                "/PAcCdhlYGV8KMc+B7sI9l4+m1xd9UU9Fj/uS7EZV7s47uVhBOZxyIa0q4W2S14FeVp2oQhrDFOhbGjG" +
                "drK8+wJm7OlqTo1BIUYNvGc9lRlXH2/n21hj8VNAnHTbRLNuAUpkCB2jUPSD7LzAKDywcdvsM+y6dJ8q" +
                "dAxu2OPs1rWGB3YsM2HhmJSlWYdlnkVRFHa4bEg4LrSHsFZszTg+klFa53nwZSPl6cG/3NbQzum4N/zx" +
                "Z/fD4CWXh4vu5+Z/XR9ahG7zexlzekMe+Rfx/geaQ1gYuQ8AAA==";
                
    }
}
