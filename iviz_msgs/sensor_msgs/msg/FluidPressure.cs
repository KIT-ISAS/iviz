/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/FluidPressure")]
    public sealed class FluidPressure : IDeserializable<FluidPressure>, IMessage
    {
        // Single pressure reading.  This message is appropriate for measuring the
        // pressure inside of a fluid (air, water, etc).  This also includes
        // atmospheric or barometric pressure.
        // This message is not appropriate for force/pressure contact sensors.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp of the measurement
        // frame_id is the location of the pressure sensor
        [DataMember (Name = "fluid_pressure")] public double FluidPressure_ { get; set; } // Absolute pressure reading in Pascals.
        [DataMember (Name = "variance")] public double Variance { get; set; } // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public FluidPressure()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public FluidPressure(in StdMsgs.Header Header, double FluidPressure_, double Variance)
        {
            this.Header = Header;
            this.FluidPressure_ = FluidPressure_;
            this.Variance = Variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FluidPressure(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            FluidPressure_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FluidPressure(ref b);
        }
        
        FluidPressure IDeserializable<FluidPressure>.RosDeserialize(ref Buffer b)
        {
            return new FluidPressure(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(FluidPressure_);
            b.Serialize(Variance);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/FluidPressure";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "804dc5cea1c5306d6a2eb80b9833befe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TwY7TMBC9R+o/jNTDtoh2ESAOlTggrYA9ICHt3qupM00sHDvYky39e94k283CXjhg" +
                "pUpjzzy/eW+GlnTnYxOE+iylDFkoC9fY2hLdt75Qh21uhPCX+z6nPntWoWPKOGJkIJa0lYqWM4aPxddC" +
                "6UhMxzD4mlbs82s6IRUvUbe+4HMoCfEuDLUUA2HtUulbyd4RLjlwTp2ofV3gt5XF/c0uJn3BED8n10+0" +
                "XIrKTqlILCkXw/mKaiVTO73mtST1AFfueisDBT6WK51ErZ5F/rmWdMzcyR4lg5OlheRYfYoXmCc2Ewtw" +
                "OIbE+uH9pNT+6RxYnw4lhUFfugPF6DsXB/W2zxAeGLVHJzObN0bDR8gOCJWauMxRQ/wR0ykuqo//eS2q" +
                "b3dfdlS03nelKdeTyosK3aYca8411FSuWXn0qfUNDN8EeZBAo+hgOp7quReU+Gg3nkaiZA7hTENBkCa4" +
                "2nVD9FBZZtMu+ciEVEw9Z/VuCJwRnzI0HHvEvDJ0PEV+DmKq3N7srFOKuEE9CJ2tPaF7Md1vb6gaoOe7" +
                "t5ZQLe9PaWPyNuieuWO0ZTWy8ms0bpR9hzteTcVtgQ11BLfUhVbj3h6fZU24xAlJn1xLKzP5rC16xxpn" +
                "tO2AUQUwjA9AvbKkq/UzZKO9o8gxXeAnxPmOf4E1lAnXatq08CxY9WVoICACMWYPmPCaDucRxAWPsaDg" +
                "D5nzubKs6cpq+dk0RhCyRkds5ktJzoa0ppPXtioYb6BfJqdaVL8BMdvsj5gEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
