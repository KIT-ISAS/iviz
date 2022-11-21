/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class Polygon : IHasSerializer<Polygon>, IMessage
    {
        //A specification of a polygon where the first and last points are assumed to be connected
        [DataMember (Name = "points")] public Point32[] Points;
    
        public Polygon()
        {
            Points = EmptyArray<Point32>.Value;
        }
        
        public Polygon(Point32[] Points)
        {
            this.Points = Points;
        }
        
        public Polygon(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Point32[] array;
                if (n == 0) array = EmptyArray<Point32>.Value;
                else
                {
                    array = new Point32[n];
                    b.DeserializeStructArray(array);
                }
                Points = array;
            }
        }
        
        public Polygon(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Point32[] array;
                if (n == 0) array = EmptyArray<Point32>.Value;
                else
                {
                    array = new Point32[n];
                    b.DeserializeStructArray(array);
                }
                Points = array;
            }
        }
        
        public Polygon RosDeserialize(ref ReadBuffer b) => new Polygon(ref b);
        
        public Polygon RosDeserialize(ref ReadBuffer2 b) => new Polygon(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Points.Length);
            b.SerializeStructArray(Points);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Points.Length);
            b.SerializeStructArray(Points);
        }
        
        public void RosValidate()
        {
            if (Points is null) BuiltIns.ThrowNullReference(nameof(Points));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 12 * Points.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Points.Length
            size += 12 * Points.Length;
            return size;
        }
    
        public const string MessageType = "geometry_msgs/Polygon";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "cd60a26494a087f577976f0329fa120e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61RzUrEMBC+5ykG9qIgK+zeBA/iQTwIgt5EJG2m7WCaCZlZ1/r0Ttru4gPY0zSZ7zeb" +
                "O5CMLXXUeiVOwB14yByn3n6OAxYEHRA6KqLgU4DobchMSQW83XqRw4gBlKFBaDklbBWDe64r+93b+7rs" +
                "3O0/f+7p5eEGeuQRtUwfo/Ryvaq6DbwOJNWOekoyZ8gs9DejbQIl6AqileBbvDiSDrDfQUMWzrZysWrE" +
                "IJdbY3y0dQE74tECL5EPgjBrLl19YakyQk1E4xZFHyrRamsLYDwncytTCkvzdmKEufDIWsGKhTMW31Ak" +
                "nWboCTmiiO+xQgIK9Wkxo/4T4ZAh2vWSqLpKIKZBqTd05DXY+n4KnFq8skesTdSSWm+J5oJmz/eRD6Fq" +
                "uy6ytwjwfZ6m8/TjfgGp0g7/SAIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Polygon> CreateSerializer() => new Serializer();
        public Deserializer<Polygon> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Polygon>
        {
            public override void RosSerialize(Polygon msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Polygon msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Polygon msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Polygon msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Polygon msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Polygon>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Polygon msg) => msg = new Polygon(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Polygon msg) => msg = new Polygon(ref b);
        }
    }
}
