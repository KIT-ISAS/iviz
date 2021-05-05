/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Feedback")]
    public sealed class Feedback : IDeserializable<Feedback>, IMessage
    {
        [DataMember (Name = "viz_id")] public string VizId { get; set; }
        [DataMember (Name = "id")] public string Id { get; set; }
        [DataMember (Name = "feedback_type")] public int FeedbackType { get; set; }
        [DataMember (Name = "entry_id")] public int EntryId { get; set; }
        [DataMember (Name = "position")] public GeometryMsgs.Vector3 Position { get; set; }
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation { get; set; }
        [DataMember (Name = "scale")] public double Scale { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Feedback()
        {
            VizId = string.Empty;
            Id = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Feedback(string VizId, string Id, int FeedbackType, int EntryId, in GeometryMsgs.Vector3 Position, in GeometryMsgs.Quaternion Orientation, double Scale)
        {
            this.VizId = VizId;
            this.Id = Id;
            this.FeedbackType = FeedbackType;
            this.EntryId = EntryId;
            this.Position = Position;
            this.Orientation = Orientation;
            this.Scale = Scale;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Feedback(ref Buffer b)
        {
            VizId = b.DeserializeString();
            Id = b.DeserializeString();
            FeedbackType = b.Deserialize<int>();
            EntryId = b.Deserialize<int>();
            Position = new GeometryMsgs.Vector3(ref b);
            Orientation = new GeometryMsgs.Quaternion(ref b);
            Scale = b.Deserialize<double>();
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
            b.Serialize(VizId);
            b.Serialize(Id);
            b.Serialize(FeedbackType);
            b.Serialize(EntryId);
            Position.RosSerialize(ref b);
            Orientation.RosSerialize(ref b);
            b.Serialize(Scale);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (VizId is null) throw new System.NullReferenceException(nameof(VizId));
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 80;
                size += BuiltIns.UTF8.GetByteCount(VizId);
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Feedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "481af93a8579e0fa1afd1a6d3702f6d1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71STUvDQBC9768Y8KJQIrTiQfAsPQiK4rVMk0m6NNmNM9PG9Nc7m9SUiuBF3NPbnXlv" +
                "33yIsg8V7P1h5Qsn482QD7qYQ0lUrDHfrrRv6fhGQblPyRXFhhJupJLrN8o18gLaKF59DN/CzztU4mAB" +
                "iOxNA4ekso6otzcgOdbk3P0fH/f48nAHPxp1F/C68QJMLZOYIQGE/RADH6BkIpAWc8rAUpcKlhtD3UND" +
                "GBQ0nphGLDwb1SrKTJWYysg0A69QRBIIUU2jwa1JUhBKbGxbE0NQxiD10I30bJRLyqpsBt2GwpiVRoLJ" +
                "RUWB2OfAvvLFyLSPmomMcCxuBlrOofN1PXoeP9MNmQjHsfdXGSxL6OMOulSQAYYCFZPQmiZfuK6T3ziD" +
                "XTI+SJw39CnaYlhbRLAi650oYZG5abYfE+ondPiXUZ+W7qdpn23i+czT7f20sanJvxb0hTrnPgGqceQb" +
                "VQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
