/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class AccelStamped : IHasSerializer<AccelStamped>, IMessage
    {
        // An accel with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public Accel Accel;
    
        public AccelStamped()
        {
            Accel = new Accel();
        }
        
        public AccelStamped(in StdMsgs.Header Header, Accel Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        public AccelStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new Accel(ref b);
        }
        
        public AccelStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new Accel(ref b);
        }
        
        public AccelStamped RosDeserialize(ref ReadBuffer b) => new AccelStamped(ref b);
        
        public AccelStamped RosDeserialize(ref ReadBuffer2 b) => new AccelStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Accel is null) BuiltIns.ThrowNullReference();
            Accel.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 48;
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
            size += 48; // Accel
            return size;
        }
    
        public const string MessageType = "geometry_msgs/AccelStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d8a98a5d81351b6eb0578c78557e7659";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTW/UMBC9+1eMtIe2aBtEizisxKESAnpAqmjFBaFq1p4kVhM72M5uw6/n2dlNqbhw" +
                "AFaRNh8zb+a9eeMVXTliraWjvU0tBakliNNC2vtgrOMkVAfuhdgZSraXmLgf1EdhI4Ha8qeuCkLBUert" +
                "X/6pT7cfNhSTue9jE1/OldWKbhNa4mCol8SGE1Pt0ZFtWgnnnezQUelVDJWvaRokVki8a20kXI04Cdx1" +
                "E40RQcmDdN+PzurMeuF6zEemhVg0cEhWjx2H30TK6LiifB+LiNfvNohxUfSYLBqagKCDcLSuwUdSo3Xp" +
                "8iIn0Iq+fvbx1Te1utv7c7yXBgIvXVBqOeWu5XEIEnPDHDco9mJmWaEIVBKUM5FOy7t7PMYzQjX0IoPX" +
                "LZ2Cws2UWu8AKLTjYHnbSQbWkAKoJznp5OwXZFegHTt/hJ8Rn2r8CaxbcDOn8xbD67IMcWygJAKH4HfW" +
                "IHQ7FRDdWXGJOrsNHCaVs+aSavW+ODLlOZbR4J9j9NpiEqY4WcUUMnoZy701/8qWjXjYL0yzN8seHB12" +
                "HFScFwNWSxb6QKk6CKgMDA23wT9Ifgn32RTB1gnkyMvGrikmy36Db7+ITj5c0iHk6fkQ938YHqoeOQbJ" +
                "HDEmkKRd+facYJX34boY1zv4vxfGTEF2yUSisQGpEKcCKs4f7LGsIQcZD/WcT8Do+QGQAhflbB4GgGGn" +
                "A7vYzcIWBelUqqZa076FqiUqu6Asb1l3qynYxpo5E4X6JZnpQG5Nqb6Ai7pu7nkuBksCJPhUEs4quq5p" +
                "8iPtMyHchMMp42krS19lCZL363zEHCCeC3rjMXvIEiM32BcXE863Sqm685zevKbH5W5a7n6on24NfrG4" +
                "BQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<AccelStamped> CreateSerializer() => new Serializer();
        public Deserializer<AccelStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<AccelStamped>
        {
            public override void RosSerialize(AccelStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(AccelStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(AccelStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(AccelStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(AccelStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<AccelStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out AccelStamped msg) => msg = new AccelStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out AccelStamped msg) => msg = new AccelStamped(ref b);
        }
    }
}
