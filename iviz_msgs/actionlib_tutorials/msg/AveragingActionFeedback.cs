/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingActionFeedback")]
    public sealed class AveragingActionFeedback : IDeserializable<AveragingActionFeedback>, IActionFeedback<AveragingFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public AveragingFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = AveragingFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = AveragingFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingActionFeedback(ref b);
        }
        
        AveragingActionFeedback IDeserializable<AveragingActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new AveragingActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W224bNxB9X8D/QEAPsYvaaZNeUgN6UCXFUeAkhq321eAuR7tsuVyV5ErW3/cM9yKp" +
                "lhA9pBZk60aeOTxzZjgfSCpyoogvicyCrqzR6WPpc//6ppLmIchQe+HjSzJakZO5tvl7IpXK7G+xaN+c" +
                "JcNv/DhLPj3cXCOyath8iBzPkoEAJ6ukU6KkIJUMUiwqnEHnBblLQysyzLdckhLx17BZkr/CxnmhvcAz" +
                "J4tzGLMRtceiUImsKsva6kwGEkGXtLcfO7UVUiylCzqrjXRYXzmlLS9fOFkSo+Pp6Z+abEZiNrnGGusp" +
                "q4MGoQ0QMkfSQzv8KJJa2/D2DW9IBvN1dYmPlCMTfXARChmYLD0tHXnmKf01YnzXHO4K2FCHEEV5cR6/" +
                "e8RHfyEQBBRoWWWFOAfzu00oKgtAEivptEwNMXAGBYD6ije9uthBZtrXwkpbdfAN4jbGKbC2x+UzXRbI" +
                "meHT+zqHgFi4dNVKKyxNNxEkM5psELCfk26T8K4mZDJ4zxpjEXbFjOBVel9lGglQYq1DkfjgGD1m41Gr" +
                "5H8z5NEiOUv4PZKb44UpcI7fdaXTfLibfp7MPt+I7jEUP+A/O5PiNlFILzYU2JMpsURZk/tWoyY40u5Q" +
                "iS3maDyf/TkVO5g/7mNyUmrnIC58mBLLdBLw3f10+uluPp30wG/2gR1lBHfDmcg6HMLfoAB8EHIRYGYd" +
                "+PSOc0RPsRRsnmyJPn8M8AefRBUaz6Ewl4YYQQffoYDo+ZxciQI03A0CXbSUH/4Yj6fTyQ7lt/uU10CW" +
                "WaHRJRSsmLEKi5pbwSEhjoUZ/f7lfqsLh/npQJi0ikdXdXTmlvvBSKqmr0rDrvAVKmEhtakdHaN3P/04" +
                "He/wG4qfn9Nz9BdlzO8gHa6pqg7/tcv3X+eYUibRViNmH6xGqwwSTLlJoFlru5JGq2MHaJ3XV8pQ/PIC" +
                "zuutZ6sQi3Brvj55vcLj0e3ttpKH4tdTCaaE24oOMjxFXeTkebb2SduFdiXfa3yD9GmIrZmZkNo7xK5N" +
                "3n2DQ5wmM5tir/yaAHxzHPHE7ZeH+S7UUPwWAUe2E6O9QIAkFLLGINSIIHsJGOWqGQQ8DG5U1C09ofY8" +
                "Y1esNku61jg+Kgex9ltnMhgZU63jSMILUQp4U23vK5Bp7yquMbEzZPEWRWmd84TVXWiBnkLyorfZbMJD" +
                "FpugGURanXzgjPOR4s0MVdeFxoQRb+WdrhINQoonolkcYOKMdUAq7CfLFsJBybNGGHSoXCJdxmA3Y/om" +
                "f2tC6B66cx9cSY67SmS0OzC0/NFg2iHDY/FaotHtJqKbXdmQ2IEpqzYBQ6X3MucMIzt+SZle6Kyrh8jA" +
                "s4EYnSe+ZgFIlXWsC7Q6jVVXXf6w6iWyF2qkR0Ow189m9DMm8C9PJ0uD6QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
