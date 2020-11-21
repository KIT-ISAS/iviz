/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/PointCloud")]
    public sealed class PointCloud : IDeserializable<PointCloud>, IMessage
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
        public PointCloud(ref Buffer b)
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
                "H4sIAAAAAAAACrVWwW4bNxC9C9A/EPIhciGrcNJDYaCHNqlTHwoEiW9FYVC7o10iXHJNcmWrX983Q+6u" +
                "FPvQQyPYsLw7fDPz5s0ML9R9a6LqKEbdkGq9raPSqvLWUpWMd8rv1bta9d64FDeqt0NUvuc32ipd1yZ/" +
                "XS4ulHF7Hzotp/TOD0mRrtp8dLtcsMm96YgRI7nog6p10kpXj4OJgrOBYx9q43QitQ8axncfcPQP0jUF" +
                "1cqfjPRrCPp4FtxW/c7uPvE/796q2PrB1mpHiCtR6AMlqpXm7MYjOWiV2tFZYw7kxkfZG7w35DtK4fjQ" +
                "xSb+WPD/+ru4zeGI66rVzpEdXbf6QIIUGdsN3Q45IGKy1BFOcjAZQ2nOZsNA2tVyRqhBJPQKcOVDoNh7" +
                "mF7fXKsnk9pzqi/U+3LCwXVknMp3HerSB42yVgSPpKyJzAneFvNb6zVy2yJRoJw/RMIlDEn5l//5s1z8" +
                "+eXjjYqpzjTnknMqXxI40aGGSJMWWiAz1ZqmpXBl6cC8JN31yETepmNPUUgQaeOnIUdBW3tUQ4RV8sLG" +
                "4EzFOkvQ5BlAkYVWvQ7garA6vNCl4PNvpMeBHAi9+3ADKxepGhJkBGfGVYF0NK7BSxgPRZj0iIP3T/6K" +
                "hdlAE1MEqLxOHDE9Q6+Rg9Xxht38kHPcAh4kERyhT9fy7AH/xksFP4iCeg8drBH+p2NqUXDW0kEHo3cW" +
                "nRBVBR4A+4YPvbk8hebQbyAY50f8DDk7+S+4bgbmtK4gmdoyBXFowCMs++APpobt7igolTXoBohxF3Q4" +
                "Lhd8LDsFyK30ZeJC5o7grom+MqhELcpfLmIK7EDq8mDq76jOVyfBpDSQlrRxUbLqfR5p3PBQEpuyqPaB" +
                "kFyvK1pL20IPO4MBACtUvMIc9O5SxHUnQsAzSJXqLFvIN8839QTxQ/qB/UQjNXBoZl0zUglsq0ShU3wF" +
                "C80kgeEJIFGNzqcyJH2PPtkZa9IxH57OjguC60DRNC4HlPRXUkOvLF7ntDgyx/Md3dLwcetLemXSJeWh" +
                "qg0PvzJoK420hCYJ/L31Qy3ul4t9nj3qef4KhYxf//l+Y0j2Uy7z+Rh8jRGZKkXNcwqTBXjivTqvTVay" +
                "zPpzJZ/M8ImazMU9vlpyDWzAU249O1AUEFmE87abFg4ITi/One6b4oRB5qhRGKwViUR8nCB/G64EOK2i" +
                "Uu7Tbf9iCdEzNg736rSGMGHsUJPMOKVWw2qjVoeVulLBP0kkuIkMnVNr9sI3Ep6slyM9lvYJowKt4JXp" +
                "wLV0ztln3AG+l46UagxxQBXQrtj2XJKodrisBOqke7FdXqAAJPmAfWFhpiOO5KI4ekIPzuy9nW9SoN/5" +
                "F0Bx4BIHj47tSrCr0Ow441tstVx8qboMynqosrRAA16XVCvwGTRC4I3y87mP9efNx81vl0UgWGHVV1nx" +
                "3KzCmI6gDA1s9kgHzt7+JBNILh/zBwRj4cn1R2Lk8eDA35EjtWjYgPeqN88o7/RuNK7BlkaTr8qlj3Vy" +
                "IoVRUnzbQqd1iMJguRSFjpZr2vIl5OLM98mQW0mGKx6WZQEw9uyxEPBte1xflQsT+5puYsX3rPDzpoCP" +
                "MnNwCxpbb7n4F3nwH7Q9CwAA";
                
    }
}
