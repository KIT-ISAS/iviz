/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciAction : IDeserializable<FibonacciAction>, IHasSerializer<FibonacciAction>, IMessage,
		IAction<FibonacciActionGoal, FibonacciActionFeedback, FibonacciActionResult>
    {
        [DataMember (Name = "action_goal")] public FibonacciActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public FibonacciActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public FibonacciActionFeedback ActionFeedback { get; set; }
    
        public FibonacciAction()
        {
            ActionGoal = new FibonacciActionGoal();
            ActionResult = new FibonacciActionResult();
            ActionFeedback = new FibonacciActionFeedback();
        }
        
        public FibonacciAction(FibonacciActionGoal ActionGoal, FibonacciActionResult ActionResult, FibonacciActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public FibonacciAction(ref ReadBuffer b)
        {
            ActionGoal = new FibonacciActionGoal(ref b);
            ActionResult = new FibonacciActionResult(ref b);
            ActionFeedback = new FibonacciActionFeedback(ref b);
        }
        
        public FibonacciAction(ref ReadBuffer2 b)
        {
            ActionGoal = new FibonacciActionGoal(ref b);
            ActionResult = new FibonacciActionResult(ref b);
            ActionFeedback = new FibonacciActionFeedback(ref b);
        }
        
        public FibonacciAction RosDeserialize(ref ReadBuffer b) => new FibonacciAction(ref b);
        
        public FibonacciAction RosDeserialize(ref ReadBuffer2 b) => new FibonacciAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) BuiltIns.ThrowNullReference();
            ActionGoal.RosValidate();
            if (ActionResult is null) BuiltIns.ThrowNullReference();
            ActionResult.RosValidate();
            if (ActionFeedback is null) BuiltIns.ThrowNullReference();
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = ActionGoal.AddRos2MessageLength(c);
            c = ActionResult.AddRos2MessageLength(c);
            c = ActionFeedback.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "actionlib_tutorials/FibonacciAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f59df5767bf7634684781c92598b2406";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VX227bRhB951cM4IfYRe00SS+pAT2otuy6cBLDVvsSBMZyORK3JXfV3aVl/X3PLClK" +
                "8qUWirgmZFOUZs+cuY9OTO6s0toMdTTOnjpVkUpvr6d4n51sfn/JoaniUsKnp7syJ8xFrvRfS6lJ95wN" +
                "vvKVfbg6Pey0VCa/jk103qgqvD65b1X2K6uCPZXplq1O1WEaXovE2TGJydemWFmU/JEc8TzkQyxaAi27" +
                "bIeuorKF8gXVHFWhoqKJA2szLdnvV3zDFQ6pesYFpW/jYsbhAAfHpQmE15Qte1VVC2oChKIj7eq6sUar" +
                "yBRNzRvncdJYUjRTPhrdVMpD3vnCWBGfeFWzoOMV+O+GrWY6Oz6EjA2sm2hAaAEE7VkFY6f4krLG2Pju" +
                "rRygHfp86cKbL9nOeO728TlPEYSeBcVSRWHNtzNkkxBW4RDKvmmtPIASeImhrgi0mz67xmPYI2gDF545" +
                "XdIuTLhYxNJZADLdKKRBXrEAa7gCqK/k0Ku9NWSboK2ybgnfIq50bANre1yxab9E8CpxQ2im8CQEZ97d" +
                "mAKi+SKB6MqwjYTM88ovMjnVqsx2TsTZEMKpFBrcVQhOG0SioLmJZRaiF/QUFknUZ6+ptepIOdaRpVC6" +
                "pirw4Dwnu5IhiOW8NAhIMkLqhuYqkJfMCTBCMuksxTvlJlyibKcMQfY3SI15yZZMJBjKQbIXecH1DD2n" +
                "qnBaMEObNXOG6h6acp4IF0WafVSInDBa92/H3xTLmMC9oLcQJb2fadJ3L1vgRNviUIwhqCmnIFCYsTYT" +
                "o1sDOwbhoEOXSmkFQKpuQgQzQvlB6mAZP4ncS7TD1AiztjpR42g4L0BifZI83ZXRD2MTJIS4rRpzN4i6" +
                "CfTcVtxhk90ZF9Lv3i8ptg8Xo4/HZx9PaXkN6Dv8bzMwpU2JulhwlORDfiAjddv+ujaxURQd5vBofPbH" +
                "iNYw32xiSl9qvEd/QU/OWTJtK+CLy9How8V4dNwDv90E9qwZnb6Q6lJokn3Wk5pEhA/1Cuu9lCHfprFg" +
                "pxn9y7WDPxRY8kLbdjGkZhULgolhiQKiu2P2NYZRJZMx8l5H+er3o6PR6HiN8rtNytJ3lC4NC+3QaPHC" +
                "pJGx+JAjHlMz/OXT5covoub7B9TkLpleNKm4V9wf1FQ0/KRrJCuCQ+eaKFM16GqP0Lsc/TY6WuM3oB/u" +
                "0/P8J+v4SAakjuaaeDddvn2aY85aoY0nzF5Zg7VBum+ak1hcjL1RFVruIwZ0mddXyoB+/B8yr08962Iq" +
                "wlXy9cHrPXw0PD9fVfKAftqWYDeQHmK4jXcRk/vR2iRtJ8bXsuPJOIzrXSAx4WLDiPU0ef8VjNjOzZIU" +
                "G+XXKpDl6ZGcOP90NV6HGtDPCXDYrwrdDgUkKhA1AeHWCap3gaActEtxt6uI3/Itai8IthNvi0vnBuY/" +
                "sKhgnRhWlZun9VwEUQp+c5VQ1I37tDWsDTM5UnDeTKfixk4o8m18ma2gG8XtXvD5S7/ov9xysPwJ+Z/X" +
                "g/436Ev++OytuO/Z7B8P9qMheA8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FibonacciAction> CreateSerializer() => new Serializer();
        public Deserializer<FibonacciAction> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FibonacciAction>
        {
            public override void RosSerialize(FibonacciAction msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FibonacciAction msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FibonacciAction msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FibonacciAction msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<FibonacciAction>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FibonacciAction msg) => msg = new FibonacciAction(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FibonacciAction msg) => msg = new FibonacciAction(ref b);
        }
    }
}
