/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class MagneticField : IDeserializable<MagneticField>, IHasSerializer<MagneticField>, IMessage
    {
        // Measurement of the Magnetic Field vector at a specific location.
        // If the covariance of the measurement is known, it should be filled in
        // (if all you know is the variance of each measurement, e.g. from the datasheet,
        //just put those along the diagonal)
        // A covariance matrix of all zeros will be interpreted as "covariance unknown",
        // and to use the data a covariance will have to be assumed or gotten from some
        // other source
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
    
        public MagneticField()
        {
            MagneticFieldCovariance = new double[9];
        }
        
        public MagneticField(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MagneticField_, double[] MagneticFieldCovariance)
        {
            this.Header = Header;
            this.MagneticField_ = MagneticField_;
            this.MagneticFieldCovariance = MagneticFieldCovariance;
        }
        
        public MagneticField(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out MagneticField_);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                MagneticFieldCovariance = array;
            }
        }
        
        public MagneticField(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out MagneticField_);
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                MagneticFieldCovariance = array;
            }
        }
        
        public MagneticField RosDeserialize(ref ReadBuffer b) => new MagneticField(ref b);
        
        public MagneticField RosDeserialize(ref ReadBuffer2 b) => new MagneticField(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in MagneticField_);
            b.SerializeStructArray(MagneticFieldCovariance, 9);
        }
        
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 96;
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
            size += 24; // MagneticField_
            size += 8 * 9; // MagneticFieldCovariance
            return size;
        }
    
        public const string MessageType = "sensor_msgs/MagneticField";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2f3b0b43eed0c9501de0fa3ff89a45aa";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWUWvbSBB+168YqocmxfHdNaVwgT4USu/ykFLacC+lmLE0kraVdnW7K9vKr79vVpYj" +
                "Qgs+uDMB2/LMNzPffvNtKKc74TB46cRGchXFRuiOayvRFPTeSFvSToroPHEkptBLYSr81LqCo3F2nWWU" +
                "0+2UWLgde8O2kBmqW6CbQN+t29sVmUihcQOwt0KVaVspyVgFujAVcdvS6IYUrEmKs8QVLpol8IpkXa+p" +
                "8q5LsSVHDo1IXAHx2xAi9UPELy4IsJ2tpyjDtbPcXmrZt8vWO47eHLSSdvIg3gXao0lt1tgovvcS0TEH" +
                "erZIG2ya7plWJbYlRUcDSs4tgb1FdAJseCcaBmAOYegACqJrF6PYaZ7gOlE8BxSPb4MvJAPlfwqXeNBM" +
                "bz955RRNJyFy18886gP9kP0s5ylClSSwx6xHxsvzUz13sjHlXHuWTCLHeYOjS9/PBjxqamppIQDwUQuI" +
                "in7cdKEOv/yVFHuNk5yEvJlScjqsaFyl+g84jK53FunhCPwvOTmuhbF0L6Hls7OxK1A3DlNsQH7pJJB1" +
                "WL4hqk6viQ8SVmfDac4H/hC0kWkHT2MpqpfeeahV97RqHcfXr778/vUJM5uFMHP6hLXr+Juu/BZNHUl7" +
                "SH2d3daveupPtuXpqmRv/uNXdvf5jxsKsZxkMC1JltPniCNnr6KJnHaxwnSNqbFUV63spKW0JWgz/RrH" +
                "XsIaifeNThEgLysebjDqRqfNBs3dYA0ULY9bNucjE6fB1LMHx0PLHvHOl8ZqeNoLRcdfkL8HUUpu390g" +
                "xgYphmjQ0AiEwkPjBn51+46yAWRev9QEsPvlkwu/fc3y+727UpJrmMDjrseGk9vKAdyHkMi/QbEX05Rr" +
                "FAFLgnJloIv0bIOv4ZJQDb1AMnDYC4zwcYRt2kcH3raiwAUny36uSc8vF8g2QVu2boafEB9rnANrT7g6" +
                "01WDw2uVhjDUYBKBvXc7UyJ0O06ab9VMqDVbz37Mksmlkln+XslGELLS0eAdVusKwyrKvYlNFmD3QJ/t" +
                "KvufZPlDi5o1hjXFUaWt5YWzVF4wSc+FrFUtt+lYnYU6YH+YGEI8ZSKxNB6peisDVbxA5ZJu29lkgNHx" +
                "d0nek24e7nuAQfGeLUws2TMeI+VCL9UV7RtcRSlKOUrSTsuAfwG8qWHuKROFulMy03G4FcXq5XTTpZ6n" +
                "Yuq0OXk3mf/l+miIuGMwQ3LGaQfTtTj3lSQSnVvNV2rqY0noR4dFAC0hcK33dIjYfrje0fTocPo0nj49" +
                "ZP8AOp3TrAEJAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MagneticField> CreateSerializer() => new Serializer();
        public Deserializer<MagneticField> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MagneticField>
        {
            public override void RosSerialize(MagneticField msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MagneticField msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MagneticField msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MagneticField msg) => msg.Ros2MessageLength;
            public override void RosValidate(MagneticField msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<MagneticField>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MagneticField msg) => msg = new MagneticField(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MagneticField msg) => msg = new MagneticField(ref b);
        }
    }
}
