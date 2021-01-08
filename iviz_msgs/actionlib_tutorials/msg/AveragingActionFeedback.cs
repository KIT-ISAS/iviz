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
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = AveragingFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingFeedback Feedback)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QCCH2EHttEnTpgZ0UCXFUeAkhq32anDJ0S5bLlcluZL17/uGq9VH" +
                "LKE6pBYs64t88/jmzXA+ktTkRZleMqmiqZ01+UMVivD6upb2PsrYBBHSSzZYkJeFccUHIp1L9beYrd/0" +
                "sv53fvSyz/fXV4isWzYfE8de9kKAk9PSa1FRlFpGKWY1zmCKkvyFpQVZ5lvNSYv0a1zNKVzyzmlpgsBf" +
                "QQ4HsXYlmoBVsRaqrqrGGSUjiWgq2gPgrcYJKebSR6MaKz021F4bx+tnXlaU8PkZ6J+GnCIxGV1hlQuk" +
                "mmhAagUM5UkG6IcfsbgxLr59wzuwcbqsL/CZCuRjw0DEUkZmTI9zT4HJynDFYV61Z7wEPEQiBNJBnKXv" +
                "HvAxnAvEAQua16oUZ6B/u4pl7YBIYiG9kbklRlbQAbAvedPL811opn4lnHR1h99CboOcguu2wHysixLJ" +
                "syxBaAroiJVzXy+Mxtp8lVCUNeSigA+99KtextvaoAD5wGJjGfal3OBVhlArg0xosTSx7GUheg6Q8vJg" +
                "dC/739x5tGJ6Gb9Hlgu8JA6c7PfrQuo+3Y6/jCZfrkX36Isf8Z99SmmjKGUQK4rs0JxYKNWaYK1UGx7p" +
                "9yjMDnQwnE7+HIsd0J/2QTk5jffQGJ7MiaU6Dfn2bjz+fDsdjzbIb/aRPSmC1WFSpB9W4W9QDSEKOYvw" +
                "tYksgOdM0WOqC1f0si3Vp48XeMIwSYjWfajUuSWGMDF0MKB6NiVfoSAt94dI5x3p+z+Gw/F4tEP67T7p" +
                "JaClKg0ah4YpFQsxa7g5HNLiaJzB71/vttJwnJ8PxMnrdHrdJIdu2R8MpRv6b3XYG6FGTcyksY2nowTv" +
                "xp/Gwx2GffHuKUFPf5FihgcJcXnVTfzWND+cwDInJdFsE+gmWoP+GSW4cs9ADzduIa3RR4+wNuCmZPri" +
                "l+cw4MaBro6pHLce3GRwq/JwcHOzLeq++PVUijnhHqODHE9SGIl5mrJ92m5mfMU3Hl8rm1Skbs1USO8f" +
                "Y9cs77/DMU6Umq2xV4htBL5Ojjnj5uv9dBerL35LiAPX6bG+VQAlNFLHKNTqIDcqMMplOyUEGN3qJF1+" +
                "ShUGBq9ZcZZ1aaAASgjBvumkuMIG1tbLNLPwUhQF3tTbWwx81hcYl5vYmcJ4i6a8KXgE21xzkR4j4z7n" +
                "JTcZtePU+l7u1AqRM8+nSnc2tF2WBuNHuq53ekwyCuk0M03SfJPmsAOCAYAcewlnpcA6YQ6iao6sWcvb" +
                "GTW0eVwSgm/AOx/Cn+S5ySRO+9NEdwi0nPUQghYNjuh9uwnphlw2J2/BJNbYiPEzBFlwspGmMCdlZkZ1" +
                "1ZFYBDYTw6fBsF0BZlWTygTtz2AZVFhnsh1VniGPsUGiDIR7/WSi72Ug8C8b7zfdFwwAAA==";
                
    }
}
