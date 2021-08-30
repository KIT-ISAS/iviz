/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/DetectedARMarkerArray")]
    public sealed class DetectedARMarkerArray : IDeserializable<DetectedARMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public DetectedARMarker[] Markers;
    
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
        [Preserve] public const string RosMd5Sum = "38745a121d365c2cc5cc1b47928542b2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTW/bRhA9e3/FAD7ELmS1TYICNZCDEaWtD0Ecxw0QGAGx4o7IbchdZncpmf71fbOU" +
                "qMgwkB4aCwS0XM6bNx9vZsGJy8Tm4vqtDl843H6mNh+iUq/+5596++HPc7Jre1+0sYo/Lx5wq+WQmG4+" +
                "Xb0pLq7/fv2OXtEv39y9v379bvEGl78q9Rdrw4Hq/DfapKFjFVOwrqLSG1YV+5ZTGEayj6Dy4cXty8/4" +
                "GhwSPDo6ps7ecUOdjzZZ72hjU033mXfVeJ1+e3n7O+x1y0EX1onzaMsjQQa/QaX+8YGm+weMVz7yDgsG" +
                "VmrpfUO1jgXfbSHjhy3XtvJFtPcMtqJtwfSYUwEVgRud7JqL5IuR5ehHtSwmM7KPZVfH9CFpZ3QwhNC0" +
                "0UnTCpWobVVzOGt4jaLGpNuODeWv0pw4B/CmtpHwVIwW6KYZqI8wSh5dadve2VJLL23LB3ggrSNNnQ7J" +
                "ln2jA+x9MNaJ+Sogf/GOJ/LXnl3JdLk4h42LXPZSJjBZVwbWUQRyuSDVo3EvngtAHd9s/BleuYKoJnJK" +
                "tU4SLN91gaPEqeM5OH4ak5vDN4rDYDGRTvJdgdd4SiBBCNz5sqYTRH41pBr6SjXTWgerlw2L4xIVgNdn" +
                "Anp2+o1nl1077fzO/ehxz/Ff3LrJr+R0VqNnjWQf+woFhGEX/NoamC6H7KRsLLtEjV0GHQYlqJFSHf8h" +
                "NYYRULkj+Ncx+tKiASYPzm74cjcKa36UGh+d6520AkurkATCo3X+JspZBUYmnS55LiK5zG31DqJoWSNj" +
                "6G9CAmhsABQbYQ6vHBji5hnZRMZzJOcTfLT6C1wyaixo3XVwBqEH7aIMprTFC+SE59V8Rpua3WglNcqK" +
                "zjNgSwq2smZEgqidwJq2yc0orZ6jxk0zxjySoWFK9lDKgNM5Xa5o8D1tJCEcwnb0PC15iitLJHk/k7nb" +
                "uni4YTAIKEuMuoKaXEwY+rmaltTddBqm0/2TtFp2H8K92LdqLJVf5Y142OeZLBS5Ntvv44bHCJAPdoeF" +
                "GsZ8dwbqfQ9BB5f97u2eRss5lJ2SMfZJo/x5MKf4kQu2YA75IN3vtOdJwt+X7rFpPKjnYfDy9nVfdxmC" +
                "7wpud9oo9S9FvTz2xAgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
