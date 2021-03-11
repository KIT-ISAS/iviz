/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingAction")]
    public sealed class AveragingAction : IDeserializable<AveragingAction>,
		IAction<AveragingActionGoal, AveragingActionFeedback, AveragingActionResult>
    {
        [DataMember (Name = "action_goal")] public AveragingActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public AveragingActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public AveragingActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingAction()
        {
            ActionGoal = new AveragingActionGoal();
            ActionResult = new AveragingActionResult();
            ActionFeedback = new AveragingActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingAction(AveragingActionGoal ActionGoal, AveragingActionResult ActionResult, AveragingActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingAction(ref Buffer b)
        {
            ActionGoal = new AveragingActionGoal(ref b);
            ActionResult = new AveragingActionResult(ref b);
            ActionFeedback = new AveragingActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingAction(ref b);
        }
        
        AveragingAction IDeserializable<AveragingAction>.RosDeserialize(ref Buffer b)
        {
            return new AveragingAction(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVX204jRxB9H4l/aImHhSiwyW4uGyQ/OGAIEbuLwMkras+UZzrp6Xb6gvHf51TPzQaj" +
                "taINWMZ229WnTt2L8T05WSpTjvOgrLmwUguZPt6V+JyNN3+/IR916CRcOj2WOScqZjL/u5Oat+e9bPSV" +
                "H3vZx9uLk1aPVrO7EIN1Smr/dotde9lvJAtyokpv2XCt9qV/yyKXZ4KtvlPFYFRyCX/7v/H3oWgoNPz2" +
                "sn1xG6QppCtETUEWMkgxtyCuyorckaZ70rgl6wUVIv0aVgvyx7g4rZQXeJZkwF/rlYgeQsGK3NZ1NCqX" +
                "gURQNW3cx01lhBQL6YLKo5YO8tYVyrD43MmaGB1PT/9EMjmJy7MTyBhPeQwKhFZAyB1JD5/hR5FFZcL7" +
                "d3wh258u7RGOVML9vXIRKhmYLD0skErMU/oT6PimMe4Y2PAOQUvhxUH67g5HfyigBBRoYfNKHID59SpU" +
                "1gCQxL1EBsw0MXAODwD1DV96c7iGzLRPhJHGdvAN4qBjF1jT47JNRxViptl6H0s4EIILZ+9VAdHZKoHk" +
                "WpEJAjnnpFtlfKtRme2fs48hhFspIniX3ttcIQCFWKpQZT44Rk/R4BR9gYJaqwxOyylsaELnKxt1gYN1" +
                "zLpJKYFwLiuFmCQ7uGjEUnrhOGc87OAcukwhT1kJr0jTakOcHUoO98kIFQRsJc95i9SgeoGeozVuM6Zv" +
                "EmdJUN1DixmhREBB5OSCRPCY0bqLW/6q6MICD4MeImMHV4uuW4FZgRtNi0MZei9LSnEQfkG5mqu8MbBl" +
                "4I9bdK6RRgCk6ugDmAkUHqSOuxBCKnuddtg0wqwpTSpK8tlcW8knJwsV/Wt26Wa67NCn0R5DRM2mt6FV" +
                "t9OpidlLVkfDZy97NEO4A37oWDaH68mns8tPF6J7jMR3eG0yM6VThXpZEfLfct4gU/OmM7YdZKNYWszx" +
                "6fTyz4lYw/x+E5NbVnQOrQddekacgTsBX99MJh+vp5OzHvjdJrCjnND70bfRE9E/+2oQch4QQdQxrHdc" +
                "nvSQBoUps4Ho08c+/lB4yQtNR8bYWmhiBBV8hwKiB1NyNcaT5lkZ6LClfPvH6elkcrZG+f0mZe5HMq8U" +
                "ZijaV8zZC/PIg3KbI55TM/71883gF1bzwxY1M5tML2Iq+oH7Vk1FpC+6hrPCW3S0uVQ6ots9Q+9m8vvk" +
                "dI3fSPz4lJ6jvyhPnXMbHe50NobH6fLtlznOKJdo7wmzVxaxSHBXTiMUq4wy91KjFT9jQJt5faWMxE8v" +
                "kHl96hkbUhEOydcHr/fw6fjqaqjkkfh5V4LtoNrGcBfvIiZPo7VJ2syVq3nr4zHZhyEtLsyEig0j1tPk" +
                "w1cwYjc3c1JslF+jgPeqZ3Li6vPtdB1qJH5JgON+hWjXKyCJAlFjEGqcIHsXMAqPanxsdxj222yH2vOM" +
                "bdnb7NKlgvlbFhisGWOt7TIt7CyIUnCbK4aEz1JHSNvE2jzjKwXNYsnDrNsVAj2E19oWuoHc7wi8wztl" +
                "3Z00pab+a7mwMLV+zd3hvP8v879uDx1CvwK+jjmDIQj6v2AQY2+fDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
