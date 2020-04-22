
namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GridMap : IMessage
    {
        // Grid map header
        public GridMapInfo info;
        
        // Grid map layer names.
        public string[] layers;
        
        // Grid map basic layer names (optional). The basic layers
        // determine which layers from `layers` need to be valid
        // in order for a cell of the grid map to be valid.
        public string[] basic_layers;
        
        // Grid map data.
        public std_msgs.Float32MultiArray[] data;
        
        // Row start index (default 0).
        public ushort outer_start_index;
        
        // Column start index (default 0).
        public ushort inner_start_index;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "grid_map_msgs/GridMap";
    
        public IMessage Create() => new GridMap();
    
        public int GetLength()
        {
            int size = 24;
            size += info.GetLength();
            for (int i = 0; i < layers.Length; i++)
            {
                size += layers[i].Length;
            }
            for (int i = 0; i < basic_layers.Length; i++)
            {
                size += basic_layers[i].Length;
            }
            for (int i = 0; i < data.Length; i++)
            {
                size += data[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public GridMap()
        {
            info = new GridMapInfo();
            layers = new string[0];
            basic_layers = new string[0];
            data = new std_msgs.Float32MultiArray[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            info.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out layers, ref ptr, end, 0);
            BuiltIns.Deserialize(out basic_layers, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out data, ref ptr, end, 0);
            BuiltIns.Deserialize(out outer_start_index, ref ptr, end);
            BuiltIns.Deserialize(out inner_start_index, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            info.Serialize(ref ptr, end);
            BuiltIns.Serialize(layers, ref ptr, end, 0);
            BuiltIns.Serialize(basic_layers, ref ptr, end, 0);
            BuiltIns.SerializeArray(data, ref ptr, end, 0);
            BuiltIns.Serialize(outer_start_index, ref ptr, end);
            BuiltIns.Serialize(inner_start_index, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "95681e052b1f73bf87b7eb984382b401";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1XbU8bORD+vhL/YUQ+lHCQQqjQFYkP6Kr2KrUSd610OiEUzO4kMezaW9shpL++z9j7" +
                "lnKn3ocrESK79sz4mWdePBnRO6cLqlRNS1YFu0zeP6r6vZlb0viXZaNeplQbdmRUxX6S+eC0WVxdp1W/" +
                "JXirvM6H4rRn66CtUeV4Qp+XPJTw0Cw4sKu0YVovdb5sNmjubEU36eWGDHNBwdIt04MqdQE9bcg64Ka5" +
                "daQo57IkO6eAExYtmIHGAHUEMPsH7IUKSuSKWeUX/uXb0qpwMv24KoO+cE5toCsiovOnXZMPygXgKPiR" +
                "9gqeKwjS0XiSrbQJx6dkV/BsFqVmUUoUf7PlqjI/1NXGfKe7k53/z5+d7OOnd2eRrRncT04PsmAHcH+P" +
                "uUF7QVdMyhSIC4I6zpr1JnWEEPbwTAK9FYWr6qVE5nqSzYXN01fkOkFR+8BmEZYSzMfDQjvOo4WraqBQ" +
                "RpHZ47b45gfiGxG/tJ6fJEXOBnERG7Ic/UESzpGBhSzeJJ9uktUF24qD2yRyor0a/35aMLrkSwRLCD4F" +
                "8K4cwHNQkn8x45d6sWR3WPIDl5JNVQ34cTdsainSEYpNe8LfgpFLqiw3tPKpjHJbVSujcxWYJLJb+qm2" +
                "FNXIPZ2vSuUgj1LTRsQjX2Idf56/rNjkTO/fnEHGeM4RWADawELuGHVmFtikmNUnU1HIRp/X9hCvvEAQ" +
                "usMRC4V68MSPNTJEcCp/hjP2k3MT2AY7jFMKtJS4NsOrHxMOAQSuLZrHHpBfbsLSpuA+KKfVbcliOAcD" +
                "sPpClF6MB5YF9hl6lbGt+WSxP+O/mDWdXfHpcImYleK9Xy1AIARrZx90AdHbTTSSlxqJSKW+dcptslhg" +
                "8chs9DbmZJDwxYjgW3lvc40AFLTWYdk0sxSNGfrhz+sOTwpAcvICZSxxggeqrXmpC8mcuWN4UqucDyTR" +
                "ZLlo9nWUlTZinW51J5RdWiREJ5D9sYKjzkS7vdzz+QgwO239ICOC0sbHmHUuwB0USES95XHXhR67p033" +
                "9PW5POj569zowoVU2mJ1G7+8fenZR6OpJtkPnGqf1j/PvX+/ksXByxKdhqm09p7QRCRQvcAHtcE9jN7p" +
                "vVo0jT7FEM7J+GHzVdWnMbITc0Ql6krU0UizJ8ZkRonf6YNGWHOu59JPm+SIZZukEj8nUwwP7Sfudp8R" +
                "xZNatewZaPzeo5QmPPAbROXo/BjfVLo/MLLFXVy7oMvHga4HrmBgcF9099CE6E0rD1OO08yGhhDnuzgh" +
                "UWV9kL4pY6dp3rdo70zIAKYrMHbRMdZuSX+tGQjYt7eNoJjZ+dzzIFS1KnCTLYhLlrADVBAsKOSOf5jP" +
                "c+SLxRjql3ZVFnTx4a+Lvz/JMLl2OgSOZSNzq98GIW25YFiQHtdkhXQ/8fNQ/BrIzrUTP+NN2hO/pw/u" +
                "Du7HdB7BXA19+EWUZ+mIq+Prfb29Mr3ev8PK/XU2ih3aN3PDAZ0c5riPDAaF01dHj69+PSJdSTHITQJH" +
                "gA0V9ACcuS0xWjTCMpyvo/dwu/clXstSOLq6OrqelOoWdgF3d8kYScJuv+X1Vwbn54QTB6sRLVZP9oFm" +
                "X9Cc0+vp8enREdGesZgxkmRDptx+dyswF82B7YhdLloROx4iWOsiLHf7nQ4ADhqsbgHA9/Hrabs9HZpr" +
                "eNjt9zqDJ4O1zlyk5WkkHc/lBw3SWzqTUO7s+oDu8JDHHwMHMVvu5T2dOHnWFvCmzciddqpoKEC1pCeQ" +
                "vsBYZ/rk7Qa6RIi0wCY63wnG0Uk6AWHeDH7cKSbWRDE9PVXdyb4Bl8WmIqQOAAA=";
                
    }
}
