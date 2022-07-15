/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class Imu : IDeserializable<Imu>, IMessageRos2
    {
        // This is a message to hold data from an IMU (Inertial Measurement Unit)
        //
        // Accelerations should be in m/s^2 (not in g's), and rotational velocity should be in rad/sec
        //
        // If the covariance of the measurement is known, it should be filled in (if all you know is the
        // variance of each measurement, e.g. from the datasheet, just put those along the diagonal)
        // A covariance matrix of all zeros will be interpreted as "covariance unknown", and to use the
        // data a covariance will have to be assumed or gotten from some other source
        //
        // If you have no estimate for one of the data elements (e.g. your IMU doesn't produce an
        // orientation estimate), please set element 0 of the associated covariance matrix to -1
        // If you are interpreting this message, please check for a value of -1 in the first element of each
        // covariance matrix, and disregard the associated estimate.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        /// <summary> Row major about x, y, z axes </summary>
        [DataMember (Name = "orientation_covariance")] public double[/*9*/] OrientationCovariance;
        [DataMember (Name = "angular_velocity")] public GeometryMsgs.Vector3 AngularVelocity;
        /// <summary> Row major about x, y, z axes </summary>
        [DataMember (Name = "angular_velocity_covariance")] public double[/*9*/] AngularVelocityCovariance;
        [DataMember (Name = "linear_acceleration")] public GeometryMsgs.Vector3 LinearAcceleration;
        /// <summary> Row major x, y z </summary>
        [DataMember (Name = "linear_acceleration_covariance")] public double[/*9*/] LinearAccelerationCovariance;
    
        /// Constructor for empty message.
        public Imu()
        {
            OrientationCovariance = new double[9];
            AngularVelocityCovariance = new double[9];
            LinearAccelerationCovariance = new double[9];
        }
        
        /// Constructor with buffer.
        public Imu(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Orientation);
            b.DeserializeStructArray(9, out OrientationCovariance);
            b.Deserialize(out AngularVelocity);
            b.DeserializeStructArray(9, out AngularVelocityCovariance);
            b.Deserialize(out LinearAcceleration);
            b.DeserializeStructArray(9, out LinearAccelerationCovariance);
        }
        
        public Imu RosDeserialize(ref ReadBuffer2 b) => new Imu(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Orientation);
            b.SerializeStructArray(OrientationCovariance, 9);
            b.Serialize(in AngularVelocity);
            b.SerializeStructArray(AngularVelocityCovariance, 9);
            b.Serialize(in LinearAcceleration);
            b.SerializeStructArray(LinearAccelerationCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (OrientationCovariance is null) BuiltIns.ThrowNullReference();
            if (OrientationCovariance.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(OrientationCovariance.Length, 9);
            if (AngularVelocityCovariance is null) BuiltIns.ThrowNullReference();
            if (AngularVelocityCovariance.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(AngularVelocityCovariance.Length, 9);
            if (LinearAccelerationCovariance is null) BuiltIns.ThrowNullReference();
            if (LinearAccelerationCovariance.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(LinearAccelerationCovariance.Length, 9);
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, Orientation);
            WriteBuffer2.Advance(ref c, OrientationCovariance, 9);
            WriteBuffer2.Advance(ref c, AngularVelocity);
            WriteBuffer2.Advance(ref c, AngularVelocityCovariance, 9);
            WriteBuffer2.Advance(ref c, LinearAcceleration);
            WriteBuffer2.Advance(ref c, LinearAccelerationCovariance, 9);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Imu";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
