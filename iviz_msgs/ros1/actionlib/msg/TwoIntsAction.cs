/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsAction : IDeserializableRos1<TwoIntsAction>, IMessageRos1,
		IAction<TwoIntsActionGoal, TwoIntsActionFeedback, TwoIntsActionResult>
    {
        [DataMember (Name = "action_goal")] public TwoIntsActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TwoIntsActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TwoIntsActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public TwoIntsAction()
        {
            ActionGoal = new TwoIntsActionGoal();
            ActionResult = new TwoIntsActionResult();
            ActionFeedback = new TwoIntsActionFeedback();
        }
        
        /// Explicit constructor.
        public TwoIntsAction(TwoIntsActionGoal ActionGoal, TwoIntsActionResult ActionResult, TwoIntsActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        public TwoIntsAction(ref ReadBuffer b)
        {
            ActionGoal = new TwoIntsActionGoal(ref b);
            ActionResult = new TwoIntsActionResult(ref b);
            ActionFeedback = new TwoIntsActionFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsAction(ref b);
        
        public TwoIntsAction RosDeserialize(ref ReadBuffer b) => new TwoIntsAction(ref b);
    
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
        public const string MessageType = "actionlib/TwoIntsAction";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6d1aa538c4bd6183a2dfb7fcac41ee50";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XTW/bRhC981cM4EPsonbaJE1TAzqotuKqcBLDVns1luSI3JZcqrtLyfr3fbP8EOVY" +
                "tVDEEmRTFHffvJl5M7Oaraqp8W6ceF2Zq0oVpMLH+wyfo9nw6S27uvDdcxvutld8ZE5jlfzdrZm399Ho" +
                "G7+iT3dX562VQsevZ4+9iH5jlbKlPFyifuV96TL3WlZML0lcvNdp50PwPrj9MnSdTxvzDbfoiO68Mqmy" +
                "KZXsVaq8onkFzjrL2Z4WvOQCm1S54JTCU79esDvDxlmuHeGdsWGrimJNtcMiX1FSlWVtdKI8k9clb+3H" +
                "Tm1I0UJZr5O6UBbrK5tqI8vnVpUs6Hg7/qdmkzBNL8+xxjhOaq9BaA2ExLJy2mR4SFGtjX/7RjZERwjk" +
                "KW45Q+R74+Rz5YUsPywgGuGp3DlsfNc4dwZsBIdhJXV0HL67x607IRgBBV5USU7HYH6z9nllAMi0VFar" +
                "uGABThABoL6STa9OBsgmQBtlqg6+QdzY2AfW9Lji02mOnBXivaszBBALF7Za6hRL43UASQrNxhPkZpVd" +
                "R7KrMRkdfZQYYxF2hYzgqpyrEo0EpLTSPo+ct4IesiHqfOniGZZEkFZLllxe1UWKm8py8Cs4glyuco2E" +
                "BCekXGilHFkRjIMTIqBpyHeQJEKiTGsMSbZLSGOVsyHtCY6yE9FCF1wu0FqKArsF0zWqWTFM99AU81y4" +
                "KErYeoXMCaNhfFv+Ou1ygvCC3lqM9HGmed+mTIodTSdDDTqnMg5JILfgRM910jjYMnBnLboUSLMApMra" +
                "eTAjVB1WnXX5k8wdqu+Fjheh8t6/I9Ve48M23WY4PN920fJ87SRduHSdt50s7Uh5Yd6PuUSPpoE0tA8d" +
                "webmZvL5cvr5irrXiH7A/0ZrQSA5KmDNXmQGJUB7SdPo2oawJf8Wc3wxm/45oQHmj9uY0oFqa9FJ0HRj" +
                "Fk3tBXxzO5l8uplNLnvgN9vAlhNGK0+ljhTaYa9vUnOP5KEy4b2VguOH0PdNFtF/vI7wh1IKUWgaLKbQ" +
                "omBB0N51KCB6PGNbYtoUMvo8n7SU7/64uJhMLgeU325Tlg6jklyz0HZ1IlGY1zL3ngrELjPjX7/cbuIi" +
                "Zt49YSaugutpHcp4w/1JS2nNz4ZGVOEq9Ki50kWN/rWD3u3k98nFgN+IfvqanuW/OPE7FBB6V1X7x3L5" +
                "/nmOMScKDTtg9sZqnAukz4aJiJOJNktVoLnucKBVXl8pI3p/AOX10jOVD0W4EV+fvD7CF+Pr600lj+jn" +
                "fQm2o+cphvtEFzn5OlvbpM1c21IOcTL4/LALBCacbjkxlMmHb+DEfmEWUWyVX2NAjkk7NHH95W42hBrR" +
                "LwFw3B8K2tMSkChF1gSEmyCoPgSCctacettTicQt3qP2nGBXEm0J6UrD/SeOJDg4jIuiWoXztyxEKdjt" +
                "Q4OidrCH88FglMmWlOM6yySM7SLPD/5w878dvs3gd3V52NHf/er7n8O//9F46F+LPe/oXy1H65IFDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
