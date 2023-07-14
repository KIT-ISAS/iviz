/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class PointCloud : IHasSerializer<PointCloud>, IMessage
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
            Points = EmptyArray<GeometryMsgs.Point32>.Value;
            Channels = EmptyArray<ChannelFloat32>.Value;
        }
        
        public PointCloud(in StdMsgs.Header Header, GeometryMsgs.Point32[] Points, ChannelFloat32[] Channels)
        {
            this.Header = Header;
            this.Points = Points;
            this.Channels = Channels;
        }
        
        public PointCloud(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point32[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point32>.Value;
                else
                {
                    array = new GeometryMsgs.Point32[n];
                    b.DeserializeStructArray(array);
                }
                Points = array;
            }
            {
                int n = b.DeserializeArrayLength();
                ChannelFloat32[] array;
                if (n == 0) array = EmptyArray<ChannelFloat32>.Value;
                else
                {
                    array = new ChannelFloat32[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ChannelFloat32(ref b);
                    }
                }
                Channels = array;
            }
        }
        
        public PointCloud(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point32[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point32>.Value;
                else
                {
                    array = new GeometryMsgs.Point32[n];
                    b.DeserializeStructArray(array);
                }
                Points = array;
            }
            {
                int n = b.DeserializeArrayLength();
                ChannelFloat32[] array;
                if (n == 0) array = EmptyArray<ChannelFloat32>.Value;
                else
                {
                    array = new ChannelFloat32[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ChannelFloat32(ref b);
                    }
                }
                Channels = array;
            }
        }
        
        public PointCloud RosDeserialize(ref ReadBuffer b) => new PointCloud(ref b);
        
        public PointCloud RosDeserialize(ref ReadBuffer2 b) => new PointCloud(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Points.Length);
            b.SerializeStructArray(Points);
            b.Serialize(Channels.Length);
            foreach (var t in Channels)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Points.Length);
            b.SerializeStructArray(Points);
            b.Serialize(Channels.Length);
            foreach (var t in Channels)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Points, nameof(Points));
            BuiltIns.ThrowIfNull(Channels, nameof(Channels));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += 12 * Points.Length;
                foreach (var msg in Channels) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Points.Length
            size += 12 * Points.Length;
            size += 4; // Channels.Length
            foreach (var msg in Channels) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "sensor_msgs/PointCloud";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d8e9c3f5afbdd8a130fd1d2763945fca";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
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
    
        public Serializer<PointCloud> CreateSerializer() => new Serializer();
        public Deserializer<PointCloud> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PointCloud>
        {
            public override void RosSerialize(PointCloud msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PointCloud msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PointCloud msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(PointCloud msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(PointCloud msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<PointCloud>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PointCloud msg) => msg = new PointCloud(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PointCloud msg) => msg = new PointCloud(ref b);
        }
    }
}
