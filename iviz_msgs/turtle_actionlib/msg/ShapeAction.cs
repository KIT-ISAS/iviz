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
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXXU8bRxR9X4n/MBIPgaqQNulHiuQHFwyhIgnCbl+t8c717rSzO+7MLMb/vufO7K5t" +
                "MIpVJcQC7PHeOffc78u4lAsa5kHb+spKI2T8OC3wORuvn92Rb0zonrp42nx+SaRmMv+nk5i354Ns8IVf" +
                "B9mH8dWZCI0LhqZJndGz148MOcjek1TkRBnfsl5wWvnCv2aR6wvBZk61SpZE+/mbr0baB5XUJ24H2aEY" +
                "B1kr6ZSoKEglgxRzC9K6KMmdGLong1uyWpAS8WlYLcif4uKk1F7gp6CanDRmJRoPoWBFbquqqXUuA4mg" +
                "K9q6j5u6FlIspAs6b4x0kLdO6ZrF505WxOj48fRvQ3VO4vriDDK1p7wJGoRWQMgdSa/rAg9F1ug6vH3D" +
                "F7LDydKe4EgFXN8rF6GUgcnSwwK5wzylP4OO75Jxp8CGdwhalBdH8bspjv5YQAko0MLmpTgC89tVKG0N" +
                "QBL30mk5M8TAOTwA1Fd86dXxBjLTPhO1rG0HnxDXOvaBrXtctumkRMwMW++bAg6E4MLZe60gOltFkNxo" +
                "qoNAvjnpVhnfSiqzw0v2MYRwK0YE79J7m2sEQImlDmXmg2P0GA1Oz6+WkDurgtNyAhtS6HxpG6NwsI5Z" +
                "p5QSCOey1IhJtIOLRiylF45zxsMOzqHrGPKYlfCKrFttiLO7R3YsS6qFDgK2kue8RWpQtUCTMQa3GdOn" +
                "xFkSVPfQYkYoEVAQObkgETxmtOnilr9WXVjgYdBDZOza1aJrUWCmcCP1NJSh97KgGAfhF5Truc6TgS0D" +
                "f9qic40kAZCqGh/ATKDwIHXahRBS2Qv3wNT9slSTpAry2dxYyScnlW78N+nJaYDs0ZXREEODKo1vqTG3" +
                "wydF6CVrIXE5yB5NC+537zqG6XA7+nhx/fFKdK+B+AF/Ux7G5ClRHStCtlvOEuRlnvpg2y+2SqPFHJ5P" +
                "rv8aiQ3MH7cxuUE1zqHRoCfPiPNtL+Dbu9How+1kdNEDv9kGdpQTOj26NDogumWf+0LOA6KHqoX1jouR" +
                "HuJYqItsTfTp6xC/KLPohdR/MaQWhhhBB9+hgOjRhFyFYWR4MgY6bimP/zw/H40uNii/3abM3UfmpcbE" +
                "RLNqcvbCvOGxuMsRz6kZ/v7pbu0XVvPTDjUzG01XTSzxNfedmlRDn3UNZ4W36F9zqU2D3vYMvbvRH6Pz" +
                "DX4D8fNTeo7+pjz2yV10uK/ZJjxOl+8/z3FGuUQzj5i9sgZrA/fgODCxuOj6Xho03mcMaDOvr5SB+OUF" +
                "Mq9PvdqGWITr5OuD13v4fHhzs67kgfh1X4LtWNrFcB/vIiZPo7VNup5rV/GOx0OxD0NcU5gJqS0jNtPk" +
                "3RcwYj83c1JslV9SwFvUMzlx82k82YQaiN8i4LBfGNplCkhCIWoMQskJsncBo/Bgxsd2Y2G/zfaoPc/Y" +
                "lr3NLl1qmL9jXcFSMTTGLuN6zoIoBbe9UEj4LHaEuDtszDK+omjWFAW7sRUK9BBefDfopnC/EfCq7rR1" +
                "U1kXhvqv5cLCxuqbbAqX/b+O/2dX6G73690L27Bmj9j+BwamZqRdDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
