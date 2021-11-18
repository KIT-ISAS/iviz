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
            b.Serialize(Position);
            b.Serialize(Orientation);
            b.Serialize(Scale);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Feedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e539fa42d3d53e71b91f9252687bd4b3";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTU/bQBC9W8p/GIkDUIVUolUPSD1UQm05VKIC9VJVaGJP7C32rtldJ5hf3zfrxBBI" +
                "RQ+FyFL2Y+bN99sQi6smlOHtV+FCPFXpLwvRG1vS0txdmWKzw8rY+O6YFiLFnPPrq9i3sj4TG32vwqW4" +
                "RnSdYM8drql1wUTj7KPL7x1H8RYX5LwBAu8Q+iF5dP4dhZxrGFOX0vml59/pqqc4LrNskn38z79J9u3i" +
                "ywmF7UxNsj26iGwL9gXBXS44Mi0cUmjKSvxRLUupocVNKwWlW01XmEHxsjKB8JVixXNd99QFCEVHuWua" +
                "zpociaFoGtnSh6axxNSyjybvavaQd74wVsUXnhtRdHxBbjqxudDZ6QlkbJC8iwYO9UDIvXDQgp6dUtYN" +
                "5YNCtne5ckfYSolGGI1TrDiqs3LbegnqJ4cT2HgzBDcDNrIjsFIEOkhnV9iGQ4IRuCCtyys6gOfnfaxQ" +
                "61gJLdkbnteiwChsDdR9Vdo/fICsbp+QZes28APivY1/gbUjrsZ0VKFmtUYfuhIJhGDr3dIUEJ33CSSv" +
                "tRepNnPPaCnVGkxme581xxCCVqoI/jkElxsUoKCVidVmWFI1dB5erCF3DNpk01vIVmRjQ4pnM33kFto8" +
                "aSKRtoUXxNVyLtmidhw/vKfbcdWPq7vXiuCeDcYwvGjLoRhI8xZHbPuvu5t7LsEQNrPsmaA2q9Vrhbfm" +
                "sZ2x0TJdbkc100E+S6PnLAa3EUbhwBGjJhQL46GKqGdAFS+IXaZkIhVOAlkXgdHwNSAFc6Da3LYAAxl5" +
                "tqEesoljqBzIrJxNaVWJHaS0jxPrJJ4yOXlTmmLQ1CSPykzr6KYUF8eYg7oefB6MoQkB4t1Qu8MZnS2o" +
                "dx2tNCAs/JoeHc1l9CuNcXRuqty4htj1toCmApfaAyGCmZ8r/ItVe9fLNHnyGgb5+UsHUkLiFWxGog0v" +
                "+Hg9dUP78NN9Kw2lBEGob9t9ONVHSY+L9f1AJqDRhxOJbn301v/ldZ9kfwAPQHBvdAgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
