/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingAction")]
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
        [Preserve] public const string RosMd5Sum = "628678f2b4fa6a5951746a4a2d39e716";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VXXVMbNxR931+hGR4CnUDapE1TZvzggkPpkIQBt6+MvLq7q1YruZLWxv++52rXay8f" +
                "wdMJxWMwwlfnnvt9d7wgL0tty3EetbNnThoh0583Jf7OxsPvryg0Jq4lfDrdlflIpGYy/3stVXTnbPSN" +
                "X9mn67PjTovRs5vYROe1NOHN+L5V2W8kFXlRpY9sc6sOZXjDEuengk2+0WpjUfJHcsTzkA9RtQRadtme" +
                "uI7SKumVqClKJaMUhQNrXVbkDw0tyOCSrOekRPo2ruYUjnBxWukg8C7JgrwxK9EECEUnclfXjdW5jCSi" +
                "rmlwHze1FVLMpY86b4z0kHdeacvihZc1MTregf5pyOYkzk+PIWMD5U3UILQCQu5JBjgMX4qs0Ta+e8sX" +
                "sr3p0h3iSCV83ysXsZKRydLtHEnEPGU4ho7vWuOOgA3nELSoIPbT/25wDAcCSkCB5i6vxD6YX65i5SwA" +
                "SSwkoj8zxMA5PADUV3zp1cEWsk3QVlq3hm8RNzp2gbU9Ltt0WCFmhq0PTQkHQnDu3UIriM5WCSQ3mmwU" +
                "SDgv/SrjW63KbO8j+xhCuJUigk8Zgss1AqDEUscqC9EzeooG5+ezl9JWUaTU6siKULnGKBycp2RXMgSx" +
                "XFYaAUlGcLmIpQzCc8IEGMEJdJ7inVISLpG2U4YgexQb7pMVOgoYSoGTFnlB9RytxhjcZszQZs2SoLqH" +
                "FjMqmIsUOfkoETlmtO3fjr9W65jAvaC3YiW9n0XRNy2rcKPtbKjBEGRJKQgizCnXhc5bAzsG4ahD5wJp" +
                "BUCqbkIEM4Gqg9TROn4cuZfogqn/ZV1RwieGwss143aEPN2O0QhjEziI+Nh05G4CdaPnua24wya7Mye4" +
                "0X1YU2wPl5PPp+efz8T6NRLf43ebgylxKlTGiiKnHzIEOZm3DbBrFIOy6DDHJ9PzPydiC/OHISZ3psZ7" +
                "dBg04xlxru0EfHk1mXy6nE5Oe+C3Q2BPOaHFK64viTbZ572QRUT4ULGw3nMh0m2aB7bMxFdee/hBiSUv" +
                "tI0X0wkJyQg6hjUKiO5PydeYQoZHYqSDjvL1Hycnk8npFuV3Q8rceWReaWLaocnZC0XD8/AhRzymZvzr" +
                "l6uNX1jNjw+omblkumpSeW+4P6hJNfSkazgrgkPvKqQ2DfraI/SuJr9PTrb4jcRP9+l5+ovy+EgGpJ7m" +
                "mng3XV4/zXFGuUQjT5i9sgb7AvffNCmxsWi7kAZN9xEDuszrK2Uk3v8PmdennnUxFeEm+frg9R4+GV9c" +
                "bCp5JH7elWA3kh5iuIt3EZP70RqStoX2NS93PBDjdhdITEgNjNhOkw/fwIjd3MxJMSi/VgGvT4/kxMWX" +
                "6+k21Ej8kgDH/bLQbVFAEgpRYxBqnSB7FzDKUbsNd9sK+222Q+0FxnbsbXbpUsP8B1YVLBRjY9wy7eUs" +
                "iFLww2VCim7gp71ha5jxFUWzpuRJtt4KIt3Gl9kLulGcFcZJ3g1qkrY/8KOJosXLLQrr58j/vCr0D6Iv" +
                "+QTaWzHYv3ov88b/df//C6uxrhWiDwAA";
                
    }
}
