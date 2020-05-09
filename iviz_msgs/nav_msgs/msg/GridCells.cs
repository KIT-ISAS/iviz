using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class GridCells : IMessage
    {
        //an array of cells in a 2D grid
        public std_msgs.Header header { get; set; }
        public float cell_width { get; set; }
        public float cell_height { get; set; }
        public geometry_msgs.Point[] cells { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GridCells()
        {
            header = new std_msgs.Header();
            cells = System.Array.Empty<geometry_msgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridCells(std_msgs.Header header, float cell_width, float cell_height, geometry_msgs.Point[] cells)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.cell_width = cell_width;
            this.cell_height = cell_height;
            this.cells = cells ?? throw new System.ArgumentNullException(nameof(cells));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GridCells(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.cell_width = BuiltIns.DeserializeStruct<float>(b);
            this.cell_height = BuiltIns.DeserializeStruct<float>(b);
            this.cells = BuiltIns.DeserializeStructArray<geometry_msgs.Point>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GridCells(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            BuiltIns.Serialize(this.cell_width, b);
            BuiltIns.Serialize(this.cell_height, b);
            BuiltIns.SerializeStructArray(this.cells, b, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (cells is null) throw new System.NullReferenceException();
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
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "nav_msgs/GridCells";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
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
