using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GridMap : IMessage
    {
        // Grid map header
        public GridMapInfo info { get; set; }
        
        // Grid map layer names.
        public string[] layers { get; set; }
        
        // Grid map basic layer names (optional). The basic layers
        // determine which layers from `layers` need to be valid
        // in order for a cell of the grid map to be valid.
        public string[] basic_layers { get; set; }
        
        // Grid map data.
        public std_msgs.Float32MultiArray[] data { get; set; }
        
        // Row start index (default 0).
        public ushort outer_start_index { get; set; }
        
        // Column start index (default 0).
        public ushort inner_start_index { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GridMap()
        {
            info = new GridMapInfo();
            layers = System.Array.Empty<string>();
            basic_layers = System.Array.Empty<string>();
            data = System.Array.Empty<std_msgs.Float32MultiArray>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridMap(GridMapInfo info, string[] layers, string[] basic_layers, std_msgs.Float32MultiArray[] data, ushort outer_start_index, ushort inner_start_index)
        {
            this.info = info ?? throw new System.ArgumentNullException(nameof(info));
            this.layers = layers ?? throw new System.ArgumentNullException(nameof(layers));
            this.basic_layers = basic_layers ?? throw new System.ArgumentNullException(nameof(basic_layers));
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
            this.outer_start_index = outer_start_index;
            this.inner_start_index = inner_start_index;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GridMap(Buffer b)
        {
            this.info = new GridMapInfo(b);
            this.layers = b.DeserializeStringArray(0);
            this.basic_layers = b.DeserializeStringArray(0);
            this.data = b.DeserializeArray<std_msgs.Float32MultiArray>(0);
            this.outer_start_index = b.Deserialize<ushort>();
            this.inner_start_index = b.Deserialize<ushort>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GridMap(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.info.Serialize(b);
            b.SerializeArray(this.layers, 0);
            b.SerializeArray(this.basic_layers, 0);
            b.SerializeArray(this.data, 0);
            b.Serialize(this.outer_start_index);
            b.Serialize(this.inner_start_index);
        }
        
        public void Validate()
        {
            if (info is null) throw new System.NullReferenceException();
            if (layers is null) throw new System.NullReferenceException();
            if (basic_layers is null) throw new System.NullReferenceException();
            if (data is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += info.RosMessageLength;
                size += 4 * layers.Length;
                for (int i = 0; i < layers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(layers[i]);
                }
                size += 4 * basic_layers.Length;
                for (int i = 0; i < basic_layers.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(basic_layers[i]);
                }
                for (int i = 0; i < data.Length; i++)
                {
                    size += data[i].RosMessageLength;
                }
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "grid_map_msgs/GridMap";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "95681e052b1f73bf87b7eb984382b401";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
