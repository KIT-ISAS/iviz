/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Feedback : IDeserializable<Feedback>, IMessage
    {
        public const byte TYPE_EXPIRED = 0;
        public const byte TYPE_BUTTON_CLICK = 1;
        public const byte TYPE_MENUENTRY_CLICK = 2;
        public const byte TYPE_POSITION_CHANGED = 3;
        public const byte TYPE_ORIENTATION_CHANGED = 4;
        public const byte TYPE_SCALE_CHANGED = 5;
        public const byte TYPE_TRAJECTORY_CHANGED = 6;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "viz_id")] public string VizId;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "entry_id")] public int EntryId;
        [DataMember (Name = "angle")] public double Angle;
        [DataMember (Name = "position")] public GeometryMsgs.Point Position;
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        [DataMember (Name = "trajectory")] public IvizMsgs.Trajectory Trajectory;
    
        /// Constructor for empty message.
        public Feedback()
        {
            VizId = "";
            Id = "";
            Trajectory = new IvizMsgs.Trajectory();
        }
        
        /// Explicit constructor.
        public Feedback(in StdMsgs.Header Header, string VizId, string Id, byte Type, int EntryId, double Angle, in GeometryMsgs.Point Position, in GeometryMsgs.Quaternion Orientation, in GeometryMsgs.Vector3 Scale, IvizMsgs.Trajectory Trajectory)
        {
            this.Header = Header;
            this.VizId = VizId;
            this.Id = Id;
            this.Type = Type;
            this.EntryId = EntryId;
            this.Angle = Angle;
            this.Position = Position;
            this.Orientation = Orientation;
            this.Scale = Scale;
            this.Trajectory = Trajectory;
        }
        
        /// Constructor with buffer.
        public Feedback(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            VizId = b.DeserializeString();
            Id = b.DeserializeString();
            Type = b.Deserialize<byte>();
            EntryId = b.Deserialize<int>();
            Angle = b.Deserialize<double>();
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            Trajectory = new IvizMsgs.Trajectory(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Feedback(ref b);
        
        public Feedback RosDeserialize(ref ReadBuffer b) => new Feedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(VizId);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(EntryId);
            b.Serialize(Angle);
            b.Serialize(in Position);
            b.Serialize(in Orientation);
            b.Serialize(in Scale);
            Trajectory.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (VizId is null) BuiltIns.ThrowNullReference(nameof(VizId));
            if (Id is null) BuiltIns.ThrowNullReference(nameof(Id));
            if (Trajectory is null) BuiltIns.ThrowNullReference(nameof(Trajectory));
            Trajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 101;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(VizId);
                size += BuiltIns.GetStringSize(Id);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Feedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "beec894d41c35d3624bcb12f27355c75";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71V32/bNhB+119xQB6aDI63JV1RBOiDl3it1zZxE3VYMQzGRTpL3CRSJSm77l+/j5Qt" +
                "26mH7mGpIcAk7+7j/fzYKu2fU/phOp6Nf59ObsdX9IJ+SNrt8c/v0/Tmenb5ZnL5GrIfd2Vvx9fvx9fp" +
                "7YdefLYrnt7cTdJJMH41un4Zoc935Te3E1iPHqg83VW5uxy9Ge8If9oVprejX8eX6U24v9d4liTO57Pa" +
                "Fe77V8K5WCrjH46t0gUt1OeZyjc7rDpEv2okwer8jER7uwo688qwf/aUWBeVJIWYWoIkYk8NlKkxTnll" +
                "9APhu5a9WA0BGauAxweUfpPMG3tOLmOgq+BXPE8t/xVFK/L9MkmSF//zL3l79/KCHiQrOaI7zzpnmxN8" +
                "5Zw909wgiaooxZ5WspAKRlw3klOUhsy5IQzTUjnCV4gWy1W1otZByRvKTF23WmXICnlVy549LJUmpoat" +
                "V1lbsYW+sbnSQX1uuZaAjs/Jx1Z0JjS5uoCOdpK1XsGhFRAyK+xCSSdXFGuKSsIgOUqX5hRbKdAK/eXk" +
                "S/bBWfnUWHHBT3YXuOO7LrghsJEcwS25o+N4NsPWnRAugQvSmKykY3g+XfkShfal0IKt4vtKAjCqWgH1" +
                "STB6crKDrCO0Zm028B3i9o7/Aqt73BDTaYmaVSF61xZIIBQbaxYqh+r9KoJkVWhEqtS9ZfRTsOquTI5+" +
                "CTmGEqxiRfDPzplMoQA5LZUvN+MSqxFG45G68cCMbRoLqfKstIvBbOaOzDx0TpxF5GxuBUE1nEk/u5/6" +
                "1apfff427m9ZYBODldBsKAMSvEcN+86H3ccthWD86mHylYg2q+W3iW1NXocCo0WU7Yc0DPM7iRNnNOa1" +
                "FkbJQA29JQxzZWGKkIdAFSsIXAakPOVGHGkTeqHmvwEpaP9gzU0DMA5EqV3VpRLHMDmWYTEc0LIU3WmF" +
                "9o1kE+lJZWRVofLOMmS4N2ZaBzcgPz9D+1dV53N3GdoPINZ0hTsZ0mROK9PSMgSEhV2zoqF76f2K0+uN" +
                "GQRKXEMcek/ATo6L0ADOg4+/WvXHKfWht+iL58/JH3+GORQXuQSbnlzdo71WXzqBRI62TcQbUgiO7Xfg" +
                "ILxC4ThfyzsCAW/uDiL69MHL/i9vefIPf32EBTsJAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
