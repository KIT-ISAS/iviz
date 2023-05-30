/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class Path : IHasSerializer<Path>, IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public GeometryMsgs.PoseStamped[] Poses;
    
        public Path()
        {
            Poses = EmptyArray<GeometryMsgs.PoseStamped>.Value;
        }
        
        public Path(in StdMsgs.Header Header, GeometryMsgs.PoseStamped[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        public Path(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.PoseStamped[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.PoseStamped>.Value;
                else
                {
                    array = new GeometryMsgs.PoseStamped[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.PoseStamped(ref b);
                    }
                }
                Poses = array;
            }
        }
        
        public Path(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                GeometryMsgs.PoseStamped[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.PoseStamped>.Value;
                else
                {
                    array = new GeometryMsgs.PoseStamped[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.PoseStamped(ref b);
                    }
                }
                Poses = array;
            }
        }
        
        public Path RosDeserialize(ref ReadBuffer b) => new Path(ref b);
        
        public Path RosDeserialize(ref ReadBuffer2 b) => new Path(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Poses.Length);
            foreach (var t in Poses)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Poses.Length);
            foreach (var t in Poses)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Poses, nameof(Poses));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var msg in Poses) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Poses.Length
            foreach (var msg in Poses) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "nav_msgs/Path";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6227e2b7e9cce15051f669a5e197bbf7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTWvbQBC9768Y8CFJqV36QQ+GHgKlH4eC2+QWghlLY2lB2lVmV3bUX9+3q1iOA6U9" +
                "tBEC7a5m3rw3Hzu7dMSqPJDfUueDBIo1R1LpVIK4GIhpxbGmrVcs1W98pOixbRq/N1+ES1Gq88dU4luJ" +
                "OqzbUIVXK8BdRW47KW9uR3BjPvzjx3y7+rykEMsx5sjHzAiBXclaEghxyZGzgNpWtei8kZ00cMrcKP+N" +
                "QydhAcfr2gbCW4kT5aYZqA8wguTCt23vbMFRKNpWTvzhaZFK6lijLfqGFfZeS+uS+Va5lYSON8hdL64Q" +
                "+vpxCRsXpOijBaEBCIUKB+sq/CTTWxffvkkONKObHz68vjWz672f41wqpH1iMRYNrOU+1S0R5rBEsBej" +
                "ygWCIEuCcGWg83y2xjZcEKKBi3S+qOkcElZDrL0DoNCO1fKmkQRcIBVAPUtOZxePkF2Gduz8AX5EPMb4" +
                "G1g34SZN8xrFa1IaQl8hkzDs1O9sCdPNkEGKxqI7qbEbZR1M8hpDmtmnlGwYwSuXBl8OwRcWlShpb2Nt" +
                "QtSEnsuytuX/asvfzgN0XlLaZzqYtq1o7omnPUNIxLHMT8YtA6S5ej7+mfh0OXC0qOnDzZH6f6uCMnRc" +
                "yMs0Lum4fPhvs22S49UefBcEEejmycB876FdXcY92j2XQFA5XAHo5cjWhdxtE39owYxnyidyzbbxHN+/" +
                "o/tpNUyrn89D/5i6g4bHt/hJPk/Jp93dMe+4KNuF+YOiw2pvzC9gdHLNRAYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Path> CreateSerializer() => new Serializer();
        public Deserializer<Path> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Path>
        {
            public override void RosSerialize(Path msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Path msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Path msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Path msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Path msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Path>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Path msg) => msg = new Path(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Path msg) => msg = new Path(ref b);
        }
    }
}
