/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FluidPressure : IDeserializable<FluidPressure>, IMessage
    {
        // Single pressure reading.  This message is appropriate for measuring the
        // pressure inside of a fluid (air, water, etc).  This also includes
        // atmospheric or barometric pressure.
        // This message is not appropriate for force/pressure contact sensors.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp of the measurement
        // frame_id is the location of the pressure sensor
        [DataMember (Name = "fluid_pressure")] public double FluidPressure_; // Absolute pressure reading in Pascals.
        [DataMember (Name = "variance")] public double Variance; // 0 is interpreted as variance unknown
    
        /// Constructor for empty message.
        public FluidPressure()
        {
        }
        
        /// Explicit constructor.
        public FluidPressure(in StdMsgs.Header Header, double FluidPressure_, double Variance)
        {
            this.Header = Header;
            this.FluidPressure_ = FluidPressure_;
            this.Variance = Variance;
        }
        
        /// Constructor with buffer.
        internal FluidPressure(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            FluidPressure_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new FluidPressure(ref b);
        
        FluidPressure IDeserializable<FluidPressure>.RosDeserialize(ref Buffer b) => new FluidPressure(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(FluidPressure_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/FluidPressure";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "804dc5cea1c5306d6a2eb80b9833befe";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TwW7bMAy96ysI5NB2WNJhG3YIsMOAYlsPAwa094CRGVuYLHkS3Sx/v0e7qbv1ssME" +
                "B44l8unxPZJWdBdSG4WGIrWORagIN9jaEN13oVKPbW6F8JeHoeShBFahQy44YmQglrQTR6sFI6QaGqF8" +
                "IKZDHENDlxzKazoiFS9Rf3XG51gz4n0cG6kGwtrnOnRSgidcsueSe1H7OsNvnMX9zS5lfcEQPy/XT7R8" +
                "TspeqUqquVTD+YpqpVA3v5a1Ig0AV+4HKwMFPpYrvSR1zyL/XCs6FO5lh5LBydJi9qwhpzPME5uZBTgc" +
                "Ymb98H5Wavd0DqxP+5rjqC/dgWL0nauHeptnCA+M2pOXhc0boxESZAeESkNcl6gx/Uj5mNzH/7zct7sv" +
                "W6ra7Pra1utZY4dWU04NlwZSKjesPJnUhRZur6M8SKRJcdCcTvU0COp79BpPK0kKx3iisSJIMyzt+zEF" +
                "SCyLY+d8ZEInpoGLBj9GLojPBQJODWJGGTqeKj9HMUlub7bWJlX8qAGETtabEL2a6Lc35EaI+e6tJbjV" +
                "/TGvTdsWrbO0i3asRlZ+Ta5Nmm9xx6u5uA2wIY7glqbS5bS3w2e9IlwCCjJk39GlOXzSDo1jXTN5tsec" +
                "AhiuR6BeWNLF1TNko72lxCmf4WfE5Y5/gTWUGddqWnfwLFr1dWwhIAIxYw8Y74b2pwnEx4CZoBj2hcvJ" +
                "WdZ8pVt9No0RhKzJERv4WrO3CW3oGLRzFbMN9PPYOPcb1lVOYZQEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
