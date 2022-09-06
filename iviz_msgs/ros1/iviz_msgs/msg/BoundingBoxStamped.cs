/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class BoundingBoxStamped : IDeserializable<BoundingBoxStamped>, IHasSerializer<BoundingBoxStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "boundary")] public BoundingBox Boundary;
    
        public BoundingBoxStamped()
        {
            Boundary = new BoundingBox();
        }
        
        public BoundingBoxStamped(in StdMsgs.Header Header, BoundingBox Boundary)
        {
            this.Header = Header;
            this.Boundary = Boundary;
        }
        
        public BoundingBoxStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Boundary = new BoundingBox(ref b);
        }
        
        public BoundingBoxStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Boundary = new BoundingBox(ref b);
        }
        
        public BoundingBoxStamped RosDeserialize(ref ReadBuffer b) => new BoundingBoxStamped(ref b);
        
        public BoundingBoxStamped RosDeserialize(ref ReadBuffer2 b) => new BoundingBoxStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Boundary.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Boundary.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Boundary is null) BuiltIns.ThrowNullReference();
            Boundary.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 80;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 80; // Boundary
            return size;
        }
    
        public const string MessageType = "iviz_msgs/BoundingBoxStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "da26f418902c5d679a0e80380a951267";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VwW7bMAy9+ysI5NB2SD2sHXYosMOKYWsPA7qt2GUYCkZibGG25EpyUvfr9yQnToMV" +
                "6A5rAwORLfKRfHyiLoS1eKrzX3HuequNrc7dHS3Smv1QFO//86/48v3zGYWob9pQhdcXY+wZfY+cImpq" +
                "JbLmyLR0SM1UtfjjRlbSwInbTjTl3Th0Eko4XtcmEJ5KrHhumoH6AKPoSLm27a1RHIWiaWXPH57GElPH" +
                "PhrVN+xh7zwISOZLz60kdDxBbnuxSujy4xlsbBDVR4OEBiAoLxxAGjap6I2NpyfJgWb085sLb34Vs+u1" +
                "O8Z3qcD0lAXFmmPKWu46LyElzOEMwV6NVZYIApYE4XSgw/ztBq/hiBANuUjnVE2HKOFqiLWzABRasTe8" +
                "aCQBK1AB1IPkdHD0ANlmaMvWbeFHxF2Mf4G1E26q6bhG85pEQ+grMAnDzruV0TBdDBlENUZspMYsfNJV" +
                "8hpDFrNPiWwYwSu3Bv8cglMGndC0NrEuQvQJPbflxujnkqVZmftRlw8OQ1GJgyj9MO5cgTdSqAWy3d/5" +
                "ISo6fwo+7+W5Mvw7F7TgA3lJMkJSHA265pbUpTTRx6UXEN2xknk6EOmz3uybbIvOkfNm61tSceWg18mg" +
                "+NqjD95m3J3dSxWIVLaHHGqNbGzIepryRy04xTnlvXKLZeM4vntLd9NqmFb3L5P+jrptDVOjoPE9PveT" +
                "T2+3O94xCtuyeKKi7Wr9MrVt1P5YYbTKe/sllWmWXuah5yxmZyuMlmFMT55w1MbDNcvwGoNfUDh0ayJp" +
                "J4GsS1po+TcgBRMoeXPXAQz3gWcbmpFKfIbLoZRVOad1LXa0ShMkD/58VRhF3lRGj56J4cmZaVPcnOLy" +
                "BBOoacacx2CQH0C8Gxt3VNLlkgbX0zoVhIXf3FCOFjLllQdodG6erqcNxCNaBy0hcJUEECLuxie7/gdj" +
                "4MDzxAcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<BoundingBoxStamped> CreateSerializer() => new Serializer();
        public Deserializer<BoundingBoxStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<BoundingBoxStamped>
        {
            public override void RosSerialize(BoundingBoxStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(BoundingBoxStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(BoundingBoxStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(BoundingBoxStamped msg) => msg.Ros2MessageLength;
            public override void RosValidate(BoundingBoxStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<BoundingBoxStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out BoundingBoxStamped msg) => msg = new BoundingBoxStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out BoundingBoxStamped msg) => msg = new BoundingBoxStamped(ref b);
        }
    }
}
