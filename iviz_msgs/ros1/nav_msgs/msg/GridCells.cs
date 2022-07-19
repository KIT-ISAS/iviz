/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GridCells : IDeserializableRos1<GridCells>, IDeserializableRos2<GridCells>, IMessageRos1, IMessageRos2
    {
        //an array of cells in a 2D grid
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "cell_width")] public float CellWidth;
        [DataMember (Name = "cell_height")] public float CellHeight;
        [DataMember (Name = "cells")] public GeometryMsgs.Point[] Cells;
    
        /// Constructor for empty message.
        public GridCells()
        {
            Cells = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// Explicit constructor.
        public GridCells(in StdMsgs.Header Header, float CellWidth, float CellHeight, GeometryMsgs.Point[] Cells)
        {
            this.Header = Header;
            this.CellWidth = CellWidth;
            this.CellHeight = CellHeight;
            this.Cells = Cells;
        }
        
        /// Constructor with buffer.
        public GridCells(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out CellWidth);
            b.Deserialize(out CellHeight);
            b.DeserializeStructArray(out Cells);
        }
        
        /// Constructor with buffer.
        public GridCells(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out CellWidth);
            b.Deserialize(out CellHeight);
            b.DeserializeStructArray(out Cells);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GridCells(ref b);
        
        public GridCells RosDeserialize(ref ReadBuffer b) => new GridCells(ref b);
        
        public GridCells RosDeserialize(ref ReadBuffer2 b) => new GridCells(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(CellWidth);
            b.Serialize(CellHeight);
            b.SerializeStructArray(Cells);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(CellWidth);
            b.Serialize(CellHeight);
            b.SerializeStructArray(Cells);
        }
        
        public void RosValidate()
        {
            if (Cells is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + Header.RosMessageLength + 24 * Cells.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, CellWidth);
            WriteBuffer2.AddLength(ref c, CellHeight);
            WriteBuffer2.AddLength(ref c, Cells);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/GridCells";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTWvcMBC961cM7CFJYVNISw+B3kI/DoVAcitlmZVm7QFZcqXxbtxf3yebbBvooYfW" +
                "GGRL772ZeTPacCIuhWfKB/ISYyXFDt3cUVc0uE/CQQr1y+IOMbO9uVmAu5MG619u9aJdb66TPIiVeTfU" +
                "rr6+z5rs67dV3bn3//hxXx4+3lK1sEZbE3YbejBOgUsgpMKBjemQUQgSlLKNcpQIEg+jBFpObR6lXoP4" +
                "2CtMqNRJksIxzjRVgCyTz8MwJfVsQqaDvOCDuTg3cjH1U+QCfC5BU4MfCg/S1PFW+T5J8kKf726BSVX8" +
                "ZIqEZij4Ilw1dTgkN8E5eAuC2zye8ha/0qEd5+BkPVtLVp7GIrXlyfUWMV6txV1DG+YIooRKl8veDr/1" +
                "ihAEKciYfU+XyPx+tj4nCAoduSjvozRhDwegetFIF1e/KadFOnHKz/Kr4q8YfyObzrqtpm2PnsVWfZ06" +
                "GAjgWPJRA6D7eRHxUSUZRd0XLrNrrDWk23xoHgME1tIRrFxr9ooGBDopprVaaepLN3YY7/80jX+4AM+D" +
                "BauMNdWlmDFXNYU9uHyYnIZrQ3QogqJG9rJer3dv6en8NZ+/fjj3E0KsLTO/AwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
