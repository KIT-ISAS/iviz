/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GridCells")]
    public sealed class GridCells : IDeserializable<GridCells>, IMessage
    {
        //an array of cells in a 2D grid
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "cell_width")] public float CellWidth { get; set; }
        [DataMember (Name = "cell_height")] public float CellHeight { get; set; }
        [DataMember (Name = "cells")] public GeometryMsgs.Point[] Cells { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GridCells()
        {
            Header = new StdMsgs.Header();
            Cells = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridCells(StdMsgs.Header Header, float CellWidth, float CellHeight, GeometryMsgs.Point[] Cells)
        {
            this.Header = Header;
            this.CellWidth = CellWidth;
            this.CellHeight = CellHeight;
            this.Cells = Cells;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GridCells(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Cells is null) throw new System.NullReferenceException(nameof(Cells));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += Header.RosMessageLength;
                size += 24 * Cells.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GridCells";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTWvcMBC9G/wfBnJIUtgU0tLDQm+hH4dCILmVskykWXtAllxpvBv31/fJbrJt6aGH" +
                "1mBkS/PevHkzOuNInDPPlPbkJIRCih26vqEuq2+bD8JeMvXL0jb7kNheXS+hu6N663/b60W73tqmkzSI" +
                "5Xk3lK68vE0a7fOXNUPbtM3bf/y0zae791sq5teEq+y2OaM74+g5e4Ic9mxM+4R6oFLyJshBAlA8jOJp" +
                "ObV5lHJVkfe9wo1CnUTJHMJMU0GUJXJpGKaojk3IdJBfCCp08XDkbOqmwBmAlL3GGr/PPMjCX98iXyeJ" +
                "TujjzRZRsYibTCFqBofLwkVjh0MET7AQLgMB4P0xbfAvHXrzrICsZ6uK5XHMUqpYLtua5sVa4xXoYZIg" +
                "kS90sezt8FsuCXmgQsbkerqA/NvZ+hTBKHTgrPwQpDI7+ADa8wo6v/yZukrfUuSYnvhXylOSv+GNJ+Ja" +
                "1qZH80K1oEwdfETkmNNBPWIf5oXFBZVoFPQhc57bpsLWpCB5V81GGHBLb7ByKckpOuHpqHV8i+WaYOnL" +
                "ro78f5vOP9yJ5zmDZcYay1LTmIqawiZcSsxRDawjtc+C0kZ28uPSvXlNj6dPFP/0+a1W8R1WSScF3AMA" +
                "AA==";
                
    }
}
