using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class GridCells : IMessage
    {
        //an array of cells in a 2D grid
        public std_msgs.Header header;
        public float cell_width;
        public float cell_height;
        public geometry_msgs.Point[] cells;
    
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
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += header.RosMessageLength;
                size += 24 * cells.Length;
                return size;
            }
        }
    
        public IMessage Create() => new GridCells();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "nav_msgs/GridCells";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
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
