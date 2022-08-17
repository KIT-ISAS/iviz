/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
    
        public Feedback()
        {
            VizId = "";
            Id = "";
            Trajectory = new IvizMsgs.Trajectory();
        }
        
        public Feedback(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out VizId);
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out EntryId);
            b.Deserialize(out Angle);
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            Trajectory = new IvizMsgs.Trajectory(ref b);
        }
        
        public Feedback(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out VizId);
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out EntryId);
            b.Deserialize(out Angle);
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            Trajectory = new IvizMsgs.Trajectory(ref b);
        }
        
        public Feedback RosDeserialize(ref ReadBuffer b) => new Feedback(ref b);
        
        public Feedback RosDeserialize(ref ReadBuffer2 b) => new Feedback(ref b);
    
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
        
        public void RosSerialize(ref WriteBuffer2 b)
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
            if (VizId is null) BuiltIns.ThrowNullReference();
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Trajectory is null) BuiltIns.ThrowNullReference();
            Trajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 101;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(VizId);
                size += WriteBuffer.GetStringSize(Id);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.AddLength(c, VizId);
            c = WriteBuffer2.AddLength(c, Id);
            c += 1; // Type
            c = WriteBuffer2.Align4(c);
            c += 4; // EntryId
            c = WriteBuffer2.Align8(c);
            c += 8; // Angle
            c += 24; // Position
            c += 32; // Orientation
            c += 24; // Scale
            c = Trajectory.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Feedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "beec894d41c35d3624bcb12f27355c75";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VXW/TSBR996+4Uh9oV2l2aQGhSjxk2yx4gTakBoEQiqb2jT279ow7M04wv54z48RJ" +
                "ShA8UCJLmZl775n7eaaRyj2l5MNkPBu/n8TT8QU9o7+iZnP899skubqcnb+Kz19C9nBb9np8+XZ8mUw/" +
                "9OKTbfHk6jpOYm/8YnT5PECfbsuvpjGsR3dUHm2rXJ+PXo23hI+3hcl09O/4PLny9/caT6LIumxW2dz+" +
                "+YJFxoaK8IdjI1VOC/llJrP1DqsO0bU1R1idnhArZ1qvMy+1cE8ekVB5yVHOumIvCdgTDWWqtZVOanVH" +
                "+KYRjo2CgLSRwBN7lN5x6rQ5JZsKoEvvVzhPjPgviFpy/TKKome/+Be9vn5+RneSFR3QtRMqEyYj+Coy" +
                "4QTNNZIo84LNcckLLmEkqpozClKfOTuEYVJIS/hyVmxEWbbUWCg5TamuqkbJFFkhJyvesYelVCSoFsbJ" +
                "tCmFgb42mVRefW5ExR4dn+XbhlXKFF+cQUdZThsn4VALhNSwsL6k8QWFmqKSMKAD+jjV9uGn6CBZ6mOc" +
                "c46e6L0gVwjnvebPtWHrHRb2DJf90UU5xCXIEuO6zNJhOJtha48It8EXrnVa0CFCmLSuQMVdwbQQRoqb" +
                "kj0wylsC9YE3enC0hawCtBJKr+E7xM0dPwOrelwf03GB4pU+DbbJkUko1kYvZAbVmzaApKXvSCrljRFo" +
                "LG/VXRkd/OOTDSVYhdLgX1irU4lKZLSUrljPTSiLn5F7ass9w7buMKTKCalsCGY9gKTnvoXCUCJnc8MI" +
                "qhYp90P8uV+1/erL73F/QwfrGAz7ZkMZkOAdjth13u9uN1yCOayG0Q8iWq+Wvye2FYvtC4wWQbYb0tAP" +
                "chwmTisMbsUCJQNH9JYwzKSBKUIeApUNI3AekHSUabaktO+FSvwPSEb7e2tR1wATnjGVLbtU4hgmhzzM" +
                "hwNaFqw6Ld++gXUCT8mUjMxl1ln6DPfGglbBDcjNT9D+Zdn53F2G9gOI0V3hjoYUz6nVDS19QFiYFT1q" +
                "uuHerzC9TuuB58YVxL6HBexkRe4bwDoQ8w+rfj+l3vcoffMOWv74yc8h28Al2PTkau/t2frWCSRytGki" +
                "sSYF79huBw78c+SPs5W8IxDw5vYgok/vPPHfedSjr7JR/4RECQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
