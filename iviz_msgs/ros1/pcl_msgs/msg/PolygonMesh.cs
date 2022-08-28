/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class PolygonMesh : IDeserializable<PolygonMesh>, IMessage
    {
        // Separate header for the polygonal surface
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Vertices of the mesh as a point cloud
        [DataMember (Name = "cloud")] public SensorMsgs.PointCloud2 Cloud;
        // List of polygons
        [DataMember (Name = "polygons")] public Vertices[] Polygons;
    
        public PolygonMesh()
        {
            Cloud = new SensorMsgs.PointCloud2();
            Polygons = EmptyArray<Vertices>.Value;
        }
        
        public PolygonMesh(in StdMsgs.Header Header, SensorMsgs.PointCloud2 Cloud, Vertices[] Polygons)
        {
            this.Header = Header;
            this.Cloud = Cloud;
            this.Polygons = Polygons;
        }
        
        public PolygonMesh(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Cloud = new SensorMsgs.PointCloud2(ref b);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Vertices>.Value
                    : new Vertices[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Vertices(ref b);
                }
                Polygons = array;
            }
        }
        
        public PolygonMesh(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Cloud = new SensorMsgs.PointCloud2(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Vertices>.Value
                    : new Vertices[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Vertices(ref b);
                }
                Polygons = array;
            }
        }
        
        public PolygonMesh RosDeserialize(ref ReadBuffer b) => new PolygonMesh(ref b);
        
        public PolygonMesh RosDeserialize(ref ReadBuffer2 b) => new PolygonMesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Cloud.RosSerialize(ref b);
            b.Serialize(Polygons.Length);
            foreach (var t in Polygons)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Cloud.RosSerialize(ref b);
            b.Serialize(Polygons.Length);
            foreach (var t in Polygons)
            {
                t.RosSerialize(ref b);
            }
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Cloud.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Polygons.Length
            foreach (var t in Polygons)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "pcl_msgs/PolygonMesh";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "45a5fc6ad2cde8489600a790acc9a38a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WUU8jNxB+7v4K6/Jw5ERSAVeKkKLqVERBunLooH1BCHntSdbqrp2zvUn3fn2/sdch" +
                "h3joQ69REMl45vPMN99MdiLuaC29jCQakpq8WDovYkNi7dph5axsRej9UiqqrrJD9qsm4k/y0SgKwi1T" +
                "REehETIIiVhjo1Ct63UVyAbnn7qwCj/esv1XNh+PpxPx0YTICON9oSqwD4/PtmrxH7+q3+9+Oxch6pzY" +
                "VanpLkqrpdcoJkoto0x8NGbVkJ+1tCHQEWW3Ji3SaRzWFOYIvG9MEHivyJKXbTuIPsApOqFc1/XWKOY4" +
                "GpC0H49IY5kxyUX3rfTwd14by+5LLztidLwDfenJKhLXF+fwsYFUHw0SGoCgPMlg7AqHoupB8skxB4iJ" +
                "ePjswtFjNbnfuhnstEIHd1mgbTJy1vT32lPghGU4x2XvcpVzXAKWCNfpIA6S7Qlfw1TgNuRCa6cacYAS" +
                "bofYOJt0sJHeyLolBlagAqhvOejtdA/ZJmgrrSvwGfH5jn8Da3e4XNOsQfNapiH0KzAJx7V3G6PhWg8J" +
                "RLWGIM3W1F76oeKofGU1uWSy4YSo1Br8lyE4ZdAJLbYmNlWIntFTW56M/m6yfH1kisrQvCBXGFjXap42" +
                "5UCGigY8YY5uZhpF2WDS7KZJDIdi2xg0qpMDQEB3lKw6rU3MbsZC5p1MGKFXaYwtW1rEsmyAF4dDQVHN" +
                "kQQBJc94YSpE55N6kE8N8fpB1K2rERyDaOXgevhSUN7Uu2aMqaAfuxXyZmkIRb0R0ns5zKtUMu3vk3wj" +
                "ChE1CedX0pqvQDzWkEsHVmat+YumOBFHvF0OepSB0SY9nYvbZ5iwF4ukEZ6iQ0GGbnSvUqqcJvrtJQpY" +
                "x2bsTtjxFCLgHd+YNOiWs2WLfRFz9iwwFJeDcvJSfelNSNQfCig2C/PF1PMkH/DuOdGF7DCdv9jBODm+" +
                "QAK+V7H3VFjco2surpej8Jk8dGpHyCFQOE8YgXOUMtkajQrhxSEt2RW+vQJaVkwGKN9SMOd0MTY6wyhM" +
                "paU2lFKNL4IwebBHvSRuWDTzKjXqkqWAH4EsiaqqnWsFXiY81QZrVhtpseCuw97M7g5+KUnlvJ/QpDVi" +
                "8XOzK6r8TCGLeogUdhHebYv/ywgcfet/hgT55h9G54fPkPOj+IB+lOlL54dYb1/T6joo8O8ye9OxMpSl" +
                "IRIS4wva8T0iEvvorcSfdbh9I1ujR0X8bxsoNeP1BcQtzKO9LjvI2aIXsOGHsdEI338AKCh582RNnYnr" +
                "m/szLn8hjkbLH6NpIY6ffY5Ok+Vkz4dNC/H+2Yd7CctPez5sWojT0XL58dMHNi3Ez/uW0/ewnFVl21ue" +
                "xrElNzKPc9JkEYxbLgPF7PApf1561/Hvih8fbZiKPKbjRUkU/PDAQRflM9meF03eDIHQ9NptqNyjXG/j" +
                "mMgVhNhJOwhqqUsbdBymnNl3ksVatVkT5Rntm+e3PEw62XPKGI5N8az+AYFYBghrCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
