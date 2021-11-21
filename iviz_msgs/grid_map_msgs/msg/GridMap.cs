/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [Preserve, DataContract (Name = "grid_map_msgs/GridMap")]
    public sealed class GridMap : IDeserializable<GridMap>, IMessage
    {
        // Grid map header
        [DataMember (Name = "info")] public GridMapInfo Info;
        // Grid map layer names.
        [DataMember (Name = "layers")] public string[] Layers;
        // Grid map basic layer names (optional). The basic layers
        // determine which layers from `layers` need to be valid
        // in order for a cell of the grid map to be valid.
        [DataMember (Name = "basic_layers")] public string[] BasicLayers;
        // Grid map data.
        [DataMember (Name = "data")] public StdMsgs.Float32MultiArray[] Data;
        // Row start index (default 0).
        [DataMember (Name = "outer_start_index")] public ushort OuterStartIndex;
        // Column start index (default 0).
        [DataMember (Name = "inner_start_index")] public ushort InnerStartIndex;
    
        /// <summary> Constructor for empty message. </summary>
        public GridMap()
        {
            Info = new GridMapInfo();
            Layers = System.Array.Empty<string>();
            BasicLayers = System.Array.Empty<string>();
            Data = System.Array.Empty<StdMsgs.Float32MultiArray>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridMap(GridMapInfo Info, string[] Layers, string[] BasicLayers, StdMsgs.Float32MultiArray[] Data, ushort OuterStartIndex, ushort InnerStartIndex)
        {
            this.Info = Info;
            this.Layers = Layers;
            this.BasicLayers = BasicLayers;
            this.Data = Data;
            this.OuterStartIndex = OuterStartIndex;
            this.InnerStartIndex = InnerStartIndex;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GridMap(ref Buffer b)
        {
            Info = new GridMapInfo(ref b);
            Layers = b.DeserializeStringArray();
            BasicLayers = b.DeserializeStringArray();
            Data = b.DeserializeArray<StdMsgs.Float32MultiArray>();
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = new StdMsgs.Float32MultiArray(ref b);
            }
            OuterStartIndex = b.Deserialize<ushort>();
            InnerStartIndex = b.Deserialize<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GridMap(ref b);
        }
        
        GridMap IDeserializable<GridMap>.RosDeserialize(ref Buffer b)
        {
            return new GridMap(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Info.RosSerialize(ref b);
            b.SerializeArray(Layers, 0);
            b.SerializeArray(BasicLayers, 0);
            b.SerializeArray(Data, 0);
            b.Serialize(OuterStartIndex);
            b.Serialize(InnerStartIndex);
        }
        
        public void RosValidate()
        {
            if (Info is null) throw new System.NullReferenceException(nameof(Info));
            Info.RosValidate();
            if (Layers is null) throw new System.NullReferenceException(nameof(Layers));
            for (int i = 0; i < Layers.Length; i++)
            {
                if (Layers[i] is null) throw new System.NullReferenceException($"{nameof(Layers)}[{i}]");
            }
            if (BasicLayers is null) throw new System.NullReferenceException(nameof(BasicLayers));
            for (int i = 0; i < BasicLayers.Length; i++)
            {
                if (BasicLayers[i] is null) throw new System.NullReferenceException($"{nameof(BasicLayers)}[{i}]");
            }
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] is null) throw new System.NullReferenceException($"{nameof(Data)}[{i}]");
                Data[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Info.RosMessageLength;
                size += BuiltIns.GetArraySize(Layers);
                size += BuiltIns.GetArraySize(BasicLayers);
                size += BuiltIns.GetArraySize(Data);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "grid_map_msgs/GridMap";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "95681e052b1f73bf87b7eb984382b401";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XbU8bORD+vhL/YUQ+lHCQQqjQFYkP6Kr2KrUSd610OiEUzO4kMezaW9shpL++z9j7" +
                "lnKn3ocrESK79sz4mWdePBnRO6cLqlRNS1YFu0zeP6r6vZlb0viXZaNeplQbdmRUxX6S+eC0WVxdp1W/" +
                "JXirvM6H4rRn66CtUeV4Qp+XPJTw0Cw4sKu0YVovdb5sNmjubEU36eWGDHNBwdIt04MqdQE9bcg64Ka5" +
                "daQo57IkO6eAExYtmIHGAHUEMPsH7IUKSuSKWeUX/uXb0qpwMv24KoO+cE5toCsiovOnXZMPygXgKPiR" +
                "9gqeKwjS0XiSrbQJx6dkV/BsFqVmUUoUf7PlqjI/1NXGfKe7k53/z5+d7OOnd2eRrRncT04PsmAHcH+P" +
                "uUF7QVdMyhSIC4I6zpr1JnWEEPbwTAK9FYWr6qVE5nqSzYXN01fkOkFR+8BmEZYSzMfDQjvOo4WraqBQ" +
                "RpHZ47b45gfiGxG/tJ6fJEXOBnERG7Ic/UESzpGBhSzeJJ9uktUF24qD2yRyor0a/35aMLrkSwRLCD4F" +
                "8K4cwHNQkn8x45d6sWR3WPIDl5JNVQ34cTdsainSEYpNe8LfgpFLqiw3tPKpjHJbVSujcxWYJLJb+qm2" +
                "FNXIPZ2vSuUgj1LTRsQjX2Idf56/rNjkTO/fnEHGeM4RWADawELuGHVmFtikmNUnU1HIRp/X9hCvvEAQ" +
                "usMRC4V68MSPNTJEcCp/hjP2k3MT2AY7jFMKtJS4NsOrHxMOAQSuLZrHHpBfbsLSpuA+KKfVbcliOAcD" +
                "sPpClF6MB5YF9hl6lbGt+WSxP+O/mDWdXfHpcImYleK9Xy1AIARrZx90AdHbTTSSlxqJSKW+dcptslhg" +
                "8chs9DbmZJDwxYjgW3lvc40AFLTWYdk0sxSNGfrhz+sOTwpAcvICZSxxggeqrXmpC8mcuWN4UqucDyTR" +
                "ZLlo9nWUlTZinW51J5RdWiREJ5D9sYKjzkS7vdzz+QgwO239ICOC0sbHmHUuwB0USES95XHXhR67p033" +
                "9PW5POj569zowoVU2mJ1G7+8fenZR6OpJtkPnGqf1j/PvX+/ksXByxKdhqm09p7QRCRQvcAHtcE9jN7p" +
                "vVo0jT7FEM7J+GHzVdWnMbITc0Ql6krU0UizJ8ZkRonf6YNGWHOu59JPm+SIZZukEj8nUwwP7Sfudp8R" +
                "xZNatewZaPzeo5QmPPAbROXo/BjfVLo/MLLFXVy7oMvHga4HrmBgcF9099CE6E0rD1OO08yGhhDnuzgh" +
                "UWV9kL4pY6dp3rdo70zIAKYrMHbRMdZuSX+tGQjYt7eNoJjZ+dzzIFS1KnCTLYhLlrADVBAsKOSOf5jP" +
                "c+SLxRjql3ZVFnTx4a+Lvz/JMLl2OgSOZSNzq98GIW25YFiQHtdkhXQ/8fNQ/BrIzrUTP+NN2hO/pw/u" +
                "Du7HdB7BXA19+EWUZ+mIq+Prfb29Mr3ev8PK/XU2ih3aN3PDAZ0c5riPDAaF01dHj69+PSJdSTHITQJH" +
                "gA0V9ACcuS0xWjTCMpyvo/dwu/clXstSOLq6OrqelOoWdgF3d8kYScJuv+X1Vwbn54QTB6sRLVZP9oFm" +
                "X9Cc0+vp8enREdGesZgxkmRDptx+dyswF82B7YhdLloROx4iWOsiLHf7nQ4ADhqsbgHA9/Hrabs9HZpr" +
                "eNjt9zqDJ4O1zlyk5WkkHc/lBw3SWzqTUO7s+oDu8JDHHwMHMVvu5T2dOHnWFvCmzciddqpoKEC1pCeQ" +
                "vsBYZ/rk7Qa6RIi0wCY63wnG0Uk6AWHeDH7cKSbWRDE9PVXdyb4Bl8WmIqQOAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
