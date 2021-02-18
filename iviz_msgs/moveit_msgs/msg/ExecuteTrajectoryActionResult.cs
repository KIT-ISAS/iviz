/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryActionResult")]
    public sealed class ExecuteTrajectoryActionResult : IDeserializable<ExecuteTrajectoryActionResult>, IActionResult<ExecuteTrajectoryResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ExecuteTrajectoryResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ExecuteTrajectoryResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ExecuteTrajectoryResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ExecuteTrajectoryResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionResult(ref b);
        }
        
        ExecuteTrajectoryActionResult IDeserializable<ExecuteTrajectoryActionResult>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8aaeab5c1cdb613e6a2913ebcc104c0d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71X227bOBB9F9B/IBBg2yzW2ebSK+AHxVESbW3JKysB9kmgJdrmVpa8JJXUf79nqIsv" +
                "jds8tDVSyzaHZ4Znzgynt4JnQrGFfTg8NbIscjlNlnqu/7wpeT4x3FSaaftwvC8irYyIFf9XpKZU60jo" +
                "KjdM2YfT/8EvZzS5+QjfWR3PbR3lEUNQRcZVxpbC8IwbzmYlDiHnC6F6uXgQOQW8XImM2VWzXgl9go3x" +
                "QmqGv7kohOJ5vmaVhpEpWVoul1UhU24EM3IpdvZjpywYZyuujEyrnCvYlyqTBZnPFF8KQsefFv9VokgF" +
                "868+wqbQxJdEQGsgpEpwLYs5FplTycKcn9EG5yh+LHv4KuZIReecmQU3FKz4sgK/FCfXH+Hj9/pwJ8AG" +
                "OQJeMs1e2d8SfNXHDE4QgliV6YK9QuTjtVmUBQAFe+BK8mkuCDgFA0B9SZteHm8hFxa64EXZwteIGx/P" +
                "gS06XDpTb4Gc5XR6Xc1BIAxXqnyQGUynawuS5lIUhkF/iqu1Q7tql87RNXEMI+yyGcGTa12mEgnI2KM0" +
                "C0cbReg2G4nMnJ+kxoM14tBHZHaOB/mnBL9vC6f+MvaCKz+4Ye2rz17jnWQp7Da24JqthSFBTgXxk9aJ" +
                "bwiqfSPn6gF1UGO6g9i/99gW5ukuJmWkUgrMQoRTQRw9C3gced5oHHtXHfDZLrASqYC0IUukHPKgX6B+" +
                "bRifGShZGjq9ogQJ2zfg2mHfeB3hH0RiWagFh6pc5YIQpNEtCgJ9FQu1RPXl1AqMOG5CntwNBp53tRXy" +
                "+W7Ij0Dm6UIKCltXKbEwq6gPPEXEITfuZRhteCE3F0+4mZb26FllZbmJ/UlPWSW+Sw2pQpcogxmXeaXE" +
                "ofAi7y9vsBVfn735OjwlqIMfUIAtqLIy+3L54/sxTkXK0VMtZuesQp80HJFSh0CnlsUDz2V26ACN8rpK" +
                "6bO3v0B5nfSK0tgi3IivS17H8MAdDjeV3GfvnhvgVOCqEk9G+Bx2kZOvs7UbdDGTakmXGl0fZrsL2EhE" +
                "tnOIbZm8/wGHeB7NJIqd8qsd0LVxQBPDcBJvQ/XZBwvoFi0Zze0BJJYhawQiahJ4RwGhnNRTgIbA88zy" +
                "Nn1G7WnCLoltovRR4vioHF7stU7nyM3z8tHOI2SIUlBUt91lhWCai4pqjG2NWLQlE9NqPicaGyMjvhjn" +
                "F15l/pVTK6AeQRqStKF003nsnQxKHxcSs4W9j7dailWHyGgW8u3oUjV3zD5P2C8K0g9OKTQRhBFHLFfI" +
                "VZ5jN2HqOnmPAq476FZ6kKRQ1FJsRNujQhM/ukszXqAVI7z1bhZmQmRTnn4mNWJHPb9inNSaz0WdGr0S" +
                "qZzJtC0GG4E+adBp1qsNENSyskWBPidhddImj4aQn5S6JaQoTZ23A0M5fI9g5RtPqVINSmJC0Mckxedf" +
                "Edi+e6ceedH7XzgvQCCVE/IN5hb8QZbqRWNgr/HJpH/a/nDt+sO7yOt/oBftrX8eD90gQBdOaN276ve6" +
                "DX5w7w79q2QUxn4YJGTY7521q1u/Jo2li/syufwn8YJ7PwqDkRfEyeDWDW68fu+83TcIgzgKh527i3bh" +
                "LnAvh14Sh4n7950fecnECyZhlADW7ffetGaxP4KX8C7u9952Z2inrH7vXc3KKudFQfL5jX1GC1ty/Jej" +
                "K646g7pjKnajOMF77OEkySDErTTB2UDF66ds7v1wiOckGbvxLcyDSRy5fhBPsOF0Q+xN6A738c52Fr8F" +
                "dL5jubXW7qJMXWy8tcm6icK7cRK4I3B++uar1T0w2Lzdt4nCy7A5Kpbf7S/jxv7U4r/fXwwvaWpqlz/U" +
                "udBrNJ3lHunXEWwShBFMrsNolLTq7J2dbpTSEAcReYNPpFFo5B6GJBRYdmxuhUzvdrEjsJGRH1yH3eJF" +
                "HdmWNHajC8LE/5RMwuFdbBN3jqCc/wFuda3w5w8AAA==";
                
    }
}
