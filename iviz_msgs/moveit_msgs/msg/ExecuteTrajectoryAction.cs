/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryAction : IDeserializable<ExecuteTrajectoryAction>,
		IAction<ExecuteTrajectoryActionGoal, ExecuteTrajectoryActionFeedback, ExecuteTrajectoryActionResult>
    {
        [DataMember (Name = "action_goal")] public ExecuteTrajectoryActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ExecuteTrajectoryActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ExecuteTrajectoryActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public ExecuteTrajectoryAction()
        {
            ActionGoal = new ExecuteTrajectoryActionGoal();
            ActionResult = new ExecuteTrajectoryActionResult();
            ActionFeedback = new ExecuteTrajectoryActionFeedback();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryAction(ExecuteTrajectoryActionGoal ActionGoal, ExecuteTrajectoryActionResult ActionResult, ExecuteTrajectoryActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal ExecuteTrajectoryAction(ref Buffer b)
        {
            ActionGoal = new ExecuteTrajectoryActionGoal(ref b);
            ActionResult = new ExecuteTrajectoryActionResult(ref b);
            ActionFeedback = new ExecuteTrajectoryActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryAction(ref b);
        
        ExecuteTrajectoryAction IDeserializable<ExecuteTrajectoryAction>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryAction(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "24e882ecd7f84f3e3299d504b8e3deae";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZa0/jRhf+7l8xElILFWEL7K0r5UMAw6YbYpoYpKpaWRN7krjreFJ7TDbvr3+fM56x" +
                "HZMsSC2AdiH2nDlzLs+5zIn7XYSFEn7G/xahktm6F6pYpleSJ4zrj8EMnx13O91I5EWiLGWmn3bRXgoR" +
                "TXj4zVJPzbPT/Y9/nOvx1Se2kPciVsEin+VvfqCl81nwSGRsrv84pWxJPCk3EkX/gpEJgjh6qJm2kzbQ" +
                "8yiRq6gUpJTS2WNjxdOIZxFbCMUjrjibSkgfz+Yi6yTiXiTYxBdLETG9qtZLkR9hoz+Pc4Z/M5GKjCfJ" +
                "mhU5iJRkoVwsijQOuRJMxQuxsR8745RxtuSZisMi4RnoZRbFKZFPM74QxB3/cvFPIdJQsP7FJ9CkOdkq" +
                "hkBrcAgzwfM4nWGROUWcqtMT2uDs+SvZwaOYwQfV4UzNuSJhxfclQEVy8vwTzvilVO4IvGEcgVOinO3r" +
                "dwEe8wOGQyCCWMpwzvYh+c1azWUKhoLd8yzmk0QQ4xAWANefadPPBw3OJPYnlvJUWvYlx/qMp7BNK76k" +
                "U2cOnyWkfV7MYEAQLjN5H0cgnaw1kzCJRaoYgJfxbO3QrvJIZ++SbAwi7NIewV+e5zKM4YCIrWI1d3KV" +
                "EXftDcLpM6Fxa3BoaBlhWT6XRRLhQWYkcoknBl+u5jEcopWgcGErnrOMAJNDCQJQX/tbQxIm4ak5DE7O" +
                "7gGN1VykLFYMioqcQAtciMUSqSdJsJt45iVqVgJHV6zZRCA+IAILRaY4PEcSNe1r5I8j6xOYF+LBLbK2" +
                "M7PJCpJF2FFmOsRgnvOZ0E5g+VKE8TQOSwWNBPmR4U4BUhJAqEWRK0jGEHWgOrL+I8+9RjbUedAZyYlU" +
                "9UuY2n58CaFapzv16eX67xJZoiHd3/QcqN0bruGg+MK7bG9c0PsgktPgAYtnUvMRVVoVqMTCX1+NhilC" +
                "OndaW27oESRL+pu/ktxaCMDa5RTZtYG1UBXYkcMRHFBvKfOYIjr/65ChKCCuFFbxwMNQJChJevHrV3CU" +
                "m9RiihBWX3Wh0zmlgVFJYSRKQOs80kuSRqDd8wRRyDgyAIU+5SlKpqhfkAgpiN5oOzNtZyJqaXkE1R1n" +
                "mkiu3r/VJjeCNd7V6jRebqjVeF9q40RFuaTzUTDN5CJAPsLCCzlzR3iYbFhiscrIMHNpU1OsWg0AJUW9" +
                "kIkpsi+VS51gtzjMqp23UE+ew3aq9qiDpWXkFJlYh2sH4Wr81OC1Lwh5JdyoKNJamuPQhd5/YLFZUvBE" +
                "IsW2xFnFgAvqe1JEpAWAkiHz47DKzW9q577ZdOleWW/mBkYaWIlIZygF5lXFrQGxQ22sjU0lGi0ZVXdq" +
                "p9rQ1MyOnO35YYc3XydP/EgY65O2W0MY30Ks4Ui2X6AZlOw9A78DZyYk2l57jm+poGG1A1pqCCPiiSey" +
                "A7fxuW4kBgtNmcUzBD3kqO3dPmYV52ozyh8cQe1KjY1/d84myl48Ueywsb1BVDFaArj21ESolUCTplby" +
                "QYLQiXWKjg2W4SEaHudOY+K03J9oBZ0/CmzIUtI1k2UOeBkljTBbVCTs0FpLflY1rGiH0VIITplJ1jux" +
                "MYozbIUOugPMdBt6SC1sJGGPVFIoLPg3sBS4Lehuc7lMKvSXNqHX2LIvjmZHh2UTrKmoW9R3M32bQ89J" +
                "8IpaCVDzZEa5Q6amJ2W+0zKXh8GF1M0aax8csf6UrWWB/hc64ENmLpG6zFq59GVHSXlIxcGw2DSoDvWq" +
                "O45TdOIc1dlWQfa9+rSuPv3vRVxdY2ybtxGguF/Y+rPhc3r6pwYoGflRheyn1QvFKiUQq5a9Oed19tvU" +
                "Z5LJb3SnSjXEclw9U4G7KVUnns70RZ/u/Jgd2Fg1JPWzoXuVO0tz/vT4DAdTE1XgKq//PBzjmDGWmV89" +
                "jzY7pXJaQyaajny0opYPN+7woj+8Yvany37F77JV07dN6ifWQplAxUU2LKcmZrqwcZc2PHvnfv/OZQ2e" +
                "x5s8aZxRZGjnFBLFRFDKeRLjm5HrXt/47kXF+GSTMfKiwFwIOKP6iWJnL8uMTxFflPB0hQNF2dnjaKcW" +
                "9OHPHv7brqqc1mCktUwEcSBoGy4QdN8X2QKFKaE5mhIHRuTx7fm56140RD7dFJnGFWhbYszXMN0oUKHz" +
                "fFrQEG2bIXYd0zvzRrVd6Ji3W46Z4HpA07dCzwRq2beeFBXiUdPo9h2Jgk15nBToWnaIN3J/d88b8nXZ" +
                "u4fiZYJiZgcC9CBEFqoNl8PHZZyIkJtyUh9WoIWgoY2uOLoK40KHOrdDAYO8KlK66BufH3kV9FDTdRDW" +
                "4KucV1n4vDcY1JHcZR+eKqCZY22T8CnWhU8eemtT6HQaZwu67NGtpnKDHmqSJCLaUKIJk4//gRJPMzOB" +
                "YiP8ygNo5roDEwNv7DdZddlvmmGvmjCa0Su18hG8RkxEaQRemYC4UB+Hj2bESXabPCH2dDeIQlZeA1Zo" +
                "CrfNN3GjwOBCrqr7AkIh25xActhMZwQ9bGwUNdoSiUkxwwVjZkeJSnzHDeE1CrMpyY5zDaq+crNMZueS" +
                "xqqCPgYhPr+EYO3jnfJLByQQur6RRzA6hgvn/D6WmVnVhWA87h6b58tef3A7cru/0Y9jXt4MesMhgjig" +
                "Vfei27HU/eFdb9C/CK49v+8NA6Lrdk7MYuNlYAh7SLbB2Z+BO7zrj7zhtTv0g/PPveGV2+2cmm3n3tAf" +
                "eYPqrLfm/e2wdzZwA98Len/c9kduMHaHY28UgGmv23lnqPz+NY7wbv1u572V3pbnbucDWWKZ8DQlyPzE" +
                "vgH5dHXAdxw2YrXPrO3Gfm/kB/jtu1AhOPeQy8ZQChb4dQvJXd8b4O84uOn5n0E9HPujXn/oj0F/bI15" +
                "5fUGbWYnzbUfcTltEjaW7CbyzVun5Z2rkXd7Ewx717Dy8bv2YosTSN63SEbemWdUxOqH1iqy+xfL/GNr" +
                "zTujAmtXgSd8J7DG3WixaebLEQgCCDAcX3qj68CCsHNigVYZC3Bxz78QFoGHO9ARKEBoLdiQlX7rNWs0" +
                "A5j+8NKr1mCsvSYMNuQaekH/SzD2BreEZED0+CXi2P3xt8n/uvevvpZ+ze+jK23sfE+LK5z/A5ASR2Gf" +
                "HwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
