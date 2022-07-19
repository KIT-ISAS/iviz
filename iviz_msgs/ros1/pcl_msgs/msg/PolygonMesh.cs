/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class PolygonMesh : IDeserializableRos1<PolygonMesh>, IDeserializableRos2<PolygonMesh>, IMessageRos1, IMessageRos2
    {
        // Separate header for the polygonal surface
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Vertices of the mesh as a point cloud
        [DataMember (Name = "cloud")] public SensorMsgs.PointCloud2 Cloud;
        // List of polygons
        [DataMember (Name = "polygons")] public Vertices[] Polygons;
    
        /// Constructor for empty message.
        public PolygonMesh()
        {
            Cloud = new SensorMsgs.PointCloud2();
            Polygons = System.Array.Empty<Vertices>();
        }
        
        /// Explicit constructor.
        public PolygonMesh(in StdMsgs.Header Header, SensorMsgs.PointCloud2 Cloud, Vertices[] Polygons)
        {
            this.Header = Header;
            this.Cloud = Cloud;
            this.Polygons = Polygons;
        }
        
        /// Constructor with buffer.
        public PolygonMesh(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Cloud = new SensorMsgs.PointCloud2(ref b);
            b.DeserializeArray(out Polygons);
            for (int i = 0; i < Polygons.Length; i++)
            {
                Polygons[i] = new Vertices(ref b);
            }
        }
        
        /// Constructor with buffer.
        public PolygonMesh(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Cloud = new SensorMsgs.PointCloud2(ref b);
            b.DeserializeArray(out Polygons);
            for (int i = 0; i < Polygons.Length; i++)
            {
                Polygons[i] = new Vertices(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new PolygonMesh(ref b);
        
        public PolygonMesh RosDeserialize(ref ReadBuffer b) => new PolygonMesh(ref b);
        
        public PolygonMesh RosDeserialize(ref ReadBuffer2 b) => new PolygonMesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Cloud.RosSerialize(ref b);
            b.SerializeArray(Polygons);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Cloud.RosSerialize(ref b);
            b.SerializeArray(Polygons);
        }
        
        public void RosValidate()
        {
            if (Cloud is null) BuiltIns.ThrowNullReference();
            Cloud.RosValidate();
            if (Polygons is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Polygons.Length; i++)
            {
                if (Polygons[i] is null) BuiltIns.ThrowNullReference(nameof(Polygons), i);
                Polygons[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += Cloud.RosMessageLength;
                size += WriteBuffer.GetArraySize(Polygons);
                return size;
            }
        }
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Cloud.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Polygons);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "pcl_msgs/PolygonMesh";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "45a5fc6ad2cde8489600a790acc9a38a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WUW/bNhB+nn4FUT80LmIPSbosCGAMxYIsAbo0WLK9FEVAiSeLGEW6JOVU/fX7jhQd" +
                "N8jDHtYZDmKd7j7efffdSTNxRxvpZSTRkVTkReu8iB2JjTPj2llpRBh8KxuqrrJD9qtm4i/yUTcUhGtT" +
                "RE+hEzIIiVhto2iMG1QVyAbnH/qwDj/esv1XNh9Pd2fivQ6REabzQlVgP356slWr//hT/X7327kIUeXE" +
                "rkpNd1FaJb1CMVEqGWXio9PrjvzC0JZAR5T9hpRId+O4obBE4H2ng8B3TZa8NGYUQ4BTdKJxfT9Y3TDH" +
                "UYOk/XhEasuMSS56MNLD33mlLbu3XvbE6PgG+jyQbUhcX5zDxwZqhqiR0AiExpMM2q5xU1QDSD455oBq" +
                "dv/oFrikNRq3OxzdkpGTpS8bT4HzlOEcZ7zJxS2BDXIIp6ggDpLtAZdhLnAIUqCNazpxgMxvx9g5m9q/" +
                "lV7L2hADN2AAqK856PV8D9kmaCutK/AZ8emMfwNrd7hc06JDzwxXH4Y1CITjxrutVnCtxwTSGE1QpNG1" +
                "l36sOCofWc0umWM4ISp1BP9lCK7RaIASjzp2VYie0VM3HrT6bmp8eVKKuNC8INeYU2cUD1njQEYTNXjC" +
                "+NwsFIqyQaeRTQMYDsVjp9GoXo4AAd1RstiU0jG7aQt19zJhhKFJ02vZYhDLsgFeHA8FxWaJJAgoebQL" +
                "UyE6n9SDfGpo1o+iNq5GcAzCyNEN8KXQeF3vmjGlgn7sNserVhOKeiWk93JcVqlk2l8j+UQUImoSzq+l" +
                "1V+BeKwglx6sLIz+m+a4I454qRwMKAMTTWq+FLdPMGEvFkkjPEWHggzdqKFJqXKa6LeXKGATu6k7YcdT" +
                "iIB3fGLSoGsXrcGaiDl7FhiKy0E5edl8HnRI1B8KKDYL89mw8wAf8Mo5UYXsMF8+W724c3yBBPzQxMFT" +
                "YXGPrqW4bifhM3no1I6QQ6BwnjAC5yhl8qgVKoQXhxiya1y9AFo2SwYoVymYc7qYGp1hGkylJRNKqdoX" +
                "Qeg82JNeEjcsmmWVGnXJUsDuz5Koqto5I/DR4aHW2K5KSytm4jrszezuxi8lqZz3A5q0QSyeMruiytMJ" +
                "WdRjpLCL8O6x+D+PwK1v/c+QIJ/8w+T88Q/I+ZN4h36U6Uv3D7HevqbVdVDg32T25lNlKEtBJCSmD7Tj" +
                "B0Qk9tFbiT/rcPpWGq0mRfxvGyg14+UFxC3Mo70pO8jZohew4cep0Qjff+4XlLx5sqbOxPXN/RmXvxJH" +
                "k+XPybQSx08+R6fJcrLnw6aVePvkw72E5ac9HzatxOlkuXz/4R2bVuLnfcvpW1jOqrLtLU/j1JIbmcc5" +
                "abIIxrVtoJgdPuTfrXc9P1f89EbDVOQxnQ5KouB3Bg66KL/JDrxo8mYIhKbXbkvlnMYNNk6JXEGIvbSj" +
                "IEN92qDTMOXMvpMsNo3JmiivZt+8tuVhUsmeU8ZwbItn9Q8rSN4QYgoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
