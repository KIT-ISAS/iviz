/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract (Name = "actionlib/TwoIntsAction")]
    public sealed class TwoIntsAction : IDeserializable<TwoIntsAction>,
		IAction<TwoIntsActionGoal, TwoIntsActionFeedback, TwoIntsActionResult>
    {
        [DataMember (Name = "action_goal")] public TwoIntsActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TwoIntsActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TwoIntsActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsAction()
        {
            ActionGoal = new TwoIntsActionGoal();
            ActionResult = new TwoIntsActionResult();
            ActionFeedback = new TwoIntsActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsAction(TwoIntsActionGoal ActionGoal, TwoIntsActionResult ActionResult, TwoIntsActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsAction(ref Buffer b)
        {
            ActionGoal = new TwoIntsActionGoal(ref b);
            ActionResult = new TwoIntsActionResult(ref b);
            ActionFeedback = new TwoIntsActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsAction(ref b);
        }
        
        TwoIntsAction IDeserializable<TwoIntsAction>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsAction(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6d1aa538c4bd6183a2dfb7fcac41ee50";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                "eTAjVB1WnXX5k8wdqu+FjofCe/+OVHuND9tzm9nwfNdFx/O1k2zh0jXedrC0E+WFeT/mEj0aBtLPPnQE" +
                "m5ubyefL6ecr6l4j+gH/G6kFfeQogDV7URmEAOklTZ9r+8GW+lvM8cVs+ueEBpg/bmNKA6qtRSNBz41Z" +
                "JLUX8M3tZPLpZja57IHfbANbThidPJUyUuiGvbxJzT2Sh8KE91bqjR9C2zdZRP/xOsIfKilEoemvGEKL" +
                "ggVBe9ehgOjxjG2JYVPI5PN80lK+++PiYjK5HFB+u01ZGoxKcs1C29WJRGFey9h7KhC7zIx//XK7iYuY" +
                "efeEmbgKrqd1qOIN9yctpTU/GxpRhavQouZKFzXa1w56t5PfJxcDfiP66Wt6lv/ixO9QQGhdVe0fy+X7" +
                "5znGnCj064DZG6txLJA2GwYiDibaLFWB3rrDgVZ5faWM6P0BlNdLz1Q+FOFGfH3y+ghfjK+vN5U8op/3" +
                "JdhOnqcY7hNd5OTrbG2TNnNtSznDydzzwy4QmHC65cRQJh++gRP7hVlEsVV+jQE5Je3QxPWXu9kQakS/" +
                "BMBxfyZoD0tAohRZExBugqD6EAjKWXPobQ8lErd4j9pzgl1JtCWkKw33nziR4NwwLopqFY7fshClYLfP" +
                "DIrauR6OB4NRJltSjusskzC2izw/+MON/3b4NoPf1eVhR3/3o+9/Dv/+N+Ohfyz2vKN/AQ9OCZ4EDwAA";
                
    }
}
