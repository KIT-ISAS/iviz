/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class MagneticField : IDeserializable<MagneticField>, IMessageRos2
    {
        // Measurement of the Magnetic Field vector at a specific location.
        //
        // If the covariance of the measurement is known, it should be filled in.
        // If all you know is the variance of each measurement, e.g. from the datasheet,
        // just put those along the diagonal.
        // A covariance matrix of all zeros will be interpreted as "covariance unknown",
        // and to use the data a covariance will have to be assumed or gotten from some
        // other source.
        /// <summary> Timestamp is the time the </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // field was measured
        // frame_id is the location and orientation
        // of the field measurement
        /// <summary> X, y, and z components of the </summary>
        [DataMember (Name = "magnetic_field")] public GeometryMsgs.Vector3 MagneticField_;
        // field vector in Tesla
        // If your sensor does not output 3 axes,
        // put NaNs in the components not reported.
        /// <summary> Row major about x, y, z axes </summary>
        [DataMember (Name = "magnetic_field_covariance")] public double[/*9*/] MagneticFieldCovariance;
        // 0 is interpreted as variance unknown
    
        /// Constructor for empty message.
        public MagneticField()
        {
            MagneticFieldCovariance = new double[9];
        }
        
        /// Explicit constructor.
        public MagneticField(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MagneticField_, double[] MagneticFieldCovariance)
        {
            this.Header = Header;
            this.MagneticField_ = MagneticField_;
            this.MagneticFieldCovariance = MagneticFieldCovariance;
        }
        
        /// Constructor with buffer.
        public MagneticField(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out MagneticField_);
            b.DeserializeStructArray(9, out MagneticFieldCovariance);
        }
        
        public MagneticField RosDeserialize(ref ReadBuffer2 b) => new MagneticField(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in MagneticField_);
            b.SerializeStructArray(MagneticFieldCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (MagneticFieldCovariance is null) BuiltIns.ThrowNullReference();
            if (MagneticFieldCovariance.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(MagneticFieldCovariance.Length, 9);
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, MagneticField_);
            WriteBuffer2.Advance(ref c, MagneticFieldCovariance, 9);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/MagneticField";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
