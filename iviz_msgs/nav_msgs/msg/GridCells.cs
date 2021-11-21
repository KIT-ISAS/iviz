/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GridCells")]
    public sealed class GridCells : IDeserializable<GridCells>, IMessage
    {
        //an array of cells in a 2D grid
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "cell_width")] public float CellWidth;
        [DataMember (Name = "cell_height")] public float CellHeight;
        [DataMember (Name = "cells")] public GeometryMsgs.Point[] Cells;
    
        /// <summary> Constructor for empty message. </summary>
        public GridCells()
        {
            Cells = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridCells(in StdMsgs.Header Header, float CellWidth, float CellHeight, GeometryMsgs.Point[] Cells)
        {
            this.Header = Header;
            this.CellWidth = CellWidth;
            this.CellHeight = CellHeight;
            this.Cells = Cells;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GridCells(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            CellWidth = b.Deserialize<float>();
            CellHeight = b.Deserialize<float>();
            Cells = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GridCells(ref b);
        }
        
        GridCells IDeserializable<GridCells>.RosDeserialize(ref Buffer b)
        {
            return new GridCells(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(CellWidth);
            b.Serialize(CellHeight);
            b.SerializeStructArray(Cells, 0);
        }
        
        public void RosValidate()
        {
            if (Cells is null) throw new System.NullReferenceException(nameof(Cells));
        }
    
        public int RosMessageLength => 12 + Header.RosMessageLength + 24 * Cells.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GridCells";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTwWrcMBC9C/YfBvaQpLAppKWHhd5C0h4KgeRWyjIrzdoCWXKl8W7cr++TTLcN9JBD" +
                "awyypTdv3rwZrTkS58wzpQNZCaGQxw7d3FKXvTOfhJ1k6ttiDiGxvrtpwN3JO+1fbvXiu15NJ2kQzfNu" +
                "KF15+5B81K/fFnazMh//8bMyXx7vt1TULfkWySuzpkfl6Dg7ghp2rEyHhFqgUfImyFECongYxVE71XmU" +
                "co3Ap97Dh0KdRMkcwkxTAUgT2TQMU/SWVUj9IC/iEdnMGzmrt1PgDHzKzscKP2QepLLjLfJ9kmiFPt9u" +
                "gYlF7KQegmYw2CxcfOxwSGaCebAXAWb9dEob/EqHjpyTk/asVaw8j1lK1cllixxvluKuwQ13BFlcocu2" +
                "t8NvuSIkgQQZk+3pEsofZu1TBKHQkbPnfZBKbOEAWC9q0MXVH8xV9pYix/SLfmH8neM1tPHMW2va9OhZ" +
                "qNWXqYOBAI45Hb0DdD83Ehu8RKXg95nzbGrUktKs76rHACGqdQQrl5KsRwMcnTwGtmiu7K0bO0z4fxvI" +
                "v9yCOpRttuCWso+l1TOm4tXDIVxBDE8F1jk6ZEFdI1tZLtmH9/R8/prPXz9QwU+hkHxuxgMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
