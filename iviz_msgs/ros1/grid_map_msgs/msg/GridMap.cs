/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
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
    
        public GridMap()
        {
            Info = new GridMapInfo();
            Layers = System.Array.Empty<string>();
            BasicLayers = System.Array.Empty<string>();
            Data = System.Array.Empty<StdMsgs.Float32MultiArray>();
        }
        
        public GridMap(ref ReadBuffer b)
        {
            Info = new GridMapInfo(ref b);
            b.DeserializeStringArray(out Layers);
            b.DeserializeStringArray(out BasicLayers);
            {
                int n = b.DeserializeArrayLength();
                Data = n == 0
                    ? System.Array.Empty<StdMsgs.Float32MultiArray>()
                    : new StdMsgs.Float32MultiArray[n];
                for (int i = 0; i < n; i++)
                {
                    Data[i] = new StdMsgs.Float32MultiArray(ref b);
                }
            }
            b.Deserialize(out OuterStartIndex);
            b.Deserialize(out InnerStartIndex);
        }
        
        public GridMap(ref ReadBuffer2 b)
        {
            Info = new GridMapInfo(ref b);
            b.Align4();
            b.DeserializeStringArray(out Layers);
            b.Align4();
            b.DeserializeStringArray(out BasicLayers);
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Data = n == 0
                    ? System.Array.Empty<StdMsgs.Float32MultiArray>()
                    : new StdMsgs.Float32MultiArray[n];
                for (int i = 0; i < n; i++)
                {
                    Data[i] = new StdMsgs.Float32MultiArray(ref b);
                }
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
            b.SerializeArray(Layers);
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
            b.SerializeArray(Layers);
            b.SerializeArray(BasicLayers);
            b.Serialize(Data.Length);
            foreach (var t in Data)
            {
                t.RosSerialize(ref b);
            }
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
                if (Layers[i] is null) BuiltIns.ThrowNullReference(nameof(Layers), i);
            }
            if (BasicLayers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < BasicLayers.Length; i++)
            {
                if (BasicLayers[i] is null) BuiltIns.ThrowNullReference(nameof(BasicLayers), i);
            }
            if (Data is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] is null) BuiltIns.ThrowNullReference(nameof(Data), i);
                Data[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Info.RosMessageLength;
                size += WriteBuffer.GetArraySize(Layers);
                size += WriteBuffer.GetArraySize(BasicLayers);
                size += WriteBuffer.GetArraySize(Data);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Info.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Layers);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, BasicLayers);
            c = WriteBuffer2.Align4(c);
            c += 4; // Data.Length
            foreach (var t in Data)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align2(c);
            c += 2; // OuterStartIndex
            c += 2; // InnerStartIndex
            return c;
        }
    
        public const string MessageType = "grid_map_msgs/GridMap";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "95681e052b1f73bf87b7eb984382b401";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}
