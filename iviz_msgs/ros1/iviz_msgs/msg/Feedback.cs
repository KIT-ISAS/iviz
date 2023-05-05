/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Feedback : IHasSerializer<Feedback>, IMessage
    {
        public const byte TYPE_DIALOG_EXPIRED = 0;
        public const byte TYPE_BUTTON_CLICK = 1;
        public const byte TYPE_MENUENTRY_CLICK = 2;
        public const byte TYPE_POSITION_CHANGED = 3;
        public const byte TYPE_ORIENTATION_CHANGED = 4;
        public const byte TYPE_SCALE_CHANGED = 5;
        public const byte TYPE_TRAJECTORY_CHANGED = 6;
        public const byte TYPE_COLLIDER_ENTERED = 7;
        public const byte TYPE_COLLIDER_EXITED = 8;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "viz_id")] public string VizId;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "preview_only")] public bool PreviewOnly;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "entry_id")] public int EntryId;
        [DataMember (Name = "angle")] public double Angle;
        [DataMember (Name = "position")] public GeometryMsgs.Point Position;
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        [DataMember (Name = "scale")] public GeometryMsgs.Vector3 Scale;
        [DataMember (Name = "trajectory")] public GeometryMsgs.Pose[] Trajectory;
        [DataMember (Name = "collider_id")] public string ColliderId;
    
        public Feedback()
        {
            VizId = "";
            Id = "";
            Trajectory = EmptyArray<GeometryMsgs.Pose>.Value;
            ColliderId = "";
        }
        
        public Feedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            VizId = b.DeserializeString();
            Id = b.DeserializeString();
            b.Deserialize(out PreviewOnly);
            b.Deserialize(out Type);
            b.Deserialize(out EntryId);
            b.Deserialize(out Angle);
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Pose[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Pose>.Value;
                else
                {
                    array = new GeometryMsgs.Pose[n];
                    b.DeserializeStructArray(array);
                }
                Trajectory = array;
            }
            ColliderId = b.DeserializeString();
        }
        
        public Feedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            VizId = b.DeserializeString();
            b.Align4();
            Id = b.DeserializeString();
            b.Deserialize(out PreviewOnly);
            b.Deserialize(out Type);
            b.Align4();
            b.Deserialize(out EntryId);
            b.Align8();
            b.Deserialize(out Angle);
            b.Deserialize(out Position);
            b.Deserialize(out Orientation);
            b.Deserialize(out Scale);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Pose[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Pose>.Value;
                else
                {
                    array = new GeometryMsgs.Pose[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Trajectory = array;
            }
            ColliderId = b.DeserializeString();
        }
        
        public Feedback RosDeserialize(ref ReadBuffer b) => new Feedback(ref b);
        
        public Feedback RosDeserialize(ref ReadBuffer2 b) => new Feedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(VizId);
            b.Serialize(Id);
            b.Serialize(PreviewOnly);
            b.Serialize(Type);
            b.Serialize(EntryId);
            b.Serialize(Angle);
            b.Serialize(in Position);
            b.Serialize(in Orientation);
            b.Serialize(in Scale);
            b.Serialize(Trajectory.Length);
            b.SerializeStructArray(Trajectory);
            b.Serialize(ColliderId);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(VizId);
            b.Align4();
            b.Serialize(Id);
            b.Serialize(PreviewOnly);
            b.Serialize(Type);
            b.Align4();
            b.Serialize(EntryId);
            b.Align8();
            b.Serialize(Angle);
            b.Serialize(in Position);
            b.Serialize(in Orientation);
            b.Serialize(in Scale);
            b.Serialize(Trajectory.Length);
            b.Align8();
            b.SerializeStructArray(Trajectory);
            b.Serialize(ColliderId);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(VizId, nameof(VizId));
            BuiltIns.ThrowIfNull(Id, nameof(Id));
            BuiltIns.ThrowIfNull(Trajectory, nameof(Trajectory));
            BuiltIns.ThrowIfNull(ColliderId, nameof(ColliderId));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 110;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(VizId);
                size += WriteBuffer.GetStringSize(Id);
                size += 56 * Trajectory.Length;
                size += WriteBuffer.GetStringSize(ColliderId);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, VizId);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size += 1; // PreviewOnly
            size += 1; // Type
            size = WriteBuffer2.Align4(size);
            size += 4; // EntryId
            size = WriteBuffer2.Align8(size);
            size += 8; // Angle
            size += 24; // Position
            size += 32; // Orientation
            size += 24; // Scale
            size += 4; // Trajectory.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Trajectory.Length;
            size = WriteBuffer2.AddLength(size, ColliderId);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/Feedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "26063628ff00b77f3c9b8ee24a254ccc";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V32/bNhB+119xQB6aDI63Jl1XBOiD53ipNjf2HHVoURQGLZ0lbjKpkpRd9a/fR8qW" +
                "7cBD97DFEGCKd/fdr+9OtVTuFSUfpqP5bTwYT+7mo/fTeDa6pdf0Q1TvpT+/S5LJ/Xw4joe/Qfb8UPZ2" +
                "dP9udJ/MPnTiq0PxdPIQJ7E3fjO4vwvQ14fyySyG9eCRyotDlYfhYDw6EP54KExmg19Hw2Ti/XcaLw81" +
                "hpPxOL4dzebwM2qT++m0/H2cBPGrKLIum69sbr9/wyJjQ0X4w7WRKqe1/DqX2e4Np4XWJVWG15I3c63K" +
                "ZuvANRVHOF1fEStnGm+1LLVwL1+QUHnJUc56xV4SvE01lKnSVjqp1SPh77VwbBQEpI0Enjih9AenTptr" +
                "sqk4gW754ydyRvwZtJpdBqkuS4n8fHhR9Po//kVvH+5u6FFFozN6cEJlwmSEEEUmnKClRqVlXrC5LHnN" +
                "JYzEquKMgtQX0/ZhmBTSEp6cFRtRlg3VFkpOI5HVqlYyRaHIyRUf2cNSKhJUCeNkWpfCQF+bTCqvvjRi" +
                "xR4dj+XPNauUKb69gY6ynNZOIqAGCKlhYX3V4lsKbUZzYUBn9HGm7fNP0Vmy0Ze45xzE6aIgVwjno+Yv" +
                "IIr1AQt7A2fftVn24QRVYrjLLJ2Huzle7QXBG2LhSqcFnSOFaeMKkMAVTGthpFiU7IHR8RKoz7zRs4sD" +
                "ZBWglVB6B98i7n38G1jV4fqcLgs0r/RlsHWOSkKxMnoNFmW0aAJIWnqSUikXRoBr3qp1GZ394osNJViF" +
                "1uBfWKtTiU5ktJGu2FEztCXw8v+h5Yn52zEMpXJCKhuS2c0k6aWnUJhT1GxpGElVIuVurr90p6Y7fX2a" +
                "8PcbYpeDYU82tAEFPlobx8H7t8/79YI5XPWjb2S0O22eJrftYjuVGK2D7Dilvh/kOEycX8hYMgItw47o" +
                "LGGYSQNTpNwHKhtG4twj6SjTbElpz4WV+AuQDPp7a1FVABN+iSpbtqXENUzOuZ/3e7QpWLVanr5h64Q9" +
                "JVMyMpdZa+kr3BkL2ibXI7e8Av3Lso25dQb6AcTotnEXfYqX1OiaNj4hHMx2PWpacBdXmF6ndc/vxi3E" +
                "qW8NtpMVuSeAdVjM3+z6U7Taf6cQ7mDfKrEbPYwhH/e555e+v8628nZMsZ0O6Q42PPq2/sPXNPobAZjU" +
                "+hUJAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Feedback> CreateSerializer() => new Serializer();
        public Deserializer<Feedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Feedback>
        {
            public override void RosSerialize(Feedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Feedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Feedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Feedback msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Feedback msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Feedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Feedback msg) => msg = new Feedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Feedback msg) => msg = new Feedback(ref b);
        }
    }
}
