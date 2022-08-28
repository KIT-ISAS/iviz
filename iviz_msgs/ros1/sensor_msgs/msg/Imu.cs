/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Imu : IDeserializable<Imu>, IMessage
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
        // If you have no estimate for one of the data elements (e.g. your IMU doesn't produce an orientation 
        // estimate), please set element 0 of the associated covariance matrix to -1
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
    
        public Imu()
        {
            OrientationCovariance = new double[9];
            AngularVelocityCovariance = new double[9];
            LinearAccelerationCovariance = new double[9];
        }
        
        public Imu(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Orientation);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                OrientationCovariance = array;
            }
            b.Deserialize(out AngularVelocity);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                AngularVelocityCovariance = array;
            }
            b.Deserialize(out LinearAcceleration);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                LinearAccelerationCovariance = array;
            }
        }
        
        public Imu(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Orientation);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                OrientationCovariance = array;
            }
            b.Deserialize(out AngularVelocity);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                AngularVelocityCovariance = array;
            }
            b.Deserialize(out LinearAcceleration);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                LinearAccelerationCovariance = array;
            }
        }
        
        public Imu RosDeserialize(ref ReadBuffer b) => new Imu(ref b);
        
        public Imu RosDeserialize(ref ReadBuffer2 b) => new Imu(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Orientation);
            b.SerializeStructArray(OrientationCovariance, 9);
            b.Serialize(in AngularVelocity);
            b.SerializeStructArray(AngularVelocityCovariance, 9);
            b.Serialize(in LinearAcceleration);
            b.SerializeStructArray(LinearAccelerationCovariance, 9);
        }
        
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
    
        public int RosMessageLength => 296 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 32; // Orientation
            c += 8 * 9; // OrientationCovariance
            c += 24; // AngularVelocity
            c += 8 * 9; // AngularVelocityCovariance
            c += 24; // LinearAcceleration
            c += 8 * 9; // LinearAccelerationCovariance
            return c;
        }
    
        public const string MessageType = "sensor_msgs/Imu";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6a62c6daae103f4ff57a132d6f95cec2";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71W227bRhB951cMogdbhSTXThGgBvpQIGirBwNpLn0JEmFEjsiNyV1ld2lZ/vqe2RUl" +
                "2knQAr0IAkSRO+fMnLlxQm8bEwhfpk5C4FooOmpcW1HFkWnjXUdsaXnzjs6XVnw03NKNcOi9dGIjvbMm" +
                "TotJMaGfy1Ja8RyNs4FC43qgrIWMpe4ifLyic+ui/qvPwnQG1Iq8i+k4MO+kdaWJ+8eGnquLIGXCX24o" +
                "NkKlu2Nv2JZCLt/pRu4glFvrdnZGJo6gNqZtpVLEc7Mhblvauz6dVAsFAcEYV7hsxsAzkkW9yHrocVUn" +
                "NCJ48KkPkbZ9xH0XBODO1vmM4Vpjm6o4Y787jt7cK4168iDeBdrBwxx0FL/1EuEuB3o2MuttCu1Z1g55" +
                "6kEHIsCnZPGYI+E1fJcSClwOoe+A6TzVLkaxOZbgOsQLEI/L3pcySK36JHPrSEI08BkywtrZo/CJFTlX" +
                "gQKdJ4Vg51O5VE6CPYMy3lU9HEIVOW9wMmVc9R5gUQzbFlILBYkDHn0/sMBzVAarIF9qiODmlyeP2Y8U" +
                "NCkPyO+htI80ZSPlbQqGkfS2TwHNL7U8lHBjfDj5MRQDSL6gz5moTPBSs6+eujsEuCiK34QraNykn6Ko" +
                "BbpHv191oQ4Xv/c45K3KMpKo2LSO44sf3v/4YXx7NfJiQq9RwR1/0lDWDjUIl/YzeiC+l/CU5g8po/PP" +
                "4XPdt+xXQ8uNiZ4+++dsrbECQB5NhzHhVx5/i1PZQFYUP/3Ln+Lmza/XFGKVPc+5Qr7fRKRX84qwOM9D" +
                "uNGYGu0ybwUawYi7rRymZdxvJSxgOEzVWjAy0eN7bdXUsqXrut6aUrsJxSGP7GGJEmTaMuZsqXnAeecr" +
                "Y1Pzee5E0fEN8rkX1Wf58hpnLGZkHw0c2gOh9KhyLf7lSyp6tMPzKzWAlu9fu3D5oZi83bm5tkmNkjx6" +
                "gerlNEDlHt0TQhpA1yD7Lke5AAlUEtBV6PZ0b4W/YUpggy+ydeiTc4Twao9ZmJspZXLdigKXnKbwmRqd" +
                "TUfINkFbtm6Az4gnjr8Da4+4GtO8QfJalSH0aE49iFF0ZyocXe/zKmm1q1CCa89+X6hVpiwmv6jYeXqk" +
                "1OiKPDX2zsSmCJgAQE9pWZnqvyrLb42Kocy8aLbSBH4yYo3OeEFIWy7TNv18mjMo5A5j6dCIdH+8Ok4D" +
                "ejhe7f6f2A4T42uB4fVAnz0OaaGdsEwl6ywqH+sa2USTHS1hWBkPU4S8AKp4QeCSXg50P2G5RWB0fKu7" +
                "x4a0LHm7BRi62bMNbZYSt2GSNtyMdg22Zzql+U9tmxrdlORNbapsqQofjZkOwc0obq7yck4+Z7K8xIe3" +
                "oeliWGc7DSht1Dxf0iYf/ErlH52bjd4DHgv6yqHJjy92xoaIyfaXWf8TMuF3bBUKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
