/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapResult : IDeserializable<GetMapResult>, IMessage, IResult<GetMapActionResult>
    {
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map;
    
        public GetMapResult()
        {
            Map = new NavMsgs.OccupancyGrid();
        }
        
        public GetMapResult(NavMsgs.OccupancyGrid Map)
        {
            this.Map = Map;
        }
        
        public GetMapResult(ref ReadBuffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public GetMapResult(ref ReadBuffer2 b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public GetMapResult RosDeserialize(ref ReadBuffer b) => new GetMapResult(ref b);
        
        public GetMapResult RosDeserialize(ref ReadBuffer2 b) => new GetMapResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) BuiltIns.ThrowNullReference();
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Map.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "nav_msgs/GetMapResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/bOBC981cM4EOTwnaddrEoAuxhgaAfh2CzbfdkGMaYGktsJVIlqbjqr99HSrLj" +
                "tih62GwgxBI18+bNmw9Zvt82oQzP/tK6a9nq/rU3BTXcqj/+4z91+/71NdkfBlQz+lCZQF5aL0FsDMT0" +
                "fHFD5chmTsbSoTK6ImH801LXD61jJdR6t+OdqU3sye0B6aYQS6XeCBfiqRp+lJrdSuQbjkx757N7yvmW" +
                "2+O5sXunMrH8jgocZhreHRYNf4Sb8wCbU4jso7ElHUys6GI1X10uiY4JAuNEzQhS85JwUlDPthRar+ZX" +
                "q9UGTv/YT9YdLEGLxdVSGRtfrjc5tHqkgoRYDAUZFALZ95FtwR66Q4pikqgyZSV+Ucu91CnjppUiE6PY" +
                "txKWUwlxlWLFc1331AUYRUfaNU1njeYoFE0jZ/7whBpMbVJRdzV72ENaY5P53nMjCR1XkM+dWC309uYa" +
                "NjaI7qIBoR4I2guHVIW3N6Q6KPfieXKgGa3fuXC1UbMPB7fAuZTogCMLlIFjYi1fUjclwhyuEezpkOUS" +
                "QaCSIFwR6CKfbfEYLgnRwEVah4a8QAp3fazcUNd79oZ3tSRgDSmA+iQ5Pbl8gGwztGXrJvgB8RTjV2Dt" +
                "ETfltKhQvDrJELoSSsIQzXdvCpju+gyia4OZodrsPPteJa8hpJq9SmLDCF65NPjlEJw2qESR21uF6BN6" +
                "LssWk/vYe+LBSE4tVrkayaDYOg+pbzgaCMQ718Uhw4o96yjeBOwGt8+H5wtnHOycPOo/rJZxDdCBA9UO" +
                "41AM6uBsm5636enBSkC/uLrLwdfNs7STNmoPw9R6p3dwuE2gpsB2WCersJkaNB+OBpVgxOK3FsPpGNR5" +
                "U6IjxowShXUzJ1yei7Q+pgnMq0W4Xhych1YtGmx0AlDenXlLTVsIQEtVisPA+36Q/S675HCPVOHv44Hb" +
                "n6elPhQVrDN7MN17QaO2rGWeFko6Lsb3ZmgAWyTKk++S1J2DiEcD9XeHPvY2457sHquFv00QVKYOxrRH" +
                "Nnb8bk38kQu2YKZ8lu7QU7//Rl+Od/3x7uv/Q/8k3Y++1Wd6npNPT59Puqdxxef45xlNdwel/gUaXAgt" +
                "oQgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
