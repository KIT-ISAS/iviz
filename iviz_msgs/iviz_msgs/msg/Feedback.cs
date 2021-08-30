/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Feedback")]
    public sealed class Feedback : IDeserializable<Feedback>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "viz_id")] public string VizId;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "feedback_type")] public int FeedbackType;
        [DataMember (Name = "entry_id")] public int EntryId;
        [DataMember (Name = "position")] public GeometryMsgs.Point Position;
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        [DataMember (Name = "trajectory")] public IvizMsgs.Trajectory Trajectory;
    
        /// <summary> Constructor for empty message. </summary>
        public Feedback()
        {
            VizId = string.Empty;
            Id = string.Empty;
            Trajectory = new IvizMsgs.Trajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Feedback(in StdMsgs.Header Header, string VizId, string Id, int FeedbackType, int EntryId, in GeometryMsgs.Point Position, in GeometryMsgs.Quaternion Orientation, in GeometryMsgs.Vector3 Scale, IvizMsgs.Trajectory Trajectory)
        {
            this.Header = Header;
            this.VizId = VizId;
            this.Id = Id;
            this.FeedbackType = FeedbackType;
            this.EntryId = EntryId;
            this.Position = Position;
            this.Orientation = Orientation;
            this.Scale = Scale;
            this.Trajectory = Trajectory;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Feedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            VizId = b.DeserializeString();
            Id = b.DeserializeString();
            FeedbackType = b.Deserialize<int>();
            EntryId = b.Deserialize<int>();
            Position = new GeometryMsgs.Point(ref b);
            Orientation = new GeometryMsgs.Quaternion(ref b);
            Scale = new GeometryMsgs.Vector3(ref b);
            Trajectory = new IvizMsgs.Trajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Feedback(ref b);
        }
        
        Feedback IDeserializable<Feedback>.RosDeserialize(ref Buffer b)
        {
            return new Feedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(VizId);
            b.Serialize(Id);
            b.Serialize(FeedbackType);
            b.Serialize(EntryId);
            Position.RosSerialize(ref b);
            Orientation.RosSerialize(ref b);
            Scale.RosSerialize(ref b);
            Trajectory.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (VizId is null) throw new System.NullReferenceException(nameof(VizId));
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            Trajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 96;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(VizId);
                size += BuiltIns.UTF8.GetByteCount(Id);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Feedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e539fa42d3d53e71b91f9252687bd4b3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTWvbQBC961cM5JCkOC4kpYdAD4XQNodCSkIvpZixNJa2kXaV3ZUd9df3zcqW48Ql" +
                "PTQxAu/HzJvvtyEWsyaU4e0X4UI8VekvC9EbW9LS/J6ZYrPDyth4dkoLkWLO+e0s9q2sz8RG36twKa4R" +
                "XSfYK4dral0w0Tj76PJbx1G8xQU5b4DAe4S+Sx6dP6OQcw1j6lI6v/H8K131FMdllmUf/vMv+3r9+ZzC" +
                "bp6yA7qObAv2BcFXLjgyLRzyZ8pK/EktS6mhxE0rBaVbzVWYQvGmMoHwlWLFc1331AUIRUe5a5rOmhxZ" +
                "oWga2dGHprHE1LKPJu9q9pB3vjBWxReeG1F0fEHuOrG50OXFOWRskLyLBg71QMi9cNBqXl5Q1g21g0J2" +
                "cLNyJ9hKiS4YjVOsOKqzct96Ceonh3PYeDMENwU2kiOwUgQ6SmczbMMxwQhckNblFR3B86s+Vih0rISW" +
                "7A3Pa1FgVLUG6qEqHR4/QLYJ2rJ1G/gBcWvjX2DtiKsxnVSoWa3Rh65EAiHYerc0BUTnfQLJa21Eqs3c" +
                "M/pJtQaT2cEnzTGEoJUqgn8OweUGBShoZWK1mZRUDR2GF+rGPTO2aSykKrKxIQWzmTtyC+2cNIvI2cIL" +
                "gmo5l2xRO47v39H9uOrH1e/XcX/LApsYvGizoQxI8A417Dqvu7sthWD8mmn2TESb1ep1YluT177AaJnu" +
                "dkOa6vxepolzFvPaCKNkoIZRE4qF8VBFyFOgihcELhMykQongazTXmj4FpCC9ldtbluAsRKlDfWQShxD" +
                "5Uim5XRCq0rsIKXtm8gm0ZPJyZvSFIOmZnhUZloHN6G4OEX71/Xg82AM7QcQ74bCHU/pckG962ilAWHh" +
                "16zoaC6jX2l6o3MTpcQ1xL73BOwUuNQGCBF8/GzVX6bU+96iJ89fkB8/dQ4lJC7BZiTX8GKv1VMnkMiP" +
                "2ybiDSmoY7sdONFXSI+L9f1AIODNh4OIPn30sv/lLc/+ANuWZw5hCAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
