/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GridCells : IDeserializable<GridCells>, IMessage
    {
        //an array of cells in a 2D grid
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "cell_width")] public float CellWidth;
        [DataMember (Name = "cell_height")] public float CellHeight;
        [DataMember (Name = "cells")] public GeometryMsgs.Point[] Cells;
    
        public GridCells()
        {
            Cells = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        public GridCells(in StdMsgs.Header Header, float CellWidth, float CellHeight, GeometryMsgs.Point[] Cells)
        {
            this.Header = Header;
            this.CellWidth = CellWidth;
            this.CellHeight = CellHeight;
            this.Cells = Cells;
        }
        
        public GridCells(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out CellWidth);
            b.Deserialize(out CellHeight);
            b.DeserializeStructArray(out Cells);
        }
        
        public GridCells(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out CellWidth);
            b.Deserialize(out CellHeight);
            b.DeserializeStructArray(out Cells);
        }
        
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
    
        public const string MessageType = "nav_msgs/GridCells";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTwWrcMBC96ysGfEhS2JSmpYeF3kKTHgqhyS2EZVYa2wJZcqXxbtyv75NNtg300ENr" +
                "DJal997MvBk1HIlz5plSS1ZCKOSxQ1fX1GXvzK2wk0z98jFtSKzvrxbg7uid9q+3evFdr6aTNIjmeTeU" +
                "rry9Sz7q49Oqbsynf/yYr/c3Wyrq1mhrwqahe+XoODtCKuxYmdqEQpCg5E2QgwSQeBjF0XKq8yjlEsSH" +
                "3sOEQp1EyRzCTFMBSBPZNAxT9JZVSP0gr/hgLs6NnNXbKXAGPmXnY4W3mQep6niLfJ8kWqEv11tgYhE7" +
                "qUdCMxRsFi4+djgkM8E5eAsCNfT4LZV3T6Z5OKYN9qVDX05ZkPasNWt5HrOUmjCXLYK9Wau8RBC4JAjn" +
                "Cp0vezv8lgtCNOQiY7I9naOEu1n7FCEodODseR+kCltYAdWzSjq7+E05LtKRY3qRXxV/xfgb2XjSrTVt" +
                "ejQvVBvK1MFJAMecDt4Bup8XERu8RKXg95nzbCprDWmaz9VsgMBaWoMvl5KsRyccHT3Gtmiu6ktbdpjz" +
                "/zSWf7gJLxMGq5R9LEsxYypePezBLcQIVVydpjYLihrZynrPPn6g59NqPq1+GPMTaZDOMMgDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
