/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciActionResult")]
    public sealed class FibonacciActionResult : IDeserializable<FibonacciActionResult>, IActionResult<FibonacciResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public FibonacciResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new FibonacciResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new FibonacciResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionResult(ref b);
        }
        
        FibonacciActionResult IDeserializable<FibonacciActionResult>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionResult(ref b);
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
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bee73a9fe29ae25e966e105f5553dd03";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwW4bNxC9L6B/IKBD7KJ22qRpUwM6qLbiqHASw1Z7KQqDS4522e6SKsmVrL/PG660" +
                "kmIJ0SG1IHslm3zz+ObNcN6T1ORFmR6ZVNE4W5n8oQ5FeHntZHUfZWyCCOmRvTO5s1Ipc0ehqaLw6dHL" +
                "Bt/41cs+3F9fIKpumbxP/HpZX4CP1dJrUVOUWkYppg78TVGSP6toThVzrWekRfpvXM4onGPjpDRB4F2Q" +
                "JS+raimagEXRCeXqurFGyUgimpp29mOnsUKKmfTRqKaSHuud18by8qmXNTE63oH+a8gqEuOrC6yxgVQT" +
                "DQgtgaA8yWBsgX+KrDE2vn7FG7L+ZOHO8JUKZKELLmIpI5OlxxkkZp4yXCDGd+3hzoENdQhRdBAn6W8P" +
                "+BpOBYKAAs2cKsUJmN8uY+ksAEnMpTcyr4iBFRQA6gve9OJ0C5lpXwgrrVvDt4ibGMfA2g6Xz3RWImcV" +
                "nz40BQTEwpl3c6OxNF8mEFUZslHAel76Zca72pBZ/x1rjEXYlTKCpwzBKYMEaLEwscxC9IyesvFgdPa/" +
                "GfJggfQy/ozkFngwBc7x23XZtF9uRx+vxh+vxfo1ED/gNzuT0jZRyiCWFNmTObFEqs39SqM2ONLu56jV" +
                "FnN4ORn/ORJbmD/uYnJSGu8hLnyYE8t0FPDt3Wj04XYyuuqAX+0Ce1IEd8OZyDocwn9BAYQo5DTCzCby" +
                "6T3niB5TKdgi2xB9+urjBz5JKrSeQ2HOKmIEE8MaBURPJuRrFGDF3SDS6Yry/R+Xl6PR1Rbl17uUF0CW" +
                "qjToEhpWVKzCtOFWsE+IQ2GGv3262+jCYX7aEyZ36ei6Sc7ccN8bSTf0VWnYFcGhEqbSVI2nQ/TuRr+P" +
                "Lrf4DcSbp/Q8/UOK+e2lwzXlmvilXb7/OseclERbTZhdsAatMkow5SaBZm3sXFZGHzrAynldpQzEz8/g" +
                "vM561sVUhBvzdcnrFL4c3txsKnkgfjmWYE64rWgvw2PURU6eZmuXtJ0aX/O9xjdIl4bUmpkJ6Z1DbNvk" +
                "7Tc4xHEysyl2yq8NwDfHAU/cfLqfbEMNxK8JcGjXYqwuECAJjawxCLUiyE4CRjlvB4EAg1c66ZYfUXuB" +
                "sR2rzZIuDI6PykGs3daZ9YdV5RZpJOGFKAV8cJv7CmRWdxXXmNgasHiLprwpCpZxtSjSY8ye9TYbX/GQ" +
                "xSZoB5GVTiFyxvlI6WaGqovSYMJIt/JWV0kGIc0T0TgNMGnG2iMV9pNlC+GgFFgjDDpUz5CuqsJuxgxt" +
                "/haE0B302n1wJXnuKonR9sCw4o8Gsxoy0I1Bb7mbiCmRzqX6lw2JHe0gi6EyBFlwhpGdMCNlpkat6yEx" +
                "CGwgRueJr10AUnWT6gKtzmDV+Tp/WPUc2YsN0mMg2Msv5vNelqVR86+/u+m0l30G8LoWhvMLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
