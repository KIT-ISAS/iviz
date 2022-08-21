/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class Path : IDeserializable<Path>, IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public GeometryMsgs.PoseStamped[] Poses;
    
        public Path()
        {
            Poses = System.Array.Empty<GeometryMsgs.PoseStamped>();
        }
        
        public Path(in StdMsgs.Header Header, GeometryMsgs.PoseStamped[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        public Path(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            {
                int n = b.DeserializeArrayLength();
                Poses = n == 0
                    ? System.Array.Empty<GeometryMsgs.PoseStamped>()
                    : new GeometryMsgs.PoseStamped[n];
                for (int i = 0; i < n; i++)
                {
                    Poses[i] = new GeometryMsgs.PoseStamped(ref b);
                }
            }
        }
        
        public Path(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Poses = n == 0
                    ? System.Array.Empty<GeometryMsgs.PoseStamped>()
                    : new GeometryMsgs.PoseStamped[n];
                for (int i = 0; i < n; i++)
                {
                    Poses[i] = new GeometryMsgs.PoseStamped(ref b);
                }
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
            b.Serialize(Poses.Length);
            foreach (var t in Poses)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Poses is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) BuiltIns.ThrowNullReference(nameof(Poses), i);
                Poses[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + WriteBuffer.GetArraySize(Poses);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Poses.Length
            foreach (var t in Poses)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
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
    }
}
