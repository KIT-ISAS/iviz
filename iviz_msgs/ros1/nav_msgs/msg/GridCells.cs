/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GridCells : IHasSerializer<GridCells>, IMessage
    {
        //an array of cells in a 2D grid
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "cell_width")] public float CellWidth;
        [DataMember (Name = "cell_height")] public float CellHeight;
        [DataMember (Name = "cells")] public GeometryMsgs.Point[] Cells;
    
        public GridCells()
        {
            Cells = EmptyArray<GeometryMsgs.Point>.Value;
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
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out CellWidth);
            b.Deserialize(out CellHeight);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.DeserializeStructArray(array);
                }
                Cells = array;
            }
        }
        
        public GridCells(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            b.Deserialize(out CellWidth);
            b.Deserialize(out CellHeight);
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Cells = array;
            }
        }
        
        public GridCells RosDeserialize(ref ReadBuffer b) => new GridCells(ref b);
        
        public GridCells RosDeserialize(ref ReadBuffer2 b) => new GridCells(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(CellWidth);
            b.Serialize(CellHeight);
            b.Serialize(Cells.Length);
            b.SerializeStructArray(Cells);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(CellWidth);
            b.Serialize(CellHeight);
            b.Serialize(Cells.Length);
            b.Align8();
            b.SerializeStructArray(Cells);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Cells, nameof(Cells));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += Header.RosMessageLength;
                size += 24 * Cells.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // CellWidth
            size += 4; // CellHeight
            size += 4; // Cells.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * Cells.Length;
            return size;
        }
    
        public const string MessageType = "nav_msgs/GridCells";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
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
    
        public Serializer<GridCells> CreateSerializer() => new Serializer();
        public Deserializer<GridCells> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GridCells>
        {
            public override void RosSerialize(GridCells msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GridCells msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GridCells msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GridCells msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(GridCells msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<GridCells>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GridCells msg) => msg = new GridCells(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GridCells msg) => msg = new GridCells(ref b);
        }
    }
}
