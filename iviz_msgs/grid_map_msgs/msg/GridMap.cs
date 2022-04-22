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
        
        /// Constructor with buffer.
        public GridMap(ref ReadBuffer b)
        {
            Info = new GridMapInfo(ref b);
            Layers = b.DeserializeStringArray();
            BasicLayers = b.DeserializeStringArray();
            Data = b.DeserializeArray<StdMsgs.Float32MultiArray>();
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = new StdMsgs.Float32MultiArray(ref b);
            }
            b.Deserialize(out OuterStartIndex);
            b.Deserialize(out InnerStartIndex);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GridMap(ref b);
        
        public GridMap RosDeserialize(ref ReadBuffer b) => new GridMap(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
            if (Info is null) BuiltIns.ThrowNullReference();
            Info.RosValidate();
            if (Layers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Layers.Length; i++)
            {
                if (Layers[i] is null) BuiltIns.ThrowNullReference($"{nameof(Layers)}[{i}]");
            }
            if (BasicLayers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < BasicLayers.Length; i++)
            {
                if (BasicLayers[i] is null) BuiltIns.ThrowNullReference($"{nameof(BasicLayers)}[{i}]");
            }
            if (Data is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] is null) BuiltIns.ThrowNullReference($"{nameof(Data)}[{i}]");
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
