/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeAction")]
    public sealed class ShapeAction : IDeserializable<ShapeAction>,
		IAction<ShapeActionGoal, ShapeActionFeedback, ShapeActionResult>
    {
        [DataMember (Name = "action_goal")] public ShapeActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ShapeActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ShapeActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeAction()
        {
            ActionGoal = new ShapeActionGoal();
            ActionResult = new ShapeActionResult();
            ActionFeedback = new ShapeActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeAction(ShapeActionGoal ActionGoal, ShapeActionResult ActionResult, ShapeActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeAction(ref Buffer b)
        {
            ActionGoal = new ShapeActionGoal(ref b);
            ActionResult = new ShapeActionResult(ref b);
            ActionFeedback = new ShapeActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeAction(ref b);
        }
        
        ShapeAction IDeserializable<ShapeAction>.RosDeserialize(ref Buffer b)
        {
            return new ShapeAction(ref b);
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
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVX224bNxB9X0D/QMAPcYraaZNeUgN6UG3ZdeEkhqX2VaB2R7tsd0mV5FrW3/cMuRdt" +
                "LKNCkdiCbYna4Zkz9/GskGuapF4ZfWVkKWT4uMjxOZn1z+7I1aVvn9pw2n1+SZQtZfp3K7FqzqNk/IVf" +
                "o+TD7OpM+Nr6khZRXamWbz4zZJT8RjIjK4rwlnSCi8rl7g2LXF8INnOhsmhJsJ+/+Wqknc+i+shtlByJ" +
                "mZc6kzYTFXmZSS/FyoC0yguyJyXdU4lbslpTJsJTv12TO+Wb80I5gZ+cNFlZlltRO0h5I1JTVbVWqfQk" +
                "vKpoAMBXlRZSrKX1Kq1LaXHB2Expll9ZWVHA519H/9SkUxLXF2eQ0o7S2iuQ2gIjtSSd0jkeQrhW2r97" +
                "yzdwcb4xJzhTjgB0DIQvpGfG9LBGBjFZ6c5YzTfRxlPAw0kERZkTx+G7BY7utYAesKC1SQtxDPq3W18Y" +
                "DUQS99IquSyJkVP4AbCv+NKr17vQTP1MaKlNix8heyWH4OoemM06KRC8kl3g6hx+hOTamnuVQXa5DShp" +
                "qUh7gcSz0m5HCV+LSgFyyc6GGO6F2OBdOmdShUhkYqN8MUqct6wgxAWpOkq+WnbuLZGYaQ1l4QpTlxkO" +
                "xjLvmF4CUd0UCpEJlnAFiY10wnLyOFgS0uk6hD6kKFwjdaMO4bb3yJJNQVooL2AtOU5ipAhVa7ScEvV4" +
                "FFBdzKANQXkHLpaEigEJkZL1EjFkTkNHt0aorI0PHA2OCJHpPS7apgV28PMRdISmh/R1MqcQD+HWlKqV" +
                "SqOZDQt32sCHmokSYFbVzoOeQC1C7LSLZYziM7fG2BSTWKWU5eSSVWkkn6zMVO1epFXHuXJAs0af9DWK" +
                "NrzFft3MpBil56yKyGWUDIdIbIHvG4rt6Xb68eL645VoX2PxHf7GdAwpVKBStoTEN5wqSM80tsamfwyq" +
                "pAWdnM+v/5yKHdDvh6Dcsmpr0XnQqZfESXcY8u3ddPrhdj696JDfDpEtpYQBgNaNpogG2tWAkCuPCKKE" +
                "4QDLhUkPYVrofJT0VB+/jvCLgguOiD0Z82tdEkMo71oYUD2ek60wpkqemp5et6Rnf5yfT6cXO6TfDUlz" +
                "N5JpoTBO0bzqlB2xqnlk7vPFk3omv366613Den7Yo2dpgvVZHWq9Z79XVVbTf3uHc8MZtLOVVGWNXvcU" +
                "wbvp79PzHYZj8eNjgpb+ojQ0zn2EuMmZ2n+eNN8ewHJJqUR/D6CdthpbBXflMEmx2Sh9L0v04adMaBKw" +
                "K5mx+Ok5ErDLQG18KMc+B7sI9l4+n9zc9EU9Fj8fSrGZVfs4HuRhBOZxyIa09UrZivdAHpVdKMIOw1Qo" +
                "G5qxmyzvv4AZB7qaU2NQiFEDL1lPZcbNp9l8F2ssfgmIk26VaHYtQIkMoWMUin6QnRcYhac1PjbLDLtu" +
                "eUgVOgY37HF260bBA3s2mbBtTMrSbMImz6IoCjvcNCQcF9pD2Cl25htfyWhZ53nwZSPl6cG/wMrQDudu" +
                "UeCV3ipjF1LnJXVfy7WBmdWLLBCX3T+a/2eFaG93q98z29CzR2z/BYAzLWyLDwAA";
                
    }
}
