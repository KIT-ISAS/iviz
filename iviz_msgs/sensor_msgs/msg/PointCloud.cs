/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/PointCloud")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud()
        {
            Points = System.Array.Empty<GeometryMsgs.Point32>();
            Channels = System.Array.Empty<ChannelFloat32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointCloud(in StdMsgs.Header Header, GeometryMsgs.Point32[] Points, ChannelFloat32[] Channels)
        {
            this.Header = Header;
            this.Points = Points;
            this.Channels = Channels;
        }
        
        /// <summary> Constructor with buffer. </summary>
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/PointCloud";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d8e9c3f5afbdd8a130fd1d2763945fca";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWTW/jNhC9C/B/GNiHdQrHRbI9FAF6aHebbQ4FFru5FUVAS2OJWIpUSMqJ++v7hqT8" +
                "keTQQ9cIEFsazrx582aGC7rvdKCeQ1AtU+dME0hR7YzhOmpnyW3pfUOD0zaGFQ1mDOQGeaMMqabR+Wu1" +
                "IG23zvcqHVIbN0ZiVXf55LqCwb3uWdwFtsF5alRUpOrHUYfkZIWozjfaqsi09QrGdx/X1R+sGvbUpX/i" +
                "5lfv1f4M1pp+l0if5cf7awqdG01DGwakyH7wHLkhJXlNRxJcit0UqNU7ttOjHGpdtex6jn7/0Ic2/Fi8" +
                "//V3CSpQUti6U9aymcJ2asfJTRDHduw3QA+0bLhnnBMg2QMpyWQFP8o26UiiBCj4Db+1857D4GB6dXNF" +
                "Tzp2ZwQv6EM5YBE4iJva9T2KMXiFUtaMeExGB2EDb4v5rXEKea2R5Lo6f4ZcC4hQzapf/ufPrPrz66cb" +
                "CrHJBOdCz5DI1whClG8gy6gSJ1AWdbrt2F8a3gkpUfUD8khv437gIAwkLeOvZcteGbOnMcAoukTFaHUt" +
                "2orQ4dn5rAZFg/LgaTTKv5KieMdf4MeRLai8+3gDGxu4HiO0g0ja1p5V0LbFS6rGokV+rBb3T+5SpNhC" +
                "CYfgKLiKApafodAgOFW4QYwfcnJr+AY7jChoyWV69oCf4YIQBBB4cKj+Esg/72OHOouCdsprtTGQfqAa" +
                "DMDrOzn07uLEs8C+gUysm9xnj8cY/8WtPfiVnC4hlMZI9mFsQSAMB+92uoHpZp+c1EajAaDAjVd+X8mp" +
                "HLJa3KYujFK+3APSJsHVGgVoktarEL14T9V40M33E+SbbT+b1AW+otI2pIwGl0eX9DfkI6aipK1nZDao" +
                "mpepTaGDjUa/wwqlrjHvnL0QRd0lAeAR1MlNVioUmycZPUHuELuXMEEn9i2aVzXiqOBak8hyAlc8oXkS" +
                "KjyBQ5Shd7EMQzegMTba6LhPR6sXC0AKwEG3NoOJ6hvTOJDB65yRoLIywtEdLU4bVxIrIy2Sg5RWMuXK" +
                "OK0VMkoEJcwfjBsbiV1t85Sh58O3/eHbP99v3qTtk4t7Pu5mb5CR5kfR7xH+wQIUyco8bkRRrwz0c/Ge" +
                "DOoDK5mGe3w1bFvYgKLcambkID7Snjsus8NOAbXx1bHTlVJiwMcRMiqCzZFwpAgnjl+CTfAO2yaX+bjF" +
                "X20ZfsZKkdY87BkMEzM2LMOMaD7OVzTfzemSvHtKKHC7GHtLS4kgtwyZnxcTMYa3EWMB0neke5AsjXL2" +
                "maa8G1L/pSqMYQT7aE7scSlFoA0uIJ771KtYHy+dwEd0HgvBwEoFnMjFsPyEjjvydn28G4F36176CaNU" +
                "1ju0Z5+Rzn27kWxvsbNyyVOt0zxsxjrrCRTgdUmzBpVeAYAsjZ/PIiy/rD6tfrsoqsCGqr+l7S29mchS" +
                "AWyhX/UWuSDW9U9p1sit4vgBtVhncqdJAGUSWDC3F5gG7YkLCrDqZ5T18K7YNuBJoaPn6QYnyjip/yQi" +
                "uT6hsXoA0FgeRZKT5ZLXuFoszuKeTLJ5ym2OgVhGvHieopW8X/bC1WW5AUmcw82qxD3q+awD1tNwwbWm" +
                "NNms+hcaDTW8AAsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
