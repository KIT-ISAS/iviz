/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = "nav_msgs/GridCells")]
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
                "H4sIAAAAAAAAE7VTTWvcMBC961cM7CFJYVNISw+B3kI/DoVAcitlmZVm7QFZcqXxbtxf3yebbBvooYfW" +
                "GGRL772ZeTPacCIuhWfKB/ISYyXFDt3cUVc0uE/CQQr1y+IOMbO9uVmAu5MG619u9aJdb66TPIiVeTfU" +
                "rr6+z5rs67dV3bn3//hxXx4+3lK1sEZbE3YbejBOgUsgpMKBjemQUQgSlLKNcpQIEg+jBFpObR6lXoP4" +
                "2CtMqNRJksIxzjRVgCyTz8MwJfVsQqaDvOCDuTg3cjH1U+QCfC5BU4MfCg/S1PFW+T5J8kKf726BSVX8" +
                "ZIqEZij4Ilw1dTgkN8E5eAuC2zye8ha/0qEd5+BkPVtLVp7GIrXlyfUWMV6txV1DG+YIooRKl8veDr/1" +
                "ihAEKciYfU+XyPx+tj4nCAoduSjvozRhDwegetFIF1e/KadFOnHKz/Kr4q8YfyObzrqtpm2PnsVWfZ06" +
                "GAjgWPJRA6D7eRHxUSUZRd0XLrNrrDWk23xoHgME1tIRrFxr9ooGBDopprVaaepLN3YY7/80jX+4AM+D" +
                "BauMNdWlmDFXNYU9uHyYnIZrQ3QogqJG9rJer3dv6en8NZ+/fjj3E0KsLTO/AwAA";
                
    }
}
