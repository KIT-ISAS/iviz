/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class PointCloud : IDeserializable<PointCloud>, IMessage
    {
        // This message holds a collection of 3d points, plus optional additional
        // information about each point.
        // Time of sensor data acquisition, coordinate frame ID.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Array of 3d points. Each Point32 should be interpreted as a 3d point
        // in the frame given in the header.
        [DataMember (Name = "points")] public GeometryMsgs.Point32[] Points;
        // Each channel should have the same number of elements as points array,
        // and the data in each channel should correspond 1:1 with each point.
        // Channel names in common practice are listed in ChannelFloat32.msg.
        [DataMember (Name = "channels")] public ChannelFloat32[] Channels;
    
        public PointCloud()
        {
            Points = System.Array.Empty<GeometryMsgs.Point32>();
            Channels = System.Array.Empty<ChannelFloat32>();
        }
        
        public PointCloud(in StdMsgs.Header Header, GeometryMsgs.Point32[] Points, ChannelFloat32[] Channels)
        {
            this.Header = Header;
            this.Points = Points;
            this.Channels = Channels;
        }
        
        public PointCloud(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Points);
            b.DeserializeArray(out Channels);
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new ChannelFloat32(ref b);
            }
        }
        
        public PointCloud(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Points);
            b.DeserializeArray(out Channels);
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new ChannelFloat32(ref b);
            }
        }
        
        public PointCloud RosDeserialize(ref ReadBuffer b) => new PointCloud(ref b);
        
        public PointCloud RosDeserialize(ref ReadBuffer2 b) => new PointCloud(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Points);
            b.SerializeArray(Channels);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Points);
            b.SerializeArray(Channels);
        }
        
        public void RosValidate()
        {
            if (Points is null) BuiltIns.ThrowNullReference();
            if (Channels is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Channels.Length; i++)
            {
                if (Channels[i] is null) BuiltIns.ThrowNullReference(nameof(Channels), i);
                Channels[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 12 * Points.Length;
                size += WriteBuffer.GetArraySize(Channels);
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Points);
            WriteBuffer2.AddLength(ref c, Channels);
        }
    
        public const string MessageType = "sensor_msgs/PointCloud";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d8e9c3f5afbdd8a130fd1d2763945fca";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWTW8bNxC9768YSIfIhazCSQ6FgR7atE5zKBAkvgWBMdod7RLhkmuSa1v99X1D7urD" +
                "8aGHRhCg1ZJ8M/PmzQyXdNuZSL3EyK1Q520Tian21kqdjHfkd/SmocEbl+KaBjtG8oOusCVuGlMeqyUZ" +
                "t/Oh53yIt35MJFx35eSmwoZb04vCRXHRB2o4MXF9P5qYQdaw6kNjHCehXWBs/vDHpvpLuJFAXf5RmN9C" +
                "4P2ZWxv6Uy191D9vXlPs/Ggb2gpcShKGIEkaYo1rPpLdpdTNhlrzIG5+VUxtqlZ8Lyns7/rYxp8n9C9f" +
                "J6PqSjZbd+yc2Nlsxw+SYaICu7Hfwnt4K1Z6wTl1pCAQayRr4LBr8pFMCbyQF3BrH4LEwWPr1fUVPZrU" +
                "nRG8pHfTAQfDUWFq3/dIxhAYqawF9oSsicoGVqftN9Yz4togyE11/g6xTk4g2l//50/19+f31xRTU+gt" +
                "aUYUnxPY4NBAk4kzIZAVdabtJFxaeVBGEvcDgsiraT9I1PCzkPFtxUlga/c0RmxKPvMwOlOrsBJEeHa+" +
                "SIFp4ACSRsvhOx0qOr5R7kdxtcryGntclHpMEA4sGVcH4Whci0WqxkmIck9L+vLJx6uv1fL20V+qIFvo" +
                "4eAF0s5JvZYn6DTGLNRrGPupRLmBEbAkMIfCXOV3d/gbLwjW4IsMHhpYIYSP+9T5ouAHDoa3VhS4BhVA" +
                "faWHXl2cILsM7dj5Gb4gHm38F1h3wNWYLiGXxioNcWzBJDYOwT+YBlu3+wxSW4MygA63gcO+0lPFZLW8" +
                "ybWYNI+lErRYoq8Nq2RV8VVMQdFzWu5M86Nk+WLpzxoDWYmNizmcwZfupSXOpRZVT7sgCGvgWla5UqGG" +
                "rUHJYxfyXKPleXehuvqQs49X0Kg0Ra/QbWlm9AjRQ/JBzUSTqXeoX24UaHJrQ1T0mZ2bkFBC2Su8ASBy" +
                "0Ps09UM/oDy2xpq0z0fnk/MMUPYlmtYVZxJ/ExoHslguEalXTrs4aqTFaeunwKaulshDR2ttdFNHrTlK" +
                "ISj7/M76sVHb1a40Gno6PO0PT//8qJaTx0/J7Hm/e4GJ3EIm5R59P+wAPzoyjxNRdasN/Vy2J436QEnh" +
                "4BaPVlyLPeCnFJkdJSpGnnPHYXaYKVyUd37sdKRMNoBxdHmdh0z2I1s4AX7ubHbvMG1Kjo9T/LspI08Y" +
                "KVqUhzmDNmLHRrSNES3GxZoWDwu6pOAfsxe4XYy9o5Va0FuGttCLmRgru4SGAN17Mj1I3mSYk8/c6P2Q" +
                "i09KyYxgH5WJOa6piLTFBSRInwsVE+Q5CDCSD5gJlrRz40RJhpNHlNuRt9fHuxF4d/45Thw1s8GjNvvi" +
                "6SK0W432xocp5TnXuRM2Y130BAqwPIVZg8rAcEDnxi9nFlaf1u/Xv19MqsCQqr/l6a2FmcniCLZQrGaH" +
                "WGDr9dvcaNZnKKAWE03vNNlBbQMOzO3VTYvaxAUFvponpPWwNu1twBOjnBf5BqfKOMn/LCK9PqGuejhg" +
                "6jhLct65kk2raKd2T9rYIse2QDecmrsiz9amuJ/XwtXl1VGrh5vVZPeo57MK2MydBdeaqciqfwGCBUWq" +
                "/woAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
