
namespace Iviz.Msgs.nav_msgs
{
    public sealed class OccupancyGrid : IMessage
    {
        // This represents a 2-D grid map, in which each cell represents the probability of
        // occupancy.
        
        public std_msgs.Header header;
        
        //MetaData for the map
        public MapMetaData info;
        
        // The map data, in row-major order, starting with (0,0).  Occupancy
        // probabilities are in the range [0,100].  Unknown is -1.
        public sbyte[] data;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/OccupancyGrid";
    
        public IMessage Create() => new OccupancyGrid();
    
        public int GetLength()
        {
            int size = 80;
            size += header.GetLength();
            size += 1 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public OccupancyGrid()
        {
            header = new std_msgs.Header();
            info = new MapMetaData();
            data = System.Array.Empty<0>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            info.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            info.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "3381f2d731d4076ec5c71b0759edbe4e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1VXWvbSBR9H8h/uOCHJovtuN2llEAfCqHZPoTN0uyTCeZ6dC1NK82oM6O42l/fMyNL" +
                "drqw9KFpELE0uh/nnPuhGd1XJpCX1ksQGwMxvVpcU+lNQQ23czKW9pXRFQnjn5a6PrWOlVDr3Za3pjax" +
                "J7dTM3Jady1b3S+V+lO4EE/V8KPU7FYiX3Nk2jmf3ZFF3XI7nRu7c7ADsPyOChxmGN7tFw1/gpvzCDan" +
                "ENlHY0vam1jR+Wq+ulgS/TVmR4wjNCOg5iXFSUk921JovZq/XK0e4PSP/Wzd3hK0WLxcKmPjm/VDTq3O" +
                "1Nuf/Hembj/eXAF+sWlCGS4Hjc6A92NkW7CH9FCjGFWqTFmJX9TyKHUi3bRSZGwU+1bCMosF5LhKseK5" +
                "rnvqAoyiI+2aprNGcxSKppEn/vCEIExtElJ3NXvYQ11jk/nOcyMpOq4gXzqxWujD9RVsbBDdRQNAPSJo" +
                "LxxSIT5ck+og3u+vkoOa3e/dAo9SovZTchSAYwIrX1MfJZwcrpDjt4HcErGhjiBLEeg8n23wGC4ISQBB" +
                "WodWPAfyuz5WbqjoI3vD2xoVDqShAKK+SE4vLk4iJ9hXZNm6MfwQ8ZjjR8LaKW7itKhQszqxD10JAWGI" +
                "tns0BUy3fQ6ia4NpodpsPfteJa8hpZq9TxrDCF65IvjlEJw2KECRG1uF6FP0XI2NKZ6vIS0/Dg15Mo5n" +
                "Y3NVrgYflFnnCfUNRwONeOu6OJCs2LOO4k3AYnC7fDgN4w0WyjjVmT9aYNgrhx1Aew5UO0xCMQiEs016" +
                "3qSnk32AlnF1l5Ovm8u0kB7UDoap6Y7v4AAWELDAalgnq/AwtmY+PBhUguGK31sMp4ekzpsSTXFglCCs" +
                "mznh8lyk3THOXt4rwvVi7zy0atFjBycEyoszr6hxBSHQUpXiMOq+H3S/yy453bMV+b8ZU4nfHZf6UFcA" +
                "zwQAducF7dqylnnaJum4OLw3Qw9YPHsz+i5J3TnoOBmovzt0s7c57tFO/TKOADP1McY+srGHT9dIAXSw" +
                "BTPqJ4xV7qzXf9DX6a6f7v79VQyO+k00Tr/YT1R9ij89fTmqn+YWH+X/JzXe7UHvGyrleYwgCAAA";
                
    }
}
