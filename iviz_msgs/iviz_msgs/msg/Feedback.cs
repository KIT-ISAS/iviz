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
        public Feedback(in StdMsgs.Header Header, string VizId, string Id, byte Type, int EntryId, in GeometryMsgs.Point Position, in GeometryMsgs.Quaternion Orientation, in GeometryMsgs.Vector3 Scale, IvizMsgs.Trajectory Trajectory)
        {
            this.Header = Header;
            this.VizId = VizId;
            this.Id = Id;
            this.Type = Type;
            this.EntryId = EntryId;
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
            b.Serialize(in Position);
            b.Serialize(in Orientation);
            b.Serialize(in Scale);
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
                int size = 93;
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
        [Preserve] public const string RosMd5Sum = "81c203474b59127ee99d69899254825c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71V32/bNhB+119xQB6aDI63JV1RBOiDl3it1zZxE3VYMQwGI50lbhKpkpRd96/fd5Qt" +
                "26mH7mGpIcBH3t13v4+tNuE5pR+m49n49+nkdnxFL+iHpN1e//w+TW+uZ5dvJpevwftxl/d2fP1+fJ3e" +
                "fujZZ7vs6c3dJJ2I8qvR9csIfb7Lv7mdQHv0QOTprsjd5ejNeIf50y4zvR39Or5Mb8R+L/EsSXzIZ7Uv" +
                "/PevWOXsqIx/uHbaFLTQn2c635xAdYhh1XAC6vyM2AS3EpmCbc1CR7SpBZsa63XQ1jxgvmtVYGfAIOs0" +
                "ENQBod84C9adk89UBWPiSbxPnforslYUejJJkhf/8y95e/fygh6kJzmiu6BMrlxO8FXlKiiaW6RNFyW7" +
                "04oXXEFJ1Q3nFLmSKz+EYlpqT/gKNuxUVa2o9RAKljJb163RGbJCQde8pw9NbUhRo1zQWVspB3nrcm1E" +
                "fO5UzYKOz/PHlk3GNLm6gIzxnLVBw6EVEDLHyksRJ1cUq4jaQSE5Spf2FEcuUPzeOIVSBXGWPzWOvfip" +
                "/AVsfNcFNwQ2ksOwkns6jnczHP0JwQhc4MZmJR3D8+kqlCh0KJkWyml1X7EAo6oVUJ+I0pOTHWQToY0y" +
                "dgPfIW5t/BdY0+NKTKclalZJ9L4tkEAINs4udA7R+1UEySppRKr0vVPoJ9HqTCZHv0iOIQStWBH8K+9t" +
                "plGAnJY6lJsBidWQYXikbjwwY5vGQqqC0sbHYDZzR3YunRNnETmbO0ZQjco4mVdWhWdP6VNPrXrq87dx" +
                "f7sFNjE4lmZDGZDgvdWw77ycPm5XCMavHiZfiWhDLb9NbOvldSgwWkTefkhDmd9JnDhrMK81K5QMq6HX" +
                "hGKuHVQR8hCo7BiB84B0oNyyJ2OlF2r1NyAZ7S/aqmkApmRRGl91qcQ1VI55WAwHtCzZdFLSvnHZxPWk" +
                "M3K60HmnKRnulRWtgxtQmJ+h/auq87kzhvYDiLNd4U6GNJnTyra0lIBAuPVWtHTPvV9xeoO1A1mJa4hD" +
                "7wm2k1eFNIAP2MdfrfrjlPrQW/TF8+f5jz9lDtnHXYJDv1z9o71WXzqBRI62TaQ2S0Ec2+/AgbxCcp2v" +
                "+d0Cwd7cHUT06YOX/V/e8uQfs+nJBS0JAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
