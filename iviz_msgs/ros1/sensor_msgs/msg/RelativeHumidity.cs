/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class RelativeHumidity : IHasSerializer<RelativeHumidity>, IMessage
    {
        // Single reading from a relative humidity sensor.  Defines the ratio of partial
        // pressure of water vapor to the saturated vapor pressure at a temperature.
        /// <summary> Timestamp of the measurement </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location of the humidity sensor
        /// <summary> Expression of the relative humidity </summary>
        [DataMember (Name = "relative_humidity")] public double RelativeHumidity_;
        // from 0.0 to 1.0.
        // 0.0 is no partial pressure of water vapor
        // 1.0 represents partial pressure of saturation
        /// <summary> 0 is interpreted as variance unknown </summary>
        [DataMember (Name = "variance")] public double Variance;
    
        public RelativeHumidity()
        {
        }
        
        public RelativeHumidity(in StdMsgs.Header Header, double RelativeHumidity_, double Variance)
        {
            this.Header = Header;
            this.RelativeHumidity_ = RelativeHumidity_;
            this.Variance = Variance;
        }
        
        public RelativeHumidity(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out RelativeHumidity_);
            b.Deserialize(out Variance);
        }
        
        public RelativeHumidity(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align8();
            b.Deserialize(out RelativeHumidity_);
            b.Deserialize(out Variance);
        }
        
        public RelativeHumidity RosDeserialize(ref ReadBuffer b) => new RelativeHumidity(ref b);
        
        public RelativeHumidity RosDeserialize(ref ReadBuffer2 b) => new RelativeHumidity(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RelativeHumidity_);
            b.Serialize(Variance);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RelativeHumidity_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 16;
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
            size += 8; // RelativeHumidity_
            size += 8; // Variance
            return size;
        }
    
        public const string MessageType = "sensor_msgs/RelativeHumidity";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8730015b05955b7e992ce29a2678d90f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61UTW/UMBC9+1eMlENbpA0tIA6VuC0fPSAh2htCq9lkdmPh2MF2dtl/zxun+1GghQPW" +
                "Skm8b97MvHk2VXRr/doJReEWb7SKoSfGp+NsN0Ld2NvW5h0l8SnEmmguK+slUe4QBVCgsKKBY7bsDFU0" +
                "RElpjKLbW84SacNDiJRDCUmcR4RJe799gHNG3iz9IFEhUhtDH1AVCLrpcboqyraXlLkfNJEy98JK1IvP" +
                "hh5fFXrkXha2JTt14UKjffg90S89o46VC5xfvzrIsjhAKnr7o7RwEv+beH8rB5Jf1peq0FV9WT+NViDq" +
                "9mGv+WOCP02DRKhTQyFX+iPX/aTQ2IkCG46WfSMPStKCrEdmhOtkOR1xo//mw9abN/95mY+3768p5XbR" +
                "p3V6PjnFwM+ZfcuxhR0yt5yZVnBZZ9edxJmTjTgqrkGZ5d+8GyTVCLzrtItEa/FwoHM7GhNAGEoT+n70" +
                "FiaRo+v28Yi0Hs4tCjaj4wh8iDhMCi9WU3b8knwfRSW5mV8D45M0o9oEmaxvcACTHsCbOZkRYr58oQFQ" +
                "98vnkK6+mupuG2Yq8hoDPno/dzg3qFomFxbxr5Hs2dRljSRQSZCuTXRe9hb4TBeEbKhFhtB0dI4WPu1y" +
                "Bw+rgcvwlrgVQNxACrCeadDZxQmzL9SefdjTT4zHHP9C6w+82tOsw/CcypDGNZQEcIhhY1tAl7tC0jgL" +
                "x5Kzy8hxZzRqSmmqdyo2QIgqo8GTUwqNLdfN1ubOpBynW266AYz5CbcRowwCBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<RelativeHumidity> CreateSerializer() => new Serializer();
        public Deserializer<RelativeHumidity> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<RelativeHumidity>
        {
            public override void RosSerialize(RelativeHumidity msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(RelativeHumidity msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(RelativeHumidity msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(RelativeHumidity msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(RelativeHumidity msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<RelativeHumidity>
        {
            public override void RosDeserialize(ref ReadBuffer b, out RelativeHumidity msg) => msg = new RelativeHumidity(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out RelativeHumidity msg) => msg = new RelativeHumidity(ref b);
        }
    }
}
