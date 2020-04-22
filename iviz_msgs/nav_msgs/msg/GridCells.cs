
namespace Iviz.Msgs.nav_msgs
{
    public sealed class GridCells : IMessage
    {
        //an array of cells in a 2D grid
        public std_msgs.Header header;
        public float cell_width;
        public float cell_height;
        public geometry_msgs.Point[] cells;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/GridCells";
    
        public IMessage Create() => new GridCells();
    
        public int GetLength()
        {
            int size = 12;
            size += header.GetLength();
            size += 24 * cells.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public GridCells()
        {
            header = new std_msgs.Header();
            cells = System.Array.Empty<geometry_msgs.Point>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out cell_width, ref ptr, end);
            BuiltIns.Deserialize(out cell_height, ref ptr, end);
            BuiltIns.DeserializeStructArray(out cells, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(cell_width, ref ptr, end);
            BuiltIns.Serialize(cell_height, ref ptr, end);
            BuiltIns.SerializeStructArray(cells, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACrVTwWrcMBC9C/YfBvaQpLAppKWHhd5C0h4KgeRWyjIrzdoCWXKl8W7cr++TTLcN9JBD" +
                "awyypTdv3rwZrTkS58wzpQNZCaGQxw7d3FKXvTOfhJ1k6ttiDiGxvrtpwN3JO+1fbvXiu15NJ2kQzfNu" +
                "KF15+5B81K/fFnazMh//8bMyXx7vt1TULfkWySuzpkfl6Dg7ghp2rEyHhFqgUfImyFECongYxVE71XmU" +
                "co3Ap97Dh0KdRMkcwkxTAUgT2TQMU/SWVUj9IC/iEdnMGzmrt1PgDHzKzscKP2QepLLjLfJ9kmiFPt9u" +
                "gYlF7KQegmYw2CxcfOxwSGaCebAXAWb9dEob/EqHjpyTk/asVaw8j1lK1cllixxvluKuwQ13BFlcocu2" +
                "t8NvuSIkgQQZk+3pEsofZu1TBKHQkbPnfZBKbOEAWC9q0MXVH8xV9pYix/SLfmH8neM1tPHMW2va9OhZ" +
                "qNWXqYOBAI45Hb0DdD83Ehu8RKXg95nzbGrUktKs76rHACGqdQQrl5KsRwMcnTwGtmiu7K0bO0z4fxvI" +
                "v9yCOpRttuCWso+l1TOm4tXDIVxBDE8F1jk6ZEFdI1tZLtmH9/R8/prPXz9QwU+hkHxuxgMAAA==";
                
    }
}
