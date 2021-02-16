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
            return new(ref b);
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
        [Preserve] public const string RosMd5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VX224bNxB9368g4IfYRe20SS+pAT2otpI6cBLDVvtqUMvZXbZcUuXFsv6+Z7irleRL" +
                "LRRxLciWKA3PnLmPxjfkZa1tPS6jdvaDk0bI/Pa6xvtivP39JYVk4krC59NdmfdEaibLv1ZSVX8uRl/5" +
                "UXy6+nDcazF6dh1TdF5LE16P71tV/EZSkRdNfinWt9pQh9cscXYq2ORrrdYWZX9kRzwP+RBVR6BjV+yJ" +
                "qyitkl6JlqJUMkpRObDWdUP+0NANGVyS7ZyUyN/G5ZzCES5OGx0EnjVZkDdmKVKAUHSidG2brC5lJBF1" +
                "S1v3cVNbIcVc+qjLZKSHvPNKWxavvGyJ0fEM9HciW5I4Oz2GjA1UpqhBaAmE0pMMcBi+FEXSNr59wxeK" +
                "venCHeJINXw/KBexkZHJ0u0cScQ8ZTiGjm86446ADecQtKgg9vNn1ziGAwEloEBzVzZiH8wvlrFxFoAk" +
                "biSiPzPEwCU8ANRXfOnVwQayzdBWWreC7xDXOnaBtQMu23TYIGaGrQ+phgMhOPfuRiuIzpYZpDSabBRI" +
                "OC/9suBbncpi7z37GEK4lSOCVxmCKzUCoMRCx6YI0TN6jgbn57OX0kZR5NTqyYrQuGQUDs5Ttisbglgu" +
                "Go2AZCO4XMRCBuE5YQKM4AQ6y/HOKQmXSNsrQ5A9ig33yQodBQylwEmLvKB2jlZjDG4zZuiyZkFQPUCL" +
                "GVXMRYqSfJSIHDPa9G/PX6tVTOBe0FuyksHPohqallW40XU21GAIsqYcBBHmVOpKl52BPYNw1KNzgXQC" +
                "INWmEMFMoOogdbSKH0fuJbpg7n9FV5SkagpFZZzkk5dKp/BynbmbJ0/3ZnTFmAJHFC/r9tyPo34OPbcV" +
                "d9gUd4YGd713K4rd4WLy+fTs8wexeozEd/jfJWTOogZlsqTIuYh0QYKWXTfsu8ZWjfSY45Pp2R8TsYH5" +
                "/TYmt6nkPdoNOvOMOPF2Ar64nEw+XUwnpwPwm21gTyWh3ysuNomeORSBkFVE+FC+sN5zVdJtHg62LsS/" +
                "PPbwh3rLXui6MEbV3BAj6BhWKCC6PyXfYiQZno+RDnrKV7+fnEwmpxuU325T5jYky0YT0w6pZC9UiYfj" +
                "Q454TM341y+Xa7+wmh8eUDNz2XSVcq2vuT+oSSV60jWcFcGhkVVSm4Qm9wi9y8nHyckGv5H48T49T39S" +
                "GR/JgNzgXIp30+XbpznOqJTo6hlzUJawPHAzzmMT64u2N9KgAz9iQJ95Q6WMxE//Q+YNqWddzEW4Tr4h" +
                "eIOHT8bn5+tKHomfdyXYz6eHGO7iXcTkfrS2SdtK+5Y3PZ6OcbMLZCaktozYTJN3X8GI3dzMSbFVfp0C" +
                "3qUeyYnzL1fTTaiR+CUDjofNoV+pgCQUosYg1DlBDi5glKNuNe5XF/bbbIfaC4zt2Nvs0oWG+Q/sLdgu" +
                "xsa4RV7SWRCl4Lc3Cyn66Z+XiI1hxlcUzVLNk2y1IkS6jS+zJPSjeFgNeGn32vlraWtDw8dy7mBn+3Ir" +
                "w+rn5X9eGobfpy/5w3Swoij+Ac/yKSaDDwAA";
                
    }
}
