/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GridCells : IDeserializable<GridCells>, IMessage
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
        internal GridCells(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            CellWidth = b.Deserialize<float>();
            CellHeight = b.Deserialize<float>();
            Cells = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GridCells(ref b);
        
        GridCells IDeserializable<GridCells>.RosDeserialize(ref Buffer b) => new GridCells(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(CellWidth);
            b.Serialize(CellHeight);
            b.SerializeStructArray(Cells);
        }
        
        public void RosValidate()
        {
            if (Cells is null) throw new System.NullReferenceException(nameof(Cells));
        }
    
        public int RosMessageLength => 12 + Header.RosMessageLength + 24 * Cells.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/GridCells";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b9e4f5df6d28e272ebde00a3994830f5";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTWvcMBC961cM7CFJYVNISw8LvYV+HAqB5FbKMpFm7QFZcqXxbtxf3yebbBvooYfW" +
                "GGRLb968eTPacCIuhWfKB/ISYyXFDt3cUlc0uE/CQQr1y+IOMbO9uVmA+5MG619u9aJdb66TPIiVeT/U" +
                "rr6+y5rs67eV3bn3//hxX+4/7qhaWLOtgt2G7o1T4BIIUjiwMR0yCoFAKdsoR4kI4mGUQMupzaPUawQ+" +
                "9AoTKnWSpHCMM00VIMvk8zBMST2bkOkgL+IRuTg3cjH1U+QCfC5BU4MfCg/S2PFW+T5J8kKfb3fApCp+" +
                "MoWgGQy+CFdNHQ7JTXAO3iLAbR5OeYtf6dCOc3Kynq2JlaexSG06ue6Q49Va3DW4YY4gS6h0uezt8Vuv" +
                "CEkgQcbse7qE8rvZ+pxAKHTkovwYpRF7OADWixZ0cfUbc5O9o8QpP9OvjL9y/A1tOvO2mrY9ehZb9XXq" +
                "YCCAY8lHDYA+zguJjyrJKOpj4TK7FrWmdJsPzWOAELV0BCvXmr2iAYFOimmtVhr70o09xvs/TeMfLsDz" +
                "YMEqY011KWbMVU1hDy4fJqfh2hAdiqCokb2s1+vdW3o6f83nrx/O/QRCrC0zvwMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
