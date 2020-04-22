
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class PointCloud : IMessage
    {
        // This message holds a collection of 3d points, plus optional additional
        // information about each point.
        
        // Time of sensor data acquisition, coordinate frame ID.
        public std_msgs.Header header;
        
        // Array of 3d points. Each Point32 should be interpreted as a 3d point
        // in the frame given in the header.
        public geometry_msgs.Point32[] points;
        
        // Each channel should have the same number of elements as points array,
        // and the data in each channel should correspond 1:1 with each point.
        // Channel names in common practice are listed in ChannelFloat32.msg.
        public ChannelFloat32[] channels;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/PointCloud";
    
        public IMessage Create() => new PointCloud();
    
        public int GetLength()
        {
            int size = 8;
            size += header.GetLength();
            size += 12 * points.Length;
            for (int i = 0; i < channels.Length; i++)
            {
                size += channels[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud()
        {
            header = new std_msgs.Header();
            points = new geometry_msgs.Point32[0];
            channels = new ChannelFloat32[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.DeserializeArray(out points, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out channels, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.SerializeArray(points, ref ptr, end, 0);
            BuiltIns.SerializeArray(channels, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d8e9c3f5afbdd8a130fd1d2763945fca";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
