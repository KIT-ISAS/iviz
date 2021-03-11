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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f59df5767bf7634684781c92598b2406";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXXU8bRxR9X4n/MBIPgaqQNulHiuQHF0xKRRIEbl+qCs3OXu9Ou55xZ2Yx/vc9d2Y/" +
                "bDCKVaVgEezFd86c+3XuzbnOrZFK6bEK2pr3VtZCxo+3JT5n55vfX5Nv6tBZuPj00OacqMil+ruzmrXP" +
                "e9noC7/2sg8370/ae2qd34YmWKdl7V9v8Wsv+4VkQU5U8S0bjs196V+zycWZYK9vdTE4FUPCf/3f+PtQ" +
                "JAqJ3162L26CNIV0hZhTkIUMUswsiOuyIndU0x3VOCXnCypE/DasFuSPcXBaaS/wU5IhJ+t6JRoPo2CF" +
                "svN5Y7SSgUTQc9o4j5PaCCkW0gWtmlo62FtXaMPmMyfnxOj48fRPQ0aRuDg7gY3xpJqgQWgFBOVIem1K" +
                "fCmyRpvw9g0fyPanS3uERyoR/v5yESoZmCzdL1BKzFP6E9zxVXLuGNiIDuGWwouD+LdbPPpDgUtAgRZW" +
                "VeIAzK9WobIGgCTuJCogr4mBFSIA1Fd86NXhGjLTPhFGGtvBJ8Thjl1gTY/LPh1VyFnN3vumRABhuHD2" +
                "ThcwzVcRRNWaTBCoOSfdKuNT6cps/5xjDCOcihnBu/TeKo0EFGKpQ5X54Bg9ZoNL9Bkaaq0zuCyn8CGl" +
                "zle2qQs8WMesU0kJpHNZaeQk+sFNI5bSC8c14+EH19BFTHmsSkRFmvY25NndoTqWFRmhg4Cv5LluURo0" +
                "X0Bz6hqnGdOnwlkSru6hRU5oEVAQilyQSB4zWg9xy18XXVoQYdBDZuwQatGpFZgVOJEkDm3ovSwp5kH4" +
                "BSk90yo52DLwxy0690gyAKl54wOYCTQerI67FMIqexk5TEKYpdZEg7PcvASP9XGygzBDD0ODJo1vgza3" +
                "4ygl6TnbIfHZyx4MDZa8dx3L9HA1+Xh28fG96F4j8Q1+p1KM9VOhQVaEgrdcKChNlaSwlYyN7mgxx6fT" +
                "i98nYg3z201M1qjGOWgNZDknLrmdgK+uJ5MPV9PJWQ/8ZhPYkSKIPYQaIgjB7MtfyFlABtG48N5xP9J9" +
                "nAymzAaij1/7+IdOi1FIEow5taiJEXTwHQqIHkzJzTGPah6OgQ5byje/nZ5OJmdrlN9uUmYBkqrSGJrQ" +
                "q0ZxFGYNT8ZtgXjqmvHPn66HuPA13225JrfR9aKJXT5w33pT0dBnQ8NV4S0kbCZ13UDenqB3Pfl1crrG" +
                "byS+f0zP0V+kolRuo8PSZpvwsFy+/jzHnJSEnkfM/rIGmwPLcJyZ2F20uZM1tPcJB9rK6ztlJH54hsrr" +
                "S8/YEJtwKL4+eX2ET8eXl0Mnj8SPuxJsJ9M2hrtEFzl5nK1N0mam3ZzXPJ6LfRripsJMqNhwYr1M3n0B" +
                "J3YLMxfFRvulC3iReqImLj/dTNehRuKnCDjud4Z2nwKSKJA1BqEUBNmHgFF4NuNju7Rw3PIdes8ztuVo" +
                "c0iXGu5v2ViwV4zr2i7jhs6GaAW3uVNIxCwqQlwf1uYZHykob8qSw9gaBboPL7UedAM5LQh//Nmv+y+5" +
                "JZz3/4H8r3tCh9Bvdy/jzuDIo/gi3/8CwD5PeosPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
