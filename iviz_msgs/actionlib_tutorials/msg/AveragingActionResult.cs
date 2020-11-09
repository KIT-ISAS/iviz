/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingActionResult")]
    public sealed class AveragingActionResult : IDeserializable<AveragingActionResult>, IActionResult<AveragingResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public AveragingResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new AveragingResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AveragingActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new AveragingResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingActionResult(ref b);
        }
        
        AveragingActionResult IDeserializable<AveragingActionResult>.RosDeserialize(ref Buffer b)
        {
            return new AveragingActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8672cb489d347580acdcd05c5d497497";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgxofYnVppk36kntFBlVRHGSfx2GqvHpBYkWhBUMWHZP37vgUpSnKs" +
                "RockGtu0JODtw9u3i31LUpETVXpksgi6sUbnD7Uv/cvrRpr7IEP0wqdHNlqRk6W25R35aIJw6ZENv/Ar" +
                "e39/fYWYquXxtmV3JkDGKumUqClIJYMUiwbkdVmRuzS0IsNE6yUpkb4NmyX5ATbOK+0FfkqyOIAxGxE9" +
                "FoVGFE1dR6sLGUgEXdPBfuzUVkixlC7oIhrpsL5xSltevnCyJkbHj6d/I9mCxGxyhTXWUxGDBqENEApH" +
                "0kM0fCmyqG14/Yo3ZGfzdXOJt1QiBX1wESoZmCw9LqEv85T+CjG+aw83ADbEIURRXpynzx7w1l8IBAEF" +
                "WjZFJc7B/HYTqsYCkMRKOi1zQwxcQAGgvuBNLy72kG2CttI2W/gWcRfjFFjb4/KZLivkzPDpfSwhIBYu" +
                "XbPSCkvzTQIpjCYbBHznpNtkvKsNmZ39wRpjEXaljOApvW8KjQQosdahynxwjJ6y8aBV9pXceLQ2Mv4X" +
                "mS3x4Pic4Dfbgmnf3E4/TGYfrsX2NRQ/4C/bktI2UUkvNhTYkDmxPkWb+E6gNjZy7lB/HeZoPJ/9NRV7" +
                "mD8eYnJGonNQFibMiTU6Cfj2bjp9fzufTnrgV4fAjgqCtWFLpBz24E/gfh+EXAQ4WQc+veME0WOqA1tm" +
                "4n9eZ/iFSZIKreFQlUtDjKCD36KA6PmcXI3qM9wKAl10lO//HI+n08ke5deHlNdAlkWliWn7WLAKi8h9" +
                "4DkhjoUZ/f7xbqcLh/npmTB5k46uYrLljvuzkVSkz0rDrvANymAhtYmOjtG7m76bjvf4DcXPn9Jz9DcV" +
                "4YgDUkE1MTy1y/ef55hTIdFTE2YfLKJPBgmm3CHQqbVdSaPVsQN0zusrZSh++QbO661nm5CKcGe+Pnm9" +
                "wuPRzc2ukofi11MJ5oSrip5leIq6yMmn2TokbRfa1Xyp8fUR9rtAYkLq4BD7NnnzBQ5xmsxsioPyawPw" +
                "tXHEEzcf7+f7UEPxWwIc2a0Y3e0BJKGQNQahVgTZS8Aog3YK8DC4UUm3/ITa84zdsNos6Vrj+KgcaZ+0" +
                "zuxsZEyzTvMIL0QpOK7b/rICme6i4hoTe6MVb1GUx5Lnqu1tFugxZN/wKptNstYB7QjSieQDp5vPk+5k" +
                "SLquNGaLdB/vtZTkDlI8C83S6BK7O+apTthPlv2DU5JngTDiUL1ErozBbsb0bfLWhNA99NZ6sCQ5bimJ" +
                "0f6o0PFHd+nGC7Ri0NscZmFBpHJZ/MNuxI52fsU46b0sqU2NX1KhF7rYFkNi4AcdOs967QKQqmMqCvQ5" +
                "jVWDbfJ4CPnqqQsRydGQ6+WToTzLFqaRPGPWJG3/hqdpRavsP8T0h3fyCwAA";
                
    }
}
