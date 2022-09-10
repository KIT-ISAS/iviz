/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class FluidPressure : IHasSerializer<FluidPressure>, IMessage
    {
        // Single pressure reading.  This message is appropriate for measuring the
        // pressure inside of a fluid (air, water, etc).  This also includes
        // atmospheric or barometric pressure.
        // This message is not appropriate for force/pressure contact sensors.
        /// <summary> Timestamp of the measurement </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is the location of the pressure sensor
        /// <summary> Absolute pressure reading in Pascals. </summary>
        [DataMember (Name = "fluid_pressure")] public double FluidPressure_;
        /// <summary> 0 is interpreted as variance unknown </summary>
        [DataMember (Name = "variance")] public double Variance;
    
        public FluidPressure()
        {
        }
        
        public FluidPressure(in StdMsgs.Header Header, double FluidPressure_, double Variance)
        {
            this.Header = Header;
            this.FluidPressure_ = FluidPressure_;
            this.Variance = Variance;
        }
        
        public FluidPressure(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out FluidPressure_);
            b.Deserialize(out Variance);
        }
        
        public FluidPressure(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out FluidPressure_);
            b.Deserialize(out Variance);
        }
        
        public FluidPressure RosDeserialize(ref ReadBuffer b) => new FluidPressure(ref b);
        
        public FluidPressure RosDeserialize(ref ReadBuffer2 b) => new FluidPressure(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(FluidPressure_);
            b.Serialize(Variance);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(FluidPressure_);
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
            size += 8; // FluidPressure_
            size += 8; // Variance
            return size;
        }
    
        public const string MessageType = "sensor_msgs/FluidPressure";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "804dc5cea1c5306d6a2eb80b9833befe";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61UTW/TQBC9+1eMlENbRFO+xCESB6QK6AEJ0d4Qiibrib1ivWt2xg3597xdN0lFhcSB" +
                "lSPH3pk3b96bNS3o1scuCI1ZVKcslIVbvFoS3fVeacBr7oTwl8cxpzF7NqFtythiZCCWrJeGFicMH9W3" +
                "QmlLTNsw+ZbO2efntEMqbmLu4oDPQRPiXZha0QLCNiQde8neEYpsOKdBrDwd4JdNifuTXUz2hCF+Tq6O" +
                "tFyKxs5IJWrKWnA+oVvJ1M+301qQeYAbD2NpAw0+tCuDRGvob2tB28yDrNEyOJW0kBybT/EAc2QzswCH" +
                "bUhsb9/MSq2P+8B6v9EUJnvqDhSjL6wO6i0fIdwzeo9OTmxeFBo+QnZAmLTEeoqa4o+YdrF5959X8/n2" +
                "44rU2vWgnV7NGjcYNePYcm4hpXHLxtWk3ndw+zLIvQSqioNm3bX9KOjvwWtcnUTJHMKeJkWQJVg6DFP0" +
                "rjh+dOyQj0zoxDRyNu+mwBnxKUPAOiDFqIKOS+XnJEWSm+tVGRMVN5kHoX2ZTYiuRfSba2omiPn6VUmA" +
                "ut++Jn35vVnc7dJlEbnDDJ3mxnq2wlp+Vfuq+CsUezZ3uUQRqCQo1yqd13drPOoFoRq4yJhcT+fF6r31" +
                "mKAyPtW8TagjD/sDUM9K0tnFI+RYoSPHdICfEU81/gU2HnFLT5c9zAtFBp06KIlAHLZ7nPOWNvsK4oLH" +
                "4aDgN5nzvilZc8lm8aGIjSBkVWvKyVdNrhzVlnbe+katfksO56dpfgNJCWrYnQQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FluidPressure> CreateSerializer() => new Serializer();
        public Deserializer<FluidPressure> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FluidPressure>
        {
            public override void RosSerialize(FluidPressure msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FluidPressure msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FluidPressure msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FluidPressure msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(FluidPressure msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<FluidPressure>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FluidPressure msg) => msg = new FluidPressure(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FluidPressure msg) => msg = new FluidPressure(ref b);
        }
    }
}
