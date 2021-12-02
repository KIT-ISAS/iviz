/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public GridMap()
        {
            Info = new GridMapInfo();
            Layers = System.Array.Empty<string>();
            BasicLayers = System.Array.Empty<string>();
            Data = System.Array.Empty<StdMsgs.Float32MultiArray>();
        }
        
        /// Explicit constructor.
        public GridMap(GridMapInfo Info, string[] Layers, string[] BasicLayers, StdMsgs.Float32MultiArray[] Data, ushort OuterStartIndex, ushort InnerStartIndex)
        {
            this.Info = Info;
            this.Layers = Layers;
            this.BasicLayers = BasicLayers;
            this.Data = Data;
            this.OuterStartIndex = OuterStartIndex;
            this.InnerStartIndex = InnerStartIndex;
        }
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new GridMap(ref b);
        
        GridMap IDeserializable<GridMap>.RosDeserialize(ref Buffer b) => new GridMap(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Info.RosSerialize(ref b);
            b.SerializeArray(Layers);
            b.SerializeArray(BasicLayers);
            b.SerializeArray(Data);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "grid_map_msgs/GridMap";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "95681e052b1f73bf87b7eb984382b401";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XbU/cRhD+7l8x4j6Eo3CBI0INEh9Qo6SRgpQ2kaoKoWOxx74F2+vsrjmcX99n1q8X" +
                "UtEPhdOJs3dnZp955mWHGX2wOqFCVbRmlbCN5P1CVR/L1JDGnyiajTK5athSqQp2i8h5q8vs8qpddVuC" +
                "N8rpeCpOu6by2pQqny/o65qnEg6aCXu2hS6ZNmsdr7sNSq0p6Lp9uaaSOSFv6IbpXuU6gZ4uyVjgptRY" +
                "UhRznpNJyeOErAcz0ZigDgBWP8GeKK9ELlkVLnOv3+dG+ePlRZ17fW6taqArIqLzp9mQ88p64Ej4gXYT" +
                "ThUE6XC+iGpd+qMTMjU8WwWpVZASxd9MXhflk7q6LH/UPfufP9HFlw+ngasVnG9dnuQAsP4eEoN2vS6Y" +
                "VJkgKIjoPOrWu7wRNtjBLYnyVggui9cSlqtFlAqVJ2/IDoKi9onLzK8lkg8HibYcBwuXxUQhDyKrh23x" +
                "5gnxRsQ/G8ePMiLmEkERG7Ic/EEGpki/RBavW5+uW6sZm4K9bVpugr0Kf54pEkPetfTCgy8epCsL5OyV" +
                "ZF7I9bXO1mwPcr7nXPKoqIA97PqmkvKcocy0I3wzRhapPG+odm0BxaYo6lLHyjNJWLf026pSVCHrdFzn" +
                "ykIeRaZLEQ9kiXV8HX+ruYyZPr47hUzpOEZUAaiBhdgyKqzMsEkhn4+XohDNvm7MAV45QwSGwxEIhUpw" +
                "xA8V0kNwKneKM/Za5xawDXIYpyRoJmFthVc3JxwCCFwZtI1dIP/c+LVpI3uvrFY3OYvhGAzA6itRejWf" +
                "WBbYp+hSpenNtxbHM/6L2XKwKz4drBGzXLx3dQYCIVhZc68TiN40wUica2Qh5frGKttEobrCkdHsfUhI" +
                "L+ELEcGvcs7EGgFIaKP9umtjbTRW6ITP1Rce5T4cPEcBS5AAX/XVLhUhaZNahhuVinlfskyWk25fB1lp" +
                "IMbqXndB0WeDbBgEoj9qeGnLYHeUeykHAaWvHOSCV7p0IVoDfviC0giQt9wdms/D8NQMT99fBv5IXe/D" +
                "EChk0Baf2+Dl7dvIO/pLsYie8Kh/2jyXb/9+A0tbz9FdmHJj7giNQ0I07n9SDW5d9EvnVNZ19jZ68EyG" +
                "DRPXxZi9SEpMDYWoK1FH84weGZOJJPy2HzS/imOdSg/t0iKUaivVknO8xKjQf8Lu8JlROKlXi56dwx/9" +
                "CQnCE6fBUoxWj0lNtRcGprOwi0sWXLkwu42oFQxMLojh4lkQvevlYcpyO56hCYRRLgxDVBgnAHARhQEn" +
                "vG9xPpiQWUsXoOt8oKvfkoZaMRCw668XQbEyaep4EqdKJbi6MuKcJeYA5QUL6ncgH+bjGMliMHG6tanz" +
                "hM4//XX+9xeZGzdWe8+hYGREddsgpA8n0hSlr3UpIR1P/DwQvyayqbbiZ7g6R+J39f7t/t2czgKYy6kP" +
                "v4jyqj3i8uhqT2+vLK/2brFydxXNQld23aCwT8cHMS6gEpPByZvDhze/HpIupBLk6oAjwIbyuQfO2OSY" +
                "JTphmcM3wXu4PfoS7mGpGl1cHl4tcnUDu4C7s2bMIH5n3HL6O4PzM8KJk9WAFqvHe0CzJ2jO6O3y6OTw" +
                "kGi3NBgqWsmOTLnubmswF8yB7YBdblYRO5oi2OjEr3fGnQEADpqsbgHA79HbZb+9nJrreNgZ9waDx5O1" +
                "wVyg5XEkLafyvwvSW9qSUG7NZp9u8RCHuX8/ZMudvLcnLl6w/ofa6meIzn+USvsExjMMceWYucP41rIh" +
                "za8LzQ+CYVCSNkCYLr2bD4otZaLYPv3kjH8A5LyRjIsOAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
