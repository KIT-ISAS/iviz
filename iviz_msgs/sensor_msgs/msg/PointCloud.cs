/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/PointCloud")]
    public sealed class PointCloud : IMessage, IDeserializable<PointCloud>
    {
        // This message holds a collection of 3d points, plus optional additional
        // information about each point.
        // Time of sensor data acquisition, coordinate frame ID.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Array of 3d points. Each Point32 should be interpreted as a 3d point
        // in the frame given in the header.
        [DataMember (Name = "points")] public GeometryMsgs.Point32[] Points { get; set; }
        // Each channel should have the same number of elements as points array,
        // and the data in each channel should correspond 1:1 with each point.
        // Channel names in common practice are listed in ChannelFloat32.msg.
        [DataMember (Name = "channels")] public ChannelFloat32[] Channels { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud()
        {
            Header = new StdMsgs.Header();
            Points = System.Array.Empty<GeometryMsgs.Point32>();
            Channels = System.Array.Empty<ChannelFloat32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointCloud(StdMsgs.Header Header, GeometryMsgs.Point32[] Points, ChannelFloat32[] Channels)
        {
            this.Header = Header;
            this.Points = Points;
            this.Channels = Channels;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PointCloud(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Points = b.DeserializeStructArray<GeometryMsgs.Point32>();
            Channels = b.DeserializeArray<ChannelFloat32>();
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new ChannelFloat32(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PointCloud(ref b);
        }
        
        PointCloud IDeserializable<PointCloud>.RosDeserialize(ref Buffer b)
        {
            return new PointCloud(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Points, 0);
            b.SerializeArray(Channels, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                foreach (var i in Channels)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
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
