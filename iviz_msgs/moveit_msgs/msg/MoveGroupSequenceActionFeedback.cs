/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceActionFeedback : IDeserializable<MoveGroupSequenceActionFeedback>, IActionFeedback<MoveGroupSequenceFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public MoveGroupSequenceFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupSequenceActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new MoveGroupSequenceFeedback();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupSequenceFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal MoveGroupSequenceActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new MoveGroupSequenceFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionFeedback(ref b);
        
        MoveGroupSequenceActionFeedback IDeserializable<MoveGroupSequenceActionFeedback>.RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionFeedback(ref b);
    
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXXMaNxR931+hGR5id2q3TfqReoYHCsQhYyceQ/vq0a4uu2p3JSppwfz7nqv9AGqY" +
                "8JCEweZLOvfo3HOv7nuSipwo4ksis6CtKXX6VPnc/3BrZTkPMtRe+PiS3Ns13Tpbr+b0b00mo3dEKpXZ" +
                "P2LZvkmGX/iR3M9vbxBfNZzeN0wHAsSMkk6JioJUMkixtDiIzgtyVyWtqWTS1YqUiL+G7Yr8NTYuCu0F" +
                "njkZcrIst6L2WBSsyGxV1UZnMpAIuqKD/dipjZBiJV3QWV1Kh/XWKW14+dLJihgdT99qI2aTG6wxnrI6" +
                "aBDaAiFzJL02OX4USa1NePOaNySDxcZe4SPlSEcfXIRCBiZLzytHnnlKf4MY3zWHuwY2xCFEUV5cxO+e" +
                "8NFfCgQBBVrZrBAXYP6wDYU1ACSxlk7LtCQGzqAAUF/xpleXe8hM+0YYaWwH3yDuYpwDa3pcPtNVgZyV" +
                "fHpf5xAQC1fOrrXC0nQbQbJSkwkCHnTSbRPe1YRMBu9YYyzCrpgRvErvbaaRACU2OhSJD47RYzaetEq+" +
                "khtP1knCb5HZHC8cnxP8tiue5sPD9ONk9vFWdI+h+BH/2ZYUt4lCerGlwIZMifXJmsS3AjWxkXO3Rh00" +
                "mKPxYvbXVOxh/nSIyRmpnYOyMGFKrNFZwA+P0+n9w2I66YFfHwI7ygjWhi2RctiDv4H7fRByGeBkHfj0" +
                "jhNEz7EOTJ7siL58DPAHk0QVGsOhKlclMYIOvkMB0YsFuQrVV3IrCHTZUp7/OR5Pp5M9ym8OKW+ALLNC" +
                "o0Uo+DBjFZY194FjQpwKM/rj0+NOFw7z85EwqY1HV3W05Y770Uiqps9Kw67wFmWwlLqsHZ2i9zj9MB3v" +
                "8RuKX17Sc/Q3ZczvKB0uKFuH/9vl+89zTCmT6KkRsw9Wo08GCabcIdCptVnLUqtTB2id11fKUPz6DZzX" +
                "W8/YEItwZ74+eb3C49Hd3a6Sh+K3cwmmhKuKjjI8R13k5GW2DkmbpXYVX2p8ffRpiH2ZmZA6OMS+Td5+" +
                "gUOcJzOb4qD8mgB8bZzwxN2n+WIfaih+j4Aj04nR3h5AEgpZYxBqRJC9BIxy3UwBHgYvVdQtPaP2PGNj" +
                "AsIFDX02GsdH5SDWYetMBqOytJs4j/BClALe2N1lBTLtRcU1JvbGLN6iKK3znGVsFwV6Dsk3vMpmkzgl" +
                "tfduJ5IPnG4+T7yTIemm0Jgt4n2811KiO0jxLDSLo0ucro7ohP1k2D84JXkWCCMOVSvkqiyxmzF9k7wN" +
                "IXQP3VkPliTHLSUy2h8VWv7oLu14gVYMetvDLHQjK7sROzBf1WXAOOm9zDm9SI1fUaaXOuuKITLw7B5G" +
                "51mvWQBSVR2LAn1OY9V1lzweQr5S6ipYUYcmbycHc0RvmfAUQsl/If79lvELAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
