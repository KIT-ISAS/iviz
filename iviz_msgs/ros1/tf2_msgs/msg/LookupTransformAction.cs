/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformAction : IDeserializableRos1<LookupTransformAction>, IMessageRos1,
		IAction<LookupTransformActionGoal, LookupTransformActionFeedback, LookupTransformActionResult>
    {
        [DataMember (Name = "action_goal")] public LookupTransformActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public LookupTransformActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public LookupTransformActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public LookupTransformAction()
        {
            ActionGoal = new LookupTransformActionGoal();
            ActionResult = new LookupTransformActionResult();
            ActionFeedback = new LookupTransformActionFeedback();
        }
        
        /// Explicit constructor.
        public LookupTransformAction(LookupTransformActionGoal ActionGoal, LookupTransformActionResult ActionResult, LookupTransformActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        public LookupTransformAction(ref ReadBuffer b)
        {
            ActionGoal = new LookupTransformActionGoal(ref b);
            ActionResult = new LookupTransformActionResult(ref b);
            ActionFeedback = new LookupTransformActionFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LookupTransformAction(ref b);
        
        public LookupTransformAction RosDeserialize(ref ReadBuffer b) => new LookupTransformAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "tf2_msgs/LookupTransformAction";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "7ee01ba91a56c2245c610992dbaa3c37";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VYX1PbRhB/16e4KQ+BDjENJGnKxJlxQVC3YFNjMu2T5yytpCuSzrk7YdxP392709kG" +
                "E5hpIB6wpdPe3m//7+pMyutmNla81plUVS8xQtankpeM28tJjtfR2SaqEeimNC2dsnebKU8A0ilPrlva" +
                "zN9H3W/8ic4vTw+ZyfYnlc713kYwJFz0G/AUFCvsT+RglWLqthFF/5iR5BOR3hXJKsdq5XnQa5M6GA5j" +
                "tMUuDa9TrlJWgeEpN5whDlaIvAD1uoQbKHETr2aQMvvULGagO7hxXAjN8C+HGhQvywVrNBIZyRJZVU0t" +
                "Em6AGVHB2n7cKWrG2YwrI5Km5ArppUpFTeSZ4hUQd/zT8KWBOgHWPz5EmlpD0hiBgBbIIVHAtahzfMii" +
                "RtTmYJ82RFvjuXyNt5CjBcLhzBTcEFi4naEvEU6uD/GMH51wHeSNygE8JdVs265N8FbvMDwEIcBMJgXb" +
                "RuQXC1PIGhkCu+FK8GkJxDhBDSDXV7Tp1c4K59qyrnktW/aO4/KMp7CtA1+S6XWBNitJet3kqEAknCl5" +
                "I1IknS4sk6QUUBuGbqe4WkS0yx0ZbZ2QjpEId1mL4C/XWiYCDZCyuTBFpI0i7tYa5KXP5I0bQ8O6lgfL" +
                "dCGbMsUbqcDKZQVBW84LgQaxQlC4sDnXTJHDaBSCHKhv7W1dElXCa38YGlndoGvMC6iZMAwFBU1Oi34B" +
                "1QwzTlnibuKpndfMAY8OrNkUMsLCWQLKcLQcIVrVr8cv0tYmqF6Et6BDgp5ZFrJWneIOl+AwBrXmOVgj" +
                "MD2DRGQicQJ6BLrjuVOAOAIEVTXaIDKGUYdUndZ+ZLkXToM2AbbnG65yMBPrRu2alo1KwK85tbkVq8e0" +
                "Udyaie5kYxC/pfGcLE3rnOIWUs8niqZSYlVJbzjG1rP569dzvytZj2d/zLim0eQt+HO3APi65wve84jx" +
                "IKboTnGivPqhBepuLuLBcX9wytpPl/2E387lrZ8WGIgLMOTt6JAYAonLtz4vrUWh59k7Gvc/x2yF55t1" +
                "npQIG6UwoWHunwJZ/0mML0ZxfH4xjo8D4/11xgoSwIqSUjiT54QwYzwzaERMECi9oriHW1t+6jxiX/ls" +
                "4T9GtNWCy/NYDGclEAdhdMsFgW6PQVVY9EqqwAZ2POTLq6OjOD5egXywDpkSHU8KAQRbNwlpIWuo/G5S" +
                "xEPH9H4djpZ6oWPebjhmKq3oGJSk8iX2jSelDTyqGvIKLTGeMy7KBtPoA/BG8e/x0Qq+Lnt3H56CfyAx" +
                "D3iATaGYQO66y+7jGKeQcKwblmc4rMH2hNK9LczYIIn6hpeY4x8QwHteiJQue/8Cnhdcr5bGBuHS+YLx" +
                "goaPemdny0jusp+fCtBXwE0In6JdtMl9a62DrjOhKuolqf6a1SxgkUC6JsSqm3z4BkI8Tc3kFGvh5w6g" +
                "bu0BnzgbXo5XWXXZL5ZhL/QmvmlDTixFqxETcErgQQXEpeOab98ckd6mT4g9TbwlaZtUOhco/obOCPuX" +
                "XlnKuR0DiBBDQa33Lpz5EmzblJWSRltSmDZ5TmpsmwC4NS/ehvhanAMmG6MWjiY8vfQDiWkXosBnfLIf" +
                "K4VyAH0/E+qvw2pnq3ZWof4xIMWeXFb3JibfbXRCw75FFrNzwF1KLB2lb5ssYXta23rSpdSmHed8bkWq" +
                "j5wVCrLuD4Uxs8O9vbm4Fh0ldUeqfM9kP3wy2cc9/gkHu+QaGXVozyWALXupTJoK3c/1duQmlY2+mkSy" +
                "i53oTuPkvWcdLjoyoXGS4K3MnJBE5FajoM0V676sGVuNKiD7odTaTS4B2BTMHHD8MHN5zz6apuMMZxHs" +
                "7nmCrXz0GYucVAduf2mVFf3Z4AZVkzKVdFp9GSE9mA0icnZjn93Bz8Iohh3nAr2M17Y3DDtxY4qpyKYg" +
                "O9som5l3KTWnEmxZQB4VvwbKYFSXMRfNZsiMr+qElnHLNnTyzq4b7ywVOZF962DfU+A0pUQuVkI/bObM" +
                "C7dLWQVzW1k6zO4wNCHNaV7bOx3Wz9hCNlgHUAa8UP71iG17W1y2WzBS7jLfUFgcqwq9kFgblsFX44zJ" +
                "cX6NslJy8/4tuw1Xi3D174uYeuljm6xdYxMkQkSv2ZzuviwdlJT8qEDt1fzZC0Wb4H1RHgwn8Wg0HNEg" +
                "E+r08I+ri7D8xi8fDQeDmCaV/vjv8HDfP4z/Go96F8Oz3rg/HISnB/5pf/C5d9Y/nvRGp1fn8WAcCN56" +
                "gnH/PB5eLdffteuj3uDyZDg6D0/eR/6Rq08+S9qbibv5PsNv+xb2f46/4WXud3qLG8SI/gOlCDY0vBYA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
