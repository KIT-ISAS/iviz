namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GridMap : IMessage
    {
        // Grid map header
        public GridMapInfo info;
        
        // Grid map layer names.
        public string[] layers;
        
        // Grid map basic layer names (optional). The basic layers
        // determine which layers from `layers` need to be valid
        // in order for a cell of the grid map to be valid.
        public string[] basic_layers;
        
        // Grid map data.
        public std_msgs.Float32MultiArray[] data;
        
        // Row start index (default 0).
        public ushort outer_start_index;
        
        // Column start index (default 0).
        public ushort inner_start_index;
    
        /// <summary> Constructor for empty message. </summary>
        public GridMap()
        {
            info = new GridMapInfo();
            layers = System.Array.Empty<string>();
            basic_layers = System.Array.Empty<string>();
            data = System.Array.Empty<std_msgs.Float32MultiArray>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            info.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out layers, ref ptr, end, 0);
            BuiltIns.Deserialize(out basic_layers, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out data, ref ptr, end, 0);
            BuiltIns.Deserialize(out outer_start_index, ref ptr, end);
            BuiltIns.Deserialize(out inner_start_index, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            info.Serialize(ref ptr, end);
            BuiltIns.Serialize(layers, ref ptr, end, 0);
            BuiltIns.Serialize(basic_layers, ref ptr, end, 0);
            BuiltIns.SerializeArray(data, ref ptr, end, 0);
            BuiltIns.Serialize(outer_start_index, ref ptr, end);
            BuiltIns.Serialize(inner_start_index, ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 24;
            size += info.GetLength();
            for (int i = 0; i < layers.Length; i++)
            {
                size += layers[i].Length;
            }
            for (int i = 0; i < basic_layers.Length; i++)
            {
                size += basic_layers[i].Length;
            }
            for (int i = 0; i < data.Length; i++)
            {
                size += data[i].GetLength();
            }
            return size;
        }
    
        public IMessage Create() => new GridMap();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "grid_map_msgs/GridMap";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "95681e052b1f73bf87b7eb984382b401";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE71X224bNxB9368Y2A+xXFux5cBoAuTBaJA0QAykTYCiMAyZ2h1pae+SG5KyrHx9z5B7" +
                "U5wifagjCNYuOTM8c+bC8T69c7qgWjVUsirYZfJ+qZr3ZmlJ40+W7Q8yldqyI6Nq9tPMB6fN6uo6rfod" +
                "wYXyOh+L04FtgrZGVZMpfS55LOGhWXBgV2vDtCl1XrYbtHS2ppv0ckOGuaBgacF0rypdQE8bsg64aWkd" +
                "Kcq5qsguKeCEVQdmpDFCHQHMv4O9UEGJXDGv/co/f1tZFc5ml+sq6Avn1Ba6IiI6f9oN+aBcAI6CH+ig" +
                "4KWCIJ1Mptlam3B6TnYNz+ZRah6lRPE3W61r80Ndbcy3uq//5092+endq8jVHM4nl0c5AKy/x8Sgg6Br" +
                "JmUKBAURnWTteps3wgZ7uCVR3gnBVf1cwnI9zZZC5fkLcr2gqH1gswqlRPLhuNCO82jhqh4pVFFk/rAr" +
                "vv2B+FbEP1rPjzIiZ4OgiA1Zjv4gA5dIv0IWb5JPN8nqim3NwW0TN9Fegz9PFIk+7xK98OBTAOnKATkH" +
                "JZkXc73Uq5LdccX3XEke1Q2wx92wbaQ891Fm2hO+K0YWqara0tqnAsptXa+NzlVgkrDu6KeqUtQg63S+" +
                "rpSDPIpMGxGPZIl1fD1/WbPJmd6/eQUZ4zlHVAFoCwu5Y1SYWWGTYj6fzUQh2/+8scd45RUi0B+OQKgg" +
                "YPmhQXoITuVf4YzD5NwUtkEO45QCzSSuzfHqJ4RDAIEbi7ZxAOQft6G0KbL3ymm1qFgM52AAVp+J0rPJ" +
                "yLKJpo0ytjOfLA5n/BezprcrPh2XiFkl3vv1CgRCsHH2XhcQXWyjkbzSyEKq9MIpt81idcUjs/23MSGD" +
                "hC9GBL/Ke5trBKCgjQ5l28ZSNObohE/VFx7lPhy8QAFLkABfddUuFSFps3QMNxqV85FkmSwX7b6OstJA" +
                "rNOd7pSyjxbZ0Atkf6zhpTPR7iD3sxwElK5ykAtBaeNjtHr88AWlESHvuNs3n4f+ads/ff058AfqOh/6" +
                "QCGDdvjcBS9vXwbe0V/qafYDj7qnzVP59u83sLT1Ct2FqbL2jtA4JETD/ge1xa2Lfum9WrWdPUUPnsmw" +
                "YfN1PWQvkhJTQy3qStTRPLNHxmQiib/pg+bXcK6X0kPbtIilmqQSOWczjArdJ+7SoB5P6tSyJ+fwW39i" +
                "gvDIabCUo9VjUlPpwsB0FndxyYIrH2e3AbWCgdEF0V88U6I3nTxMOU7jGRdplIvDENXWC4Bg04AT33c4" +
                "703IrKVr0HXR09VtSUNtGAjYd9eLoJjb5dLzKE6NKgpplVxxnQohCBbUb08+zOc5ksVi4vSlXVcFXXz4" +
                "6+LvTzI3bpwOgWPByIjqd0FIHy6kKUpfa1NCOp74eSx+jWSX2omf8eociD/QR7dHdxN6HcFcjX34RZTn" +
                "6Yir0+tDvbsyuz68xcrddbYfu7JvB4UjOjvOcQEZTAbnL04eXvx6QrqWSpCrg+TudSife+DMbYVZohWW" +
                "OXwTvV/wyJd4D0vV6Prq5HpaqQXsAu5eyZhBwt6w5fVXJtnCiaPViBarZ4dAcyhoXtPL2en5yQnRgbEY" +
                "KpJkS6Zcd7drMBfNge2IfdIaPB0j2OgilHvDTg8AB41WdwDg9/TlrNuejc21POwNe73Bs9Faby7S8jiS" +
                "jpfyvwvSW9qSUO7s5ohu8ZDHuf8oZsudvKcTpz+x/vva6maI1n+USnoC4ysMcWbI3H58S2xI82tD841g" +
                "HJSkDRCmy+AnvWKiTBTT03fO+AfkvJGMiw4AAA==";
                
    }
}
