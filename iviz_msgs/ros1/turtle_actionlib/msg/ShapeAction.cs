/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeAction : IDeserializable<ShapeAction>, IMessageRos1,
		IAction<ShapeActionGoal, ShapeActionFeedback, ShapeActionResult>
    {
        [DataMember (Name = "action_goal")] public ShapeActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ShapeActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ShapeActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public ShapeAction()
        {
            ActionGoal = new ShapeActionGoal();
            ActionResult = new ShapeActionResult();
            ActionFeedback = new ShapeActionFeedback();
        }
        
        /// Explicit constructor.
        public ShapeAction(ShapeActionGoal ActionGoal, ShapeActionResult ActionResult, ShapeActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        public ShapeAction(ref ReadBuffer b)
        {
            ActionGoal = new ShapeActionGoal(ref b);
            ActionResult = new ShapeActionResult(ref b);
            ActionFeedback = new ShapeActionFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ShapeAction(ref b);
        
        public ShapeAction RosDeserialize(ref ReadBuffer b) => new ShapeAction(ref b);
    
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
        public const string MessageType = "turtle_actionlib/ShapeAction";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VXXW/bNhR9168gkIcmw5Ju7T66AH7wEjfLkLZB7O3VoMUriRtFeiQVx/9+51KybLf2" +
                "YgxNYjixZV2ee+731biScxrmUTt75aQRMn2dlviejdf37ig0Jq7u+nS1ef89kZrJ/O+VRNFdZ4Ov/Mo+" +
                "jK/ORWx8NDRtlRk9ez3eNiP7jaQiL6r0kfVy0zqU4TVLXF8KtnGqVWtGMj5Z/TSMQ1St8pZZdiTGUVol" +
                "vRI1RalklKJwYKzLivypoXsyOCTrOSmR7sblnMIZDk4qHQTeJVny0pilaAKEohO5q+vG6lxGElHXtHUe" +
                "J7UVUsyljzpvjPSQd15py+KFlzUxOt6B/mnI5iSuL88hYwPlTdQgtARC7kkGbUvcFFmjbXz7hg9kR5OF" +
                "O8UllfB7r1zESkYmSw9zZA3zlOEcOr5pjTsDNpxD0KKCOE6/TXEZTgSUgALNXV6JYzC/XcbKWQCSuJde" +
                "y5khBs7hAaC+4kOvTjaQbYK20roVfIu41nEIrO1x2abTCjEzbH1oSjgQgnPv7rWC6GyZQHKjyUaBZPPS" +
                "LzM+1arMjt6zjyGEUyki+JQhuFwjAEosdKyyED2jp2hwbj5RNu4siJRaHVkRKtcYhQvnKdmVDEEsF5VG" +
                "QJIRXC5iIYPwnDABRnACXad4p5SES6TtlCHI/h6psajICh0FDKXASYu8oHqO3mIMTjNmaLNmQVDdQ4sZ" +
                "FcxFipx8lIgcM9r0b8dfq1VM4F7QW7KS3s+i6DuVVTjRtjLUYAiypBQEEeaU60LnrYEdg3DWoXOBtAIg" +
                "VTchgplA1UHqbBU/jtyztr7U9LK2GkmVFLLCOMlXXirdhBfow+3IeLwTow/GJnAM8dE2427adGPmaajv" +
                "ZZJ9Nh64x71b0WsvbkcfL68/XonVayC+w/82/VLOVCiKJUXOPCQH0jFve1/XI7YqosMcXkyu/xyJDczv" +
                "tzG5KTXeo7mgD8+I0+wg4Nu70ejD7WR02QO/2Qb2lBO6u+LSkuiQfcoLWUSEDsUK6z3XID2kUWDLTPzH" +
                "6wh/qK7khbbnYjDNDTGCjmGFAqLHE/I1BpDhaRjppKM8/uPiYjS63KD8dpsyNx2ZV5qYdmhy9kLR8Cjc" +
                "5Yh9aoa/frpb+4XV/LBDzcwl01WTKnvNfacm1dCjruGsCA5tq5DaNGhpe+jdjX4fXWzwG4gfv6Tn6S/K" +
                "454MSO3MNfHzdPn2cY4zyiV6eMLslTVYFbj1piGJZUXbe2nQb/cY0GVeXykD8dMzZF6fetbFVITr5OuD" +
                "13v4Ynhzs67kgfj5UILdNNrF8BDvIiZfRmubtC20r3mv41kYN7tAYkJqy4jNNHn3FYw4zM2cFFvl1yrg" +
                "zWlPTtx8Gk82oQbilwQ47PeEboECklCIGoNQ6wTZu4BRztpFuFtU2G+zA2ovMLZjb7NLFxrm79hSsEsM" +
                "jXGLtJKzIErBb+8RUnSzPq0MG4OMjyiaNWXJbuyEIj3EZ14JuvnbLwK8m3vt/FTa0lD/s5w7GFi/wIKw" +
                "emb8XytC/8D5Ik+aPfUs+xc9YwRQQQ8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
