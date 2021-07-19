/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/DetectedARMarkerArray")]
    public sealed class DetectedARMarkerArray : IDeserializable<DetectedARMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public DetectedARMarker[] Markers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DetectedARMarkerArray()
        {
            Markers = System.Array.Empty<DetectedARMarker>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DetectedARMarkerArray(DetectedARMarker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DetectedARMarkerArray(ref Buffer b)
        {
            Markers = b.DeserializeArray<DetectedARMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new DetectedARMarker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DetectedARMarkerArray(ref b);
        }
        
        DetectedARMarkerArray IDeserializable<DetectedARMarkerArray>.RosDeserialize(ref Buffer b)
        {
            return new DetectedARMarkerArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Markers, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Markers)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/DetectedARMarkerArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "77cdae4e43f4daab309e1a8859b678c6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UXW/TShB9zv6KkfpAi9LABYREJR4qwuX2AdGWgoQqZG3sib0Xe9fsrpO6v54z68Qh" +
                "VSV4gEaWsl7POTNz5mPOkfPIxenle+2/sb/+Sk06BKVe/+Gfev/x3QmZlbnNmlCGJ/M7vtWij0xXX87f" +
                "ZqeXn958oNf09Ke7i8s3H+ZvcfmPUv+xLthTlf4Gm9i3rEL0xpaUu4JVya7h6PvB2We4cv759Yuv+Oot" +
                "EpxMDqg1N1xT64KJxllam1jRbfK7rJ2OL19cv4K9btjrzFghDyafCNK7NZT633ka7+94PHeBt1h4YKUW" +
                "ztVU6ZDxzQYyfNj42iifBXPLk+RETuCnprmPW7CZ51pHs+IsumxwNvlblQuxGLwP6qsD+hi1LbQvCKHp" +
                "QkdNSwhSmbJif1zzCtqGqJuWC0pfpUZhBuBVZQLhKRmV0HXdUxdgFB2K0zSdNbmWkpqG9/BAQg1NrfbR" +
                "5F2tPeydL4wV86VH/sKOJ/D3jm3OdDY/gY0NnHciEzwZm3vWQfrkbE6qQ/2ePxOAOrhau2O8coneGp1T" +
                "rHSUYPmm9RwkTh1O4OPxkNwM3BCH4aUIdJjuMryGI9RPQuDW5RUdIvLzPlZos1gxrbQ3elGzEOdQAKyP" +
                "BPTo6Cdmm6ittm5LPzDufPwOrR15JafjCjWrJfvQlRAQhq13K1PAdNEnkrw2bCPVZuG175WgBpfq4F/R" +
                "GEZApYrgX4fgcoMCFGl+tjOYqpGZ4m91473jvW0tz1IqJIHwaJW+SecsPSOTVuc8kyY5S2V1Fk3RsEbG" +
                "6L8RCWBhPKBYDDOwsmc0N0/JRCocB7IugqPR30DJ0FjQum1Bhkb32gYZTCmLE8ghz8rZlNYV28FKNEod" +
                "nWbA5ORNaYoBCUfNCNa0SW5KcfkMGtf1EPPgDAVTso5iAhzN6GxJvetoLQnh4Dej52jBY1ypRaJzU5m7" +
                "DcXdDYNBgCwh6FJ2UIgY+pkad9XNeOrH0+2DlFp2H8I93ZVqkMot00bcr/NUFopcF5vvw6LHCJDzZotF" +
                "Nwz5bg3URYeG9jbx7uweppdTKNtOxthHDfnTYI7xIxdswRTyXrq/KM+DhL+T7r5p3NNzP3h5+77TXYbg" +
                "lw23Pa2V+gGfU9qFywgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
