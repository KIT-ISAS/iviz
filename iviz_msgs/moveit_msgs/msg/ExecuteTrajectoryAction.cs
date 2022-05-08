/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
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
        public ExecuteTrajectoryAction(ref ReadBuffer b)
        {
            ActionGoal = new ExecuteTrajectoryActionGoal(ref b);
            ActionResult = new ExecuteTrajectoryActionResult(ref b);
            ActionFeedback = new ExecuteTrajectoryActionFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ExecuteTrajectoryAction(ref b);
        
        public ExecuteTrajectoryAction RosDeserialize(ref ReadBuffer b) => new ExecuteTrajectoryAction(ref b);
    
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
        public const string MessageType = "moveit_msgs/ExecuteTrajectoryAction";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "24e882ecd7f84f3e3299d504b8e3deae";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VZa2/buBL9rl9BIMDdZBG7m0efgD+4iZJ661hZWwlwsSgEWqJtbWXRK1JxfX/9PUNR" +
                "sqTYbYDdJkEQW+JwOI8zD07cbyLMtfAz/pcItcw2/VDHMr2WPGHcfA3m+O64u+nGQuWJLikz87SP9kqI" +
                "aMrDryX1zD47vX/5x7mZXH9gS/kgYh0s1Vy92iMRael8EjwSGVuYD6eQLYmnxUaiGFwyMkEQR481M3Yy" +
                "Bvo5SigdFYIUUjoHbKJ5GvEsYkuhecQ1ZzMJ6eP5QmSdRDyIBJv4ciUiZlb1ZiVUFxv9RawYfuciFRlP" +
                "kg3LFYi0ZKFcLvM0DrkWTMdL0diPnXHKOFvxTMdhnvAM9DKL4pTIZxlfCuKOXyX+zkUaCja4/ACaVJGt" +
                "Ygi0AYcwE1zF6RyLzMnjVJ+d0gbnwF/LDh7FHD6oDmd6wTUJK76tACqSk6sPOOPXQrkueMM4AqdEih2a" +
                "dwEe1RHDIRBBrGS4YIeQ/HajFzIFQ8EeeBbzaSKIcQgLgOsvtOmXoxrn1LBOeSpL9gXH7RlPYZtWfEmn" +
                "zgI+S0h7lc9hQBCuMvkQRyCdbgyTMIlFqhmAl/Fs49Cu4kjn4IpsDCLsMh7BJ1dKhjEcELF1rBeO0hlx" +
                "N94gnP4kNO4MDgMtKyxTC5knER5kJoxeRhH4cr2I4RCjBIULW3PFMgKMghIEoIHxt4EkTMJTexicnD0A" +
                "GuuFSFmsGRQVikALXIjlCqknSbCbeKoCNWuBoyvWbCpmJAtnocg0h+dIorp9rfxxVPoE5oV4GzqksjOb" +
                "VckrjbCjyHSIQaX4XBgnMLUSYTyLw0JBK4HqWu4UIAUBhFrmSkMyhqgDVbf0H3nuJbKhyYPOWE6l3r6E" +
                "qcuvzyFU63Rne3qx/rtElqhJ9xc9B3r/hhs4KL70rtobl/Q+iOQseMTiJ6n5A1VaFajAwp9frIYpQlo5" +
                "rS239AiSFX2qF5LbCAFYu5wie2tgI1QFduRwBAfUW0kVU0SrP48ZigLiSmMVDzwMRYKSZBa/fAFH2aQW" +
                "M4Sw/mIKnckpNYxKCiNRANrkkX6S1ALtgSc5BTQyQFxka0XJFPULEnFl3hg7M2NnImpp2YXqjjNLJNdv" +
                "zo3JrWC1d1t1ai8batXeF9o4UV4smXwUzDK5DJCPsPBMztwTHjYbFlisMjLMXNjUFqtWA0BJ0SxkYobs" +
                "S+XSJNgdDivVVi3Uk+ewnao96mBhGTlDJjbh2kG4Wj/VeB0KQl4BNyqKtJYqHLo0+49KbBYUPJFIsS1x" +
                "1jHggvqe5JEwRSfLkPlxWOXmV1vnvmq69KCoNwsLIwOsRKRzlAL7quJWg9ixMVZjU4HGkoyqOzPMm9A0" +
                "zLrO7vywx5svkye+J0zpk7ZbQxi/hFjNkewwXxH63jDwO3LmQqLtLc/xSypoWO1QtqAj4onnlEq/deGm" +
                "lhhKaMosnscGalt7t49Zx0o3o/zREWkj3P/ZOU2UPXui2GPj8gZRxagqY8l6air0WqBJ02v5KEGYxDpD" +
                "xwbL8BANj3NvMHFW7E+Mgs4fOTZkKemaySIHPI+SVpgdKhJ2aK0lP6saVrTDaCkEp8wktzuxMYozYVrY" +
                "boEVakOPqYWNJOyRSgqFJf8KlgK3BdNtrlZJhf7EOl3SlkPRnXePiybYUFG3aO5m5jaHnpPgFbUSoOHJ" +
                "rHLHTM9Oi3xnZC4Ogwupm7XWPuqywYxtZI7+FzrgS2YvkabMlnKZy46W8piKg2XRNKgJ9ao7jlN04hzV" +
                "uayC7Fv1bVN9+9+zuHqLsV3eTilOq/rT8Dk9/b0FKBn5hwqV39bPFKuUQEq1ypuz2ma/pj7TTH6lO1Vq" +
                "IKZw9UwF7qZUnXg6Nxd9uvOrbhWrlmT7bOle5M5Snz/9eIYzgUtzRTc+fDwe49gxlp1f/Rxt9krltIZM" +
                "NB15V4paPNy6o8vB6JqVPz32G/4WrZq5bVI/sRHaBiousmExNbHThcZd2vLsX/iDe5fVeJ40edI4I8/Q" +
                "zmkkiqmglPMkxrdj17259d3LivFpkzHyoogfaKZD9RPFrrwsMz7T1HVqZiocKIrOHkc77Ds/B4xVXVUx" +
                "rQnlcpUI4kDQtlwg6KEvsiUKU0JzNC2OrMiTu4sL172siXzWFJnGFWhbYkFiqzwkK8xyGqLtMsS+Y/of" +
                "vfHWLnTM+Y5jptKojrpPJt/KvvOkKBc/NI1p3yX14zxOcnQte8Qbu7+7FzX5euz1Y/EyQTGzBwFmECJz" +
                "3YbL8Y9lnIqQ23KyPSxHC0FDG1NxTBXGhQ51bo8CFnlVpPTQN/585FXQQ003QbgFX+W8ysIX/eFwG8k9" +
                "9vapAto51i4Jn2Jd+OSxt5pCp7M4W9Jlj241up4FjCQiaihRh8m7f0GJp5mZQNEIv+IAmrnuwcTQm/h1" +
                "Vj323jDsVxNGO3qlVj6C14iJKIzAKxMQl25RWO2Ik+w2fULsmW5QkrXJpGs0hbvmm7hR9JNErqv7AkIh" +
                "a04gObNTQjNsrBU12hKJaT6fkxktkRbf9MsME21JdpwbUA20m2Uyu5A0VhX0NQjx/TkEax/vFP90eKBR" +
                "54HxCEcvPBUL/hDLzK6aQjCZ9E7s81V/MLwbu7339OPYl7fD/miEIA5o1b3sdUrqwei+PxxcBjeeP/BG" +
                "AdH1Oqd2sfYysIR9JNvg438Dd3Q/GHujG3fkBxef+qNrt9c5s9suvJE/9obVWef2/d2o/3HoBr4X9P+4" +
                "G4zdYOKOJt44ANN+r/PaUvmDGxzh3fm9zptS+rI89zpvyRKrhKcpQeY/7CuQT1eHsJrNFz4rbTfx+2M/" +
                "wF/fhQrBhYdcNoFSsMBvO0juB94Qn5Pgtu9/AvVo4o/7g5E/Af1Jacxrrz9sMzutr32Py1mdsLZUbiLf" +
                "nDst71yPvbvbYNS/gZVPXrcXW5xA8qZFMvY+elZFrL5trSK7fy6Zv2uteR+pwJar78n6aoO70bJp5qsx" +
                "CAIIMJpceeOboARh5/SkAoU1FuDiXnwmLAIP96AjUICwtGBNVvpr1kqjWcAMRldetXZOMtVg0JBr5AWD" +
                "z8HEG975xk9nJy/Y+Zf/Tf7HvX/1b+mX/H90pU053zPiCuf/kBJHYZ8fAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
