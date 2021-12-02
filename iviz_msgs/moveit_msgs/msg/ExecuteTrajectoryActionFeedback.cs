/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryActionFeedback : IDeserializable<ExecuteTrajectoryActionFeedback>, IActionFeedback<ExecuteTrajectoryFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ExecuteTrajectoryFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public ExecuteTrajectoryActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new ExecuteTrajectoryFeedback();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ExecuteTrajectoryFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal ExecuteTrajectoryActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new ExecuteTrajectoryFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryActionFeedback(ref b);
        
        ExecuteTrajectoryActionFeedback IDeserializable<ExecuteTrajectoryActionFeedback>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryActionFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgxofYndptkzZJPaODKjGOMk7isdVePSCwItGCpAqAkvXv+xb8kFRL" +
                "Ex2SaGTrC3j78PbtYt+T1OREEV8SqYKpK2uyx9Ln/qebWtqHIEPjhY8vSfpEqgk0d/JvUqF2m3dEOpPq" +
                "H7Ho3iSjr/xIPj7cXCO+bjm9b5meCRCrtHRalBSklkGKRY2DmLwgd2lpRZZJl0vSIv4aNkvyV9g4L4wX" +
                "eOZUkZPWbkTjsSjUQtVl2VRGyUAimJL29mOnqYQUS+mCUY2VDutrp03FyxdOlsToeHr6t6FKkZhNr7Gm" +
                "8qyZAaENEJQj6U2V40eRNKYKr17yhuRsvq4v8ZFypGMILkIhA5Olp6Ujzzylv0aMH9rDXQEb4hCiaC/O" +
                "43eP+OgvBIKAAi1rVYhzML/bhKKuAEhiJZ2RmSUGVlAAqC9404uLHWSmfS0qWdU9fIu4jXEKbDXg8pku" +
                "C+TM8ul9k0NALFy6emU0lmabCKKsoSoIeNBJt0l4VxsyOXvHGmMRdsWM4FV6XyuDBGixNqFIfHCMHrPx" +
                "aHTyjdx4tE4SfovM5njh+Jzgt33xtB/u0k/T2acb0T9G4mf8Z1tS3CYK6cWGAhsyI9ZHtYnvBGpjI+du" +
                "hTpoMceT+eyvVOxg/rKPyRlpnIOyMGFGrNFJwHf3afrxbp5OB+CX+8COFMHasCVSDnvwN3C/D0IuApxs" +
                "Ap/ecYIo9g6ETrZEnz/O8AeTRBVaw6Eql5YYwQTfo4Do+Zxcieqz3AoCXXSUH/6cTNJ0ukP51T7lNZCl" +
                "KgxahIYPFauwaLgPHBLiWJjxH5/vt7pwmF8PhMnqeHTdRFtuuR+MpBv6ojTsCl+jDBbS2MbRMXr36Yd0" +
                "ssNvJH57Ts8Rd/EjDogFVTfh/3b58cscM1ISPTViDsEa9MkgwZQ7BDq1qVbSGn3sAJ3zhkoZidffwXmD" +
                "9ao6xCLcmm9I3qDwZHx7u63kkXhzKsGMcFXRQYanqIucPM/WPulqYVzJlxpfH0MaYl9mJqT3DrFrk7df" +
                "4RCnycym2Cu/NgBfG0c8cfv5Yb4LNRK/R8Bx1YvR3R5AEhpZYxBqRZCDBIxy1U4BHga3OuqWnVB7nrFr" +
                "VpslXRscH5WDWPutMzkbW1uv4zzCC1EKeFNvLyuQ6S4qrjGxM2bxFk1Zk+csY7co0FNIvuNVNpvGKam7" +
                "d3uRPMa9tqTjnQxJ14XBbBHv452WEt1BmmehWRxd4nR1QCfsp4r9g1OSZ4Ew4lC5RK6sxW7G9G3y1oTQ" +
                "A3RvPViSHLeUyGh3VOj4o7t04wVaMeht9rPQj6zsRuzAfNXYgHHSe5lzepEavyRlFkb1xRAZeHYPo/Os" +
                "1y4AqbKJRYE+Z7Dqqk8eDyHfKHUlrGhCm7ejgzmid0x4CqHkP+W/ek3xCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
