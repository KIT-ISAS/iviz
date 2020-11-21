/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract (Name = "grid_map_msgs/GridMap")]
    public sealed class GridMap : IDeserializable<GridMap>, IMessage
    {
        // Grid map header
        [DataMember (Name = "info")] public GridMapInfo Info { get; set; }
        // Grid map layer names.
        [DataMember (Name = "layers")] public string[] Layers { get; set; }
        // Grid map basic layer names (optional). The basic layers
        // determine which layers from `layers` need to be valid
        // in order for a cell of the grid map to be valid.
        [DataMember (Name = "basic_layers")] public string[] BasicLayers { get; set; }
        // Grid map data.
        [DataMember (Name = "data")] public StdMsgs.Float32MultiArray[] Data { get; set; }
        // Row start index (default 0).
        [DataMember (Name = "outer_start_index")] public ushort OuterStartIndex { get; set; }
        // Column start index (default 0).
        [DataMember (Name = "inner_start_index")] public ushort InnerStartIndex { get; set; }
    
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
        public GridMap(ref Buffer b)
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
                size += 4 * Layers.Length;
                foreach (string s in Layers)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * BasicLayers.Length;
                foreach (string s in BasicLayers)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                foreach (var i in Data)
                {
                    size += i.RosMessageLength;
                }
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
                "H4sIAAAAAAAACr1XbU8bRxD+bon/MIIPAQoOmAg1SHxAjZJWSqS0iVRVCJnlbmwv3N1edtcY59f3mdl7" +
                "M6RKPpRYgO92Z2afZ9522KF33uZUmpoWbHL2WyNZ+GDqP6qZI4s/W6Ot0U4vVpg1e6pMyWG8NQrR22p+" +
                "eZWWwyPZGxNsNtSgXVdH6ypT7I3p84KHElDeoZwj+9JWTKuFzRbNDs28K+k6vVxTxZxTdHTDdG8Km4ui" +
                "rch54KeZ82Qo46IgN6OIM+YtnIHKELpimH6TQG6iUdF8WoZ5ePm2cCaeTD4si2gvvDdrqItMUvvLrShE" +
                "4yPQ5PxAuznPDETpaA9GlraKx6fklmA4VbGpiiXd31yxLKvvq9uqeqp+/j9/tkYfPr07U8dN4YbEfZAX" +
                "Avh3TRfajbZkMlWOGCHCe1ujZqNNJ3UMB9CTwG/E5LJ8KXG6AruZOPb0FflOMmm+52oeFxLdh8Pces7U" +
                "yGU51ClUZvrwWGP9PY110vjoAj/JlYwrxEnMyLJyQ3LOkJm5LF4neteN4Tm7kqNfJ0+pwRp/ni0yXUIm" +
                "ZwuLTxFBMB7oORrJSa2EhZ0v2B8WfM+FJFdZA7/uxnWtFbyDOrSB8DNnpJYpijUtQyqwzJXlsrKZiUwS" +
                "5w0DTdUZqpGLNlsWxkMBRWgrkVeXqX35DfxlyVXG9MebM0hVgTNEGaDWsJF5RglWc2xSSvSTiWhA8fPK" +
                "HeKd54hFhwAhMSiSQPxQI2EErAlncsx+4jiGeTiJcVCOnqNrU7yGPcI5QMG1Q3PZBfyP67hwKcj3xltz" +
                "U7BYzuAHmH0hSi/2hqYF+hm6WeVa+8lkf8iP2K16w0LrcIHgFeKCsJzDj5Csvbu3OWRv1molKyxSkgp7" +
                "441H5mrd6aEw8lbzM0ogNTb4NiG4zCISOa1sXLT9LsVlKk3z+frGk2oQnhcobgkXWJi2FUiRSBLNPINM" +
                "bTI+kKST5bzZtyorDcZ52+qOkScfHRKjk9ga/bkEWV+p5V7yJ9IEnK6ckBnR2Cpo6DoWYIRyUdwbpPvO" +
                "hCbWPiLG7ePXn8aid2JHpYsacmrDtZsc5O1LHwI0nxLV/wPM2sfVM5L87wtcL4AC/YepcO6O0FgkZL3A" +
                "e7PGlY2uGoKZN3dAiiYo6sjismXZJzVyFaNHKfpG9KXHAsVjezLZ6Hf6oEPWnNmZ9NomU7SQk1TjpJMJ" +
                "xo32o9vdZ4f0sFZPjnx+Zz4mlVKGB+Thrgz3AgY/k64XDHu6i6sZPgs6CvbQgXtneJ10F9WY6E2rAFue" +
                "07CHJqGToc5UVLqgEHBx6ZSkC4+831mRwc2W8NtF57d2S1pvzQDBobuNBMnUzWaBByGrTY7Lbk5csCQA" +
                "gEXBg+oehAEnZBmSx2GMDQu3LHK6eP/3xT+fZBZdeRsjayXJ3Bs2cUjHzrV3SvtrEkQao9A9FHYD4Zn1" +
                "yjbdt30Idu3B7cHdHp0rosshkV9EfZpOuTy+2rebK5Or/Vus3F3BYGrgoZkxDujkMMOVVWGoOH119PDq" +
                "1yOypZSHXDVgA3yoqXtgzVyBMaQR1hF/pT4A+Z5Qur21mGx5eXQ1LswNTAPz9oIxwcTtwV6wXxnuPyec" +
                "OlxW0Fg+2QekfYF0Tq8nx6dHR0S7lcNIkiQbt8odebuEC9Ue/K4E9EYWueMhiJXN42J7sNVhwFHD5Q0M" +
                "+D5+Pen2J0OLjUO2B5udzZPhYmexcdDTwHqeyb9IyHppXOJ/71YHdIuHTP+pOND0uZP3dOr45zaHN22O" +
                "djNI4weUUHqC9+cYBqs+nfsxMHlFGmQTp0eSOm1JjyBMqjEggK1mcp1opqdvnfIvCkOTAwAPAAA=";
                
    }
}
