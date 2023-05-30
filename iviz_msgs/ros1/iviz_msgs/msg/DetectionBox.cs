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
            BuiltIns.ThrowIfNull(Id, nameof(Id));
            BuiltIns.ThrowIfNull(Classes, nameof(Classes));
            BuiltIns.ThrowIfNull(Scores, nameof(Scores));
            BuiltIns.ThrowIfNull(PointCloud, nameof(PointCloud));
            PointCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 93;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetArraySize(Classes);
                size += 8 * Scores.Length;
                size += PointCloud.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Action
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size = WriteBuffer2.Align8(size);
            size += 80; // Bounds
            size = WriteBuffer2.AddLength(size, Classes);
            size = WriteBuffer2.Align4(size);
            size += 4; // Scores.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * Scores.Length;
            size = PointCloud.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/DetectionBox";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6827ec41cab2096a1051660738072ba9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XUW/bNhB+nn7FoXloUjjeknRdUCAY0qZdA6RJ12Z9KYqAls42UYl0SMqu+uv3HSnK" +
                "dpahe1hqtIhE3h3v7vt4d2q1Ccd0+vL6/Ory5vTsjE7ol6LdXHz/6u3Vx1dYP7hv/fTiAluHReFDddP4" +
                "mf/5DauKHc3jn2LSBSZVBm0NRJw2M9JVUbywranw8sJ+pYk8+6Lf/vSZylp5z76Y1laFZ0+x4kvrWETY" +
                "eOvSOe8svHlZ27Y6pIU835TyUuB38j//ircf/nhOdyIsduhDUKZSrqKGg6pUUDS1iFzP5uz2a15yDSXV" +
                "LLiiuBu6BfsxFK/n2hP+zdiwU3XdUeshFCyVtmlao0uFtAXd8JY+NLUhRQvlgi7bWjnIW4dEivjUqYbF" +
                "Ov55vm3ZlEznZ88hYzyXbdBwqIOF0rHygsT5GUVMjw5FgXbo03vrDz4XO9cru491ngHIwQsKcxXEa/66" +
                "ABrisPLPcdiTFOUYhyBLjOMqT7tx7Qavfo9wGnzhhS3ntIsQ3nVhbg0MMi2V02pSsxgukQpYfSxKj/c2" +
                "LJto2ihjs/lkcX3GfzFrBrsS0/4c4NWSBt/OkEkILpxd6gqiky4aKWvNJlCtJ065rhCtdGSx81qSDSFo" +
                "RWjwF6S1pQYSFa10mGe6R1huhPQPQ0u91N8SLzcuVTFjC1K6Lt8Vj2gQC2i7vfORy2DdEfL5jR/Kw3/6" +
                "AghOybHQCE4pqQ5kp7jGcBM4Th0j0QtV8kguhCxX/b6OskCOrNNZd0xFrAaDQPFnCxyciXbXcj8qQLiS" +
                "LznYGpQ2PvJp8B+xqFS0tsPNJY++Dk/d8PTtx7i/Tl2OYQAKHN/K57bz8na7zjtKYTMuvhNRflr9mNh6" +
                "tt8XGC3j3nZIY6ml57HoWYPa2bACZCjTgyYUK+24TDS8RuFnBA7e6kCVZU/GChca9QUm0b1YtNViAWPo" +
                "B04ZX6dUYhkquzyejUe0mrNJUlJBYuGPrUKX5PRMV0lTMjwoK+qDG1GYHqIC1XXyOR0G+sGIswm4vTGd" +
                "T6mzLa0kIDy4vkNZmvDgVyygwdqRtKfexD1cR1q8VzMhgA/ojd9F/WGg/pfhIIOdnZzbuhK8S4uuUObr" +
                "eLlfobobj1dVp6vpBQeNjtWoDkb6m0yqquIlhpg2awh8C0kleGOlhq70T9gL3Yg4lJEbsJIufW4ZHoDF" +
                "Ngp/JujirqNJbSfCHk+1Ai6QZV86PRm6Uu9KZC0cl7ryaKoZQT0i5ZzqkH4Jmfuz4lSUTkQgAq91M2VQ" +
                "7ys6rNA3G2Rlv9ZfeA87dFBBe7dFGJhxuAJT3q3N+A1dOA31qO2zZTTQqi2jq+ImGp/D/eBFmPfo+CFP" +
                "YIpjKyfGZmyn+9Mag1NI3kunRXBJKTmvyttWp/o5ig0gdug744+MNLsyhB1VOdl+b1xsz6TYOTyDA64t" +
                "Q+s4Z3EjXfF6pAlAkgekhoSMYEX8xCLsHERPVrpChDoV+ZrNDG/3GM2zVjKQ36Ky+HTWA53MlBhPDNc+" +
                "h6pdJoROE07Pl5gbIc049cDXQgWMzIkSRTGxtib8tL+ZaBSRSqOGo6j5jeFl2Pg9O9VP1ABpAd0duhiC" +
                "2uhbMtv7QcPZVZa/q4GtbfljOCgn/9QLf3oPOn+mU+CRb1/cH8W5RDK7m80/Sdnb6yNDWFUsq/0P3HEt" +
                "NGL2ga3Cf4PSapaqRt1MjHiwL4S7FSiCcX8BEgjT1V7kGmRN5guy4boeaKhvfupkK6nyjPtvsvPL62MJ" +
                "f/2V9le/JB9ng8zBs7hytCEjSyf0dC0jWGLl1w0ZWTqhZ/3K64urU1k6od82V1DZT+g4f8ZhWG84Q3Kp" +
                "0nWOnMyEsdOp55AErtLz1NlGBmwX0rQnqUjXtD8okkK+okTpLD+zaaXQpMrg0bvVxC45n1NiKg69I29A" +
                "xEaZjrjmJlbQ/jIlz4q/AZVaARkODwAA";
                
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
