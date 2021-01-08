/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsActionFeedback")]
    public sealed class TwoIntsActionFeedback : IDeserializable<TwoIntsActionFeedback>, IActionFeedback<TwoIntsFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TwoIntsFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionFeedback(ref b);
        }
        
        TwoIntsActionFeedback IDeserializable<TwoIntsActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionFeedback(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QCCH2EXstEnbpAZ0UCXFUeEkhq32anDJ0S5bLlcluZL17/uGq9WH" +
                "LaE6JBEs64t88/jmzXA+ktTkRZleMqmiqZ01+UMVivD6upb2PsrYBBHSSzZd1hMXwwcinUv1j5it3/Sy" +
                "/ld+9LJP99dXiKtbLh8Tw172QoCR09JrUVGUWkYpZjVOYIqS/IWlBVlmW81Ji/RrXM0pXPLOaWmCwF9B" +
                "jry0diWagFWxFqquqsYZJSOJaCraA+Ctxgkp5tJHoxorPTbUXhvH62deVpTw+Rno34acIjEZXWGVC6Sa" +
                "aEBqBQzlSQbjCvyIxY1x8e0b3oGNUPYCn6lANjYMRCxlZMb0OPcUmKwMVxzmh/aMl4CHSIRAOoiz9N0D" +
                "PoZzgThgQfNaleIM9G9XsawdEEkspDcyt8TICjoA9iVvenm+C83Ur4STru7wW8htkFNw3RaYj3VRInmW" +
                "JQhNAR2xcu7rhdFYm68SirKGXBRwoZd+1ct4WxsUIB9YbCzDvpQbvMoQamWQCS2WJpa9LETPAVJeHozu" +
                "Zd/MnUfrpZfxe2S5wEviwMl+vy6j7tPt+PNo8vladI+++BH/2aeUNopSBrGiyA7NiYVSrQnWSrXhkX6/" +
                "4NJoQQfD6eSvsdgB/WkflJPTeA+N4cmcWKrTkG/vxuNPt9PxaIP8Zh/ZkyJYHSZF+mEV/gbVEKKQswhf" +
                "m8gCeM4UPaa6cEUv21J9/niBJwyThGjdh0qdW2IIE0MHA6pnU/IVCtJyf4h03pG+/3M4HI9HO6Tf7pNe" +
                "Alqq0qBxaJhSsRCzhpvDIS2Oxhn8/uVuKw3H+flAnLxOp9dNcuiW/cFQuqH/V4e9EWrUxEwa23g6SvBu" +
                "/Md4uMOwL355TtDT36SY4UFCXF51E5+a5tUJLHNSEs02gW6iNeifUYIr9wz0cOMW0hp99AhrA25Kpi9+" +
                "/R4G3DjQ1TGV49aDmwxuVR4Obm62Rd0X706lmBPuMTrI8SSFkZjnKdun7WbGV3zj8bWySUXq1kyF9P4x" +
                "ds3y/isc40Sp2Rp7hdhG4OvkmDNuvtxPd7H64reEOHCdHutbBVBCI3WMQq0OcqMCo1y2U0KA0a1O0uWn" +
                "VGFg8JoVZ1mXBgqghBDsSSfFFTawtl6mmYWXoijwpt7eYuCzvsC43MTODMZbNOVNUSQt16siPUbG/Z6X" +
                "3GTUjlPre7lTK0TOPJ8q3dnQdlkajB/put7pMckopNPMNEnzTZrDDggGAHLsJZyVAuuEOYiqObJmLW9n" +
                "1NDmcUkIvgHvfAh/kucmkzjtTxPdIYzuhhC0aHBE79tNSDfksjl5CyaxxkaMnyHIgpONNIU5KTMzqquO" +
                "xCKwmRg+DYbtCjCrmlQmaH8Gy6DCOpPtqPLt8/j6yRTPMf8D28XAYwgMAAA=";
                
    }
}
