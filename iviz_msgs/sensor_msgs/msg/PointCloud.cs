/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PointCloud : IDeserializable<PointCloud>, IMessage
    {
        // This message holds a collection of 3d points, plus optional additional
        // information about each point.
        // Time of sensor data acquisition, coordinate frame ID.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Array of 3d points. Each Point32 should be interpreted as a 3d point
        // in the frame given in the header.
        [DataMember (Name = "points")] public GeometryMsgs.Point32[] Points;
        // Each channel should have the same number of elements as points array,
        // and the data in each channel should correspond 1:1 with each point.
        // Channel names in common practice are listed in ChannelFloat32.msg.
        [DataMember (Name = "channels")] public ChannelFloat32[] Channels;
    
        /// Constructor for empty message.
        public PointCloud()
        {
            Points = System.Array.Empty<GeometryMsgs.Point32>();
            Channels = System.Array.Empty<ChannelFloat32>();
        }
        
        /// Explicit constructor.
        public PointCloud(in StdMsgs.Header Header, GeometryMsgs.Point32[] Points, ChannelFloat32[] Channels)
        {
            this.Header = Header;
            this.Points = Points;
            this.Channels = Channels;
        }
        
        /// Constructor with buffer.
        internal PointCloud(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Points = b.DeserializeStructArray<GeometryMsgs.Point32>();
            Channels = b.DeserializeArray<ChannelFloat32>();
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new ChannelFloat32(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PointCloud(ref b);
        
        PointCloud IDeserializable<PointCloud>.RosDeserialize(ref Buffer b) => new PointCloud(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Points);
            b.SerializeArray(Channels);
        }
        
        public void RosValidate()
        {
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
            if (Channels is null) throw new System.NullReferenceException(nameof(Channels));
            for (int i = 0; i < Channels.Length; i++)
            {
                if (Channels[i] is null) throw new System.NullReferenceException($"{nameof(Channels)}[{i}]");
                Channels[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 12 * Points.Length;
                size += BuiltIns.GetArraySize(Channels);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/PointCloud";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d8e9c3f5afbdd8a130fd1d2763945fca";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWwW7jNhC96ysG9mGdwnGR7B6KAD20u802hwKLbm5FEdDSWCKWIhWScuJ+fd+QlGwn" +
                "OfTQNQLEloYzb968meGS7jsdqOcQVMvUOdMEUlQ7Y7iO2llyO3rf0OC0jWFNgxkDuUHeKEOqaXT+Wi1J" +
                "253zvUqH1NaNkVjVXT65qWBwr3sWd4FtcJ4aFRWp+nHUITlZI6rzjbYqMu28gvHdp031O6uGPXXpn7j5" +
                "xXt1OIO1od8k0hf58f6aQudG09CWASmyHzxHbkhJXtORBJdiNwVq9Z7t9CiH2lQtu56jPzz0oQ0/Fu9/" +
                "/V2CCpQUtu6UtWymsJ3ac3ITxLEd+y3QAy0b7hnnBEj2QEoyWcOPsk06kigBCn7Db+285zA4mF7dXNGT" +
                "jt0ZwUv6WA5YBA7ipnZ9j2IMXqGUNSMek9FB2MDbYn5rnEJeGyS5qc6fIdcCAtn+/D9/qj++fr6hEJtM" +
                "by4zsvgawYbyDTQZVSIEsqJOtx37S8N7YSSqfkAS6W08DBwk/SRk/LVs2StjDjQGGEWXeBitrkVYESI8" +
                "O5+loGhQHiSNRvlXOhTv+Av8OLIFj3efbmBjA9djhHAQSdvaswratnhJ1ViEyI/V8v7JXYoOW8hgDo5q" +
                "qyhg+RnyDIJThRvE+CEnt4FvkMOIgn5cpWcP+BkuCEEAgQeH0q+A/MshdiiyyGevvFZbA90HqsEAvL6T" +
                "Q+8uTjwL7BtoxLrJffZ4jPFf3NrZr+R0CZU0RrIPYwsCYTh4t9cNTLeH5KQ2GuqH/LZe+UMlp3LIanmb" +
                "WjBK+XIDSI8EV2sUoElCr0L04j1V40E330uNb3b8JC2QFZW2IaUzuDy0pLOhHbEUGe08I61B1bxKDQoR" +
                "bDU6HVaoc41J5+yFyOkuVR+PIE1uskwh1zzD6Alah9K9hAk6UW/RtqoRRwXWhkSTE7jiCZ2TUOEJHKIG" +
                "vYtlDLoBXbHVRsdDOlq9GP3CPgfd2gwmqm9M40AGr3NGgsrK8EZrtDhtXEmsDLNIDjpay3wrg7RWyCgR" +
                "lDB/NG5sJHa1y/OFnudvh/nbP99r0qStkyt7PubeYCJNjqLcI/bZAvzIpjwuQtGtzPFz2Z7M55mSzME9" +
                "vhq2LWzAT24yM3IQH2m9HXfYvErAa3x17HSTlBjwcYSMcmBhJBwpwonjl2ATvHnJ5Bofl/er5cLP2CTS" +
                "lPN6wRgxY8MyxogW42JNi/2CLsm7p4QCl4qxt7SSCHK5kMl5MRFjeBcxEKB7R7oHydIlZ59pvrshNV+q" +
                "whhGsI/OxPqWUgTa4t7huU+NisXx0gl8ROexCgysVMCJXAzLT2i3I2/XxysReLfupZ8wSmW9Q2/2GenC" +
                "t1vJ9hbbKpc81TpNwmass55AAV6XNGtQ6RUAyLr46SzC6s/15/WvF0UV2E31t7S0pTETWSqALTSr3iEX" +
                "xLr+kAaNXCaOH1CLRSZXmQRQxoAFcweBadCbuJcAq35GWed3xbYBTwrtvEgXN1HGSf0nEcmtCX3VA4DG" +
                "2iiSnCxXvMGNYnkW92SMLVJuC0zDMtzF8xSt5P2yF64uy8VH4swXqhL3qOezDthMkwW3mdJk1b/9pd0r" +
                "9goAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
