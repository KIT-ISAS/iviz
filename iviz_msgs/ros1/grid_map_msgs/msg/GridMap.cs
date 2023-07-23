/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class GridMap : IHasSerializer<GridMap>, IMessage
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
    
        public GridMap()
        {
            Info = new GridMapInfo();
            Layers = EmptyArray<string>.Value;
            BasicLayers = EmptyArray<string>.Value;
            Data = EmptyArray<StdMsgs.Float32MultiArray>.Value;
        }
        
        public GridMap(ref ReadBuffer b)
        {
            Info = new GridMapInfo(ref b);
            Layers = b.DeserializeStringArray();
            BasicLayers = b.DeserializeStringArray();
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.Float32MultiArray[] array;
                if (n == 0) array = EmptyArray<StdMsgs.Float32MultiArray>.Value;
                else
                {
                    array = new StdMsgs.Float32MultiArray[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new StdMsgs.Float32MultiArray(ref b);
                    }
                }
                Data = array;
            }
            b.Deserialize(out OuterStartIndex);
            b.Deserialize(out InnerStartIndex);
        }
        
        public GridMap(ref ReadBuffer2 b)
        {
            Info = new GridMapInfo(ref b);
            b.Align4();
            Layers = b.DeserializeStringArray();
            b.Align4();
            BasicLayers = b.DeserializeStringArray();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                StdMsgs.Float32MultiArray[] array;
                if (n == 0) array = EmptyArray<StdMsgs.Float32MultiArray>.Value;
                else
                {
                    array = new StdMsgs.Float32MultiArray[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new StdMsgs.Float32MultiArray(ref b);
                    }
                }
                Data = array;
            }
            b.Align2();
            b.Deserialize(out OuterStartIndex);
            b.Deserialize(out InnerStartIndex);
        }
        
        public GridMap RosDeserialize(ref ReadBuffer b) => new GridMap(ref b);
        
        public GridMap RosDeserialize(ref ReadBuffer2 b) => new GridMap(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Info.RosSerialize(ref b);
            b.Serialize(Layers.Length);
            b.SerializeArray(Layers);
            b.Serialize(BasicLayers.Length);
            b.SerializeArray(BasicLayers);
            b.Serialize(Data.Length);
            foreach (var t in Data)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(OuterStartIndex);
            b.Serialize(InnerStartIndex);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Info.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Layers.Length);
            b.SerializeArray(Layers);
            b.Align4();
            b.Serialize(BasicLayers.Length);
            b.SerializeArray(BasicLayers);
            b.Align4();
            b.Serialize(Data.Length);
            foreach (var t in Data)
            {
                t.RosSerialize(ref b);
            }
            b.Align2();
            b.Serialize(OuterStartIndex);
            b.Serialize(InnerStartIndex);
        }
        
        public void RosValidate()
        {
            Info.RosValidate();
            BuiltIns.ThrowIfNull(Layers, nameof(Layers));
            BuiltIns.ThrowIfNull(BasicLayers, nameof(BasicLayers));
            BuiltIns.ThrowIfNull(Data, nameof(Data));
            foreach (var msg in Data) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 16;
                size += Info.RosMessageLength;
                size += WriteBuffer.GetArraySize(Layers);
                size += WriteBuffer.GetArraySize(BasicLayers);
                foreach (var msg in Data) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Info.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Layers);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, BasicLayers);
            size = WriteBuffer2.Align4(size);
            size += 4; // Data.Length
            foreach (var msg in Data) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align2(size);
            size += 2; // OuterStartIndex
            size += 2; // InnerStartIndex
            return size;
        }
    
        public const string MessageType = "grid_map_msgs/GridMap";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "95681e052b1f73bf87b7eb984382b401";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XbU8bORD+vr9iBB9KOEghVOhaqR/QVe1VaqW+SadThIKzO0kMu/bWdgjpr79n7H0L" +
                "9MR9OBohsmvPjJ955sWTfXrndEGVqmnFqmCXyftHVb83C0sa/7Jsv5cp1ZYdGVWxH2c+OG2W08u06ncE" +
                "58rrfChOB7YO2hpVjsb0bcVDCQ/NggO7ShumzUrnq2aDFs5WdJVersgwFxQszZluVakL6GlD1gE3Lawj" +
                "RTmXJdkFBZywbMEMNAaoI4DZT7AXKiiRK2aVX/rnb0urwtnk47oM+sI5tYWuiIjOF7shH5QLwFHwHR0U" +
                "vFAQpJPROFtrE07Pya7h2SxKzaKUKP5hy3VlHtXVxtzXff0/f7KPX9+9ilzN4HxyeZADwPpnTAw6CLpi" +
                "UqZAUBDRUdasN3kjbLCHWxLlnRBMq+cSlstxthAqz1+Q6wRF7QObZVhJJO+OC+04jxam1UChjCKzu13x" +
                "7SPiWxH/ZD0/yIicDYIiNmQ5+oMMXCD9Clm8Sj5dJatLthUHt03cRHs1/j1RJLq8S/TCg68BpCsH5ByU" +
                "ZF7M9ZVertgdl3zLpeRRVQN73A3bWspzH2WmPeFvycgiVZZbWvtUQLmtqrXRuQpMEtYd/VRVimpknc7X" +
                "pXKQR5FpI+KRLLGOP8/f12xypvdvXkHGeM4RVQDawkLuGBVmltikmM9nE1GgfZp+sf70Mtv/trHHWOcl" +
                "QtGhQERUENR8VyNPBLDyr3DYYfJyjEPAEuO4Al0lrs3w6keE04CFa4v+cQAXPm3DyqYQ3yqn1bxkMZyD" +
                "Clh9JkrPRgPLJpo2ytjWfLLYn/FfzJrOrvh0vELwSqHBr5dgEoK1s7e6gOh8G43kpUY6UqnnTrltFsss" +
                "Hpntv42ZGSSOMTT4Vt7bXCMSBW10WDX9LIVlhpb4VA3iQRHAwQtUsgQJ8FVb9lIakj8Lx3CjVjkfSbrJ" +
                "ctHs6ygrncQ63eqOKftkkQ2dQPZ5DS+diXZ7uV/lIKC0JYRcCEobH6PV4YcvqJEIecfdrgvddU/b7unH" +
                "r4HfU9f60AUKGbTD5y54efve845GU42zRzxqnzZP5du/X8XS30u0GabS2htC45AQ9fsf1BbXLxqn92rZ" +
                "tPgUPXgmU4fN11WfvUhKjA+VqCtRRxfNHhiT0SR+pw+6YM25XkgzbdIilmqSSuScTTAztJ+4S716PKlV" +
                "y56cw/v+xAThgdNgKUfPx8im0s2BMS3u4rYFVz4OcT1qBQODm6K7gcZEb1p5mHKc5jQu0kwXpyKqrBcA" +
                "waZJJ77vcN6ZkKFLV6DroqOr3ZKGWjMQsG/vGUExs4uF50GcalUU0iq55CoVQhAsqN+OfJjPcySLxejp" +
                "V3ZdFnTx4a+Lv7/KALlxOgSOBSOzqt8FIX24kKYofa1JCel44uex+DWQXWgnfsY7tCf+QB9dH92M6HUE" +
                "Mx368Jsoz9IR09PLQ727Mrk8vMbKDe7U2JV9MzEc0dlxjgvIYEQ4f3Fy9+L3E9KVVIJcHSR3r0P53AJn" +
                "bksMFY2wDOSb6P2cB77Ee1iqRlfTk8txqeawC7h7K8YwEvb6La9/MMkWThysRrRYPTsEmkNB85peTk7P" +
                "T06IDozFdJEkGzLlurteg7loDmxH7KPG4OkQwUYXYbXX73QAcNBgdQcAvk9fTtrtydBcw8Nev9cZPBus" +
                "deYiLQ8j6XghP2KQ3tKWhHJnN0d0jYc8/gA4itlyI+/pxPEvrP+uttoZovEfpZKewPgS05zpM7eb4xIb" +
                "0vya0NwTjIOStAHCmBn8qFNMlIlievrJGf8AjriCxpQOAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GridMap> CreateSerializer() => new Serializer();
        public Deserializer<GridMap> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GridMap>
        {
            public override void RosSerialize(GridMap msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GridMap msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GridMap msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GridMap msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(GridMap msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<GridMap>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GridMap msg) => msg = new GridMap(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GridMap msg) => msg = new GridMap(ref b);
        }
    }
}
