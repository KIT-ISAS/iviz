/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class DetectionBox : IHasSerializer<DetectionBox>, System.IDisposable, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "bounds")] public BoundingBox Bounds;
        [DataMember (Name = "velocity")] public GeometryMsgs.Vector3 Velocity;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "classes")] public string[] Classes;
        [DataMember (Name = "scores")] public double[] Scores;
        [DataMember (Name = "point_cloud")] public SensorMsgs.PointCloud2 PointCloud;
    
        public DetectionBox()
        {
            Id = "";
            Classes = EmptyArray<string>.Value;
            Scores = EmptyArray<double>.Value;
            PointCloud = new SensorMsgs.PointCloud2();
        }
        
        public DetectionBox(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            Id = b.DeserializeString();
            b.Deserialize(out Bounds);
            b.Deserialize(out Velocity);
            b.Deserialize(out Color);
            Classes = b.DeserializeStringArray();
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                    array = new double[n];
                    b.DeserializeStructArray(array);
                }
                Scores = array;
            }
            PointCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public DetectionBox(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            b.Align4();
            Id = b.DeserializeString();
            b.Align8();
            b.Deserialize(out Bounds);
            b.Deserialize(out Velocity);
            b.Deserialize(out Color);
            Classes = b.DeserializeStringArray();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                    array = new double[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Scores = array;
            }
            PointCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public DetectionBox RosDeserialize(ref ReadBuffer b) => new DetectionBox(ref b);
        
        public DetectionBox RosDeserialize(ref ReadBuffer2 b) => new DetectionBox(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(in Bounds);
            b.Serialize(in Velocity);
            b.Serialize(in Color);
            b.Serialize(Classes.Length);
            b.SerializeArray(Classes);
            b.Serialize(Scores.Length);
            b.SerializeStructArray(Scores);
            PointCloud.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Align4();
            b.Serialize(Id);
            b.Align8();
            b.Serialize(in Bounds);
            b.Serialize(in Velocity);
            b.Serialize(in Color);
            b.Serialize(Classes.Length);
            b.SerializeArray(Classes);
            b.Align4();
            b.Serialize(Scores.Length);
            b.Align8();
            b.SerializeStructArray(Scores);
            PointCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            BuiltIns.ThrowIfNull(Classes, nameof(Classes));
            BuiltIns.ThrowIfNull(Scores, nameof(Scores));
            PointCloud.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 133;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetArraySize(Classes);
                size += 8 * Scores.Length;
                size += PointCloud.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Action
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size = WriteBuffer2.Align8(size);
            size += 80; // Bounds
            size += 24; // Velocity
            size += 16; // Color
            size = WriteBuffer2.AddLength(size, Classes);
            size = WriteBuffer2.Align4(size);
            size += 4; // Scores.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * Scores.Length;
            size = PointCloud.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/DetectionBox";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "21696b2cced6fecdc078c1e8e4c52f32";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71X308cNxB+7v4Vo/AQiI5rgTRFkVBFck2CRIASmpcoQr61786K177YXsjmr+839nrv" +
                "jiClD4UV6LzjmfH8+Dwz22obD+n49dXJ+dn18WRCR/Rb1a4TL/96f/7xL9D37qMfn55ia7+qQpTXTZiH" +
                "X98pIZWnRfrpRUQdtbPg8drOScuqeuVaK/Hyyn2jKa9DNVeuUdF3Wc1HVUfnD+hGGVfr2K0OeO2M85dv" +
                "Xx1TzauqV/vpM9VGhKBCNTNOxBfPQQm18yBUQdngfJa/cLDptXGt3Kclr69rfqnwHP3PT/X+w9uXdCc0" +
                "1RZ9iMJK4SXBYSFFFDRzCJmeL5TfNQo+Q0g0SyUp7cZuqcIYglcLHQh/c2WVF8Z01AYwRYdYNE1rdS2i" +
                "oqgbtSEPSW1J0FL4qOvWCA9+55EAZp950SjWjr+gvrbK1opOJi/BY4Oq26hhUAcNtVcicAZPJpQye7DP" +
                "ArRFny5d2PtcbV3dul3Q1RwIGKyguBCRrVbflsgGGyzCSxz2LHs5xiGIksJxMtB2ol3jNewQToMtaunq" +
                "BW3DhYsuLpyFQkU3wmsxNYoV1wgFtD5loac7a5ptUm2FdUV91rg647+otYNe9ml3geQZDkNo54gkGJfe" +
                "3WgJ1mmXlNRGKxvJ6KkXvqtYKh9Zbb3hYIMJUik1+AVoAXFkQtKtjotyTVJarvmyPAws9Y3+nnG5dhnv" +
                "3MILxI1q+ALY3n8/g/6uHsrCH21BCo7JK4YRjBJcVcjNcI1hJvI48wqBXopajfhCMFn2+zrxInPkvC6y" +
                "Y6pSNRgYqr9b5MHbpHfF91gOwpRyyYHWKLQNCU+D/fBF5KK16W4pefRtWHXD6vvjmL8KXfFhSBQwvhHP" +
                "TeP57esq7iiFzbj6iUdldfs4vvVov88xdCje23RpzLX0JBU9Z1E7GyWQMpTpQRKCUntVZxheofArOA7c" +
                "6kjSqUDWMRYa8QUq0b0US4vlEsrQD7ywweRQggyRbTWej0d0u1A2c3EFSYU/tQpdk9dzLbMkR3gQFtQ7" +
                "N6I420cFMibbnA8D/KDEu5y4nTGdzKhzLd2yQ1j4vkM5mqrBrlRAo3Mjbk+9inuwjrCEIOYMgBDRG3+a" +
                "9YdJ9Y+TRT4S3c0Pq/mwmg4r8WATw/3jSoFfCdvCGckIxBxkMpK4QJztSvQbG/AqTC4WgZGh0UMb0UFJ" +
                "X1tISJnKCti0XYEitOAUjEBQDGS5o0Nf7EakYp3QCi25DJUmFgCh1NhhzxRzhe9oatyU8RzICCAFvCrU" +
                "Xk+HPtmbku4RDOdK92SmFZx6QsJ70QEQ7LLqz0pzWj4RjjDgnJ8Liw4kaV+ikzeIyq7RX9QOdmhPQnq7" +
                "hRuYupQEdi9WasKaLIyGeJIORTNaumzrZCqbiVbscWPVMi767IQhTsCuV45PTOOBm+3ODEa5mK3n3g/n" +
                "slA2XtRfW50r+ii1pDQz3BnIeMja5rHwQJZgh51xtTleY2d/AgN8W8fWqxLFtXClC5tnEg4eMjUEZAQt" +
                "bCeI0LOXLLnVEh7q3HaMsnO83aO0TH9ZQXlLwmzTpE90VlNjYLLKhOKq9gUQOs9cPV5SbBg049yV3zAU" +
                "MMRnSFTV1DlDeHS4nmqUNanRVVBmw9o4NWz8WYzqZ3wkaQnZLTodnFrrpNMu4jOhSHh3W/jvSmBrk/8Q" +
                "BvLJv/TMny4B5890jHyU25f2R2lS4shuF/XPcvR2es/glkyFvn+AHd9CIkUfuRX4tyj29kYYVPKMiEer" +
                "QCkZ9xcgTmG+2stSg5wteEE0fNcnGuLrH19FS6484/5b8eTs6pDdX31w/tOT+Dtz4Nl7kSgHazxMOqLn" +
                "Kx7OJSi/r/Ew6Yhe9JQ3p+fHTDqiP9Yp6DVHdFg+LPH50KiSkjORr3PCZAGMm82CipnhPK9n3jU88vuY" +
                "508ORb6m/UEJFPxdx0KTsla25UKTK0PANCGm7kaVc2rM6bE35B2A2AjbkTKqSRW0v0zZsupf3dEuOtkP" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            PointCloud.Dispose();
        }
    
        public Serializer<DetectionBox> CreateSerializer() => new Serializer();
        public Deserializer<DetectionBox> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DetectionBox>
        {
            public override void RosSerialize(DetectionBox msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DetectionBox msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DetectionBox msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DetectionBox msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(DetectionBox msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DetectionBox>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DetectionBox msg) => msg = new DetectionBox(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DetectionBox msg) => msg = new DetectionBox(ref b);
        }
    }
}
