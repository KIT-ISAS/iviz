/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingAction : IHasSerializer<AveragingAction>, IMessage,
		IAction<AveragingActionGoal, AveragingActionFeedback, AveragingActionResult>
    {
        [DataMember (Name = "action_goal")] public AveragingActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public AveragingActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public AveragingActionFeedback ActionFeedback { get; set; }
    
        public AveragingAction()
        {
            ActionGoal = new AveragingActionGoal();
            ActionResult = new AveragingActionResult();
            ActionFeedback = new AveragingActionFeedback();
        }
        
        public AveragingAction(AveragingActionGoal ActionGoal, AveragingActionResult ActionResult, AveragingActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public AveragingAction(ref ReadBuffer b)
        {
            ActionGoal = new AveragingActionGoal(ref b);
            ActionResult = new AveragingActionResult(ref b);
            ActionFeedback = new AveragingActionFeedback(ref b);
        }
        
        public AveragingAction(ref ReadBuffer2 b)
        {
            ActionGoal = new AveragingActionGoal(ref b);
            ActionResult = new AveragingActionResult(ref b);
            ActionFeedback = new AveragingActionFeedback(ref b);
        }
        
        public AveragingAction RosDeserialize(ref ReadBuffer b) => new AveragingAction(ref b);
        
        public AveragingAction RosDeserialize(ref ReadBuffer2 b) => new AveragingAction(ref b);
    
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
            BuiltIns.ThrowIfNull(ActionGoal, nameof(ActionGoal));
            ActionGoal.RosValidate();
            BuiltIns.ThrowIfNull(ActionResult, nameof(ActionResult));
            ActionResult.RosValidate();
            BuiltIns.ThrowIfNull(ActionFeedback, nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = ActionGoal.AddRos2MessageLength(size);
            size = ActionResult.AddRos2MessageLength(size);
            size = ActionFeedback.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "actionlib_tutorials/AveragingAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VX227bRhB951cs4IfYRe00SS+pAT2otuKqcBLDVvtSFMaKOyK3Xe6qe7Gsv++ZJUVJ" +
                "vtRCEdeCbIrS7Jkz9+HwhrystK2GZdTOnjlphMwfryt8Lobbv19SSCauJHy+uyvzgUhNZfnXSmrW3ReD" +
                "L/wqPl6dHXdajJ5exxSd19KE18P7VhU/k1TkRZ0vxfpUE6rwmiXGp4JNvtZqbVH2R3bE85APUbUEWnbF" +
                "nriK0irplWgoSiWjFDMH1rqqyR8auiGDQ7KZkxL517icUzjCwUmtg8C7IgvyxixFChCKTpSuaZLVpYwk" +
                "om5o6zxOaiukmEsfdZmM9JB3XmnL4jMvG2J0vAP9nciWJManx5CxgcoUNQgtgVB6kgEOw4+iSNrGd2/5" +
                "gNgTv1+68OaPYm+ycIf4nioEoWchYi0js6bbObKJCctwDGVftVYeQQm8RFCngtjP313jNhwIaAMXmruy" +
                "Fvsw4WIZa2cBSOJGIg2mhhi4hCuA+ooPvTrYQLYZ2krrVvAt4lrHLrC2x2WbDmsEz7AbQqrgSQjOvbvR" +
                "CqLTZQYpjSYbBTLPS78s+FSrstj7wM6GEE7l0OAqQ3ClRiSUWOhYFyF6Rs9h4UR99praqI6cYx1ZEWqX" +
                "jMKN85TtyoYglotaIyDZCK4bsZBBeM6cACM4k8Y53jk34RJpO2UIskfV4TxZoaOAoRQ4e5EX1MzRc4zB" +
                "acYMbdYsCKp7aDGlGXORoiQfJSLHjDb92/HXahUTuBf0lqyk97OY9d3LKpxoWxyKMQRZUQ6CCHMq9UyX" +
                "rYEdg3DUoXOltAIg1aQQwUyg/CB1tIofR+4l2mFuhEVbnaQqCsXMOMl3Xiqdwsu16HawPN2k0R5jChxR" +
                "XNZ9uptL3UB6bivusCnuTA9uf+9XFNubi9Gn0/GnM7F6DcQ3+N8mZM6iGmWypMi5iHRBgpZtN+y6xlaN" +
                "dJjDk8n4t5HYwHyzjcltKnmPdoMWPSVOvJ2ALy5Ho48Xk9FpD/x2G9hTSWj8iotNomf2RSDkLCJ8KF9Y" +
                "77kq6TZPCVsV4l9ee/hDvWUvtF0YM2tuiBF0DCsUEN2fkG8wmwwPykgHHeWrX09ORqPTDcrvtilzG5Jl" +
                "rYlph1SyF2aJp+RDjnhMzfCnz5drv7Cabx9QM3XZdJVyra+5P6hJJXrSNZwVwaGRzaQ2CU3uEXqXo19G" +
                "Jxv8BuK7+/Q8/UllfCQDcoNzKd5Nl6+f5jilUqKrZ8xeWcIWwc04j03sMdreSIMO/IgBXeb1lTIQ3/8P" +
                "mdennnUxF+E6+frg9R4+GZ6fryt5IH7YlWA3nx5iuIt3EZP70dombWfaN7zy8XSMm10gMyG1ZcRmmrz/" +
                "Akbs5mZOiq3yaxXwLvVITpx/vppsQg3Ejxlw2G8O3UoFJKEQNQah1gmydwGjHLU7cre6sN+mO9ReYGzH" +
                "3maXLjTMf2BvwXYxNMYt8rbOgigFv71ZSNFN/7xEbAwzPqJomiqeZKsVIdJtfJkloRvF/WrAS7vXzl9L" +
                "Wxnqv5ZzBzubl1sZVs+Z/3lp6B9UX/IJtbeiKP4BpSluoIwPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<AveragingAction> CreateSerializer() => new Serializer();
        public Deserializer<AveragingAction> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<AveragingAction>
        {
            public override void RosSerialize(AveragingAction msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(AveragingAction msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(AveragingAction msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(AveragingAction msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(AveragingAction msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<AveragingAction>
        {
            public override void RosDeserialize(ref ReadBuffer b, out AveragingAction msg) => msg = new AveragingAction(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out AveragingAction msg) => msg = new AveragingAction(ref b);
        }
    }
}
