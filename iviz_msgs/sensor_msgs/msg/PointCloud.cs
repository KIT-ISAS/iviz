using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
    public sealed class PointCloud : IMessage
    {
        // This message holds a collection of 3d points, plus optional additional
        // information about each point.
        
        // Time of sensor data acquisition, coordinate frame ID.
        [DataMember] public std_msgs.Header header { get; set; }
        
        // Array of 3d points. Each Point32 should be interpreted as a 3d point
        // in the frame given in the header.
        [DataMember] public geometry_msgs.Point32[] points { get; set; }
        
        // Each channel should have the same number of elements as points array,
        // and the data in each channel should correspond 1:1 with each point.
        // Channel names in common practice are listed in ChannelFloat32.msg.
        [DataMember] public ChannelFloat32[] channels { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud()
        {
            header = new std_msgs.Header();
            points = System.Array.Empty<geometry_msgs.Point32>();
            channels = System.Array.Empty<ChannelFloat32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointCloud(std_msgs.Header header, geometry_msgs.Point32[] points, ChannelFloat32[] channels)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.points = points ?? throw new System.ArgumentNullException(nameof(points));
            this.channels = channels ?? throw new System.ArgumentNullException(nameof(channels));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PointCloud(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.points = b.DeserializeStructArray<geometry_msgs.Point32>();
            this.channels = b.DeserializeArray<ChannelFloat32>();
            for (int i = 0; i < this.channels.Length; i++)
            {
                this.channels[i] = new ChannelFloat32(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new PointCloud(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.SerializeStructArray(this.points, 0);
            b.SerializeArray(this.channels, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (points is null) throw new System.NullReferenceException();
            if (channels is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += header.RosMessageLength;
                size += 12 * points.Length;
                for (int i = 0; i < channels.Length; i++)
                {
                    size += channels[i].RosMessageLength;
                }
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/PointCloud";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d8e9c3f5afbdd8a130fd1d2763945fca";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
    }
}
