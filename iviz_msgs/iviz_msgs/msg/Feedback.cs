/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public Feedback()
        {
            VizId = string.Empty;
            Id = string.Empty;
            Trajectory = new IvizMsgs.Trajectory();
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal Feedback(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            VizId = b.DeserializeString();
            Id = b.DeserializeString();
            FeedbackType = b.Deserialize<int>();
            EntryId = b.Deserialize<int>();
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            Trajectory = new IvizMsgs.Trajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Feedback(ref b);
        
        Feedback IDeserializable<Feedback>.RosDeserialize(ref Buffer b) => new Feedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(VizId);
            b.Serialize(Id);
            b.Serialize(FeedbackType);
            b.Serialize(EntryId);
            b.Serialize(ref Position);
            b.Serialize(ref Orientation);
            b.Serialize(ref Scale);
            Trajectory.RosSerialize(ref b);
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
                size += BuiltIns.GetStringSize(VizId);
                size += BuiltIns.GetStringSize(Id);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Feedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e539fa42d3d53e71b91f9252687bd4b3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTWvbQBC961cM5JCkOC4kpYdAD4XQNodCSkIvpZixNJa2kXaV3ZUd9df3zcqW48Ql" +
                "PTQxAu/HzJvvtyEWsyaU4e0X4UI8VekvC9EbW9LS/J6ZYrPDyth4dkoLkWLO+e0s9q2sz8RG36twKa4R" +
                "XSfYK4dral0w0Tj76PJbx1G8xQU5b4DAe4S+Sx6dP6OQcw1j6lI6v/H8K131FMdllmUf/vMv+3r9+ZzC" +
                "bp6yA7qObAv2BcFXLjgyLRzyZ8pK/EktS6mhxE0rBaVbzVWYQvGmMoHwlWLFc1331AUIRUe5a5rOmhxZ" +
                "oWga2dGHprHE1LKPJu9q9pB3vjBWxReeG1F0fEHuOrG50OXFOWRskLyLBg71QMi9cNBqXl5Q1g21g0J2" +
                "cLNyJ9hKiS4YjVOsOKqzct96Ceonh3PYeDMENwU2kiOwUgQ6SmczbMMxwQhckNblFR3B86s+Vih0rISW" +
                "7A3Pa1FgVLUG6qEqHR4/QFa3z8mydRv4AXFr419g7YirMZ1UqFmt0YeuRAIh2Hq3NAVE530CyWttRKrN" +
                "3DP6SbUGk9nBJ80xhKCVKoJ/DsHlBgUoaGVitZmUVA0dhhfqxj0ztmkspCqysSEFs5k7cgvtnDSLyNnC" +
                "C4JqOZdsUTuO79/R/bjqx9Xv13F/ywKbGLxos6EMSPAONew6r7u7LYVg/Jpp9kxEm9XqdWJbk9e+wGiZ" +
                "7nZDmur8XqaJcxbz2gijZKCGUROKhfFQRchToIoXBC4TMpEKJ4Gs015o+BaQgvZXbW5bgIGDPNtQD6nE" +
                "MVSOZFpOJ7SqxA5S2r6JbBI9mZy8KU0xaGqGR2WmdXATiotTtH9dDz4PxtB+APFuKNzxlC4X1LuOVhoQ" +
                "Fn7Nio7mMvqVpjc6N1FKXEPse0/AToFLbYAQwcfPVv1lSr3vLXry/AX58VPnUELiEmxGcg0v9lo9dQKJ" +
                "/LhtoqGIIAV1bLcDJ/oK6XGxvh8IBLz5cBDRp49e9r+85dkf25ZnDmEIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
