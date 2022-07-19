/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class PointCloud : IDeserializableRos1<PointCloud>, IDeserializableRos2<PointCloud>, IMessageRos1, IMessageRos2
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
        public PointCloud(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Points);
            b.DeserializeArray(out Channels);
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new ChannelFloat32(ref b);
            }
        }
        
        /// Constructor with buffer.
        public PointCloud(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Points);
            b.DeserializeArray(out Channels);
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new ChannelFloat32(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new PointCloud(ref b);
        
        public PointCloud RosDeserialize(ref ReadBuffer b) => new PointCloud(ref b);
        
        public PointCloud RosDeserialize(ref ReadBuffer2 b) => new PointCloud(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Points);
            b.SerializeArray(Channels);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Points);
            b.SerializeArray(Channels);
        }
        
        public void RosValidate()
        {
            if (Points is null) BuiltIns.ThrowNullReference();
            if (Channels is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Channels.Length; i++)
            {
                if (Channels[i] is null) BuiltIns.ThrowNullReference(nameof(Channels), i);
                Channels[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 12 * Points.Length;
                size += WriteBuffer.GetArraySize(Channels);
                return size;
            }
        }
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Points);
            WriteBuffer2.AddLength(ref c, Channels);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/PointCloud";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d8e9c3f5afbdd8a130fd1d2763945fca";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWTW8bNxC9768YyIfIgazCTg+FgR7atE5zKBA0vhWFMdod7RLhkmuSK1v99X1D7urD" +
                "8SGHRhCg1ZJ8M/PmzQwv6L4zkXqJkVuhztsmElPtrZU6Ge/Ib+ldQ4M3LsUVDXaM5AddYUvcNKY8Vhdk" +
                "3NaHnvMh3vgxkXDdlZPrChvuTS8KF8VFH6jhxMT142hiBlnBqg+NcZyEtoGx+eNv6+oP4UYCdflHYX4J" +
                "gfdnbq3pd7X0Sf+8u6HY+dE2tBG4lCQMQZI0xBrXfCS7S6mbDbVmJ25+VUytq1Z8LynsH/rYxh8m9L//" +
                "mYyqK9ls3bFzYmezHe8kw0QFdmO/gffwVqz0gnPqSEEg1khWwGHX5COZEnghr+DWPgSJg8fW69trejKp" +
                "OyP4gt5PBxwMR4Wpfd8jGUNgpLIW2BOyJiobWJ2231nPiGuNINfV+TvEOjmBaH/+nz/Vn58/3FJMTaG3" +
                "pBlRfE5gg0MDTSbOhEBW1Jm2k3BlZaeMJO4HBJFX036QqOFnIePbipPA1u5pjNiUfOZhdKZWYSWI8Ox8" +
                "kQLTwAEkjZbDVzpUdHyjPI7iapXlLfa4KPWYIBxYMq4OwtG4FotUjZMQ5bG6uH/yV6rDFjI4GEe2Oamz" +
                "8gx5xpj1eQsbb0twa2CDHIEV1OMyv3vA33hJMAIXZPBI/RKef9qnzhfh7jgY3lhR4BoMAPWNHnpzeYLs" +
                "MrRj52f4gni08S2w7oCrMV1BJY3V6OPYgkBsHILfmQZbN/sMUlsD9UN+m8BhX+mpYrK6uMslmDR9pQC0" +
                "RqKvDatSVehVTEHRczYeTPO91Phqxc/SAlmJjYs5nMGXpqWVzaUEVUbbIAhr4FqWuUAhgo1BpWMX8lyj" +
                "03l3qXL6mLOPV5CmNEWmkGvpYfQErUPpQc1Ek6l3KFtuFGhya01UZJmdm5BQOdkrvAEgctD7NLVBP6Aq" +
                "NsaatM9H55Nz61f2JZrWFWcSfxEaB7JYLhGpV06bN0qjxWnrp8CmZpbIQ0cr7W9TI605SiEo+/ze+rFR" +
                "29W29Bd6PjztD0//fq9Ok6dOyex5m3uFidw5JuUefT/sAD86KY+DUHWrffxctif9+UBJ4eAej1Zciz3g" +
                "pxSZHSUqRh5vxxl2GCVclHd+7HSSTDaAcXR5lWdL9iNbOAF+6Wx27zBkSo6Pw/ur4SLPmCRalIfxgjZi" +
                "x0a0jREtxsWKFrsFXVHwT9kLXCrG3tFSLejlQjvn5UyMlW1CQ4DuPZkeJK8zzMln7u9+yMUnpWRGsI/K" +
                "xPjWVETa4N4RpM+FisHxEgQYyQeMAkvasHGiJMPJE8rtyNvN8UoE3p1/iRNHzWzwqM2+eLoI7UajvfNh" +
                "SnnOde6EzVgXPYECLE9h1qAyMBzQcfHTmYXlX6sPq18vJ1VgNtVf8tDWwsxkcQRbKFazRSywdfNjbjSr" +
                "MxRQi0GmV5nsoLYBB+b26qZFbeJeAl/NM9J6WJv2NuCJUc6LfHFTZZzkfxaR3ppQVz0cMHWcJTnvXMq6" +
                "VbRTuydtbJFjW6AbTs1dkWdrU9wva+H66vqo1cOFarJ71PNZBaznzoLbzFRk1X/9pd0r9goAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
